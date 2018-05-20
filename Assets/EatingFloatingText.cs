using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EatingBhvr))]
public class EatingFloatingText : MonoBehaviour {

    [SerializeField]
    private EatingBhvr _eatingBhvr;
    [SerializeField]
    private float _worldTextOffset = 1.0f;
    [SerializeField]
    private Color _color;


    private FloatingText _curEatingText;
    private float _totalEarting = 0;

    private void OnValidate()
    {
        if (_eatingBhvr != null)
            _eatingBhvr.GetComponent<EatingBhvr>();
    }

    private void Start()
    {


        _eatingBhvr.AddOnEatingEventListener(EatingChanged);
    }

    public void StopFloatingText()
    {
        if (_curEatingText != null)
            Destroy(_curEatingText.gameObject);
    }

    private void EatingChanged(int arg0)
    {
        FloatingText cur;
        string amountString = null;

        _totalEarting += arg0;
        cur = _curEatingText;
        amountString = _totalEarting.ToString();

        Vector2 offset = new Vector2(UnityEngine.Random.value - 0.5f, _worldTextOffset);
        Vector3 pos = new Vector2(this.transform.position.x, this.transform.position.y) + offset;

        if(cur == null)
        {
            _totalEarting = arg0;
            _curEatingText = FloatingText.CreateFloatingText(_totalEarting.ToString(), pos, null, _color);
            amountString = _totalEarting.ToString();
            cur = _curEatingText;
        }
        cur.ResetAll(pos);
        cur.SetText(amountString, _color);
           
    }
}
