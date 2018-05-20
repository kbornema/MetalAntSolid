using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShadowSprite : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _basedOn;
    [SerializeField]
    private Color _shadowColor = new Color(0.0f, 0.0f, 0.0f, 0.25f);
    [SerializeField]
    private Vector2 _shadowDirection = new Vector2(-0.25f, -0.25f);
    [SerializeField]
    private Vector3 _localScale = new Vector3(0.99f, 0.99f, 0.99f);

    [SerializeField]
    private SpriteRenderer _shadowSprite;

    private Vector3 _lastPos;

    private const float POS_DIFF = 0.001f;

    private void Reset()
    {
        _basedOn = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if(_basedOn && !_shadowSprite)
            Create();

        if(_shadowSprite)
        {
            var shadowT = _shadowSprite.transform;

            if (!Application.isPlaying || (_lastPos - shadowT.position).sqrMagnitude > POS_DIFF)
            {   
                _lastPos = _shadowSprite.transform.position;

                shadowT.localPosition = shadowT.InverseTransformDirection(_shadowDirection);
                shadowT.localScale = _localScale;

                _shadowSprite.sortingOrder = _basedOn.sortingOrder - 1;

                if (_shadowSprite.sprite != _basedOn.sprite)
                    _shadowSprite.sprite = _basedOn.sprite;

                if(_shadowSprite.color != _shadowColor)
                    _shadowSprite.color = _shadowColor;
            }
        }
    }

    private void Create()
    {
        _shadowSprite = new GameObject("shadow").AddComponent<SpriteRenderer>();
        _shadowSprite.transform.SetParent(_basedOn.transform);
        _shadowSprite.sortingLayerName = _basedOn.sortingLayerName;
        _shadowSprite.transform.localScale = _localScale;
        _shadowSprite.transform.localRotation = Quaternion.identity;
    }
}
