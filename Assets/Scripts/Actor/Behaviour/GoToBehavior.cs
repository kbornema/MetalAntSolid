using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBehavior : SteeringAntBehavior {

    [SerializeField]
    FollowerTarget walkingTarget;

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
            if (walkingTarget != null)
            {
                walkingTarget.RemoveFollower(this.gameObject);
                walkingTarget = null;
            }
            this.gameObject.SetActive(false);
            return new WalkingBehavior(direction, 0.0f);
        }
    }
    

    public void SetTarget(FollowerTarget target)
    {
        if (walkingTarget != null)
        {
            walkingTarget.RemoveFollower(this.gameObject);
        }
        walkingTarget = target;
        target.RegisterFollower(this.gameObject);
    }

    public GameObject GetTarget()
    {
        if (walkingTarget != null && walkingTarget.isActiveAndEnabled)
            return walkingTarget.gameObject;
        return null;
    }
}
