using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// 검색 관련 모든 스크립트
public class ARSearch : MonoBehaviour
{   
    [SerializeField]
    private TMP_InputField searchTextField;

    private TouchScreenKeyboard keyboard;
    public string text;


    private void Start() {
    }

    public void OnKeyboard(){
        Debug.Log("키보드 ON");
        keyboard = TouchScreenKeyboard.Open(text, TouchScreenKeyboardType.Search);
    }

    public void OffKeyboard(){

    }

    public void searchTextClear(){
        searchTextField.text = string.Empty;
        if(keyboard != null){
            keyboard.text = string.Empty;
        }
    }

    public void searchButtonEvent(){
        // 모두 검색인 경우
        if (searchTextField.text == "모두"){
            bool search = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().findAll();
            
            // 다시 리스트 만들기
            if (SceneManager.GetActiveScene().name == "MenuList Scene"){
                GetComponent<SearchList>().searchResult(search);
            }
            return ;
        }

        // 검색 로직 시작 
        bool IsFind = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().findMenuOrName();

        if (SceneManager.GetActiveScene().name == "MenuList Scene"){
            // 다시 리스트 만들기
            GetComponent<SearchList>().searchResult(IsFind);
        }
    }

    // 검색어을 외부로 받아 싱글톤에 전달한다.
    public void searchTextTransToARData(string text){
        text = Regex.Replace(text, @"\s", ""); // 공백 제거
        GameObject.Find("ARMenuData").GetComponent<ARMenuData>().searchText = text;
    }

    private void searchReturnEvent(){        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            searchButtonEvent();    // 검색 시작
            if (SceneManager.GetActiveScene().name == "Main Scene")
            {
                SceneManager.LoadScene("MenuList Scene");
            }
        }
        
        if(keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done){

            searchButtonEvent();
            if (SceneManager.GetActiveScene().name == "Main Scene")
            {
                SceneManager.LoadScene("MenuList Scene");
            }
        }
    }

    private void Update() {
        // 검색창 조건 검사
        #if UNITY_EDITOR
        searchTextTransToARData(searchTextField.text);   // 검색어를 싱글톤 객체에 전달.
        
        #endif
        #if UNITY_ANDROID
        searchTextTransToARData(searchTextField.text);   // 검색어를 싱글톤 객체에 전달.
        #endif

        if (searchTextField.text != string.Empty){
            searchReturnEvent();  // 키 입력을 받는 이벤트
        }else{
        }
    }
}
