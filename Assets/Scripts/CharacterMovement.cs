using UnityEngine;


[SelectionBase]
public class CharacterMovement : MonoBehaviour, ICharacterMovement {

    /************************ FIELDS ************************/

    private CharacterMovementController characterMovementController;
    private float moveSpeed;
    private CharacterController characterController;
    private Vector3 moveDir;
    public static MovementState movementState = MovementState.Run;
    private PlayerMovementDataSO playerMovementData;
    private PlayerInput playerInput;

    //Properties for testing
    public PlayerInput PlayerInput { get; }
    public PlayerMovementDataSO PlayerMovementData { get; }


    /************************ INITIALIZE ************************/
    private void Awake() {
        characterController = GetComponent<CharacterController>();
        playerInput = new PlayerInput();
        playerMovementData = (PlayerMovementDataSO)Resources.Load("PlayerMovementData");
        characterMovementController = new CharacterMovementController();
    }

    private void Start() {
        moveSpeed = playerMovementData.runSpeed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /************************ LOOPING ************************/
    private void Update() {
        moveSpeed = characterMovementController.SetMovementState(PlayerInput,playerMovementData);
        moveDir = characterMovementController.SetMovementVector(transform, playerInput);
    }

    private void FixedUpdate() {
        characterMovementController.Move(characterController, moveDir, moveSpeed, playerMovementData);
    }

    /************************ METHODS ************************/
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        characterMovementController.ControlRigidbodyCollision(hit, transform, playerMovementData);
    }



    //humble object pattern, class that delegates all the logic from monobehaviour
    public class CharacterMovementController {
        public void ControlRigidbodyCollision(ControllerColliderHit hit, Transform transform, PlayerMovementDataSO playerMovementData) {
            if (hit.rigidbody == null) return;
            Vector3 pushDir = hit.transform.position - transform.position;
            hit.rigidbody.AddForce(pushDir * playerMovementData.moveRigidbodiesForceAmplification * Time.deltaTime, ForceMode.Impulse);
        }

        public float SetMovementState(IPlayerInput playerInput,PlayerMovementDataSO playerMovementData) {
            float moveSpeed;
            if (playerInput.isWalkButtonPressed && playerMovementData.isGrounded) {
                moveSpeed = playerMovementData.walkSpeed;
            }
            else if (playerInput.isSprintButtonPressed && playerInput.isForwardButtonPressed && playerMovementData.isGrounded) {
                moveSpeed = playerMovementData.sprintSpeed;
            }
            else {
                moveSpeed = playerMovementData.runSpeed;
            }
            return moveSpeed;
        }
        public Vector3 SetMovementVector(Transform transform, PlayerInput PlayerInput) {
            Vector3 moveDir = (transform.forward * PlayerInput.Vertical + transform.right * PlayerInput.Horizontal).normalized;
            return moveDir;
        }

        public void Move(CharacterController characterController, Vector3 moveDir, float moveSpeed, PlayerMovementDataSO playerMovementData) {
            characterController.Move(moveDir * moveSpeed * Time.deltaTime + playerMovementData.gravityVector * Time.deltaTime);
            characterController.Move(Vector3.down * Time.deltaTime);
        }
    }
}
