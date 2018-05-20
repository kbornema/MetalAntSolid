using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Movement : MonoBehaviour {

    [SerializeField]
    private Actor _actor;

    [SerializeField]
    public Rewired.Player playerInput;
    public Rigidbody2D rgbd;

    Vector2 moveDirection;

    public float movementSpeed;
    public float rotationSpeed;

    public AntVisual antVisual;

    // Use this for initialization
    void Start () {
        playerInput = Rewired.ReInput.players.GetPlayer(_actor.PlayerID);
        moveDirection = new Vector2(0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        moveDirection = new Vector2(playerInput.GetAxis("MoveX"), playerInput.GetAxis("MoveY"));
        if( moveDirection.magnitude > 1)
            moveDirection.Normalize();

        antVisual.SetMovePercent(moveDirection.magnitude);
        
        Vector2 viewDirection = moveDirection;



        if (moveDirection.magnitude > 0.05)
        {
            float angle = Mathf.Atan2(viewDirection.y, viewDirection.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

            rgbd.position += (new Vector2(transform.up.x, transform.up.y) * movementSpeed * moveDirection.magnitude * Time.deltaTime);
        }
    }
}
