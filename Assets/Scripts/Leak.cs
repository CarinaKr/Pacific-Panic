using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leak : Obstacle {
    
    public string LEDChar;
    public int minTime, maxTime;
    public float maxDamage;
    public int arduinoOutputNum;
    

    private ManualInputs manualInput;
    private Arduino_ManualInputs arduinoOutput;
    private float counter,damageCounter;
    private bool isLeaking;
    private float damageOld;
    private BoatManager boatManager;

	// Use this for initialization
	//new void Start () {
 //       base.Start();
 //       boatManager = boat.GetComponent<BoatManager>();
 //       manualInput = ManualInputs.instance;
 //       arduinoOutput = manualInput.arduinos[arduinoOutputNum];
 //   }

    void OnEnable()
    {
        if(boatManager==null)
        {
            base.Start();
            boatManager = boat.GetComponent<BoatManager>();
            manualInput = ManualInputs.instance;
            arduinoOutput = manualInput.arduinos[arduinoOutputNum];
        }
        
        StartCoroutine("startSound");
        toggleLED();
        isLeaking = true;
        Debug.Log("Leak enabled: " + gameObject.name);
    }

    void OnDisable()
    {
        audioSource.Pause();
        toggleLED();
        Debug.Log("Leak disabled: " + gameObject.name);
    }
	
	// Update is called once per frame
	void Update () {

        counter += Time.deltaTime;
        if(button.button(arduinoNum,btnNum))   
        {
            isLeaking = false;
            audioSource.Pause();
        }
        else
        {
            isLeaking = true;
            if(!audioSource.isPlaying)
            { audioSource.Play(); }
        }

        if (isLeaking)
        {
            damageCounter += Time.deltaTime;
            if (damageCounter > minTime )
            {
                damage = ((damageCounter - minTime) / maxTime) * maxDamage;
                boatManager.damage += (damage - damageOld);
                damageOld = damage;
            }
        }

        if(counter>maxTime+minTime)
        {
            counter = 0;
            damageCounter = 0;
            damageOld = 0;
            gameObject.SetActive(false);
        }
    }

    public void toggleLED()
    {
        //Debug.Log("LED toggled "+gameObject.name);
        arduinoOutput.sendData(LEDChar);
    }

    private IEnumerator startSound()
    {
        audioSource.clip = getHitClip;
        audioSource.Play();
        yield return new WaitForSeconds(getHitClip.length);
        audioSource.Pause();
        audioSource.clip = hitBoatClip;
        audioSource.Play();
    }
}
