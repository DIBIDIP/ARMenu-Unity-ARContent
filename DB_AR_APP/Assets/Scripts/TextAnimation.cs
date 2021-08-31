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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BounceFontSize(90); // 사이즈 값 부터 사인값으로 바운스, 효과
    }

    void BounceFontSize(int Size) {
        elapsed  += speed * Time.deltaTime;
        wave = Mathf.Sin( elapsed  ) * max + offset;
        noticeText.fontSize = Size +  (int) (wave * 10);

    }
}
