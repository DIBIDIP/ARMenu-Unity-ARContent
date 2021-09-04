using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARContent;
using UnityEngine.UI;
public class MenuDetail : MonoBehaviour
{   
    public Text menuName;
    public Text menuDesc;
    public Text allergy;
    public Text ingredients;
    public GameObject nutrient;
    
    [SerializeField]
    private ARRestaurant item;

    
    private void Awake() {
        item = GameObject.Find("ARMenuData").GetComponent<ARMenuData>().getDetailItem();

        applyDataToContent();
    }

    // DB데이터들을 반영함.
    private void applyDataToContent(){
        menuName.text = item.menuName;
        menuDesc.text = item.description;
        allergy.text = item.allergies;
        ingredients.text = item.ingredients;

        // 영양성분
        nutrient.GetComponentsInChildren<Text>()[0].text = item.calories + "cal";
        nutrient.GetComponentsInChildren<Text>()[1].text = item.totalCarbohydrate + "g";
        nutrient.GetComponentsInChildren<Text>()[2].text = item.protein + "g";
        nutrient.GetComponentsInChildren<Text>()[3].text = item.totalFat + "g";
        nutrient.GetComponentsInChildren<Text>()[4].text = item.sodium + "mg";
        nutrient.GetComponentsInChildren<Text>()[5].text = item.totalSugars + "g";
        nutrient.GetComponentsInChildren<Text>()[6].text = item.SaturatedFat + "g";
        nutrient.GetComponentsInChildren<Text>()[7].text = item.transFat + "g";
        nutrient.GetComponentsInChildren<Text>()[8].text = item.cholesterol + "mg";
    }
}
