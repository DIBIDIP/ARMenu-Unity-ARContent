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
    private List<ARRestaurant> Restaurants;
    public string searchText;
    public bool searchSuccess = false; // 검색 성공?
    public int searchIndex = -1;        // 검색 성공하면 받을 인덱스

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
    }


    private void Update() {
    }

    public bool findMenuOrName(){
        // 검색어 (메뉴 이름 또는 가게 이름) 이 검색되면 관련된 아이템을 보여준다.

        if(searchText.Length > 0){
            bool resName = false;
            bool menName = false;
            
            ARRestaurant findItem = null; // 찾은 결과
            int cnt = 0;
            foreach(ARRestaurant index in Restaurants){
                resName = index.restaurantName.Contains(searchText);
                menName = index.menuName.Contains(searchText);
                if(resName || menName){
                    findItem = index;
                    searchIndex = cnt;
                    break; // 즉시 검색 로직 종료
                }
                cnt++;
            }

            if(findItem != null){
                Debug.Log("검색 결과 " + findItem.restaurantName);
                searchSuccess = true;
                return true;
            }
        }
        return false;
    }

    public ARRestaurant getIndexOfRestaurant(int index){
        if(index >= 0 && index <= Restaurants.Count){
            return Restaurants[index];
        }
        return null;
    }

    public ARRestaurant getSerachSuccessRestaurant(){
        if(!searchSuccess){
            return null;
        }
        return Restaurants[searchIndex];
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
            Restaurants.Add(new ARRestaurant(
                data[i]["_id"].ToString(), data[i]["restaurantName"].ToString(), data[i]["menuName"].ToString(), data[i]["description"].ToString(), data[i]["allergies"].ToString()
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