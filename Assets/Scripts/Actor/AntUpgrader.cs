using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntUpgrader : MonoBehaviour {

    [HideInInspector]
    Health health;
    [HideInInspector]
    Hero_Wpn_Info weaponInfo;
    [HideInInspector]
    Hero_Wpn_Controller heroWeapon;
    [HideInInspector]
    Standard_Weapon_Controller standardWeapon;
    [HideInInspector]
    AntVisual antVisual;

    [HideInInspector]
    SpawnFollower spawnFollower;

    bool isHero;

    public int antMaxLevel;
    public int antLevel;

	// Use this for initialization
	void Start () {
        health = this.GetComponent<Health>();
        antVisual = this.GetComponentInChildren<AntVisual>();
        heroWeapon = this.GetComponentInChildren<Hero_Wpn_Controller>();
        standardWeapon = this.GetComponentInChildren<Standard_Weapon_Controller>();
        if (heroWeapon != null)
        {
            isHero = true;
            weaponInfo = heroWeapon.wpnInfo;
            spawnFollower = this.GetComponent<SpawnFollower>();
        }
        else
        {
            isHero = false;
            weaponInfo = standardWeapon.wpnInfo;
        }
        antVisual.EnableArmorLevel(antLevel);

	}

    public void UpgradeHero()
    {
        if (antLevel < antMaxLevel)
        {
            antLevel++;
            antVisual.EnableArmorLevel(antLevel);
            PermaBuffHero(10f, 1, 1.3f, 1.3f);
            foreach (GameObject ant in spawnFollower.currentFollowers)
            {
                ant.GetComponent<AntUpgrader>().UpgradeAnt();
            }
        }
    }

    public void UpgradeAntToLevel(int level)
    {
        while (antLevel < level && antLevel < antMaxLevel)
        {
            UpgradeAnt();
        }
    }

    public void UpgradeAnt()
    {
        if (antLevel < antMaxLevel)
        {
            antLevel++;
            antVisual.EnableArmorLevel(antLevel);
            PermaBuffAnt(2.5f, 0, 1.2f, 1.2f);
        }
    }

    public void PermaBuffHero(float additionalHealth, int additionalDamage, float precisionMultiplier, float firingSpeedMultiplier)
    {
        PermaBuffAnt(additionalHealth, additionalDamage, precisionMultiplier, firingSpeedMultiplier);
    }

    public void PermaBuffAnt(float additionalHealth, int additionalDamage, float precisionMultiplier, float firingSpeedMultiplier)
    {
        weaponInfo.AddPermanentWeaponBuff(additionalDamage, precisionMultiplier, firingSpeedMultiplier);
        health.PermanentHealthBuff(additionalHealth);
    }

    public void TempBuffHero(float time, float additionalHealth, int additionalDamage, float precisionMultiplier, float firingSpeedMultiplier)
    {
        StartCoroutine(timedHealthBuff(time, additionalHealth));
        StartCoroutine(timedWeaponBuff(time, additionalDamage, precisionMultiplier, firingSpeedMultiplier));
    }

    public void TempBuffAnt(float time, float additionalHealth, int additionalDamage, float precisionMultiplier, float firingSpeedMultiplier)
    {
        StartCoroutine(timedHealthBuff(time, additionalHealth));
        StartCoroutine(timedWeaponBuff(time, additionalDamage, precisionMultiplier, firingSpeedMultiplier));
    }

    IEnumerator timedHealthBuff( float time, float additionalHealth)
    {
        health.AddTimedHealthBuff(additionalHealth, time);
        yield return new WaitForSeconds(time);
        Debug.Log("Removing additional Health");
        health.RemoveTimedHealthBuff(additionalHealth);
    }

    IEnumerator timedWeaponBuff(float time, int additonalDamage, float precisionMultiplier, float firingSpeedMultiplier)
    {
        weaponInfo.AddTempWeaponBuff(additonalDamage, precisionMultiplier, firingSpeedMultiplier);
        yield return new WaitForSeconds(time);
        Debug.Log("Removing temoporal WeaponBuff");
        weaponInfo.RemoveTempWeaponBuff(additonalDamage, precisionMultiplier, firingSpeedMultiplier);
    }
}
