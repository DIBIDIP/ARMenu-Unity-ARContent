using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApp : MonoBehaviour
{
    public GameObject exit_bg;
    public GameObject exit_rect;
    public GameObject exit_yes;
    public GameObject exit_no;
    public GameObject exit_text;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exit_bg.SetActive(true);
            exit_rect.SetActive(true);
            exit_yes.SetActive(true);
            exit_no.SetActive(true);
            exit_text.SetActive(true);
        }
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void No()
    {
        exit_bg.SetActive(false);
        exit_rect.SetActive(false);
        exit_yes.SetActive(false);
        exit_no.SetActive(false);
        exit_text.SetActive(false);
    }
}
