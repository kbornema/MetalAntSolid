using System;
using UnityEngine;

public class AntBodyPart : MonoBehaviour
{
    public enum EBodyType { None, Main, Armor, Eye, Weapon }
    public enum ELimb { Head, Sensors, LegsFront, LegsCenter, LegsBack, BodyFront, BodyCenter, BodyEnd }

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer GetSpriteRenderer() { return _spriteRenderer; }

    public EBodyType BodyType = EBodyType.Main;

    public int ArmorLevel = 0;
    public ELimb Limb;

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
