using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float degreePerSeconds = 5.0f;
    void Update()
    {
        float speed = degreePerSeconds * Time.deltaTime;
        gameObject.transform.Rotate(Vector3.up * speed, Space.World);
    }

    private void OnEnable() {
        Debug.Log("자동회전 활성화");
    }
}
