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
            button.GetComponentInChildren<Text>().text = obj.Current.name;
            var temp = obj.Current; // 임시 저장
            button.onClick.AddListener(() => selectClick(temp));
        }
    }

    private void selectClick(GameObject obj){
        gameObject.GetComponent<ARPlaceOnPlane>().setPlaceObject(obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
