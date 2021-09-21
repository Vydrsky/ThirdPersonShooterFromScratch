public interface IPlayerInput {
    float Vertical { get; }
    float Horizontal { get; }

    bool isWalkButtonPressed { get; }
    bool isSprintButtonPressed { get; }
    bool isForwardButtonPressed { get; }
    bool isBackwardButtonPressed { get; }
    bool isLeftButtonPressed { get; }
    bool isRightButtonPressed { get; }
    bool isJumpButtonPressed { get; }
}
