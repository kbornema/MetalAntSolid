using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBorder : MonoBehaviour {

    public Vector2 bouncingNormal;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ants")
        {
            if(other.GetComponent<Hero_Movement>() != null)
            {
                Debug.Log("Cannot Bounce Hero");
                return;
            }
            AntMovement ant = other.gameObject.GetComponent<AntMovement>();
            ant.Direction = Vector2.Reflect(ant.Direction, bouncingNormal);
        }
    }

}
