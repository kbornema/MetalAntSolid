using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntPool : ObjectPool {
    
    public override void ObjectInit(PooledObject pooledObject)
    {
        AntMovement ant = pooledObject.GetComponent<AntMovement>();
    }
}
