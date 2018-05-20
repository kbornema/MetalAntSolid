using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour {

    public GameObject attackTarget;
    [SerializeField]
    private FloatTimer attackCooldown;
    private ObjectPool bulletPool;

    private TeamAssignment teamAssignment;

    public AntVisual antVisual;

    [SerializeField]
    private  Vector3 direction;

    public void Awake()
    {
        ObjectPoolManager objectPoolManager = FindObjectOfType<ObjectPoolManager>();
        this.bulletPool = objectPoolManager.normalBulletPool.GetComponent<ObjectPool>();
        teamAssignment = gameObject.GetComponent<TeamAssignment>();
    }
    

    public Vector3 Direction
    {
        get { return direction; }
        set {
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

    public float speed = 1.0f;
    
	// Use this for initialization
	void Start () {
        teamAssignment = gameObject.GetComponent<TeamAssignment>();
    }
	
	// Update is called once per frame
	void Update () {
        attackCooldown.Update();
        if (attackTarget != null)
        {
            if (attackCooldown.Finished)
            {
                BaseBullet bullet = bulletPool.GetObject() as BaseBullet;
                bullet.InitBullet(transform.position, attackTarget.transform.position - transform.position, teamAssignment.Team);
                attackCooldown.Reset();
            }
           
        }
        else
        {
            transform.position += direction * Time.deltaTime* speed;
            antVisual.SetMovePercent(speed);
        }
    }
}
