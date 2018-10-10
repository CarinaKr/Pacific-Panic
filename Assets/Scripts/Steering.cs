using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour {

    public float maxAccTilt;
    public float maxSpeed,speed;
    public Rigidbody2D boat;
    public float leftEdge, rightEdge;

    private AccelerometerInput acc;
    private float inputTiltX, boatSpeed;

	// Use this for initialization
	void Start () {
        acc = AccelerometerInput.instance;
	}
	
	// Update is called once per frame
	void Update () {
        inputTiltX = acc.getTilt[0];
        //if (Input.GetButtonDown("Steering"))
        //{
        //    inputTiltX += Input.GetAxisRaw("Steering"); //TODO take out! for keyboard use only
        //}
        boatSpeed = (inputTiltX / maxAccTilt) * maxSpeed;
        move(boatSpeed *Time.deltaTime);        //TODO *(-1) if left and right are reverse
	}

    public void move(float pSpeed)
    {
        if (boat.velocity.x <= boatSpeed && boat.position.x<=rightEdge)
        {
            boat.AddForce(new Vector2(pSpeed, 0));
        }
        else if(boat.velocity.x>= boatSpeed && boat.position.x >= leftEdge)
        {
            boat.AddForce(new Vector2(pSpeed, 0));
        }
        else if(boat.position.x<leftEdge)
        {
            boat.position = new Vector2(leftEdge,boat.position.y);
        }
        else if (boat.position.x >rightEdge)
        {
            boat.position = new Vector2(rightEdge, boat.position.y);
        }
    }
}
