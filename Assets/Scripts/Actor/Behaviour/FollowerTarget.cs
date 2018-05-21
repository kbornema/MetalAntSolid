using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerTarget : MonoBehaviour {

    LinkedList<GameObject> followerList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject follower in followerList)
        {
            if (follower == null)
            {

            }
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
