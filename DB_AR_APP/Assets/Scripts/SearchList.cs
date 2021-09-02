using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARContent;
using UnityEngine.UI;

// 검색 결과를 토대로 UI에 아이템을 그림.
public class SearchList : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text noticeText;
    void Start()
    {
        // 아이템 생성
        // 먼저 검색 결과가 있는지 확인한다.
        searchButtonClick();
    }
    public void searchButtonClick(){
        // 검색 로직 시작 
        bool resultSearch = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().findMenuOrName();
        
        // 검색 성공하면 가게를 보여준다.
        if (resultSearch){
            ARRestaurant findItem = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().getSerachSuccessRestaurant();
            Debug.Log(findItem.menuName + "를 그려라");
            noticeText.text = "가게명 : " + findItem.menuName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
