using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Obstacle {

    public float startDistance;
    public int maxVolume;
    public float thrh;
    public float range;

    private MicrophoneInput mic;
    private int volume;

    // Use this for initialization
    new void Start()
    {
        base.Start();
		gameManager.sharkRange.SetActive(true);
        mic = MicrophoneInput.instance;
        gameManager = GameManager.instance;
        //range = gameManager.boatRange.transform.lossyScale.x / 2;
    }
	
	// Update is called once per frame
	void Update () {
		if(Vector2.Distance(transform.position,boat.position)<range)
        {
            volume = mic.getVolume;
            if(Input.GetButtonDown("Microphone") || ((float)volume/(float)maxVolume)>thrh)  
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

    void OnDisable()
    {
		gameManager.sharkRange.SetActive(false);
    }
}
