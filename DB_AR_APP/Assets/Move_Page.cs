using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Page : MonoBehaviour
{
    public GameObject[] Tutorial;
    public GameObject left_btn;
    public GameObject right_btn;
    private int cnt = 0;

    public void Update()
    {
        if (cnt == 0)
        {
            right_btn.SetActive(true);
            left_btn.SetActive(false);
        }
        else if (cnt == Tutorial.Length - 1)
        {
            right_btn.SetActive(false);
            left_btn.SetActive(true);
        }
        else
        {
            right_btn.SetActive(true);
            left_btn.SetActive(true);
        }


    }

    public void Prev()
    {
        if (cnt > 0)
        {
            Tutorial[cnt].SetActive(false);

            cnt--;
            Tutorial[cnt].SetActive(true);
        }
    }
    
    public void Next()
    {
        if (cnt < Tutorial.Length - 1)
        {
            Tutorial[cnt].SetActive(false);
            
            cnt++;
            Tutorial[cnt].SetActive(true);
        }
    }
}
