using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private float hp;

    [SerializeField]
    private float maxHP;

    public bool invincible;

    HealthBar healthbar = null;

    Hero_Movement heroMovement;

    void Start()
    {
        healthbar = GetComponentInChildren<HealthBar>();
        heroMovement = GetComponent<Hero_Movement>();
        invincible = false;
    }

#if UNITY_EDITOR
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
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

    public void ResetHP()
    {
        HP = maxHP;
    }

    public void Damage(int damage)
    {
        if(invincible == false)
            HP -= damage;

        if (heroMovement != null && damage > 0)
        { 

            Debug.Log("Vibrating");
            heroMovement.playerInput.SetVibration(0, ((float)damage / (float)5), 0.25f, true);
        }
    }

    private void Die()
    {
        if (this.GetComponent<Hero_Movement>() != null)
        {
            // Todo fix this;
            int playerID = this.GetComponent<Actor>().PlayerID;
            Debug.Log("Player " + playerID + " died... Respawning!");
            MatchManager.Instance().StartCoroutine(MatchManager.Instance().DelayedPlayerRespawn(gameObject));
        }
        else if (gameObject.tag == "Ants")
        {
            PooledObject pooledObject = gameObject.GetComponent<PooledObject>();
            pooledObject.ReturnToPool();
        }
    }


}
