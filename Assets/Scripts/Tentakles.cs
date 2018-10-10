using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentakles : Obstacle {

    public float speed;
    public float maxDistance,reachDistance;
    public int btnLeft, btnRight;

    private bool isMoving;
    

    // Use this for initialization
    //void Start () {
        //float posX = transform.position.x * (boat.position.x + Random.Range(reachDistance, maxDistance+1));    
        //float posY = transform.position.y + Random.Range(reachDistance, maxDistance + 2);       //TODO no magic number!!
        //transform.position = new Vector2(posX, posY);
	//}
	
	// Update is called once per frame
	void Update () {
        if(Vector2.Distance(transform.position,boat.position)<maxDistance)
        {
            isMoving = true;
        }

        if(isMoving)
        {
            //transform.position = Vector3.MoveTowards(transform.position, new Vector2(boat.position.x, transform.position.y), speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, boat.position, speed * Time.deltaTime);
            if (Mathf.Abs(transform.position.x - boat.position.x) < reachDistance)
            {
				gameManager.tentakleRange.SetActive(true);
                if ((Input.GetAxisRaw("Stick")==-1 || button.button(arduinoNum,btnLeft)) && transform.position.x < boat.transform.position.x) 
                {
                    if(!gettingDestroyed)
                    {
                        gettingDestroyed = true;
                        StartCoroutine("playSound", getHitClip);
                    }
                    
                    //Destroy(gameObject);
                }
                else if((Input.GetAxisRaw("Stick") == 1 || button.button(arduinoNum, btnRight)) && transform.position.x >= boat.transform.position.x) 
                {
                    if (!gettingDestroyed)
                    {
                        gettingDestroyed = true;
                        StartCoroutine("playSound", getHitClip);
                    }
                    //Destroy(gameObject);
                }
            }
        }
	}

    void OnDisable()
    {
		gameManager.tentakleRange.SetActive(false);
    }
}
