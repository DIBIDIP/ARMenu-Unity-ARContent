using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuItemImage : MonoBehaviour
{
    [SerializeField]
    private string itemID;
    private string imageBinaryString;

    [SerializeField]
    private Texture2D texture;
    
    [SerializeField]
    private Sprite sprite;

    public void setItemID(string id){
        this.itemID = id;
    }
    private void Start() {
        if(itemID == string.Empty)
        {
            Debug.Log("아이디가 비어있음");
            return ;
        }

        // 싱글톤 객체를 통해 Image 스프라이트를 받는다
        
        sprite = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().getImageSprite(itemID);

        GetComponent<Image>().sprite = sprite;
    }
}
