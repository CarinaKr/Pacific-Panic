using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public GameObject prefab; 
    public bool randomPosition;
    public bool instanciateNew;
    public Vector2 position;

    private Transform boat;

	void Start()
    {
        boat = GameObject.Find("Boat").GetComponent<Transform>();
        if(randomPosition)
        {
            int posX = Random.Range(0, 2);
            posX = posX == 0 ? -1 : 1;
            position = new Vector2(position.x * posX, position.y);
        }
    }

    public void activateObject()
    {
        if(instanciateNew)
        {
            Instantiate(prefab, position, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            prefab.SetActive(true);
        }
    }

}
