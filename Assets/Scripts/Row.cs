using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed, maxSpeed;
    public int btnRowLeft, btnRowRight;
    public int arduinoNum;
    public string rowButton;   //only for use with keyboard

    private int _rowCoutner=1;
    private Button button;

	// Use this for initialization
	void Start () {
        button = Button.instance;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(rowButton)|| button.buttonDown(arduinoNum,btnRowLeft) || button.buttonDown(arduinoNum,btnRowRight))  
        {
            if(button.buttonDown(arduinoNum,btnRowLeft))
            {
                Debug.Log("Row counter, left button: "+_rowCoutner);
            }
            else if(button.buttonDown(arduinoNum,btnRowRight))
            {
                Debug.Log("Row counter, right button: " + _rowCoutner);
            }
            //if(Input.GetAxisRaw(rowButton)==_rowCoutner) 
            if((button.buttonDown(arduinoNum, btnRowLeft) && _rowCoutner == -1) || (button.buttonDown(arduinoNum, btnRowRight) && _rowCoutner == 1))
            {
                _rowCoutner *= -1;  
                //_rowCoutner =(_rowCoutner+1)%2;   //don't needt this when _rowCounter alternates between -1 and 1
                move(speed * Time.deltaTime);
            }
        }
	}

    public void move(float pSpeed)
    {
        if(rb.velocity.y<maxSpeed)
        {
            rb.AddForce(new Vector2(0,pSpeed));
        }
    }

    public int rowCounter
    {
        get
        {
            return _rowCoutner;
        }
    }
}
