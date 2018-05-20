using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TeamColorSetting : ScriptableObject
{
    public Color AntBaseColor = Color.white;
    public Color AntArmorColor = Color.gray;
    public Color AntEyeColor = Color.black;

    public Color WeaponColor = Color.gray;
}
