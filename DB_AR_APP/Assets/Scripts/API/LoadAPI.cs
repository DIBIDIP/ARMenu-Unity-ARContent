using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAPI : MonoBehaviour
{
    private string resultJson; // Json 데이터
    
    public bool IsSuccesLoadData = false;
    [SerializeField]
    private string URL = "http://146.56.159.134:3000";

    [SerializeField]
    private float loadProgress;

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

    public float getProgress(){
        return loadProgress;
    }

    // HTTP 통신을 통해 Json 받아오기
    public IEnumerator LoadData() {
        string getDataURL = URL + "/api/menus/all";
        // HTTP Get 요청 보내기
        using(UnityWebRequest request = UnityWebRequest.Get(getDataURL)) 
        {
            var asyncOperation = request.SendWebRequest();    // 요청 보내기
            while (!asyncOperation.isDone){
                //Debug.Log(request.downloadProgress);
                loadProgress = request.downloadProgress;
                yield return null;
            }

            if(request.error != null){
                Debug.LogError("API 통신 중 에러 발생", this);
                yield break;
            }
            else{  
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
