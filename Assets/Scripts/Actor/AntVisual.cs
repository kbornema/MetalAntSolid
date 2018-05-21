using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntVisual : MonoBehaviour
{
    [SerializeField]
    private AntBodyPart[] _bodyParts;

    [SerializeField]
    private TeamColorSetting _colorSetting;

    [SerializeField]
    private Animator _animator;

    [SerializeField, Range(0.0f, 1.0f)]
    private float _movePercent = 0.0f;
    private float _lastMovePercent = -1.0f;

    [SerializeField]
    private int _armorLevel = 0;

    private void Start()
    {
        EnableArmorLevel(_armorLevel);
    }

    public void EnableArmorLevel(int level)
    {
        for (int i = 0; i < _bodyParts.Length; i++)
        {
            if (_bodyParts[i].ArmorLevel <= level)
                _bodyParts[i].gameObject.SetActive(true);
            else
                _bodyParts[i].gameObject.SetActive(false);
        }

        _armorLevel = level;
    }


    public void IncreaseArmorLevel()
    {
        EnableArmorLevel(_armorLevel + 1);
    }

    private void OnValidate()
    {
        Colorize();
    }

    private void Update()
    {
        if (_lastMovePercent != _movePercent)
        {
            _animator.SetFloat("MovePercent", _movePercent);
            _lastMovePercent = _movePercent;
        }
    }

    public void HideParts(params AntBodyPart.EBodyType[] bodyType)
    {
        SetBodyParts(false, bodyType);
    }

    public void ShowParts(params AntBodyPart.EBodyType[] bodyType)
    {
        SetBodyParts(true, bodyType);
    }

    public void SetBodyParts(bool val, params AntBodyPart.EBodyType[] bodyType)
    {
        foreach (var p in _bodyParts)
        {
            for (int i = 0; i < bodyType.Length; i++)
            {
                if (p.BodyType == bodyType[i])
                    p.gameObject.SetActive(val);
            }
        }
    }

    public void SetEating(bool val)
    {
        _animator.SetBool("Eating", val);
    }

    public bool IsEating()
    {
        return _animator.GetBool("Eating");
    }
    
	public void Colorize()
    {
        if (!_colorSetting)
            return;

        Dictionary<string, float> _randValues = new Dictionary<string, float>();

        foreach (var p in _bodyParts)
        {
            var key = p.Limb.ToString();

            float rand = 0.0f;

            if (_randValues.ContainsKey(key))
                rand = _randValues[key];
            else
            {
                rand = Random.value;
                _randValues.Add(key, rand);
            }


            if (p.BodyType == AntBodyPart.EBodyType.Main)
                p.SetColor(_colorSetting.GetBaseColor(rand));

            else if (p.BodyType == AntBodyPart.EBodyType.Armor)
                p.SetColor(_colorSetting.AntArmorColor);

            else if (p.BodyType == AntBodyPart.EBodyType.Eye)
                p.SetColor(_colorSetting.AntEyeColor);

            else if (p.BodyType == AntBodyPart.EBodyType.Weapon)
                p.SetColor(_colorSetting.WeaponColor);
        }          
    }


#if UNITY_EDITOR
    public void FindBodyTypes()
    {
        _bodyParts = gameObject.GetComponentsInChildren<AntBodyPart>();

        UnityEditor.EditorUtility.SetDirty(gameObject);
    }
#endif

    public void SetMovePercent(float p)
    {
        _movePercent = Mathf.Clamp(p, 0.0f, 1.0f);
    }
}
