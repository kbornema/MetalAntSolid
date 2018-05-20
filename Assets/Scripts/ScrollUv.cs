using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUv : MonoBehaviour {

    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private Vector2 _dir;

    private Material _mat;

    [SerializeField]
    private float _sinusSpeed = 1.0f;

    [SerializeField]
    private float _sinusAmplitude = 1.0f;

    private float _time = 0.0f;

    [SerializeField]
    private Vector2 _scale = new Vector2(1.0f, 1.0f);

	// Use this for initialization
	void Start ()
    {
        _mat = _renderer.material;
        _time = Random.Range(0.0f, Mathf.PI * 2.0f);
        _mat.mainTextureOffset = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        _mat.mainTextureScale = _scale;
	}
	
	// Update is called once per frame
	void Update () {

        _time += Time.deltaTime;

        float t = Mathf.Sin(_time * _sinusSpeed) * _sinusAmplitude;

        _mat.mainTextureOffset += _dir * t * _speed * Time.deltaTime;
	}
}
