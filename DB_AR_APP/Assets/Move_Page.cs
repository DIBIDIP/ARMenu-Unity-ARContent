using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Page : MonoBehaviour
{
    public GameObject[] Tutorial;
    public GameObject left_btn;
    public GameObject right_btn;

    // 부모 컴포넌트
    [SerializeField]
    private GameObject view;
    [SerializeField]
    private UnityEngine.UI.Text debugText;
    // 마우스 좌표 (x 좌표만)
    float MovsPos_x = 0.0f;
    float DownPost_x = 0.0f;
    bool IsMoved = false;   // 드래그 중인지 체크

    // 현재 페이지 위치
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
        // 터치 이벤트
        TouchSlide();
    }


    private void NowPage(){
    }

    // 터치로 화면 
    private void TouchSlide(){

        #if UNITY_EDITOR
        // 마우스 처리
        // 마우스 최초 클릭
        if (Input.GetMouseButtonDown(0)){
            MovsPos_x = Input.mousePosition.x;        
        }
        // 마우스 움직일때
        if (Input.GetMouseButton(0)){
            DownPost_x = Input.mousePosition.x;
        }

        if (Input.GetMouseButtonUp(0) && IsMoved == false){            
            float result = 590 - (DownPost_x - MovsPos_x);
            if (result < 400 && cnt != 0){
                cnt--;
                // 위치 이동
                Debug.Log("왼쪽으로 이동" + -1080 * cnt);
                IsMoved = true;
            }
            else if (result > 800  && cnt != 2){
                cnt++;
                Debug.Log("오른쪽으로 이동" + -1080 * cnt);
                IsMoved = true;
            }
        }

        if(IsMoved){
            view.transform.localPosition = Vector2.Lerp(view.transform.localPosition, new Vector2(-1080 * cnt, view.transform.localPosition.y), 0.1f);
            // 도착
            //Debug.Log(Mathf.Approximately((int)view.transform.position.x, (-1080 * cnt + 541)));
            if(closePosX(view.transform.localPosition, (-1080 * cnt))){
                IsMoved = false;
                //view.transform.position = new Vector3(-1080*cnt, view.transform.position.y, view.transform.position.z);
                Debug.Log("도착");
            }
        }
        //Debug.Log("움직임 : " + result);
        #endif
        #if UNITY_ANDROID
        // 터치 처리
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began){
                MovsPos_x = touch.position.x;

            }
            if(touch.phase == TouchPhase.Moved){
                DownPost_x = touch.position.x;
            }
            if(touch.phase == TouchPhase.Ended && IsMoved == false){
                float result = 590 - (DownPost_x - MovsPos_x);  // 중앙 값
                if (result < 400 && cnt != 0){
                  cnt--;
                    // 위치 이동
                    Debug.Log("왼쪽으로 이동" + -1080 * cnt);
                    IsMoved = true;
                }
                else if (result > 800 && cnt != 2){
                    cnt++;
                    Debug.Log("오른쪽으로 이동" + -1080 * cnt);
                    IsMoved = true;
                }
            }
        }
      
        if(IsMoved){
            view.transform.localPosition = Vector2.Lerp(view.transform.localPosition, new Vector2(-1080 * cnt, view.transform.localPosition.y), 0.1f);
            // 도착
            //Debug.Log(Mathf.Approximately((int)view.transform.localPosition.x, (-1080 * cnt)));
            if(closePosX(view.transform.localPosition, (-1080 * cnt))){
                IsMoved = false;
                //view.transform.position = new Vector3(-1080*cnt, view.transform.position.y, view.transform.position.z);
                Debug.Log("도착");
            }
        }

        #endif
    }

    private bool closePosX(Vector2 v1, float target_x){
        if((int)v1.x > target_x - 2 && (int)v1.x < target_x + 2){
            return true;
        }
        return false;
    }

    private bool IsButtonClick = false;
    public void Prev()
    {
        if(!IsButtonClick){
            IsButtonClick = true;
            cnt--;
            view.transform.localPosition = new Vector2((-1080 * cnt), view.transform.localPosition.y);
            Invoke("ButtonClickDelay", 0.7f);   // Delay On
        }
    }

    // 반복 클릭 방지
    private void ButtonClickDelay(){
        IsButtonClick = false;
    }
    
    public void Next()
    {
        if(!IsButtonClick){
            IsButtonClick = true;
            cnt++;
            view.transform.localPosition = new Vector2((-1080 * cnt), view.transform.localPosition.y);
            Invoke("ButtonClickDelay", 0.7f); // Delay On
        }
    }
}
