using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public int arduinoNum, btnNum;
    public int loadScene;
    public Sprite[] sprites;
    public Image background;
    public bool buzzInMenu;
    public int buzzArduinoNum;
    public string[] buzzChars;

    private ManualInputs manualInput;
    private Arduino_ManualInputs arduino;
    private Button button;
    private int counter;

	// Use this for initialization
	void Start () {
        button = Button.instance;
        if(buzzInMenu)      //get Arduino and start buzzing
        {
            manualInput = ManualInputs.instance;
            arduino = manualInput.arduinos[arduinoNum];
            for (int i = 0; i < buzzChars.Length; i++)
            {
                //arduino.sendData(buzzChars[i]);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(button.buttonDown(arduinoNum,btnNum)||Input.anyKeyDown)
        {
            if(counter==sprites.Length-1)
            {
                for (int i = 0; i < buzzChars.Length; i++)  //stop buzzing
                {
                   // arduino.sendData(buzzChars[i]);
                }
                SceneManager.LoadScene(loadScene);
            }
            else
            {
                counter++;
                background.sprite = sprites[counter];
            }
        }
	}
    
}
