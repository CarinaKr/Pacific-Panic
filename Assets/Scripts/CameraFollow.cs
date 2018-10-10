using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform follow;
    public float leftEdge, rightEdge, endPosition;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - follow.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 followPosition = follow.position + offset;
        if(followPosition.x<=leftEdge)
        { followPosition.x = leftEdge; }
        else if(followPosition.x>=rightEdge)
        {followPosition.x = rightEdge;}
        

        if(followPosition.y>endPosition)
        {
            followPosition.y = endPosition;
        }

        transform.position = followPosition;
    }
}
