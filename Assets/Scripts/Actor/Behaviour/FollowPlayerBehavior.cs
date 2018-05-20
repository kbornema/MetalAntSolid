using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerBehavior : SteeringAntBehavior {

    public GameObject followTarget;
    bool wait = true;

    [SerializeField, Range(5, 15)]
    private float innerFollowRadius;

    [SerializeField, Range(15, 25)]
    private float outerFollowRadius;

    public void Start()
    {
    }

    public override WalkingBehavior GetWalkingBehavior()
    {
        Vector2 direction = followTarget.transform.position - transform.position;

        if (wait)
        {
            if (direction.magnitude > outerFollowRadius)
                wait = false;  
        }
        else
        {
            if (direction.magnitude < innerFollowRadius)
                wait = true;
        }

        if (wait)
            return new WalkingBehavior(direction, 0.0f);
        else
            return new WalkingBehavior(direction, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ants" && collision.GetComponent<TeamAssignment>().Team != GetComponentInParent<TeamAssignment>().Team)
        {
            gameObject.SetActive(false);
        }
    }

}
