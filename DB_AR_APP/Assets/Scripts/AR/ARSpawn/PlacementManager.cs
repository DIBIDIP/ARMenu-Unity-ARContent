using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager arRaycastManager;
    [SerializeField]
    private GameObject pointObject;

    [SerializeField]
    private bool objectSpawn = false;   // 오브젝트 소환에 대한 정보
    
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
        // 오브젝트이 소환되었는가?
        objectSpawn = GameObject.Find("AR Session Origin").GetComponent<ARPlaceOnPlane>().getARIsSpawn();

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        // 화면 중심으로
        arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if(hits.Count > 0){
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            // 포인터가 죽어있으면 살림
            if(!objectSpawn && !pointObject.activeInHierarchy){
                pointObject.SetActive(true);
            }
            // 오브젝트가 소환되있으면 Kill
            if(objectSpawn){
                pointObject.SetActive(false);
            }
        }
    }
}
