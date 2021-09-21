using UnityEngine;

[CreateAssetMenu]
public class PlayerMovementDataSO : ScriptableObject {

    public float runSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float turnSpeed = 15f;
    public float jumpHeight = 2f;
    public float moveRigidbodiesForceAmplification = 10f;
    public float animationSwitchSpeed = 3f;
    public float aimDuration = 0.3f;
    public float jumpTimerMax = 1f;
    [HideInInspector] public LayerMask groundMask;
    [HideInInspector] public Vector3 gravityVector;
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool jumpPressed = false;
    public float jumpTimer;
    [Header("Player Movement KeyBindings")]
    public KeyCode walkButton = KeyCode.LeftAlt;
    public KeyCode sprintButton = KeyCode.LeftShift;
    public KeyCode jumpButton = KeyCode.Space;
    public KeyCode forwardButton = KeyCode.W;
    public KeyCode backwardButton = KeyCode.S;
    public KeyCode leftButton = KeyCode.A;
    public KeyCode rightButton = KeyCode.D;
}
