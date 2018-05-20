using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PooledObject : MonoBehaviour
{

    public ObjectPool Pool { get; set; }

    public void ReturnToPool()
    {
        if (Pool)
        {
            Pool.AddObject(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}