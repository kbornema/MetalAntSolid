using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerTarget : MonoBehaviour {

    [SerializeField]
    LinkedList<GameObject> followerList = new LinkedList<GameObject>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        bool hasFollowers = false;
        foreach (GameObject follower in followerList)
        {
            if (follower != null)
            {
                hasFollowers = true;
                break;
            }
        }
        if (!hasFollowers)
        {
            Destroy(this.gameObject);
        }
	}

    public void RegisterFollower(GameObject follower)
    {
        followerList.AddLast(follower);
    }

    public void RemoveFollower(GameObject follower)
    {
        followerList.Remove(follower);
    }

}
