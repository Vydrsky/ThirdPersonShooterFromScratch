using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterRig : MonoBehaviour {

    /************************ FIELDS ************************/

    [SerializeField] private Rig aimLayer;
    private RaycastWeapon raycastWeapon;
    private PlayerMovementDataSO playerMovementData;

    /************************ INITIALIZE ************************/
    private void Awake() {
        raycastWeapon = GetComponentInChildren<RaycastWeapon>();
        playerMovementData = (PlayerMovementDataSO)Resources.Load("PlayerMovementData");
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        HandleAimRigSwitching();
        RaycastWeaponShootControl();
    }

    /************************ METHODS ************************/

    private void HandleAimRigSwitching() {
        if (CharacterMovement.movementState == MovementState.Sprint) {
            aimLayer.weight -= Time.deltaTime / playerMovementData.aimDuration;
        }
        else {
            aimLayer.weight += Time.deltaTime / playerMovementData.aimDuration;
        }
    }

    private void RaycastWeaponShootControl() {
        if (CharacterMovement.movementState != MovementState.Sprint && aimLayer.weight != 0f) {
            if (Input.GetButtonDown("Fire1")) {
                raycastWeapon.StartFiring();
            }
            if (Input.GetButtonUp("Fire1")) {
                raycastWeapon.StopFiring();
            }
        }
    }
}
