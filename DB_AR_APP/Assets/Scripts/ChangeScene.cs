using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string  sceneName;
    public void LoadScene(){
        EditorSceneManager.LoadScene(sceneName);
    }
}
