using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    /************************ FIELDS ************************/

    private const float WALK_ANIMATION_EDGE_VALUE = 1f;
    private const float RUN_ANIMATION_EDGE_VALUE = 2f;
    private const float SPRINT_ANIMATION_EDGE_VALUE = 3f;

    private Animator animator;

    private float animationX;
    private float animationZ;
    private PlayerMovementDataSO playerMovementData;

    private PlayerInput PlayerInput { get; set; }

    /************************ INITIALIZE ************************/
    private void Awake() {
        animator = GetComponent<Animator>();
        PlayerInput = new PlayerInput();
        playerMovementData = (PlayerMovementDataSO)Resources.Load("PlayerMovementData");
    }


    /************************ LOOPING ************************/
    private void Update() {
        SetMovementState();
        ControlAnimator();
    }

    /************************ METHODS ************************/

    private void SetMovementState() {
        if (Input.GetKey(playerMovementData.walkButton) && playerMovementData.isGrounded) {
            CharacterMovement.movementState = MovementState.Walk;
        }
        else if (Input.GetKey(playerMovementData.sprintButton) && Input.GetKey(playerMovementData.forwardButton) && playerMovementData.isGrounded && animationZ >= RUN_ANIMATION_EDGE_VALUE) {
            CharacterMovement.movementState = MovementState.Sprint;
        }
        else if (playerMovementData.isGrounded) {
            CharacterMovement.movementState = MovementState.Run;
        }
    }

    private void ControlAnimator() {


        switch (CharacterMovement.movementState) {
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
        if (PlayerInput.Vertical > 0.1f && animationZ < edgeValue) {
            animationZ += playerMovementData.animationSwitchSpeed * Time.deltaTime;
            if (animationZ > edgeValue) {
                animationZ = edgeValue;
            }
        }
        if (PlayerInput.Vertical < 0.1f && animationZ > -edgeValue) {
            animationZ -= playerMovementData.animationSwitchSpeed * Time.deltaTime;
            if (animationZ < -edgeValue) {
                animationZ = -edgeValue;
            }
        }
        if (PlayerInput.Horizontal > 0.1f && animationX < edgeValue) {
            animationX += playerMovementData.animationSwitchSpeed * Time.deltaTime;
            if (animationX > edgeValue) {
                animationX = edgeValue;
            }
        }
        if (PlayerInput.Horizontal < 0.1f && animationX > -edgeValue) {
            animationX -= playerMovementData.animationSwitchSpeed * Time.deltaTime;
            if (animationX < -edgeValue) {
                animationX = -edgeValue;
            }
        }
        if (PlayerInput.Horizontal > -0.1f && PlayerInput.Horizontal < 0.1f) {
            animationX = 0f;
        }
        if (PlayerInput.Vertical > -0.1f && PlayerInput.Vertical < 0.1f) {
            animationZ = 0f;
        }

        if (animationX > edgeValue) {
            animationX -= playerMovementData.animationSwitchSpeed * Time.deltaTime;
        }
        if (animationZ > edgeValue) {
            animationZ -= playerMovementData.animationSwitchSpeed * Time.deltaTime;
        }
    }
}
