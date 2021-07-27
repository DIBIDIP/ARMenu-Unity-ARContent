using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField]
    private float degreePerSeconds = 10.0f;

    // 회전량
    private float rotationDeltaY = 0.0f; 
    void Update()
    {
        this.rotationDeltaY += (degreePerSeconds * Time.deltaTime);
        transform.localEulerAngles = new Vector3(transform.rotation.x, rotationDeltaY, transform.rotation.z);
    }

    private void OnEnable() {
        Debug.Log("자동회전 활성화");
    }
}
