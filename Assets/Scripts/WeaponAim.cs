using UnityEngine;

public class WeaponAim : MonoBehaviour {

    /************************ FIELDS ************************/

    [SerializeField] private Transform target;
    [SerializeField] private Transform weaponPivot;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        transform.rotation = Quaternion.LookRotation(target.position - weaponPivot.position, Vector3.up);
    }

    /************************ METHODS ************************/
    
    
}
