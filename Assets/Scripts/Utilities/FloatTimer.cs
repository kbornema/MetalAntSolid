using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatTimer
{
    [SerializeField]
    private float _timer = 1;
    [SerializeField]
    private bool _autoReset = false;


    #region Properties

    public bool IsReached { get { return _isReached; } set { _isReached = value; } }
    public bool AutoReset { get { return _autoReset; } set { _autoReset = value; } }
    public float Timer { get { return _timer; } set { _timer = value; } }
    public int Counter { get { return _counter; } }

    #endregion

    private float _internTimer;
    private int _counter = 0;
    private bool _isReached = false;
    private bool _finished = false;

    public bool Finished { get { return _finished; } }
    public FloatTimer() { }

    public FloatTimer(float timer, bool autoReset)
    {
        _timer = timer;
        _autoReset = autoReset;
    }

    public float GetIncreasingValue()
    {
        return _internTimer;
    }

    public float GetDecreasingValue()
    {
        return _timer - _internTimer;
    }

    public float GetPercent()
    {
        return _internTimer / _timer;
    }

    public void Reset()
    {
        _internTimer = 0;
        _counter = 0;
        _isReached = false;
        _finished = false;
    }

    #region Event 
    [System.Serializable]
    public class FloatTimerEvent : UnityEvent<FloatTimer> { }

    [HideInInspector]
    private FloatTimerEvent onTimerReachedEvent = new FloatTimerEvent();

    public void InvokeTimerReached()
    {
        onTimerReachedEvent.Invoke(this);
    }

    public void AddTimerReachedListenerEvent(UnityAction<FloatTimer> listener)
    {
        onTimerReachedEvent.AddListener(listener);
    }
    #endregion

    private void Init(float timer)
    {
        _timer = timer;
    }

    public void Update()
    {

        if (_isReached)
        {
            if (_autoReset)
            {
                _isReached = false;
                return;
            }
            else
            {
                _isReached = true;
                return;
            }
        }

        _internTimer += Time.deltaTime;

        if (_internTimer >= _timer)
        {
            InvokeTimerReached();
            _internTimer = 0;
            _isReached = true;
            _finished = true;
            _counter++;
        }
    }
}

