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
    [SerializeField]
    private Text smallNoticeText;

    // 가게 버튼들이 담겨질 리스트
    [SerializeField]
    private List<Button> menuList = new List<Button>();   // 동적 생성으로 할 것이므로 초기화를 해줌.
    [SerializeField]
    private List<ARRestaurant> resultList;  // 검색결과가 담겨질 List
    [SerializeField]
    private GameObject menuItemObject;
    [SerializeField]
    private RectTransform content;  // ListContent

    private void Awake() {
        bool IsFind = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().searchSuccess;
        // 아이템 생성
        // 먼저 검색 결과가 있는지 확인한다.
        searchResult(IsFind);
    }

    // 메뉴리스트 초기화
    public void allDeleteList(){
        if (menuList.Count > 0){
            foreach (var item in menuList){
                Destroy(item.gameObject);
            }
            menuList.Clear();
        }
    }

    // 검색결과를 리스트로 만든다.
    public void createResultList(bool IsSearched){
        // LIst 초기화
        allDeleteList();

        if(IsSearched){
            resultList = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().getSerachSuccessRestaurants();

            // 검색 결과 수를 반복문으로 돌려서 추가.
            for (int i = 0; i < resultList.Count; i++){
                // 프리팹 추가.
                menuList.Add(Instantiate(menuItemObject, content).GetComponent<Button>());
            }

            // 동적 생성한 버튼에 정보, 이벤트 추가
            IEnumerator<ARRestaurant> data = resultList.GetEnumerator();
            foreach(Button btn in menuList){
                data.MoveNext();
                // 버튼에 정보 추가
                btn.GetComponentsInChildren<Text>()[0].text = data.Current.restaurantName;
                btn.GetComponentsInChildren<Text>()[1].text = data.Current.description;
                // TODO : Image 추가하기
                //btn.GetComponentsInChildren<Image>()
                var temp = data.Current;   // 임시 저장
                // 클릭이벤트 추가
                btn.onClick.AddListener(() => selectClick(temp));
            }
        }
    }

    private void selectClick(ARRestaurant obj){
        GameObject.Find("ARMenuData").GetComponent<ARMenuData>().setDetailID(obj._id);
    }

    public void searchResult(bool Searched){
        // 검색 로직 시작         
        // 검색 성공하면 가게를 보여준다.
        createResultList(Searched);
        searchResultNotice(Searched); // 최초로 결과 텍스트로 반영.
    }

    public void searchResultNotice(bool Searched){
        if(Searched){
            smallNoticeText.text = + resultList.Count + "개의 검색결과가 있습니다.";
            smallNoticeText.gameObject.SetActive(true);
            noticeText.gameObject.SetActive(false);
        } else { 
            smallNoticeText.gameObject.SetActive(false);
            noticeText.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
