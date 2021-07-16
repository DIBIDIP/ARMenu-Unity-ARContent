using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTouch : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    private void Update() {
        
    }

    // AR 오브젝트를 터치할 시,
    private void touchARObject(){

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);    // 첫번째 터치

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if(arRaycaster.Raycast(
                touch.position, hits, TrackableType.Planes
            )){
                Pose hitPose = hits[0].pose;
                
            }
        }
    }
}
