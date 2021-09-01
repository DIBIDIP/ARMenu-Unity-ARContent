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
        }
        else
        {
            search_btn.SetActive(false);
        }
    }
}
