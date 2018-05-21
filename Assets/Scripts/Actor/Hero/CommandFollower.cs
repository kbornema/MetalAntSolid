using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandFollower : MonoBehaviour {

    [SerializeField]
    private SpawnFollower spawnfollower;

    [SerializeField]
    private BulletSpawnPoint bulletSpawnPoint;

    [SerializeField]
    private GameObject targetPrefab;

    // Use this for initialization
    void Start () {
        spawnfollower = gameObject.GetComponent<SpawnFollower>();
        GameObject antVisual = GetComponentInChildren<AntVisual>().gameObject;
        bulletSpawnPoint = antVisual.GetComponentInChildren<BulletSpawnPoint>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 targetPosition = bulletSpawnPoint.transform.position + (bulletSpawnPoint.transform.position - bulletSpawnPoint.transform.parent.position)*3;

            GameObject target = Instantiate(targetPrefab);
            target.transform.position =  targetPosition;

            foreach (GameObject follower in spawnfollower.currentFollowers)
            {
                GoToBehavior goToBehavior = follower.GetComponentInChildren<GoToBehavior>(true);
                goToBehavior.gameObject.SetActive(true);
                goToBehavior.SetTarget(target);
            }
        }
    }
}
