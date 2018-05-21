using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAimMode
{
    PlayerControlled,
    Automated
}

public class Hero_Wpn_Controller : MonoBehaviour {

    Rewired.Player playerInput;

    public Hero_Wpn_Info wpnInfo;
    private HealthBar healthBar;

    public EAimMode aimMode;
    public GameObject Target;
    [HideInInspector]
    public GameObject Weapon;
    [HideInInspector]
    public BulletSpawnPoint bulletSpawnPoint;

    public Vector2 targetDirection;
    public Vector2 aimDirection;
    private Actor _actor;

    float currentCoolDown;
    public float currentHeat;
    public float currentHeatCooldown;
    public float currentSpray;
    TeamAssignment team;

	// Use this for initialization
	void Start () {
        aimDirection = Vector2.up;

        _actor = GetComponentInParent<Actor>();
        playerInput = Rewired.ReInput.players.GetPlayer(_actor.PlayerID);

        switchToWeapon(wpnInfo);
    }

    public void switchToWeapon(Hero_Wpn_Info _wpnInfo)
    {
        GameObject antVisual = this.transform.parent.GetComponentInChildren<AntVisual>().gameObject;
        bulletSpawnPoint = antVisual.GetComponentInChildren<BulletSpawnPoint>();
        Weapon = bulletSpawnPoint.transform.parent.gameObject;

        wpnInfo = _wpnInfo;
        currentCoolDown = _wpnInfo.fireSpeed;
        currentHeatCooldown = 0;
        currentHeat = 0;
        currentSpray = 0;

        healthBar = this.transform.parent.GetComponentInChildren<HealthBar>();

        team = GetComponentInParent<TeamAssignment>();
    }

    // Update is called once per frame
    void LateUpdate () {
        Vector2 preferredAimDirection = new Vector2(playerInput.GetAxis("LookX"), playerInput.GetAxis("LookY"));
        aimDirection = new Vector2(Weapon.transform.up.x, Weapon.transform.up.y);

        if(currentCoolDown > 0 )
            currentCoolDown -= Time.deltaTime;

        if (currentHeatCooldown > 0)
            currentHeatCooldown -= wpnInfo.heatDecreasePerSecond * Time.deltaTime;
        if (currentHeat > 0 && currentHeatCooldown <= 0)
            currentHeat -= wpnInfo.heatDecreasePerSecond * Time.deltaTime;

        if (currentSpray > wpnInfo.minSprayAngle)
        {
            currentSpray -= wpnInfo.sprayDecreasePerSecond * Time.deltaTime;
        }

        if (aimMode == EAimMode.PlayerControlled)
        {
            if(preferredAimDirection.magnitude > 0.05)
                RotateWeaponTowardsDirection(preferredAimDirection);
            if (ReadyToFire() && playerInput.GetAxis("LTrigger") > 0.5)
            {
                Fire();
            }
        }
	}

    public void RotateWeaponTowardsDirection(Vector2 preferredTargetDirection)
    {
        float angle = Mathf.Atan2(preferredTargetDirection.y, preferredTargetDirection.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        Weapon.transform.rotation = Quaternion.RotateTowards(Weapon.transform.rotation, q, Time.deltaTime * wpnInfo.rotationSpeed);

    }

    public bool ReadyToFire()
    {
        return currentCoolDown <= 0 && currentHeat < wpnInfo.heatLimit && currentHeatCooldown <= 0;
    }

    public void Fire()
    {
        currentCoolDown = wpnInfo.fireSpeed;
        BaseBullet bullet = wpnInfo.bulletPool.GetObject() as BaseBullet;

        Vector2 currentAimDirection = Quaternion.AngleAxis(Random.Range(-currentSpray, currentSpray), Vector3.forward) * aimDirection;
        bullet.InitBullet(bulletSpawnPoint.transform.position, currentAimDirection, team.Team);
        bullet.AddAdditonalDamage(wpnInfo.additionalDamage);


        currentHeat += wpnInfo.heatGenerationPerShot;
        if(currentHeat > wpnInfo.heatLimit)
        {
            currentHeatCooldown = wpnInfo.overHeatingCoolDown;
            WeaponOverheating();
            currentHeat = 0;
        }
        currentSpray += wpnInfo.sprayIncreasePerShot;
        currentSpray = Mathf.Clamp(currentSpray, wpnInfo.minSprayAngle, wpnInfo.maxSprayAngle);

        healthBar.ResetHealthBarFadeOut();
    }

    public void WeaponOverheating()
    {
        Debug.Log("Weapon is overheated for another: " + (currentHeat - wpnInfo.heatLimit));
    }

}
