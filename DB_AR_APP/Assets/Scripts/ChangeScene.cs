using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string  sceneName;

    [SerializeField]
    private string prevScene;

    [SerializeField]
    private TMPro.TMP_Text scanNotice;
    private bool scanSceneIDNone = false;

    private void Start() {
        prevScene = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().getPrevSceneName();
    }

    public void LoadScene(){
        if(prevScene == "MenuInfo Scene"){
            SceneManager.LoadScene("ARScan Scene");
            GameObject.Find("ARMenuData").GetComponent<ARMenuData>().setPrevSceneName(string.Empty);
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
    public void setScanSceneIDNone(bool None){
        scanSceneIDNone = None;
    }
    public void ScanSceneInfoClick(){
        if (scanSceneIDNone){
            scanNotice.gameObject.SetActive(true);
            Invoke("deleteNotice", 1.0f);
            return ;
        }
        SceneManager.LoadScene("MenuInfo Scene");
        GameObject.Find("ARMenuData").GetComponent<ARMenuData>().setPrevSceneName("MenuInfo Scene");
    }
        
    private void deleteNotice(){
        scanNotice.gameObject.SetActive(false);
    }

}
