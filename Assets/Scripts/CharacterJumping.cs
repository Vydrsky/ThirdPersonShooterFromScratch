using UnityEngine;

public class CharacterJumping : MonoBehaviour {

    /************************ FIELDS ************************/

    [SerializeField] private PlayerMovementDataSO playerMovementData;
    [SerializeField] private Transform groundCheckPosition;
    private CharacterController characterController;
    private Animator animator;
    private int jumpCount;

    /************************ INITIALIZE ************************/
    private void Awake() {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }


    /************************ LOOPING ************************/
    private void Update() {
        JumpDelay();
        CheckForGrounded();
        CalculateGravity();
        CheckForJumpInput();
    }

    private void FixedUpdate() {
        TryJump();
    }


    /************************ METHODS ************************/

    private void CheckForGrounded() {
        if (characterController.isGrounded || Physics.Raycast(groundCheckPosition.position,Vector3.down,0.5f,playerMovementData.groundMask)) {
            playerMovementData.isGrounded = true;
        }
        else {
            playerMovementData.isGrounded = false;
        }
    }

    private void CheckForJumpInput() {
        if (Input.GetKey(playerMovementData.jumpButton) && JumpDelayPassed()) {
            playerMovementData.jumpPressed = true;
        }
        else if(playerMovementData.isGrounded && !Input.GetKeyDown(playerMovementData.jumpButton)) {
            playerMovementData.jumpPressed = false;
        }
    }

    private void TryJump() {
        if (playerMovementData.isGrounded && playerMovementData.jumpPressed && jumpCount < 1) {
            animator.SetBool("Jump", true);
            playerMovementData.gravityVector += Vector3.up * playerMovementData.jumpHeight;
            jumpCount++;
        }
        else {
            animator.SetBool("Jump", false);
        }

        if(playerMovementData.gravityVector.y <= 0f) {
            jumpCount = 0;
        }
    }
    private void CalculateGravity() {
        playerMovementData.gravityVector.y += (Physics.gravity.y * Time.deltaTime);
        animator.SetFloat("VelocityY", playerMovementData.gravityVector.y);
        if (playerMovementData.isGrounded && !playerMovementData.jumpPressed && playerMovementData.gravityVector.y < 0f) {
            playerMovementData.gravityVector.y = 0f;
        }
    }

    private void JumpDelay() {
        if (playerMovementData.isGrounded) {
            playerMovementData.jumpTimer += Time.deltaTime;
        }
        else {
            playerMovementData.jumpTimer = 0f;
        }
    }

    private bool JumpDelayPassed() {

        if (playerMovementData.jumpTimer >= playerMovementData.jumpTimerMax) {
            return true;
        }
        return false;
    }

}