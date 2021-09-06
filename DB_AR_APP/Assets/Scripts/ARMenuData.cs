using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARContent;
using Newtonsoft.Json;
using LitJson;

public class ARMenuData : MonoBehaviour {
    // Json 데이터
    //

    private static ARMenuData instance;

    [SerializeField]
    private Dictionary<string, ARRestaurant> Restaurants;

    [SerializeField]
    private List<ARRestaurant> searchResultRestaurant;    // 검색결과
    public string searchText;
    public bool searchSuccess = false; // 검색 성공?
    [SerializeField]
    private string detailID;   // ID

    [SerializeField]
    private string prevSceneName;

    // 인스턴스 property
    public static ARMenuData Instance
    {
        get 
        {
            if(instance == null){
                var obj = FindObjectOfType<ARMenuData>();
                if(obj != null){
                    instance = obj;
                } else{
                    var newObj = new GameObject().AddComponent<ARMenuData>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    private void Awake() {
        var objs = FindObjectsOfType<ARMenuData>();
        if (objs.Length != 1){ // 이미 객체 존재
            Destroy(gameObject);
            return ;
        }
        DontDestroyOnLoad(gameObject);

        // 딕셔너리 형식으로 gameObject 정보를 저장한다.
        Restaurants = new Dictionary<string, ARRestaurant> ();
        prevSceneName = string.Empty;
    }


    private void Update() {
    }

    public void setPrevSceneName(string name){
        prevSceneName = name;
    }
    public string getPrevSceneName(){
        return prevSceneName;
    }
    // detail id 설정
    public void setDetailID(string id){
        this.detailID = id;
    }

    public string findByIdAndImage(string id){
        return Restaurants[id].imgUrl;
    }

    // ID로 DB에 저장된 값을 가져온다.
    public ARRestaurant getDetailItem(){
        return Restaurants[detailID];
    }

    public string getDetailID(){
        return this.detailID;
    }

    // DB에 저장된 모든 가게
    public bool findAll(){
        searchResultRestaurant.Clear(); // 리스트 초기화

        // 딕셔너리 모든 아이템 순회
        foreach(KeyValuePair<string, ARRestaurant> pair in Restaurants){
            ARRestaurant item = pair.Value;
            searchResultRestaurant.Add(item);
        }
        
        if(searchResultRestaurant.Count > 0){
            searchSuccess = true;
            return true;
        }
        
        return false;
    }

    // 검색 로직
    public bool findMenuOrName(){
        // 검색어 (메뉴 이름 또는 가게 이름) 이 검색되면 관련된 아이템을 보여준다.
        searchResultRestaurant.Clear(); // 리스트 초기화

        if(searchText.Length > 0){
            // 메뉴,가게명을 찾았는지 검사함.
            bool resName = false;
            bool menName = false;
            
            int cnt = 0;
            foreach(KeyValuePair<string, ARRestaurant> pair in Restaurants){
                ARRestaurant item = pair.Value;
                resName = item.restaurantName.Contains(searchText);
                menName = item.menuName.Contains(searchText);
                if(resName || menName){
                    searchResultRestaurant.Add(item); // 연관된 모든 아이템을 추가한다.
                }
                cnt++;
            }

            if(searchResultRestaurant.Count > 0){
                searchSuccess = true;
                return true;
            }
        }
        searchSuccess = false;
        return false;
    }

    public ARRestaurant getIdOfRestaurant(string id){
        return Restaurants[id];
    }

    // 검색 결과를 반환한다.
    public List<ARRestaurant> getSerachSuccessRestaurants(){
        if(!searchSuccess){
            return null;
        }
        // 
        return searchResultRestaurant;
    }


    // json to Object
    private T JsonToObject<T>(string json){
        return JsonConvert.DeserializeObject<T>(json);
    }

    public bool LitJsonToARContent(string json){
        if(json == null){
            return false;
        }
        JsonData data = JsonMapper.ToObject(json);

    
        for (int i = 0 ; i < data.Count; i++){
            // 알레르기 변환 
            string allerg = "";
            foreach (var word in data[i]["allergies"]){
                allerg += word.ToString();
            }
            // ID, DB 정보로 저장
            Restaurants.Add(data[i]["_id"].ToString() ,new ARRestaurant(
                data[i]["_id"].ToString(), data[i]["restaurantName"].ToString(), data[i]["menuName"].ToString(), data[i]["description"].ToString(), allerg
                , data[i]["ingredients"].ToString(), data[i]["nutrients"]["nt_calories"].ToString(),  data[i]["nutrients"]["nt_totalCarbohydrate"].ToString(), data[i]["nutrients"]["nt_totalSugars"].ToString(),
                data[i]["nutrients"]["nt_protein"].ToString(), data[i]["nutrients"]["nt_totalFat"].ToString(), data[i]["nutrients"]["nt_SaturatedFat"].ToString(),
                data[i]["nutrients"]["nt_transFat"].ToString(), data[i]["nutrients"]["nt_cholesterol"].ToString(), data[i]["nutrients"]["nt_sodium"].ToString(), data[i]["cookingTime"].ToString(),
                data[i]["releaseData"].ToString(), data[i]["fileUrl"].ToString(), data[i]["imgUrl"].ToString()
            ));
        }


        return true;
    }
    public bool JsonToARContent(string json){
        if(json == null){
            return false;
        }

        var datas = JsonToObject<List<ARRestaurant>>(json); 

        foreach(var data in datas){
            Debug.Log(data.menuName);
            Debug.Log(data.restaurantName);
            Debug.Log(data.cookingTime);
        }

        return true;
    }


    
}