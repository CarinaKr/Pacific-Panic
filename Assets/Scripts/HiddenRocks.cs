using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRocks : Obstacle {

    public float minTime,maxTime;
    public float maxDamage;
    public string[] sendChars;

    private ManualInputs manualInput;
    private Arduino_ManualInputs arduino;
    private bool _gotHit;
    private float counter;
    private BoatManager boatManager;
    private float damageOld;

    // Use this for initialization
    //new void Awake () {
    //       base.Awake();
    //       boatManager = boat.GetComponent<BoatManager>();
    //       //float posX = transform.position.x * (boat.position.x + Random.Range(0, 5));
    //       //float posY = transform.position.y + Random.Range(3, 7 + 2);       //TODO no magic number!!
    //       //transform.position = new Vector2(posX, posY);
    //       manualInput = ManualInputs.instance;
    //       arduino = manualInput.arduinos[arduinoNum];
    //   }

    new void Start()
    {
        base.Start();
        boatManager = boat.GetComponent<BoatManager>();
        //float posX = transform.position.x * (boat.position.x + Random.Range(0, 5));
        //float posY = transform.position.y + Random.Range(3, 7 + 2);       //TODO no magic number!!
        //transform.position = new Vector2(posX, posY);
        manualInput = ManualInputs.instance;
        arduino = manualInput.arduinos[arduinoNum];
    }

    // Update is called once per frame
    void Update () {
	    if(_gotHit)
        {
            counter += Time.deltaTime;
            if(counter>minTime)
            {
                damage = ((counter-minTime) / maxTime) * maxDamage;
                boatManager.damage += (damage-damageOld);
                damageOld = damage;
            }
            if(Input.GetButtonDown("Jump") || button.buttonDown(arduinoNum, btnNum) || counter > maxTime+minTime)
            {
                if(!gettingDestroyed)
                {
                    gettingDestroyed = true;
                    stopBuzzing();
                    //Destroy(gameObject);
                    if(button.button(arduinoNum,btnNum))
                    { StartCoroutine("playSound", getHitClip);}
                }
                
            }
        }	
	}

    public void startBuzzing()
    {
        for(int i=0;i<sendChars.Length;i++)
        {
            arduino.sendData(sendChars[i]);
        }
    }
    public void stopBuzzing()
    {
        for (int i = 0; i < sendChars.Length; i++)
        {
            arduino.sendData(sendChars[i]);
        }
    }

    public bool gotHit
    {
        set
        {
            _gotHit = value;
            if(_gotHit)
            { startBuzzing(); }
        }
    }
}
