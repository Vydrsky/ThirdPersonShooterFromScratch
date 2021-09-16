using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterMovement : MonoBehaviour {

    /************************ FIELDS ************************/

    private const float WALK_ANIMATION_EDGE_VALUE = 1f;
    private const float RUN_ANIMATION_EDGE_VALUE = 2f;
    private const float SPRINT_ANIMATION_EDGE_VALUE = 3f;

    private float moveSpeed;
    private Animator animator;
    private CharacterController characterController;
    private Vector3 input;
    private Vector3 moveDir;
    private Camera mainCamera;
    private float animationX;
    private float animationZ;
    private MovementState movementState = MovementState.Run;
    [SerializeField] private PlayerMovementDataSO playerMovementData;
    [SerializeField] private Rig aimLayer;

    /************************ INITIALIZE ************************/
    private void Awake() {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        moveSpeed = playerMovementData.runSpeed;
    }

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /************************ LOOPING ************************/
    private void Update() {
        HandleAimRigSwitching();

        float cameraYRot = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, cameraYRot, 0f), playerMovementData.turnSpeed * Time.deltaTime);

        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        SetMovementState();
        ControlAnimator();
        moveDir = (transform.forward * input.z + transform.right * input.x).normalized;
        
    }


    private void FixedUpdate() {
        characterController.Move(moveDir * moveSpeed * Time.deltaTime + playerMovementData.gravityVector * Time.deltaTime);
        characterController.Move(Vector3.down * Time.deltaTime);
    }

    /************************ METHODS ************************/
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.rigidbody == null) return;
        Vector3 pushDir = hit.transform.position - transform.position;
        hit.rigidbody.AddForce(pushDir * playerMovementData.moveRigidbodiesForceAmplification * Time.deltaTime, ForceMode.Impulse);
    }

    private void HandleAimRigSwitching() {
        if (movementState == MovementState.Sprint) {
            aimLayer.weight -= Time.deltaTime / playerMovementData.aimDuration;
        }
        else {
            aimLayer.weight += Time.deltaTime / playerMovementData.aimDuration;
        }
    }

    private void SetMovementState() {
        if (Input.GetKey(playerMovementData.walkButton) && playerMovementData.isGrounded) {
            movementState = MovementState.Walk;
            moveSpeed = playerMovementData.walkSpeed;
        }
        else if (Input.GetKey(playerMovementData.sprintButton) && Input.GetKey(playerMovementData.forwardButton) && playerMovementData.isGrounded && animationZ >= RUN_ANIMATION_EDGE_VALUE) {
            movementState = MovementState.Sprint;
            moveSpeed = playerMovementData.sprintSpeed;
        }
        else if (playerMovementData.isGrounded) {
            movementState = MovementState.Run;
            moveSpeed = playerMovementData.runSpeed;
        }
    }

    private void ControlAnimator() {


        switch (movementState) {
            case MovementState.Run: {
                    HandleAnimatorMovementData(RUN_ANIMATION_EDGE_VALUE);
                    break;
                }
            case MovementState.Sprint: {
                    HandleAnimatorMovementData(SPRINT_ANIMATION_EDGE_VALUE);
                    break;
                }
            case MovementState.Walk: {
                    HandleAnimatorMovementData(WALK_ANIMATION_EDGE_VALUE);
                    break;
                }
        }
        animator.SetFloat("VelocityX", animationX, 1f, Time.deltaTime * 10f);
        animator.SetFloat("VelocityZ", animationZ, 1f, Time.deltaTime * 10f);
        animator.SetBool("isGrounded", playerMovementData.isGrounded);
    }

    private void HandleAnimatorMovementData(float edgeValue) {
        if (input.z > 0.1f && animationZ < edgeValue) {
            animationZ += playerMovementData.animationSwitchSpeed * Time.deltaTime;
            if (animationZ > edgeValue) {
                animationZ = edgeValue;
            }
        }
        if (input.z < 0.1f && animationZ > -edgeValue) {
            animationZ -= playerMovementData.animationSwitchSpeed * Time.deltaTime;
            if (animationZ < -edgeValue) {
                animationZ = -edgeValue;
            }
        }
        if (input.x > 0.1f && animationX < edgeValue) {
            animationX += playerMovementData.animationSwitchSpeed * Time.deltaTime;
            if (animationX > edgeValue) {
                animationX = edgeValue;
            }
        }
        if (input.x < 0.1f && animationX > -edgeValue) {
            animationX -= playerMovementData.animationSwitchSpeed * Time.deltaTime;
            if (animationX < -edgeValue) {
                animationX = -edgeValue;
            }
        }
        if (input.x > -0.1f && input.x < 0.1f) {
            animationX = 0f;
        }
        if (input.z > -0.1f && input.z < 0.1f) {
            animationZ = 0f;
        }

        if (animationX > edgeValue) {
            animationX -= playerMovementData.animationSwitchSpeed * Time.deltaTime;
        }
        if (animationZ > edgeValue) {
            animationZ -= playerMovementData.animationSwitchSpeed * Time.deltaTime;
        }
    }


    private enum MovementState {
        Walk,
        Run,
        Sprint
    }
}
