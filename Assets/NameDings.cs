using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDings : MonoBehaviour {

    Quaternion TextBarRotation;
    

    private void Awake()
    {
        TextBarRotation = this.transform.rotation;
    }
    private void LateUpdate()
    {
        this.transform.rotation = TextBarRotation;
    }
}
