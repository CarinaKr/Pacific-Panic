using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    public Transform mainCamera;
    public GameObject[] ground;
    //public Obstacle obstacles;
    public float tileSize;

    private int _counter;
    private int middleTile = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.transform.position.y > ground[middleTile].transform.position.y)
        {
            nextTile();
        }
    }

    public void nextTile()
    {
        _counter++;
        ground[(_counter) % ground.Length].transform.position = new Vector2(0, _counter * tileSize);
        middleTile = (middleTile + 1) % ground.Length;
        //obstacles.nextTile(ground[(_counter) % ground.Length].transform);
    }
}
