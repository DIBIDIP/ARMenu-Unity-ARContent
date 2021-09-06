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
}
