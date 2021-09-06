using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemImage : MonoBehaviour
{
    [SerializeField]
    private string itemID;

    [SerializeField]
    private string itemImageBinary;

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

        // 싱글톤 객체를 통해 Image 바이너리 데이터를 받아온다
        itemImageBinary = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().findByIdAndImage(itemID);
        byte[] imageData = System.Text.Encoding.UTF8.GetBytes(itemImageBinary);

        texture = new Texture2D(165, 165);
        texture.LoadImage(imageData);

        sprite = Sprite.Create(texture, new Rect(0, 0, 165, 165), new Vector2(0.5f, 0.5f));
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
