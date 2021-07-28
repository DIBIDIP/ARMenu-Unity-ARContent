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
    private Button goWebButton;

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
            goWebButton.gameObject.SetActive(false);
            return;
        }
        // 선택 설정
        selected.Selected = true;

        goWebButton.gameObject.SetActive(true);
    }
}
