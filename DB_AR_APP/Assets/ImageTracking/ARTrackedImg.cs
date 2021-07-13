using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ARTrackedImg : MonoBehaviour
{
    
    // 매니저
    public ARTrackedImageManager trackedImageManager;
    // 오브젝트 리스트
    public List<GameObject> _objectList = new List<GameObject>();
    // 프리팹 Dict 형
    private Dictionary<string, GameObject> _prefabDict = new Dictionary<string, GameObject>();
    
    private void Awake() {
        // 오브젝트를 딕셔너리에 각각 추가
        foreach(GameObject obj in _objectList){

            string tName = obj.name;
            _prefabDict.Add(tName, obj); // Key 이름 : Value GameOBJ
        }
    }

    private void OnEnable() {
        // 스크립트 활성화 시
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable() {
        // 스크립트 비 활성화 시
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs){
        // 공식문서 참조
        foreach(ARTrackedImage trackedImage in eventArgs.added){
            UpdateImage(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated){
            UpdateImage(trackedImage);
        }
    }

    // 이미지 트래킹하여 인식되는 오브젝트를 Dict List에서 찾아 활성화
    private void UpdateImage(ARTrackedImage trackedImage){
        string name = trackedImage.referenceImage.name;
        GameObject tObj = _prefabDict[name];    // obj 찾기
        tObj.transform.position = trackedImage.transform.position;
        tObj.transform.rotation = trackedImage.transform.rotation;
        tObj.SetActive(true);
    }
}
