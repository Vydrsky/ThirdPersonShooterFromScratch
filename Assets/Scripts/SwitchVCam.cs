using UnityEngine;
using Cinemachine;

public class SwitchVCam : MonoBehaviour {

    /************************ FIELDS ************************/

    [SerializeField] private int priorityBoost = 10;
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineVirtualCamera followCamera;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            StartAim();
        }
        else if(Input.GetMouseButtonUp(1)) {
            CancelAim();
        }
    }

    /************************ METHODS ************************/
    
    private void StartAim() {
        virtualCamera.Priority += priorityBoost;
        virtualCamera.transform.position = followCamera.transform.position;
        virtualCamera.transform.rotation = followCamera.transform.rotation;
    }

    private void CancelAim() {
        virtualCamera.Priority -= priorityBoost;
        followCamera.transform.position = virtualCamera.transform.position;
        followCamera.transform.rotation = virtualCamera.transform.rotation;
    }
}
