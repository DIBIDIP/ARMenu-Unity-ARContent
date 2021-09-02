using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAPI : MonoBehaviour
{
    string resultJson;
    
    public bool IsSuccesLoadData = false;
    [SerializeField]
    private string URL = "http://146.56.159.134:3000";

    // Start is called before the first frame update
    void Start()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable){
            Debug.LogError("인터넷 연결 안됨.");
        }else{
            StartCoroutine(LoadData());
        }
    }
    
    // json 처리
    void JsonParsing(){
        string json = JsonUtility.ToJson(resultJson);
        Debug.Log(json);
    }   

    T JsonToObject<T>(string json){
        return JsonUtility.FromJson<T>(json);
    }

    // HTTP 통신을 통해 Json 받아오기
    IEnumerator LoadData() {
        string getDataURL = URL + "/api/menus/all";
        // HTTP Get 요청 보내기
        using(UnityWebRequest request = UnityWebRequest.Get(getDataURL)) 
        {
            yield return request.SendWebRequest();    // 요청 보내기

            if (request.isNetworkError || request.isHttpError){ // 불러오기 실패 
                Debug.Log("요청 실패 : " + request.error);
            }else{  
                IsSuccesLoadData = true;
                resultJson = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);  
                //resultJson = request.downloadHandler.text;
                // 싱글톤 객체로 넘긴다.
                
                GameObject.Find("ARMenuData").GetComponent<ARMenuData>().LitJsonToARContent(resultJson);
                
                if (request.responseCode != 200 ){ // 통신 성공
                    // json으로 받기
                    Debug.LogError("통신 실패");
                }
            }
        }
    }

}
