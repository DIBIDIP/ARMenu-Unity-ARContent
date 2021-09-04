using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextAnimation : MonoBehaviour
{
    [SerializeField]
    Text noticeText;
    
    [Header("속도, 최대, 웨이브, 주기, 추가값")]
    [SerializeField]    [Range(0f, 10f)]
    private float speed;
    public float max = 0.5f;
    public float wave = 0.0f;
    private float elapsed  = 0.0f;
    public float offset;

    private int textSize;

    bool startAni = false;
    int plusDotCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        textSize = noticeText.fontSize;
        InvokeRepeating("plusDot", 0f, 0.5f);
    }

    public void setAni(bool start){
        startAni = start;
    }

    void plusDot(){
        if(plusDotCount == 3){
            plusDotCount = 0;
            noticeText.text = noticeText.text.Replace(".", string.Empty);
        }
        noticeText.text += ".";
        plusDotCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (startAni){
            plusDotCount = 1;
            if(plusDotCount != 0){
                plusDotCount = 0;
                CancelInvoke("plusDot");
            }
            BounceFontSize(textSize); // 사이즈 값 부터 사인값으로 바운스, 효과
        }else{
            
        }
    }

    void BounceFontSize(int Size) {
        elapsed  += speed * Time.deltaTime;
        wave = Mathf.Sin( elapsed  ) * max + offset;
        noticeText.fontSize = Size +  (int) (wave * 10);

    }
}
