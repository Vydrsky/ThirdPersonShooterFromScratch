using UnityEngine;

public class CharacterRotation : MonoBehaviour {

    /************************ FIELDS ************************/

    private Camera mainCamera;
    private PlayerMovementDataSO playerMovementData;

    /************************ INITIALIZE ************************/
    private void Awake() {
        mainCamera = Camera.main;
        playerMovementData = (PlayerMovementDataSO)Resources.Load("PlayerMovementData");
    }


    /************************ LOOPING ************************/
    private void Update() {
        HandleRotation();
    }

    /************************ METHODS ************************/
    
    private void HandleRotation() {
        float cameraYRot = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, cameraYRot, 0f), playerMovementData.turnSpeed * Time.deltaTime);
    }
}
