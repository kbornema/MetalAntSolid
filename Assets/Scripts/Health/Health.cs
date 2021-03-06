﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{   
    [SerializeField]
    private float hp;

    [SerializeField]
    private float maxHP;

    public bool invincible;

    HealthBar healthbar = null;

    Hero_Movement heroMovement;


    private AntVisual _antVisual;

    private BloodSpawner _bloodSpawner;

    [SerializeField]
    private List<MyAudio> _hurtSounds;
    [SerializeField, Range(0.0f, 1.0f)]
    private float _chanceSound = 0.25f;

    void Start()
    {
        healthbar = GetComponentInChildren<HealthBar>();
        heroMovement = GetComponent<Hero_Movement>();
        invincible = false;

        _antVisual = GetComponentInChildren<AntVisual>();

        if(heroMovement != null)
        {
            this.GetComponentInChildren<EatingBhvr>().AddOnEatingEventListener(GetHpFromFood);
        }

        _bloodSpawner = GetComponent<BloodSpawner>();
        if(_bloodSpawner)
            _bloodSpawner.SetAntVisual(_antVisual);
    }

#if UNITY_EDITOR
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            invincible = !invincible;
            Debug.Log("Switching to Godmode");
        }
    }
#endif

    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (healthbar != null)
            {
                healthbar.setHealth(hp / maxHP);
            }

            if (hp < 0) Die();
        }
    }

    public void GetHpFromFood(int value)
    {
        HP = Mathf.Min(hp + 1, maxHP);
    }

    public void ResetHP()
    {
        HP = maxHP;
    }

    public void Damage(int damage)
    {
        if (invincible == false)
            HP -= damage;

        if(_antVisual)
            _antVisual.Hurt(1.0f, 0.25f);

        if (_bloodSpawner)
            _bloodSpawner.SpawnBlood();

        if(_hurtSounds.Count > 0 && Random.value < _chanceSound)
        {
            MyAudio.Create(_hurtSounds[Random.Range(0, _hurtSounds.Count)], transform.position);
        }

        if (heroMovement != null && damage > 0)
        {
            heroMovement.playerInput.SetVibration(0, ((float)damage / (float)5), 0.25f, true);

           
        }
    }

    private void Die()
    {
        if (this.GetComponent<Hero_Movement>() != null)
        {

            int playerID = this.GetComponent<Actor>().PlayerID;
            Debug.Log("Player " + playerID + " died... Respawning!");
            MatchManager.Instance().StartCoroutine(MatchManager.Instance().DelayedPlayerRespawn(gameObject));
        }
        else if (gameObject.tag == "Ants")
        {
            PooledObject pooledObject = gameObject.GetComponent<PooledObject>();
            FollowPlayerBehavior followPlayerBehavior = this.GetComponent<FollowPlayerBehavior>();
            if (followPlayerBehavior != null)
            {
                Debug.Log("Trying to remove follower from antlist");
                followPlayerBehavior.followTarget.GetComponent<SpawnFollower>().RemoveFollower(followPlayerBehavior.gameObject);
            }
            GetComponent<AntUpgrader>().StopAllCoroutines();
            pooledObject.ReturnToPool();


            GameManager.Instance().AddKillValue();
        }
    }

    public void PermanentHealthBuff(float additionalHealth)
    {
        maxHP += additionalHealth;
        HP += additionalHealth;
    }

    public void AddTimedHealthBuff(float additionalHealth, float time)
    {
        maxHP += additionalHealth;
        HP += additionalHealth;
    }

    public void RemoveTimedHealthBuff(float additionalHealth)
    {
        maxHP -= additionalHealth;
        HP -= Mathf.Min(HP - 1, additionalHealth);
    }

}
