using System;
using UnityEngine;

public class AntBodyPart : MonoBehaviour
{
    public enum EBodyType { None, Main, Armor, Eye, Weapon }

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer GetSpriteRenderer() { return _spriteRenderer; }

    public EBodyType BodyType = EBodyType.Main;

    public int _armorLevel = 0;

    private void Reset()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color baseColor)
    {
        if (_spriteRenderer)
            _spriteRenderer.color = baseColor;
    }
}
