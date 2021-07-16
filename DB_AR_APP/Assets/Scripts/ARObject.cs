using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    [SerializeField]
    private bool IsSelected;

    public bool Selected{
        get {
            return this.IsSelected;
        }
        set{
            IsSelected = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
