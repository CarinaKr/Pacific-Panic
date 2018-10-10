using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AccelerometerInput : MonoBehaviour
{
    //private Arduino_ManualInputs arduino;
    private ManualInputs manualInput;
    private Arduino_ManualInputs arduino;
    public static AccelerometerInput instance = null;
    public int arduinoNum;
    public int[] inputPins = new int[3];

    private int[] tilt = new int[3];
    private int[] baseline = new int[3];
    private int[] counter = new int[3];
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

        for (int i=0;i<counter.Length;i++)
        {
            counter[i] = 3;
        }
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
                    tilt[i] = arduino.GetAnalogInput(inputPins[i]) - baseline[i];
                }
                //Debug.Log("X: " + tilt[0] + " Y: " + tilt[1] + " Z: " + tilt[2]);
            }
        }   
    }

    public IEnumerator setBaseline()
    {
        while (!baselined)
        {
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < 3; i++)
            {
                int analogInput = arduino.GetAnalogInput(inputPins[i]);
                if (analogInput != 0)
                {
                    baseline[i] = analogInput;
                    counter[i]--;
                }
            }
            if (baseline[0] != 0 && baseline[1] != 0 && baseline[2] != 0 && counter[0] == 0 && counter[1] == 0 && counter[2] == 0)
            {
                baselined = true;
            }
        }
    }

    public int[] getTilt
    {
        get
        {
            return tilt;
        }
    }

    public void resetBaseline()
    {
        baselined = false;
        for (int i = 0; i < counter.Length; i++)
        {
            counter[i] = 3;
        }
        StartCoroutine("setBaseline");
    }
}
