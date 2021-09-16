using UnityEngine;
using System.Collections.Generic;

public class ModelFadeInOut : MonoBehaviour {

    /************************ FIELDS ************************/

    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    private Camera mainCamera;
    [SerializeField] private Transform headTransform;
    private Color invisible;
    private Color materialColor;

    /************************ INITIALIZE ************************/
    private void Awake() {
        mainCamera = Camera.main;
    }

    private void Start() {
        materialColor = meshRenderer.material.color;
        invisible.a = 0;
    }

    /************************ LOOPING ************************/
    private void Update() {
        float distance = Vector3.Distance(headTransform.position, mainCamera.transform.position);
        if (distance < 1f) {
            meshRenderer.material.color = invisible;

        }
        else {
            meshRenderer.material.color = materialColor;
        }
    }

    /************************ METHODS ************************/

}
