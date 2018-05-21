using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandFollower : MonoBehaviour {
    Rewired.Player playerInput;

    [SerializeField]
    private SpawnFollower spawnfollower;

    [SerializeField]
    private BulletSpawnPoint bulletSpawnPoint;

    [SerializeField]
    private GameObject targetPrefab;

    FloatTimer cooldown = new FloatTimer(0.5f, false);

    // Use this for initialization
    void Start () {
        spawnfollower = gameObject.GetComponent<SpawnFollower>();
        GameObject antVisual = GetComponentInChildren<AntVisual>().gameObject;
        bulletSpawnPoint = antVisual.GetComponentInChildren<BulletSpawnPoint>();

        Actor _actor = GetComponentInParent<Actor>();
        playerInput = Rewired.ReInput.players.GetPlayer(_actor.PlayerID);
    }

    // Update is called once per frame
    void Update () {
        cooldown.Update();

		if (cooldown.Finished && playerInput.GetAxis("RTrigger") > 0.5)
        {
            cooldown.Reset();
            Vector3 targetPosition = bulletSpawnPoint.transform.position + (bulletSpawnPoint.transform.position - bulletSpawnPoint.transform.parent.position)*3;

            FollowerTarget target = Instantiate(targetPrefab).GetComponent<FollowerTarget>();
            target.transform.position = targetPosition;

            foreach (GameObject follower in spawnfollower.currentFollowers)
            {
                GoToBehavior goToBehavior = follower.GetComponentInChildren<GoToBehavior>(true);
                goToBehavior.gameObject.SetActive(true);
                goToBehavior.SetTarget(target);
            }
        }
    }
}
