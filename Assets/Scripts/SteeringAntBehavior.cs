using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Polarith.AI.Move;

public class SteeringAntBehavior : MonoBehaviour {

    [SerializeField]
    protected AIMContext aimContext;

    // Use this for initialization
    void Start () {
		aimContext = GetComponent<AIMContext>();
    }

    public virtual void Init() { }
   
    public virtual WalkingBehavior GetWalkingBehavior()
    {
        return new WalkingBehavior(aimContext.DecidedDirection, 1.0f);
    }
}
