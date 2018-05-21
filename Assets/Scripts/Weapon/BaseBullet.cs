using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseBullet : PooledObject
{
    [SerializeField]
    private Vector2 _dir;
    private int team;

    public Vector3 Direction
    {
        get { return _dir; }
        set
        {
            _dir = value.normalized;
            float angle = Mathf.DeltaAngle(Mathf.Atan2(0, 0) * Mathf.Rad2Deg,
                               Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg);

            this.transform.eulerAngles = new Vector3(
                this.transform.eulerAngles.x,
                this.transform.eulerAngles.y,
                angle % 360
            );
        }
    }


    [SerializeField]
    private Rigidbody2D _body2D;
    [SerializeField]
    private float _forwardForce;
    [SerializeField]
    private float knockBackForce;

    [SerializeField]
    private int damage;

    [SerializeField]
    private FloatTimer _lifeTime;

    public void InitBullet(Vector2 startPos, Vector2 dir, int team)
    {
        InitBullet(dir);
        transform.position = startPos;
        this.team = team;
    }

    public void InitBullet(Vector2 dir)
    {
        Direction = dir.normalized;
        _body2D.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _lifeTime.Update();
        if (_lifeTime.IsReached)
            ReturnToPool();
    }

    private void FixedUpdate()
    {
        _body2D.velocity = Direction * _forwardForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ants" || collision.tag == "Player")
        {
            if (collision.GetComponent<TeamAssignment>().Team != team)
            {
                collision.GetComponent<Health>().Damage(damage);
                Vector2 knockBack = Direction.normalized * knockBackForce;
                Debug.Log("Knocking ant back with: " + knockBack);
                collision.GetComponent<Rigidbody2D>().position += knockBack;
                ReturnToPool();
            }
        }
        else if (collision.tag == "Environment")
        {
            ReturnToPool();
        }
    }

    public void AddAdditonalDamage(int additionalDamage)
    {
        damage += additionalDamage;
    }
}



