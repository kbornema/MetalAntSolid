using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDecay : MonoBehaviour {

    [SerializeField]
    public SpriteRenderer TheSpriteRenderer;

    [SerializeField]
    private float _startAlpha = 0.6f;
    [SerializeField]
    private float _duration = 5.0f;
    [SerializeField]
    private float _randTimeDeviation = 2.0f;
    [SerializeField, Range(0.0f, 1.0f)]
    private float _chancePermanent;


    private float _maxDur;
    private float _curDur;

	// Use this for initialization
	void Start ()
    {
        _maxDur = _duration + Random.Range(-0.5f * _randTimeDeviation, 0.5f * _randTimeDeviation);
        _curDur = _maxDur;

        SetColor(_startAlpha);

        if (Random.value < _chancePermanent)
            enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float alpha = (_curDur / _maxDur) * _startAlpha;

        SetColor(alpha);

        _curDur -= Time.deltaTime;

        if(_curDur <= 0.0f)
        {
            Destroy(gameObject);
        }

    }

    private void SetColor(float alpha)
    {
        Color c = TheSpriteRenderer.color;
        c.a = alpha;
        TheSpriteRenderer.color = c;
    }
}
