using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MeshOrder : MonoBehaviour {

    public string layerName;
    public int orderInLayer;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    private void OnValidate()
    {
        Set();
    }

    private void Set()
    {
        if (_meshRenderer)
        {
            _meshRenderer.sortingLayerName = layerName;
            _meshRenderer.sortingOrder = orderInLayer;
        }
    }
}
