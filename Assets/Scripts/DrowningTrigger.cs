using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowningTrigger : MonoBehaviour {

    public GameObject drowningAnimation;
    public GameObject effectContainer;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ants")
        {
            // Stops GameObject2 moving
            AntMovement ant = other.gameObject.GetComponent<AntMovement>();
            GameObject splashAnim = Instantiate(drowningAnimation, effectContainer.transform);
            splashAnim.transform.position = other.transform.position;
            Destroy(other.gameObject);
        }
    }

}
