using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Polarith.AI.Move;

public class OrbitingAnt : SteeringAntBehavior {

    
    [SerializeField]
    GameObject homeZone;
    public GameObject HomeZone
    {
        set { homeZone = value; }
    }

    public AIMOrbit orbit;

    [SerializeField, Range(5, 25)]
    private float minOrbitRadius;

    [SerializeField, Range(5, 25)]
    private float maxOrbitRadius;

    public void Start()
    {
        orbit.Target = this.homeZone;
        orbit.Orbit.Radius = Random.Range(minOrbitRadius, maxOrbitRadius);
        if (Random.value > 0.5)
        {
            orbit.Orbit.DeltaAngle *= -1; 
        }
    }

    public override WalkingBehavior GetWalkingBehavior()
    {
        if (orbit.Target == null)
        {
            this.gameObject.SetActive(false);
        }
        return new WalkingBehavior(aimContext.DecidedDirection, 1.0f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.gameObject.SetActive(false);
            Debug.Log("deactive Orbit");
        }
        if (collision.tag == "Ants" && collision.GetComponent<TeamAssignment>().Team != GetComponentInParent<TeamAssignment>().Team)
        {
            this.gameObject.SetActive(false);
            Debug.Log("deactive Orbit");
        }
    }
}
