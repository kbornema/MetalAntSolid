using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFollower : MonoBehaviour {

    [SerializeField]
    GameObject followerAnt;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnAnt();
    }

    public void SpawnAnt()
    {
        GameObject newAnt = Instantiate(followerAnt);
        newAnt.transform.position = this.transform.position - (Vector3.down * 5);
        newAnt.GetComponentInChildren<FollowPlayerBehavior>().followTarget = gameObject;
    }
}
