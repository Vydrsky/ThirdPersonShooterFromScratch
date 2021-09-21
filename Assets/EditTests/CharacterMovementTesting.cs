using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterMovementTesting
{
    
    [Test]
    public void PlayerHoldsWalkingButton_PlayerIsGrounded_MoveSpeedIsWalkingSpeed()
    {
        //ARRANGE
        CharacterMovement.CharacterMovementController characterMovementController = new CharacterMovement.CharacterMovementController();
        ICharacterMovement characterMovement = Substitute.For<ICharacterMovement>();
        characterMovement.PlayerMovementData.Returns((PlayerMovementDataSO)Resources.Load("PlayerMovementData"));
        characterMovement.PlayerMovementData.isGrounded = true;
        IPlayerInput playerInput = Substitute.For<IPlayerInput>();
        playerInput.isWalkButtonPressed.Returns(true);

        //ACT
        float moveSpeed = characterMovementController.SetMovementState(playerInput,characterMovement.PlayerMovementData);

        //ASSERT
        Assert.AreEqual(characterMovement.PlayerMovementData.walkSpeed,moveSpeed);
    }

    [Test]
    public void PlayerIsGrounded_MoveSpeedIsRunningSpeed() {
        //ARRANGE
        CharacterMovement.CharacterMovementController characterMovementController = new CharacterMovement.CharacterMovementController();
        ICharacterMovement characterMovement = Substitute.For<ICharacterMovement>();
        characterMovement.PlayerMovementData.Returns((PlayerMovementDataSO)Resources.Load("PlayerMovementData"));
        characterMovement.PlayerMovementData.isGrounded = true;
        IPlayerInput playerInput = Substitute.For<IPlayerInput>();
        playerInput.isWalkButtonPressed.Returns(false);

        //ACT
        float moveSpeed = characterMovementController.SetMovementState(playerInput, characterMovement.PlayerMovementData);

        //ASSERT
        Assert.AreEqual(characterMovement.PlayerMovementData.runSpeed, moveSpeed);
    }

    [Test]
    public void PlayerHoldsSprintButtonAndForwardButton_PlayerIsGrounded_MoveSpeedIsSprintingSpeed() {
        //ARRANGE
        CharacterMovement.CharacterMovementController characterMovementController = new CharacterMovement.CharacterMovementController();
        ICharacterMovement characterMovement = Substitute.For<ICharacterMovement>();
        characterMovement.PlayerMovementData.Returns((PlayerMovementDataSO)Resources.Load("PlayerMovementData"));
        characterMovement.PlayerMovementData.isGrounded = true;
        IPlayerInput playerInput = Substitute.For<IPlayerInput>();
        playerInput.isSprintButtonPressed.Returns(true);
        playerInput.isForwardButtonPressed.Returns(true);

        //ACT
        float moveSpeed = characterMovementController.SetMovementState(playerInput, characterMovement.PlayerMovementData);

        //ASSERT
        Assert.AreEqual(characterMovement.PlayerMovementData.sprintSpeed, moveSpeed);
    }

}
