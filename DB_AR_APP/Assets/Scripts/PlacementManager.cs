using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementManager : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager arRaycastManager;
    [SerializeField]
    private GameObject pointObject;
    
    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        arRaycastManager = FindObjectOfType<ARRaycastManager>();   
        pointObject = this.transform.GetChild(0).gameObject;
        pointObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        // 화면 중심으로
        arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if(hits.Count > 0){
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            // 포인터가 죽어있으면 살림
            if(!pointObject.activeInHierarchy){
                pointObject.SetActive(true);
            }
        }
    }
}
