using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFollower : MonoBehaviour {

    [SerializeField]
    GameObject followerAnt;

    List<GameObject> currentFollowers;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            currentFollowers.Add(SpawnAnt());
    }

    public GameObject SpawnAnt()
    {
        GameObject newAnt = Instantiate(followerAnt);
        newAnt.transform.position = this.transform.position - (Vector3.down * 5);
        newAnt.GetComponentInChildren<FollowPlayerBehavior>().followTarget = gameObject;
        return newAnt;
    }
}
