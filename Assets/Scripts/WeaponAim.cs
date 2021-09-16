using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponAim : MonoBehaviour {

    /************************ FIELDS ************************/

    [SerializeField] private Transform target;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private Rig aimRig;
    
    /************************ INITIALIZE ************************/

    /************************ LOOPING ************************/
    private void Update() {
        if(aimRig.weight>0)
            transform.rotation = Quaternion.LookRotation(target.position - weaponPivot.position, Vector3.up);
    }

    /************************ METHODS ************************/
    
    
}
