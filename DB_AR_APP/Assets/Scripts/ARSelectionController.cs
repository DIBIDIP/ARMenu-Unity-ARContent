using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

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
                        if(arObject.Selected){ // 다시 선택시 선택해제
                            arObject.Selected = false;
                            goWebButton.gameObject.SetActive(false);

                            Debug.Log(arObject.name + "선택해제");
                            return;
                        }
                        // 컨트롤
                        SelectedObject(arObject);
                        Debug.Log(arObject.name + "선택됨");

                        goWebButton.GetComponent<Text>().text = arObject.name + "버튼";
                        goWebButton.gameObject.SetActive(true);
                        Debug.Log("버튼 활성화");
                    }
                }
            }
        }
    }

    private void SelectedObject(ARObject selected)
    {
        foreach (ARObject current in arObjects)
        {
            if (selected != current)
            { // 선택된 오브젝트와 현재 오브젝트가 다르면
                current.Selected = false;
            }
            else
            {
                current.Selected = true;
            }
        }
    }
}
