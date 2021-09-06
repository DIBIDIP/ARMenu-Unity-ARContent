using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems; 
using UnityEngine.SceneManagement;

public class ARSelectionController : MonoBehaviour
{
    [SerializeField]
    private List<ARObject> arObjects;
    [SerializeField]
    private GameObject objectPool;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private Button ARButton;

    [SerializeField]
    private Button InfoButton;

    [SerializeField]
    private Button DeleteButton;

    private Vector2 touchPosition = default;
    private void Awake()
    {
        for (int i = 0; i < objectPool.transform.childCount; i++){
            ARObject child = objectPool.transform.GetChild(i).gameObject.GetComponent<ARObject>();
            arObjects.Add(child);
        }
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

    public void SelectedObject(ARObject selected)
    {   
        // 선택 해제
        if(selected.Selected){ 
            selected.Selected = false;
            // AR 오브젝트 선택 시 해당하는 오브젝트 정보 버튼 활성화/비활성화
            ObjectButtonSelect(false);
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
        // 선택에 따라 동작
        selected.Selected = true;
        // 선택 시 
        // TODO : 오브젝트 삭제 버튼 활성화
        ObjectButtonSelect(true);
    }
    // 선택된 오브젝트를 삭제합니다.
    public void DeleteObject(){
        // 오브젝트들 중 선택된 오브젝트를 찾습니다.
        foreach (ARObject obj in arObjects){
            // 선택된 오브젝트를 비활성화합니다.
            if(obj.Selected){
                obj.gameObject.SetActive(false);
                Debug.Log(obj.name + "비활성화됨");
                deSelectedAll(); // 선택해제
                ObjectButtonSelect(false);
            }
        }
        if(SceneManager.GetActiveScene().name == "ARSpawn Scene"){
            GetComponent<ARPlaceOnPlane>().setARIsSpawn(false); // 소환 해제 ( 삭제 되었으므로 )
        }
    }
    
    // 조건에 따라, 버튼이미지, 삭제버튼 비/활성화하는 함수
    public void ObjectButtonSelect(bool Selected){
        if(SceneManager.GetActiveScene().name == "ARScan Scene"){
            InfoObjectButton(Selected);
            return;
        }
        
        ARObjectButton(!Selected);
        DeleteObjectButton(Selected);
        InfoObjectButton(Selected);    
    }

    public void Scan_Select(bool Selected){
        InfoObjectButton(Selected);
    }

    public void Spawn_Select(bool Selected){
        ARObjectButton(!Selected);
        DeleteObjectButton(Selected);
        InfoObjectButton(Selected);
    }
    // 모든 선택을 해제 합니다.
    public void deSelectedAll(){
        // 선택 관련 모두 해제
        // 모든 오브젝트 선택 해제
        foreach (ARObject obj in arObjects){
            obj.Selected = false;
        }
    }

    // 오브젝트 삭제 버튼을 비/활성화 합니다.
    public bool DeleteObjectButton(bool Selected){
        // 선택되지 않으면 비활성화
        if(!Selected){
            DeleteButton.gameObject.SetActive(false);
            return false;
        }
        // 선택되었으면 활성화
        DeleteButton.gameObject.SetActive(true);
        return true;
    }

    private bool ARObjectButton(bool Selected){
        if(SceneManager.GetActiveScene().name == "ARScan Scene"){
            return false;
        }
        // 선택되지 않으면 비활성화
        if(!Selected){
            ARButton.gameObject.SetActive(false);
            return false;
        }
        // 선택되었으면 활성화
        ARButton.gameObject.SetActive(true);
        return true;
    }
    

    // 오브젝트 정보 버튼을 비/활성화 합니다
    public bool InfoObjectButton(bool Selected){
        // 선택되지 않으면 비활성화
        if(!Selected){
            InfoButton.gameObject.SetActive(false);
            return false;
        }
        // 선택되었으면 활성화
        InfoButton.gameObject.SetActive(true);
        return true;
    }

}
