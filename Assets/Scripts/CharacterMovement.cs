using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterMovement : MonoBehaviour {

    /************************ FIELDS ************************/

    private const float WALK_ANIMATION_EDGE_VALUE = 1f;
    private const float RUN_ANIMATION_EDGE_VALUE = 2f;
    private const float SPRINT_ANIMATION_EDGE_VALUE = 3f;


    private Animator animator;
    private CharacterController characterController;
    private Vector3 input;
    private Vector3 moveDir;
    private Camera mainCamera;
    [SerializeField] private Rig aimLayer;
    private float runSpeed;
    private float walkSpeed;
    private float sprintSpeed;
    private float animationX;
    private float animationZ;
    private MovementState movementState;
    private Vector3 characterVelocity;
    private Vector3 lastFramePosition;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveRigidbodiesForceAmplification = 10f;
    [SerializeField]private float animationSwitchSpeed = 3f;
    [SerializeField] private float aimDuration = 0.3f;
    [SerializeField] private KeyCode walkButton = KeyCode.LeftAlt;
    [SerializeField] private KeyCode sprintButton = KeyCode.LeftShift;

    public Transform origin;
    public Transform target;

    /************************ INITIALIZE ************************/
    private void Awake() {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        runSpeed = moveSpeed;
        sprintSpeed = runSpeed * 1.5f;
        walkSpeed = runSpeed * 0.5f;
    }

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        movementState = MovementState.Run;
    }

    /************************ LOOPING ************************/
    private void Update() {

        if (Input.GetMouseButton(1)) {
            aimLayer.weight += Time.deltaTime / aimDuration;
        }
        else {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }

        float cameraYRot = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, cameraYRot, 0f), turnSpeed * Time.deltaTime);
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        SetState();
        ControlAnimator();
        moveDir = (transform.forward * input.z + transform.right * input.x).normalized;

        characterVelocity.x = transform.position.x - lastFramePosition.x;
        characterVelocity.y = transform.position.x - lastFramePosition.y;
        characterVelocity.y = transform.position.y - lastFramePosition.y;

        lastFramePosition = transform.position;
    }

    private void FixedUpdate() {
        
        characterController.Move(moveDir * moveSpeed * Time.fixedDeltaTime);
        characterController.Move(Vector3.down * Time.fixedDeltaTime);
    }

    /************************ METHODS ************************/
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.rigidbody == null) return;
        Vector3 pushDir = hit.transform.position - transform.position;
        hit.rigidbody.AddForce(pushDir * moveRigidbodiesForceAmplification * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    private void SetState() {
        if (Input.GetKey(walkButton) && characterController.isGrounded) {
            movementState = MovementState.Walk;
            moveSpeed = walkSpeed;
        }
        else if (Input.GetKey(sprintButton) && Input.GetKey(KeyCode.W) && characterController.isGrounded && animationZ >=2f) {
            movementState = MovementState.Sprint;
            moveSpeed = sprintSpeed;
        }
        else if (characterController.isGrounded) {
            movementState = MovementState.Run;
            moveSpeed = runSpeed;
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
    }

    private void HandleAnimatorMovementData(float edgeValue) {
        if (input.z > 0.1f && animationZ < edgeValue) {
            animationZ += animationSwitchSpeed * Time.deltaTime;
            if (animationZ > edgeValue) {
                animationZ = edgeValue;
            }
        }
        if (input.z < 0.1f && animationZ > -edgeValue) {
            animationZ -= animationSwitchSpeed * Time.deltaTime;
            if (animationZ < -edgeValue) {
                animationZ = -edgeValue;
            }
        }
        if (input.x > 0.1f && animationX < edgeValue) {
            animationX += animationSwitchSpeed * Time.deltaTime;
            if (animationX > edgeValue) {
                animationX = edgeValue;
            }
        }
        if (input.x < 0.1f && animationX > -edgeValue) {
            animationX -= animationSwitchSpeed * Time.deltaTime;
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
            animationX -= animationSwitchSpeed * Time.deltaTime;
        }
        if (animationZ > edgeValue) {
            animationZ -= animationSwitchSpeed * Time.deltaTime;
        }
    }

    private enum MovementState {
        Walk,
        Run,
        Sprint
    }
}
