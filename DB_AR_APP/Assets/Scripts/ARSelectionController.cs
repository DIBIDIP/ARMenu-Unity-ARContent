using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems; 

public class ARSelectionController : MonoBehaviour
{
    [SerializeField]
    private ARObject[] arObjects;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private Button ARButton;

    [SerializeField]
    private Sprite PlusImage;
    [SerializeField]
    private Sprite InfoImage;

    private Vector2 touchPosition = default;
    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            
            if(EventSystem.current.IsPointerOverGameObject(0)){ // UI 클릭할 시 
                return;
            }
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    ARObject arObject = hitObject.transform.GetComponent<ARObject>();
                    if (arObject != null)
                    {
                        // 컨트롤
                        SelectedObject(arObject);
                    }
                }
            }
        }
    }

    private void SelectedObject(ARObject selected)
    {   
        // 선택 해제
        if(selected.Selected){ 
            selected.Selected = false;
            // AR 오브젝트 선택 시 해당하는 오브젝트 정보 버튼 활성화/비활성화
            ChangeButtonImage(false);
            return;
        }
        // 오브젝트가 여러개 있을 시, 하나의 오브젝트만 선택되게 한다
        foreach (ARObject current in arObjects){
            // 선택한 오브젝트가 아닌 오브젝트들은 false를 준다.
            if(current != selected){
                current.Selected = false;
            }
            else{
                current.Selected = true;
                Debug.Log(selected.Selected + "선택한 오브젝트 상태");
            }
        }
        // 선택 설정
        selected.Selected = true;
        ChangeButtonImage(true);
    }

    public bool ChangeButtonImage(bool Selected){
        // 이미지가 인식되고 있지 않으면 false 반환
        if (!Selected){
            // 인식되지 않으면, 기존 Plus 이미지로 변경
            ARButton.GetComponent<Image>().sprite = PlusImage;
            return false; 
        }
        // 인식되고 있으면, Info 이미지로 변경
        ARButton.GetComponent<Image>().sprite = InfoImage;
        return true;
    }
}
