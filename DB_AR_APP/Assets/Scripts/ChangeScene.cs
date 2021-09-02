using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string  sceneName;
    public void LoadScene(){
        SceneManager.LoadScene(sceneName);
    }

    public void searchButtonClick(){
        // 검색 로직 시작 
        GameObject.Find("ARMenuData").GetComponent<ARMenuData>().findMenuOrName();
    }
}
