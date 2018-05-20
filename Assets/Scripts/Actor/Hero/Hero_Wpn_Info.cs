using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Wpn_Info : MonoBehaviour {


    public enum WeaponType {MachineGun, NormalGun, Sniper };
    public WeaponType weaponType;
    public ObjectPool bulletPool;

    public float fireSpeed; // time between 2 bullets
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
    
    // Use this for initialization
    void Start () {
        ObjectPoolManager objectPoolManager = MatchManager.Instance().PoolManager;


        switch (weaponType){
            case WeaponType.MachineGun:
                bulletPool = objectPoolManager.normalBulletPool.GetComponent<ObjectPool>();
                break;
            case WeaponType.NormalGun:
                bulletPool = objectPoolManager.normalBulletPool.GetComponent<ObjectPool>();
                break;
            case WeaponType.Sniper:
                bulletPool = objectPoolManager.sniperBulletPool.GetComponent<ObjectPool>();
                break;
            default: Debug.Log("no weapon type assigned"); break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
