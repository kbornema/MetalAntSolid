using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Polarith.AI.Move;


public class RangedAttackBehavior : SteeringAntBehavior {

    public GameObject followTarget;
    bool wait = true;

    [SerializeField, Range(5, 15)]
    private float innerFollowRadius;

    [SerializeField, Range(15, 25)]
    private float outerFollowRadius;

    FloatTimer retargetTimer = new FloatTimer(5, false);

    [SerializeField]
    private float stopPursuing;

    public void Start()
    {
        
    }

    public override WalkingBehavior GetWalkingBehavior()
    {
        retargetTimer.Update();
        if (retargetTimer.Finished)
        {
            followTarget = SearchClosestEnemy();
            retargetTimer.Reset();
        }

        if (followTarget == null)
        {
            gameObject.SetActive(false);
            return null;
        }

        Vector2 direction = followTarget.transform.position - transform.position;
        if (direction.magnitude > stopPursuing)
        {
            // Debug.Log("direction.magnitude: " + direction.magnitude + ", " + stopPursuing);
            gameObject.SetActive(false);
            return new WalkingBehavior(direction.normalized, 0.0f);
        }


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
            return new WalkingBehavior(direction.normalized, 0.0f);
        else
            return new WalkingBehavior(direction.normalized, 1.0f);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ants" && collision.GetComponent<TeamAssignment>().Team != GetComponent<TeamAssignment>().Team)
        {
            gameObject.SetActive(false);
        }
    }

    private GameObject SearchClosestEnemy()
    {
        int ownTeam = GetComponentInParent<TeamAssignment>().Team;

        GameObject closestEnemy = null;
        float minDistance = float.MaxValue;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<TeamAssignment>().Team == ownTeam)
                continue;

            float distance = (player.transform.position - transform.position).magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = player;
            }
        }


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ants");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<TeamAssignment>().Team == ownTeam)
                continue;

            float distance = (enemy.transform.position - transform.position).magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public override void Init() {
        followTarget = SearchClosestEnemy();
    }

}
