using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {

    AntMovement antMovement;
    TeamAssignment ownTeamAssignment;

    public void Start()
    {
        antMovement = GetComponentInParent<AntMovement>();
        ownTeamAssignment = GetComponentInParent<TeamAssignment>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ants")
        {
            // Stops GameObject2 moving
            TeamAssignment otherAntsTeam = other.gameObject.GetComponent<TeamAssignment>();
            if (otherAntsTeam.Team != ownTeamAssignment.Team)
            {
                antMovement.attackTarget = other.gameObject;
                Debug.Log("TargetLocked");
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (antMovement.attackTarget == collision.gameObject)
        {
            Debug.Log("TargetLost");
            antMovement.attackTarget = null;
        }
    }

}
