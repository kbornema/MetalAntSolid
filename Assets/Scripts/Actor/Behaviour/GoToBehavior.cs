using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBehavior : SteeringAntBehavior {

    [SerializeField]
    GameObject walkingTarget;

    [SerializeField]
    float targetDistance = 2;
    

    public override WalkingBehavior GetWalkingBehavior()
    {
        Vector3 direction = walkingTarget.transform.position - this.transform.position;
        if (direction.magnitude > targetDistance)
        {
            return new WalkingBehavior(direction, 1.0f);
        }
        else
        {
            walkingTarget = null;
            this.gameObject.SetActive(false);
            return new WalkingBehavior(direction, 0.0f);
        }
    }
    

    public void SetTarget(GameObject target)
    {
        walkingTarget = target;
    }

    public GameObject GetTarget()
    {
        return walkingTarget;
    }
}
