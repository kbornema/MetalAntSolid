using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenAnt : MonoBehaviour {

    [SerializeField]
    GameObject[] path;
    
    Rigidbody2D rb2d;
    AntVisual antVisual;
    [SerializeField]
    float speed = 3;

    [SerializeField]
    private Vector3 direction;

    int currentTarget = 0;

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

    public void Init(GameObject[] path)
    {
        this.path = path;
    }


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        antVisual = GetComponentInChildren<AntVisual>();
    }
	
	// Update is called once per frame
	void Update () {
		if (path != null)
        {
            if(currentTarget == path.Length)
            {
                Destroy(this.gameObject);
                return;
            }
            GameObject go = path[currentTarget];
            Vector3 direction = go.transform.position - transform.position;

            float angle = Mathf.Abs(Vector3.Angle(direction, Direction));
            if (angle > 5)
            {
                Direction = Vector3.Lerp(Direction, direction, 3 / angle);
            }
            else
                Direction = direction;
            antVisual.SetMovePercent(1.0f);
            rb2d.velocity = Direction * speed;

            if (direction.magnitude < 1)
                currentTarget = (currentTarget + 1);// % path.Length;
        }
	}
}
