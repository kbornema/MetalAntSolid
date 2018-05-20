using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WalkingBehavior
{
    Vector2 _direction;
    public Vector2 direction { get { return _direction; } }

    float _movePercent;
    public float movePercent { get { return _movePercent; } }

    public WalkingBehavior(Vector2 direction, float MovePercent)
    {
        _movePercent = Mathf.Clamp(MovePercent, 0f, 1f);
        _direction = direction;
    }

}