using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSelectionController : MonoBehaviour
{
    [SerializeField]
    private ARObject[] arObjects;
    [SerializeField]
    private Camera arCamera;

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
                        // 컨트롤
                        SelectedObject(arObject);
                        Debug.Log(arObject.name + "선택됨");
                    }
                }
            }
        }
    }

    private void SelectedObject(ARObject selected)
    {
        foreach (ARObject current in arObjects)
        {
            if(selected != current){ // 선택된 오브젝트와 현재 오브젝트가 다르면
                current.Selected = false;
            }
            else{
                current.Selected = true;
            }
        }
    }
}
