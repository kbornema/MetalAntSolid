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

    public void Damage(int damage)
    {
        HP -= damage;
    }

    private void Die()
    {
        if (this.GetComponent<Hero_Movement>() != null)
        {
            int playerID = gameObject.GetComponent<Actor>().PlayerID;
            Destroy(gameObject);
            Debug.Log("Player " + playerID + " died... Respawning!");
            MatchManager.Instance().InitPlayer(playerID);
        }
        else if (gameObject.tag == "Ants")
        {
            PooledObject pooledObject = gameObject.GetComponent<PooledObject>();
            pooledObject.ReturnToPool();
        }
    }
}
