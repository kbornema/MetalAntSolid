using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TeamColorSetting : ScriptableObject
{   
    public Color AntArmorColor = Color.gray;
    public Color AntEyeColor = Color.black;

    public Color WeaponColor = Color.gray;

    [SerializeField]
    private Gradient _baseColor;

    [SerializeField]
    private Gradient _bloodColor;

    public Color GetBaseColor(float rand)
    {
        return _baseColor.Evaluate(rand);
    }

    public Color GetBloodColor(float rand)
    {
        return _bloodColor.Evaluate(rand);
    }
}
