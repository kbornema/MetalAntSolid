using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standard_Weapon_Controller : MonoBehaviour {

    public Hero_Wpn_Info wpnInfo;

    public EAimMode aimMode;
    public GameObject Target;
    [HideInInspector]
    public GameObject antVisual;
    [HideInInspector]
    public GameObject Weapon;

    public Vector2 targetDirection;
    public Vector2 aimDirection;
    TeamAssignment teamAssignment;

    float currentCoolDown;
    float aimThreshold = 1;

    private BulletSpawnPoint bulletSpawnPoint;
    private FloatTimer reaimTimer = new FloatTimer(1, false);
    [SerializeField]
    RangedAttackBehavior rangedAttackBehavior;

    // Use this for initialization
    void Start () {
        aimMode = EAimMode.Automated;
        switchToWeapon(wpnInfo);
        aimDirection = Vector2.up;
        teamAssignment = GetComponentInParent<TeamAssignment>();
	}

    /*
    public void switchToWeapon(Hero_Wpn_Info _wpnInfo)
    {
        wpnInfo = _wpnInfo;
        currentCoolDown = _wpnInfo.fireSpeed;
        weaponSpawnPoint = Weapon.GetComponentInChildren<BulletSpawnPoint>().gameObject;

    }*/


    public void switchToWeapon(Hero_Wpn_Info _wpnInfo)
    {
        AntVisual antVisual = this.transform.parent.GetComponentInChildren<AntVisual>();
        if (antVisual != null)
        {
            bulletSpawnPoint = antVisual.GetComponentInChildren<BulletSpawnPoint>();
            Weapon = bulletSpawnPoint.transform.parent.gameObject;
        }
        else
        {
            bulletSpawnPoint = this.transform.parent.GetComponentInChildren<BulletSpawnPoint>();
            Weapon = bulletSpawnPoint.transform.parent.gameObject;
        }

        wpnInfo = _wpnInfo;
        currentCoolDown = _wpnInfo.fireSpeed;

        BehaviorHandler behaviorHandler = gameObject.GetComponentInParent<BehaviorHandler>();
        if (behaviorHandler != null)
        {
            rangedAttackBehavior = this.transform.parent.gameObject.GetComponentInChildren<RangedAttackBehavior>();
        }
    }

    // Update is called once per frame
    void LateUpdate () {
        aimDirection = new Vector2(Weapon.transform.up.x, Weapon.transform.up.y);
        if(currentCoolDown > 0 )
            currentCoolDown -= Time.deltaTime;

        if (aimMode == EAimMode.Automated)
        {
            reaimTimer.Update();
            if (reaimTimer.Finished)
            {
                Target = FindTarget();
                reaimTimer.Reset();
            }
            if (Target != null)
            {
                targetDirection = Target.transform.position - this.transform.position;
                RotateWeaponTowardsTarget(Target);
            }
            if (ReadyToFire() == true && Target != null)
            {
                
                if (ReadyToFire() == true && IsAimOnTarget() == true)
                {
                    Fire();
                }
            }
        }
        else if (aimMode == EAimMode.PlayerControlled)
        {

        }
	}

    
    public GameObject FindTarget()
    {
        if (rangedAttackBehavior != null)
        {
            return rangedAttackBehavior.followTarget;
        }
        
        List<GameObject> targetList = new List<GameObject>();
        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, wpnInfo.aimRange))
        {
            TeamAssignment otherAntsTeam = coll.gameObject.GetComponent<TeamAssignment>();
            if ((coll.tag == "Ants" || coll.tag == "Player") && otherAntsTeam.Team != teamAssignment.Team)
            {
                targetList.Add(coll.gameObject);
            }
        }

        if (targetList.Count == 0 )
        {
            return null;
        }

        float closestTarget = float.MaxValue;
        GameObject currentTarget = null;
        foreach (GameObject go in targetList)
        {
            Vector2 currentTargetDirection = go.transform.position - this.transform.position;
            if (currentTargetDirection.magnitude < closestTarget)
            {
                closestTarget = currentTargetDirection.magnitude;
                currentTarget = go;
            }
        }
        return currentTarget;
    }

    public void RotateWeaponTowardsTarget(GameObject Target)
    {
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        Weapon.transform.rotation = Quaternion.RotateTowards(Weapon.transform.rotation, q, Time.deltaTime * wpnInfo.rotationSpeed);
    }

    public bool IsAimOnTarget()
    {
        Vector2 currentTargetDirection = Target.transform.position - this.transform.position;
        return Mathf.Abs(Vector2.Angle(aimDirection, currentTargetDirection)) < aimThreshold;
    }

    public bool ReadyToFire()
    {
        return currentCoolDown <= 0;
    }

    public void Fire()
    {
        currentCoolDown = wpnInfo.fireSpeed;
        BaseBullet bullet = wpnInfo.bulletPool.GetObject() as BaseBullet;
        bullet.InitBullet(bulletSpawnPoint.transform.position, aimDirection, teamAssignment.Team);
        bullet.AddAdditonalDamage(wpnInfo.additionalDamage);
        
    }

}
