using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ARTrackedImg : MonoBehaviour
{
    public float _timer;
    public ARTrackedImageManager trackedImageManager;
    // 오브젝트 리스트
    public List<GameObject> _objectList = new List<GameObject>();
    // 프리팹 Dict 형
    private Dictionary<string, GameObject> _prefabDict = new Dictionary<string, GameObject>();
    private List<ARTrackedImage> _trackedImg = new List<ARTrackedImage>();
    // ARTrackedImage 와 그 이미지들의 타이머를 저장
    private List<float> _trackedTimer = new List<float>();

    // 이미지 트래킹 제거 상태
    private void Awake()
    {
        // 오브젝트를 딕셔너리에 각각 추가
        foreach (GameObject obj in _objectList)
        {
            string tName = obj.name;
            _prefabDict.Add(tName, obj); // 오브젝트이름, object로 저장
        }
    }

    private void Update()
    {
        // 타이머 연동
        if (_trackedImg.Count > 0)
        {
            // 임시 리스트 변수
            List<ARTrackedImage> tNumlist = new List<ARTrackedImage>();

            for (var i = 0; i < _trackedImg.Count; i++)
            {
                // Limited 상태
                if (_trackedImg[i].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
                {
                    // 오브젝트 삭제
                    if (_trackedTimer[i] > _timer)
                    { // 일정시간 이상 Limited 상태가 되면 Removed
                        string name = _trackedImg[i].referenceImage.name;
                        GameObject tObj = _prefabDict[name];
                        tObj.SetActive(false);
                        tNumlist.Add(_trackedImg[i]);
                        Debug.Log("타이머 종료, 오브젝트 삭제");
                    }
                    else
                    {
                        _trackedTimer[i] += Time.deltaTime;
                    }
                }
            }

            // 리스트에서 타이머 종료된 obj 삭제
            if (tNumlist.Count > 0)
            {
                for ( var i = 0 ; i < tNumlist.Count; i++){
                    int num = _trackedImg.IndexOf(tNumlist[i]);
                    _trackedImg.Remove(_trackedImg[num]);
                    _trackedTimer.Remove(_trackedTimer[num]);
                    Debug.Log("리스트 삭제");
                }
            }
        }
    }

    private void OnEnable()
    {
        // 스크립트 활성화 시
        trackedImageManager.trackedImagesChanged += ImageChanged;
        Debug.Log("스크립트 활성화");
    }

    private void OnDisable()
    {
        // 스크립트 비 활성화 시
        trackedImageManager.trackedImagesChanged -= ImageChanged;
        Debug.Log("스크립트 비 활성화");
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Added 상태
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // trackedImage 추가 됬는지 검사
            if (!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
            }
        }
        // Updated 상태
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // 타이머 리셋
            if (!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
            }
            else
            {
                int tIndex = _trackedImg.IndexOf(trackedImage);
                _trackedTimer[tIndex] = 0;
            }
            UpdateImage(trackedImage);
            Debug.Log("Updated 상태");
        }
    }

    // 이미지 트래킹하여 인식되는 오브젝트를 Dict List에서 찾아 활성화
    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject tObj = _prefabDict[name];    // obj 찾기
        tObj.transform.position = trackedImage.transform.position;
        tObj.transform.rotation = trackedImage.transform.rotation;
        tObj.SetActive(true);
        Debug.Log("이미지트래킹 활성화");
    }
}
