using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Wpn_Info : MonoBehaviour {


    public enum WeaponType {MachineGun, NormalGun, Sniper, ShotGun, Laser };
    public WeaponType weaponType;
    public ObjectPool bulletPool;

    public float fireSpeed; // time between 2 bullets
    public int additionalDamage;
    public int numberOfBulletsPerShot;
    public float rotationSpeed;
    public float aimRange;
    [Space]
    public float heatGenerationPerShot;
    public float heatDecreasePerSecond;
    public float heatLimit;
    public float overHeatingCoolDown;
    [Space]
    public float minSprayAngle;
    public float maxSprayAngle;
    public float sprayIncreasePerShot;
    public float sprayDecreasePerSecond;

    public MyAudio shootAudio;
    
    // Use this for initialization
    void Start () {
        SetObjectPoolManager();
	}
	
    public void SetObjectPoolManager()
    {
        ObjectPoolManager objectPoolManager = MatchManager.Instance().PoolManager;

        switch (weaponType)
        {
            case WeaponType.MachineGun:
                bulletPool = objectPoolManager.normalBulletPool.GetComponent<ObjectPool>();
                break;
            case WeaponType.NormalGun:
                bulletPool = objectPoolManager.normalBulletPool.GetComponent<ObjectPool>();
                break;
            case WeaponType.Sniper:
                bulletPool = objectPoolManager.sniperBulletPool.GetComponent<ObjectPool>();
                break;
            case WeaponType.ShotGun:
                bulletPool = objectPoolManager.shotGunBulletPool.GetComponent<ObjectPool>();
                break;
            case WeaponType.Laser:
                bulletPool = objectPoolManager.lazerBulletPool.GetComponent<ObjectPool>();
                break;
            default: Debug.Log("no weapon type assigned"); break;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void AddPermanentWeaponBuff(int additionalDamage, float precisionMultiplier, float firingSpeedMultiplier)
    {
        this.additionalDamage += additionalDamage;
        minSprayAngle *= 1 / precisionMultiplier;
        maxSprayAngle *= 1 / precisionMultiplier;
        fireSpeed *= 1 / firingSpeedMultiplier;
    }

    public void AddTempWeaponBuff(int additionalDamage, float precisionMultiplier, float firingSpeedMultiplier)
    {
        this.additionalDamage += additionalDamage;
        minSprayAngle *= 1 / precisionMultiplier;
        maxSprayAngle *= 1 / precisionMultiplier;
        fireSpeed *= 1 / firingSpeedMultiplier;
    }

    public void RemoveTempWeaponBuff(int additionalDamage, float precisionMultiplier, float firingSpeedMultiplier)
    {
        this.additionalDamage += additionalDamage;
        minSprayAngle *= precisionMultiplier;
        maxSprayAngle *= precisionMultiplier;
        fireSpeed *= firingSpeedMultiplier;
    }
}
