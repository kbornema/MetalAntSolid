using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EatingBhvr : MonoBehaviour {

    Rewired.Player _player;
    #region Events
    [HideInInspector]
    public class EatingEvent : UnityEvent<int> { }
    [HideInInspector]
    private EatingEvent _eatingEvent = new EatingEvent();

    [SerializeField]
    private AntVisual _visual;

    public void InvokeEatingEvent(int a)
    {
        _eatingEvent.Invoke(a);
    }
    public void AddOnEatingEventListener(UnityAction<int> listener)
    {
        _eatingEvent.AddListener(listener);
    }
    #endregion
    [SerializeField]
    private Actor _actor;

    [SerializeField]
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _player = Rewired.ReInput.players.GetPlayer(_actor.PlayerID);

        if(_rigidbody)
            _rigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    private void Update()
    {
        if (_visual)
        {
            if (_player.GetButtonDown("ButtonA") && !_visual.IsEating())
                _visual.SetEating(true);
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Trash")
        {
            var trash = collision.gameObject.GetComponent<Trash>();

            if (_player.GetButtonDown("ButtonA"))
            {
                var value = trash.EatingTrash();
                InvokeEatingEvent(value);
            }
        }
    }



}
