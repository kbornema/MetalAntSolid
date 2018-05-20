using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public float speed;
    public AntPool antPool;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float mov_x = Input.GetAxis("Horizontal");
        float mov_y = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(mov_x, mov_y);
        movement = movement.normalized;
        this.transform.position += new Vector3(movement.x*Time.deltaTime*speed, movement.y * Time.deltaTime * speed);

        if (Input.GetButtonDown("Jump"))
        {
            AntMovement ant = antPool.GetObject().gameObject.GetComponent<AntMovement>();
            ant.transform.position = this.transform.position;
            ant.Direction = Vector3.left;
            Debug.Log("Spawn Ant");
        }
	}
}
