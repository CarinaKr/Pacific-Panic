using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public static Button instance = null;
    public int arduinoNum;
    private ManualInputs manualInput;
    private Arduino_ManualInputs[] arduino;
    public int digitalInput;

    private bool[,] _button;
    private bool[,] _buttonDown, _buttonUp;
    private bool _anyButtonDown;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);

        _button = new bool[arduinoNum,digitalInput];
        _buttonDown = new bool[arduinoNum,digitalInput];
        _buttonUp = new bool[arduinoNum,digitalInput];
    }

    void Start()
    {
        manualInput = ManualInputs.instance;
        arduino = new Arduino_ManualInputs[arduinoNum];
        for(int i=0;i<arduinoNum;i++)
        {
            arduino[i] = manualInput.arduinos[i];
        }
    }
	
	// Update is called once per frame
	void Update () {

        _anyButtonDown = false;
        for (int j = 0; j < arduinoNum; j++)
        {
            for (int i = 0; i < digitalInput; i++)
            {
                bool nextButtonState = arduino[j].GetDigitalInput(i);
                if (nextButtonState && !_button[j,i] && !_buttonDown[j, i])
                {
                    Debug.Log("Arduino: "+j+"Button down: " + i);
                    _buttonDown[j, i] = true;
                    _anyButtonDown = true;

                }
                else if (nextButtonState && _button[j, i] && _buttonDown[j, i])
                {
                    _buttonDown[j, i] = false;
                }

                else if (!nextButtonState && _button[j, i] && !_buttonUp[j, i])
                {
                    _buttonUp[j, i] = true;
                }
                else if (!nextButtonState && !_button[j, i] && _buttonUp[j, i])
                {
                    _buttonUp[j, i] = false;
                }
                _button[j, i] = nextButtonState;
            }
        }
	}

    public bool button(int pArduino,int pButton)
    {
        return _button[pArduino,pButton];
    }
    public bool buttonDown(int pArduino, int pButton)
    {
        return _buttonDown[pArduino, pButton];
    }
    public bool buttonUp(int pArduino, int pButton)
    {
        return _buttonUp[pArduino, pButton];
    }
    public bool buttonDown()
    {
        return _anyButtonDown;
    }
}
