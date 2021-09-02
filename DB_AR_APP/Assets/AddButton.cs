using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButton : MonoBehaviour
{
    public GameObject search_btn;
    public Text search;

    private void Update()
    {
        if (search.text != string.Empty)
        {
            search_btn.SetActive(true);
            // 싱글톤 객체에 검색어를 넘긴다.
            GameObject.Find("ARMenuData").GetComponent<ARMenuData>().searchText = search.text;
        }
        else
        {
            search_btn.SetActive(false);
        }
    }
}
