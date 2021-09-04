using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AddButton : MonoBehaviour
{
    public GameObject search_btn;
    public GameObject clear_btn;
    public TMP_InputField search;

    private void Update()
    {
        if (search.text != string.Empty)
        {
            search_btn.SetActive(true);
            clear_btn.SetActive(true);
        }
        else
        {
            search_btn.SetActive(false);
            clear_btn.SetActive(false);
        }
    }
}
