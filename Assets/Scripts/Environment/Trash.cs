using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    private const float SHAKEDURATION = 0.1f;

    [SerializeField]
    private int _capacity;
    [SerializeField]
    private int _eatingSteps;
    [SerializeField]
    private bool _isShaking = true;

    public int EatingTrash()
    {
        _shakeDuration = SHAKEDURATION;
        int result = 0;

        if(_capacity - _eatingSteps <= 0)
        {
            result = _capacity;
            _capacity = 0;
            return result;
        }
        else
        {
            _capacity -= _eatingSteps;
            return _eatingSteps;
        }
    }
    private float _shakeDuration = 0;
    
    [SerializeField]
    private float _shakeAmount = 0.15f;

    private Vector2 _originalPosition;

    private void Awake()
    {
        _originalPosition = this.transform.position;
    }
    private void Update()
    {   
        if (_capacity == 0)
            Destroy(this.gameObject);

        if (!_isShaking)
            return;

        if (_shakeDuration > 0)
        {
            this.gameObject.transform.position = new Vector3(_originalPosition.x, _originalPosition.y, 0) + Random.insideUnitSphere * _shakeAmount;
            _shakeDuration -= Time.deltaTime * 1;
        }
        else
        {
            _shakeDuration = 0;
            this.gameObject.transform.position = _originalPosition;

        }
    }
}
