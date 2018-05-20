using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public Vector2 direction;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ants")
        {
            AntMovement ant = other.gameObject.GetComponent<AntMovement>();
            ant.Direction = this.direction;
        }

    }
}
