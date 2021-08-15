using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObject;

    GameObject spawnObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //UpdateCenterObject(); 
        //PlaceObjectByTouch();
    }

    public void PlaceObjectByButton(){
        UpdateCenterObject();
    }

    public void setPlaceObject(GameObject obj){
        this.placeObject = obj;
    }

    private void PlaceObjectByTouch()
    {
        // 터치 체크
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // 첫번째 터치 반환
            
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;
                // 인스턴스 화
                if(!spawnObject){
                    spawnObject = Instantiate(placeObject, hitPose.position, hitPose.rotation);
                }
                else{
                    spawnObject.transform.position = hitPose.position;
                    spawnObject.transform.rotation = hitPose.rotation;
                }
                
            }
        }
    }

    private void UpdateCenterObject()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)); // 화면 중심 좌표

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        // 화면 중심, List, 평면만
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        // 충돌
        if (hits.Count > 0)
        {
            Pose placementPose = hits[0].pose;
            placeObject.SetActive(true);
            // 위치 설정
            placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            // 오브젝트 소환 시, 오브젝트 선택 활성화 그리고, 오브젝트 삭제 버튼 활성화
            gameObject.GetComponent<ARSelectionController>().deSelectedAll();
            placeObject.GetComponentInChildren<ARObject>().Selected = true;
            gameObject.GetComponent<ARSelectionController>().ObjectSelected(true);
        }
        else
        {
            placeObject.SetActive(false);
            gameObject.GetComponent<ARSelectionController>().ObjectSelected(false);
            gameObject.GetComponent<ARSelectionController>().deSelectedAll();
        }
    }
}
