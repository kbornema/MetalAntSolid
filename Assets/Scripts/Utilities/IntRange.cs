using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntRange  {

    [SerializeField]
    private int _min;
    public int Min { get { return _min; } }
    [SerializeField]
    private int _max;
    public int Max { get { return _max; } }


}
