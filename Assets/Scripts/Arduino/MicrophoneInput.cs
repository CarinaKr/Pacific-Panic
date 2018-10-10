using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInput : MonoBehaviour {

    private ManualInputs manualInput;
    private Arduino_ManualInputs arduino;
    public static MicrophoneInput instance = null;
    public int arduinoNum;
    public int inputPin;

    private int volume;
    private int baseline;
    private int counter;
    private bool baselined;

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
    }

    void Start()
    {
        manualInput = ManualInputs.instance;
        arduino = manualInput.arduinos[arduinoNum];

        counter = 3;
        StartCoroutine("setBaseline");
    }

    void Update()
    {

        if (arduino.Ready())
        {
            if (baselined)
            {
                for (int i = 0; i < 3; i++)
                {
                    volume = arduino.GetAnalogInput(inputPin) - baseline;
                }
                Debug.Log("Volume"+volume);
            }
        }
    }

    public IEnumerator setBaseline()
    {
        while (!baselined)
        {
            yield return new WaitForSeconds(0.3f);

            int analogInput = arduino.GetAnalogInput(inputPin);
            if (analogInput != 0)
            {
                baseline = analogInput;
                counter--;
            }

            if (baseline != 0 && counter == 0)
            {
                baselined = true;
            }
        }
    }


    public int getVolume
    {
        get
        {
            return volume;
        }
    }

    public void resetBaseline()
    {
        baselined = false;
        counter = 3;
        StartCoroutine("setBaseline");
    }
}
