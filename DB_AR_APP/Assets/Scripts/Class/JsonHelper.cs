using System;
using UnityEngine;
using System.Collections;

public static class JsonHelper
{    
    [Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }

    public static T[] FromJson<T>(string json){
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
        if(wrapper.items == null){
            Debug.LogError("Json 값을 해당 자료형으로 변환 실패");
            return null;
        }
        Debug.Log(wrapper.items.Length);
        return wrapper.items;
    }

    public static string ToJson<T>(T[] array){
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return UnityEngine.JsonUtility.ToJson(wrapper);
    }
}