using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectList : MonoBehaviour
{  
    [SerializeField]
    private GameObject objectPool;
    // 자식들이 담겨질 리스트
    [SerializeField]
    private List<GameObject> arObjectList = new List<GameObject>();
    // 버튼들이 담겨질 리스트
    [SerializeField]
    private List<Button> buttonList = new List<Button>();

    [SerializeField]
    private GameObject arobjButton;
    [SerializeField]
    private RectTransform content;

    
    private bool listOnOff = false;

    private void Awake() {
        // 리스트에 pool 자식을 iterator로 접근 후 하나씩 추가.
        for (int i = 0; i < objectPool.transform.childCount; i++){
            var child = objectPool.transform.GetChild(i).gameObject;
            arObjectList.Add(child);

            // 버튼 리스트에 추가.
            buttonList.Add(Instantiate(arobjButton, content).GetComponent<Button>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {   
        // 동적 생성한 버튼에 이벤트 추가.
        IEnumerator<GameObject> obj = arObjectList.GetEnumerator();
        foreach (Button button in buttonList){
            obj.MoveNext();
            // 버튼에 그릴 정보
            button.GetComponentInChildren<Text>().fontSize = 70;
            button.GetComponentInChildren<Text>().text = obj.Current.name;
            // 클로저 공유 방지
            var temp = obj.Current; // 임시 저장
            button.onClick.AddListener(() => selectClick(temp));
        }
    }

    private void selectClick(GameObject obj){
        Debug.Log(obj.name);
        GameObject.Find("AR Session Origin").gameObject.GetComponent<ARPlaceOnPlane>().placeObject = obj;
    }

    // 목록 버튼 이벤트
    public void listButtonClick(){
        listOnOff = !listOnOff;
        // 스크롤바 위치 초기화
        transform.FindChild("Scroll View").gameObject.GetComponentInChildren<Scrollbar>().value = 0.0f;
        transform.FindChild("Scroll View").gameObject.SetActive(listOnOff);
    }
}
