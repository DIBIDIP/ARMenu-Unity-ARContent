using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARTrackedImg : MonoBehaviour
{   
    [SerializeField]
    private List<GameObject> trackedPrefabs; // 이미지를 인식했을 때 출력되는 프리팹 목록

    [SerializeField]
    private GameObject objPool; // 이미지를 인식했을 떄 출력되는 오브젝트 목록
    
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    [SerializeField]
    private TMPro.TMP_Text notice_qr;

    [SerializeField]
    private TMPro.TMP_Text notice2;

    private void Awake()
    {
        for (int i = 0; i < objPool.transform.childCount; i++){
            var child = objPool.transform.GetChild(i).gameObject;
            trackedPrefabs.Add(child);
        }

        // AR Session Origin 오브젝트에 컴포넌트로 적용했을 때 사용 가능
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        // trackedPrebas 배열에 있는 모든 프리팹을 instaniate()로 생성 한 후
        // spawnedObjects 딕셔너리에 저장, 비활성화
        // 카메라에 이미지가 인식되면 이미지와 동일한 이름의 key에 있는 value 오브젝트를 출력
        foreach( GameObject prefab in trackedPrefabs){
            GameObject clone = Instantiate(prefab); // 오브젝트 생성
            clone.name = prefab.name;               // 생성한 오브젝트의 이름 설정
            clone.SetActive(false);                 // 오브젝트 비활성화
            spawnedObjects.Add(clone.name, clone);  // 딕셔너리에 저장
        }

        notice_qr.gameObject.SetActive(true);

        // 현재 씬 매니저 Scan ID None 으로 초기값 설정
        GameObject.Find("Manager").GetComponent<ChangeScene>().setScanSceneIDNone(false);
    }

    private void OnEnable() {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable() {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs){
        // 카메라에 이미지가 인식되었을 때
        foreach(var trackedImage in eventArgs.added){
            Debug.Log("이미지 트래킹 시작");
            UpdateImage(trackedImage);

            notice_qr.gameObject.SetActive(false);
        }

        // 카메라에 이미지가 인식되어 업데이트되고 있을 때 (프레임당 갱신함)
        foreach ( var trackedImage in eventArgs.updated){
            UpdateImage(trackedImage);
        }
        // 현재 removed가 잘 안됨 - updated 에서 해야함,
        // 인식되어 있는 이미지가 카메라에서 사라졌을 때
        foreach(var trackedImage in eventArgs.removed){
            Debug.Log("이미지 트래킹 해제");
            // 비활성화
            spawnedObjects[trackedImage.name].SetActive(false); 
            // TODO: 해제 시에 AR Obeject Seleted 또한 해제해야함, ARButton 활성화
            // 트래킹 해제 시
            gameObject.GetComponent<ARSelectionController>().deSelectedAll();
            gameObject.GetComponent<ARSelectionController>().Scan_Select(false);    // 강제 비활성화
            notice_qr.gameObject.SetActive(true);
        }
    }

    // 이미지 트래킹하여 인식되는 오브젝트를 Dict List에서 찾아 활성화
    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject trackedObject =spawnedObjects[name];
        // 트래킹된 오브젝트의 상태를 가져온다.
        bool Selected = trackedObject.GetComponent<ARObject>().Selected;
        // 이미지의 추적 상태가 추적중(Tracking) 일 때
        if ( trackedImage.trackingState == TrackingState.Tracking){
            // 이미지 위치로 계속 따라다님
            trackedObject.transform.position = trackedImage.transform.position;
            trackedObject.transform.rotation = trackedImage.transform.rotation;
            // 활성화
            trackedObject.SetActive(true);

            notice_qr.gameObject.SetActive(false);
            
            // 싱글톤 객체에 넘기기
            if(trackedObject.GetComponent<ARScanObjectInfo>().ID != string.Empty){
                string trackedObjID = trackedObject.GetComponent<ARScanObjectInfo>().ID;
                GameObject.Find("ARMenuData").GetComponent<ARMenuData>().setDetailID(trackedObjID);
                GameObject.Find("Manager").GetComponent<ChangeScene>().setScanSceneIDNone(false);
            }
            else {
                // ID가 없는 오브젝트를 스캔했다면 정보 클릭을 실행하는 Load Scene에 넘긴다 아이디를
                GameObject.Find("Manager").GetComponent<ChangeScene>().setScanSceneIDNone(true);
            }
        }else {
            trackedObject.SetActive(false);
            notice_qr.gameObject.SetActive(true);
            // 강제 비활성화
            gameObject.GetComponent<ARSelectionController>().deSelectedAll();
            // gameObject.GetComponent<ARSelectionController>().Scan_Select(false);    // 강제 비활성화
        }
    }
}
