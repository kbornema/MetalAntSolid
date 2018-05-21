using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFollower : MonoBehaviour {

    [SerializeField]
    GameObject followerAnt;

    public List<GameObject> currentFollowers;

    private void Awake()
    {
        currentFollowers = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            currentFollowers.Add(SpawnAnt(followerAnt));
    }
    
    public void RemoveFollower(GameObject follower)
    {
        if (currentFollowers.Contains(follower))
            currentFollowers.Remove(follower);
    }

    public GameObject SpawnAntAsFollower(GameObject antPrefab)
    {
        GameObject ant = SpawnAnt(antPrefab);
        currentFollowers.Add(ant);
        return ant;
    }

    public GameObject SpawnAnt(GameObject antPrefab)
    {
        GameObject newAnt = Instantiate(antPrefab);
        newAnt.transform.position = this.transform.position - (Vector3.down * 5);
        newAnt.GetComponentInChildren<FollowPlayerBehavior>().followTarget = gameObject;
        return newAnt;
    }

    public void SpawnAntGroup()
    {
        float radius = 5f;
        int antNumber = 5;

        float angle = 360 / 5;
        Vector2 antOffset = Vector2.up * radius;
        for (int i = 0; i < antNumber; i++)
        {
            GameObject ant;
            if(i == 0)
            {
                ant = SpawnAntAsFollower(followerAnt);
            }
            else
            {
                ant = SpawnAntAsFollower(followerAnt);
            }
            ant.transform.position = this.transform.position + Quaternion.Euler(0, 0, i * angle) * antOffset;
            ant.transform.rotation = Quaternion.Euler(0, 0, i * angle + 180);
        }
    }
}
