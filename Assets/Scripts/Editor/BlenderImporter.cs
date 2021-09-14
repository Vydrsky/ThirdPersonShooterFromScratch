using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BlenderImporter : AssetPostprocessor {


    private void OnPreprocessAnimation() {
        ModelImporter importer = assetImporter as ModelImporter;
        var animations = importer.defaultClipAnimations;

        if(animations.Length == 0) {
            return;
        }
        else {
            foreach(var animationClip in animations) {
                animationClip.loopTime = true;
                animationClip.keepOriginalOrientation = true;
                animationClip.lockRootRotation = true;
                animationClip.keepOriginalPositionXZ = true;
                animationClip.lockRootPositionXZ = true;
                animationClip.lockRootHeightY = true;
                animationClip.heightFromFeet = true;
                if (animationClip.name == "Armature|Strafe_Left" || animationClip.name == "Armature|Run_Strafe_Right") {
                    animationClip.cycleOffset = 0.5f;
                }
            }
            importer.clipAnimations = animations;
            
        }
    }

}
