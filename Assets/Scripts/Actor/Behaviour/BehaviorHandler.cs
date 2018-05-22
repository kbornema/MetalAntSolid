using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorHandler : MonoBehaviour {

    [SerializeField]
    GameObject BehaviorContainer;

    [SerializeField]
    SteeringAntBehavior[] steeringAntBehaviors;
    [SerializeField]
    GoToBehavior goToBehavior;

    [SerializeField]
    int currentBehavior = 0;
    
    Rigidbody2D rb2d;
    AntVisual antVisual;
    [SerializeField]
    float speed = 3;

    [SerializeField]
    private Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
        set
        {
            direction = value.normalized;
            float angle = Mathf.DeltaAngle(Mathf.Atan2(0, 0) * Mathf.Rad2Deg,
                               Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            this.transform.eulerAngles = new Vector3(
                this.transform.eulerAngles.x,
                this.transform.eulerAngles.y,
                (angle - 90) % 360
            );
        }
    }


    // Use this for initialization
    void Start () {
        steeringAntBehaviors = BehaviorContainer.GetComponentsInChildren<SteeringAntBehavior>();
        foreach(SteeringAntBehavior behavior in steeringAntBehaviors)
        {
            goToBehavior = behavior.GetComponent<GoToBehavior>();
            if (goToBehavior != null)
                break;
        }

        rb2d = GetComponent<Rigidbody2D>();
        antVisual = GetComponentInChildren<AntVisual>();
    }
	
	// Update is called once per frame
	void Update () {
        WalkingBehavior wb = null;

        if (goToBehavior != null && goToBehavior.isActiveAndEnabled && goToBehavior.GetTarget() != null)
        {
            wb = goToBehavior.GetWalkingBehavior();
        } else
        {
            while (currentBehavior < steeringAntBehaviors.Length)
            {
                if (steeringAntBehaviors[currentBehavior].isActiveAndEnabled == false)
                {
                    currentBehavior++;
                    if (currentBehavior >= steeringAntBehaviors.Length)
                        break;
                    steeringAntBehaviors[currentBehavior].Init();
                    Debug.Log("Switch Behavior to " + steeringAntBehaviors[currentBehavior].gameObject.name);
                }
                else
                {
                    wb = steeringAntBehaviors[currentBehavior].GetWalkingBehavior();
                    break;
                }
            }
        }
        


        if (wb == null)
        {
            //if no behavior is active: stepwise reduce speed
            if (rb2d.velocity.magnitude < 0.1)
            {
                rb2d.velocity = rb2d.velocity * 0.2f;
                antVisual.SetMovePercent(rb2d.velocity.magnitude);
            }
            else
            {
                antVisual.SetMovePercent(0f);
                rb2d.velocity = Vector2.zero;
            }
            //Reset all behaviors and start over
            foreach (SteeringAntBehavior behavior in steeringAntBehaviors)
            {
                behavior.gameObject.SetActive(true);
            }
            currentBehavior = 0;
        }
        else
        {
            //set behavior decision
            float angle = Mathf.Abs(Vector3.Angle(wb.direction, Direction));
            if (angle > 5)
            {
                Direction = Vector3.Lerp(Direction, wb.direction, 3 / angle);
            }
            else
                Direction = wb.direction;
            antVisual.SetMovePercent(wb.movePercent);
            rb2d.velocity = Direction * wb.movePercent * speed;
        }
        
	}
}
