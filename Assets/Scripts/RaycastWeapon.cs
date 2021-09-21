using UnityEngine;

public class RaycastWeapon : MonoBehaviour {

    /************************ FIELDS ************************/

    [SerializeField] private bool isFiring = false;
    [SerializeField] private ParticleSystem[] muzzleFlashPS;
    
    /************************ INITIALIZE ************************/
    

    /************************ LOOPING ************************/
    

    /************************ METHODS ************************/
    
    public void StartFiring() {
        isFiring = true;
        foreach (var ps in muzzleFlashPS) {
            ps.Emit(1);
        }
    }

    public void StopFiring() {
        isFiring = false;
    }
}
