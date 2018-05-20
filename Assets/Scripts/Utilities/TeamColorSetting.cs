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

    public Color GetBaseColor(float rand)
    {
        return _baseColor.Evaluate(rand);
    }
}
