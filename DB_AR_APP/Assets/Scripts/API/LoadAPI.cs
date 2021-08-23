using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAPI : MonoBehaviour
{

    string resultJson;
    
    public bool IsSuccesLoadData = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadData());
    }

    // HTTP 통신을 통해 Json 받아오기
    IEnumerator LoadData() {
        string getDataURL = "";
        // HTTP Get 요청 보내기
        using(UnityWebRequest www = UnityWebRequest.Get(getDataURL)) 
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError){ // 불러오기 실패 
                Debug.Log(www.error);
            }else{
                if (www.isDone){ // 통신 성공
                    // json으로 받기
                    IsSuccesLoadData = true;
                    resultJson = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    
                    Debug.Log(resultJson);
                }
            }
        }
    }

}
