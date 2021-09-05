using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    private bool IsButtonClick = false; // 반복 클릭 방지

    // 현재 페이지 위치
    public int cnt = 0;

    public void Update()
    {
        // Page에 따라 버튼 보여짐
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


    // 터치로 화면 
    private void TouchSlide(){
        // 버튼과 동시에 실행 안되게 종료
        if(IsButtonClick){
            return;
        }
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
                IsMoved = true;
            }
            else if (result > 800  && cnt != 2){
                cnt++;
                IsMoved = true;
            }
        }

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
                    IsMoved = true;
                }
                else if (result > 800 && cnt != 2){
                    cnt++;
                    IsMoved = true;
                }
            }
        }
      
        #endif
        if(IsMoved){
            DoMove(0.5f, Ease.OutQuad);
        }

    }

    private void DoMove(float duration, Ease ease){
        view.transform.DOLocalMoveX(-1080 * cnt, duration).SetEase(ease).OnComplete(() => {
            IsMoved = false;
        });
    }

    // 버튼 클릭 하면 실행되는 함수
    public void MoveClick(bool left){
        if(IsButtonClick){
           return; 
        }
        // 방향 조건 검사
        if(left){
            cnt--;
        }else{
            cnt++;
        }
        // 움직이는 로직 시작
        IsButtonClick = true;
        DoMove(1.0f, Ease.InOutQuad);
        Invoke("ButtonClickDelay", 0.7f);   // Delay On

    }
    // 반복 클릭 방지
    private void ButtonClickDelay(){
        IsButtonClick = false;
    }
}
