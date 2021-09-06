using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
public class ARPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;    // 레이캐스팅
    public GameObject objPool;  // 모델링 파일들이 담긴 오브젝트
    public Dictionary<string, GameObject> placeList = new Dictionary<string, GameObject>(); // 이걸로 사용

    public string currentMenuName;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject spawnObject; // 소환할 오브젝트

    bool ARIsSpawn = false;   // 소환 버튼 눌러졌는가 -> Tab 종료

    [SerializeField]
    private Button SpawnButton;

    [SerializeField]
    private TMP_Text notice_scan;
    [SerializeField]
    private TMP_Text notice_tap;

    void Start()
    {
        // id = key value GameObject
        // GameObject인 Food AR 모델링 파일은 id로 파일명(name)을 갖고 objPool을 순회하며 해당 키로 저장한다.
        List<GameObject> tempList = new List<GameObject>();
        for (int i = 0 ; i < objPool.transform.childCount; i++){
            var child = objPool.transform.GetChild(i).gameObject;
            tempList.Add(child); // 임시 변수에 하나씩 저장
        }
        // 오브젝트 Pool로 받아와서 해당 Name을 Key로, Value를 자신으로 Dict에 저장한다.

        foreach(GameObject item in tempList){
            placeList.Add(item.name, item.gameObject);
        }

        // 싱글톤 객체로 부터 Detail ID를 currentID로 저장한다.
        currentMenuName = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().getDetailItem().menuName;
        // 공백제거
        currentMenuName = Regex.Replace(currentMenuName, @"\s", ""); // 공백 제거

        spawnObject = placeList[currentMenuName];

        notice_scan.gameObject.SetActive(false);
        notice_tap.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateCenterObject(); 
        //PlaceObjectByTouch();

        // 평면인식됬는지 확인
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if(hits.Count > 0){ // 인식되면
            // TAP Notice ON
            if(!notice_tap.gameObject.activeInHierarchy){
                if(ARIsSpawn){
                    TabNoticeShow(false);
                } else {
                    TabNoticeShow(true);
                }
                notice_scan.gameObject.SetActive(false);
            }
        } else {    // 인식 안되었으면,
            // SCAN Notice ON
            if(!notice_scan.gameObject.activeInHierarchy){
                notice_scan.gameObject.SetActive(true);
                TabNoticeShow(false);
            }
        }
    }
    public void TabNoticeShow(bool IsShow){
        notice_tap.gameObject.SetActive(IsShow);
    }

    public void setARIsSpawn(bool IsSpawn){
        ARIsSpawn = IsSpawn;
    }

    public bool getARIsSpawn(){
        return this.ARIsSpawn;
    }

    public void PlaceObjectByButton(){
        PlaceCenterObject();
    }

    // Key로 value 전달.
    public GameObject getPlaceObject(string id){
        return placeList[id];
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
                    spawnObject = Instantiate(placeList[currentMenuName], hitPose.position, hitPose.rotation);
                }
                else{
                    spawnObject.transform.position = hitPose.position;
                    spawnObject.transform.rotation = hitPose.rotation;
                }
                
            }
        }
    }

    private void PlaceCenterObject()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)); // 화면 중심 좌표

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        // 화면 중심, List, 평면만
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        // 충돌
        if (hits.Count > 0)
        {
            ARIsSpawn = true;
            Pose placementPose = hits[0].pose;
            spawnObject.SetActive(true);
            // 위치 설정
            spawnObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

            // 오브젝트 소환 시, 오브젝트 선택 활성화 그리고, 오브젝트 삭제 버튼 활성화
            gameObject.GetComponent<ARSelectionController>().deSelectedAll();
            spawnObject.GetComponentInChildren<ARObject>().Selected = true;
            gameObject.GetComponent<ARSelectionController>().Spawn_Select(true);
            notice_tap.gameObject.SetActive(false);
        }
        else
        {
            ARIsSpawn = false;
            spawnObject.SetActive(false);
            gameObject.GetComponent<ARSelectionController>().Spawn_Select(false);
            gameObject.GetComponent<ARSelectionController>().deSelectedAll();
        }
    }
}
