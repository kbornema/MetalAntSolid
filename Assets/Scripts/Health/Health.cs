using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private float hp;

    [SerializeField]
    private float maxHP;

    HealthBar healthbar = null;

    void Start()
    {
        healthbar = GetComponentInChildren<HealthBar>();
    }

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
        HP -= damage;
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
