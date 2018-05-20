using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour {

    public GameObject[] path;
    public int currentTarget = 0;
    public float speed;
    
	// Update is called once per frame
	void Update () {
		if ((path[currentTarget].transform.position - this.transform.position).magnitude < 0.5)
        {
            currentTarget = (currentTarget + 1) % path.Length;
        }
        transform.position = transform.position + ((path[currentTarget].transform.position - transform.position).normalized * Time.deltaTime * speed);
    }
}
