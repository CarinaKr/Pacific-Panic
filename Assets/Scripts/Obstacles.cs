using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public GameObject obstaclePrefab;
    public int obstacleNum, tileNum;
    public float minDistanceY;
    public Transform[] lanes;
    public Transform startTile;
    public float tileSize;

    private GameObject[] obstacles;
    private float maxDistanceY,maxDistanceX;
    private float[] posX,posY;
    private int counter;
    private float lanesWidth;

    // Use this for initialization
    void Start()
    {
        lanesWidth = lanes[0].transform.lossyScale.x/2;
        posX = new float[lanes.Length];
        posY = new float[lanes.Length];
        obstacles = new GameObject[obstacleNum * tileNum];
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i] = Instantiate(obstaclePrefab, transform);
            obstacles[i].transform.position = new Vector3(-100, -100);
        }
        counter = -1;
        nextTile(startTile);
    }

    public void nextTile(Transform tile)
    {
        counter = (counter + 1) % tileNum;
        maxDistanceY = tileSize / obstacleNum;
        float startPositionY = tile.position.y - (tileSize / 2);
        findY(startPositionY);
        findX();
        int obstacleCounter = (counter * obstacleNum);
        for (int i = 0; i < obstacleNum; i++)
        {
            obstacles[obstacleCounter].transform.position = new Vector2(posX[i], posY[i]);
            obstacleCounter++;
        }
    }

    private void findY(float startPosition)
    {
        float lastPositionY = startPosition;
        for(int i=0;i<obstacleNum;i++)
        {
            posY[i] = lastPositionY + Random.Range(minDistanceY, maxDistanceY);
            lastPositionY = posY[i];
        }
    }
    private void findX()
    {
        bool[] lanesTaken = new bool[lanes.Length];
        int chosenLane;
        for(int i=0;i<obstacleNum;i++)
        {
            do
            {
                chosenLane = Random.Range(0, lanes.Length);
            } while (lanesTaken[chosenLane]);
            lanesTaken[chosenLane] = true;
            posX[i] = lanes[chosenLane].transform.position.x + Random.Range((-1) * lanesWidth, lanesWidth);
        }
    }
}
