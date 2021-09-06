using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField]
    Image progressBar;

    [SerializeField]
    Text notice;

    bool IsLoadingDone = false;
    bool IsError = false;

    // Start is called before the first frame update
    void Start()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable){
            notice.text = "인터넷 연결을 확인해주세요.";
            notice.gameObject.SetActive(true);
            IsError = true;
            return;
        }
        notice.GetComponent<TextAnimation>().setAni(false);
        StartCoroutine(Load());

    }

    private void Update() {
        #if UNITY_EDITOR
        if(IsLoadingDone && Input.GetMouseButtonDown(0)){
            SceneManager.LoadScene("Main Scene");
        }
        if (IsError && Input.GetMouseButtonDown(0)){
            Debug.Log("종료");
            Application.Quit();
        }

        #endif
        #if UNITY_ANDROID
        if(IsLoadingDone && Input.touchCount > 0){
            SceneManager.LoadScene("Main Scene");
        }
        if (IsError && Input.touchCount > 0){
            Debug.Log("종료");
            Application.Quit();
        }
        #endif
    }

    IEnumerator Load(){
        Debug.Log("로딩 시작");

        notice.text = "Loading";
        notice.gameObject.SetActive(true);
        // API Load Data
        bool apiLoad = GameObject.Find("APIManager").GetComponent<LoadAPI>().IsSuccesLoadData;
        while(!apiLoad){
            apiLoad = GameObject.Find("APIManager").GetComponent<LoadAPI>().IsSuccesLoadData;
            progressBar.fillAmount = GameObject.Find("APIManager").GetComponent<LoadAPI>().getProgress();
            yield return new WaitForSeconds(0.1f);
        }

        // DONE
        progressBar.fillAmount = 1f;

        // ERROR
        notice.gameObject.SetActive(true);

        if(apiLoad == false){
            notice.text = "서버와 연결이 해제됬습니다.";
            IsError = true;
        }
        else {
            IsLoadingDone = true;
            notice.text = "화면을 터치해주세요.";
            notice.GetComponent<TextAnimation>().setAni(true);
        }
    }

    IEnumerator LoadScene() {
        yield return null;

        // TODO :  비동기 통신
        AsyncOperation op = SceneManager.LoadSceneAsync("Main Scene");
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone){
            yield return null;

            timer += Time.deltaTime;
            if (op.progress < 0.9f)            
            {                
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)                
                {                    
                    timer = 0f;                
                }            
                else            
                {                
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                }
                if (progressBar.fillAmount == 1.0f)                
                {                    
                    op.allowSceneActivation = true;                    
                    yield break;                
                }           
            }
        }
    }
}

/*
씬은 슬라이더 값이 99% 이상일 떄 불러올 것이기 때문에 미리 operation.allowSceneActivation = false; 로 해둔다.
씬 전환은 1 프레임만에 이루어졌다. 그때 그때 경우에 따라 다르겠지만 이렇게 씬 전환이 눈깜짝할 새에 이루어지는 경우도 있고 이럴 땐 바로 operation.progress 값이 0.9 가 되버렸다.
그래서 씬 전환이 빨리 이루어졌다면 while 문 내의 if 는 실행 안되고 바로 else 만 실행 될지도 모른다.
operation.progress는 0.9 가 최대값인 것을 확인했다. 그냥 씬 전환이 완료되면 0.9 값이 되나보다.
그래도 슬라이더를 90%인 상태에서 멈출 수는 없으니, operation.progress가 0.9 이상일 때부인 else문에서는 슬라이더 값이 100%가 될때까지 timer로 보간해가며 부드럽게 100% 를 향해 슬라이더 값을 올린다.
Lerp 보간을 사용하면 딱 떨어지게 1f 가 되기는 힘들다. 그래서 슬라이더 값이 0.99f 만 넘게 되면 바로 로딩하는 씬을 활성화시키도록 하였따. 이때 비로소 씬을 활성화한다. operation.allowSceneActivation = true;
이 스크립트는 씬 전환이 완료되도 게임 씬에서 저장했던 내용 그대로를 불러오는 작업을 해야 하기 때문에 (즉, 📜SaveAndLoad의 LoadData 함수를 실행해야 하기 때문에) 싱글톤으로 만들었다. 씬이 전환되도 파괴되지 않고 전환된 게임 씬에서 theSaveAndLoad.LoadData();를 실행해야 함.
*/
