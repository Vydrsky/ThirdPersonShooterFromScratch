using UnityEngine;

public class PlayerInput : IPlayerInput {

    PlayerMovementDataSO playerMovementData;

    public PlayerInput() {
        playerMovementData = (PlayerMovementDataSO)Resources.Load("PlayerMovementData");
    }

    public float Vertical { get => Input.GetAxisRaw("Vertical");  }
    public float Horizontal { get => Input.GetAxisRaw("Horizontal"); }
    public bool isWalkButtonPressed => Input.GetKey(playerMovementData.walkButton);

    public bool isSprintButtonPressed => Input.GetKey(playerMovementData.sprintButton);

    public bool isForwardButtonPressed => Input.GetKey(playerMovementData.forwardButton);

    public bool isBackwardButtonPressed => Input.GetKey(playerMovementData.backwardButton);

    public bool isLeftButtonPressed => Input.GetKey(playerMovementData.leftButton);

    public bool isRightButtonPressed => Input.GetKey(playerMovementData.rightButton);

    public bool isJumpButtonPressed => Input.GetKey(playerMovementData.jumpButton);
}
