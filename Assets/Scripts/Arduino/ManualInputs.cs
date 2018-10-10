using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualInputs : MonoBehaviour {

    public static ManualInputs instance = null;
    public int arduinoNum;
    public string[] portNames;
    public GameObject arduinoPrefab;

    private Arduino_ManualInputs[] _arduinos;

    void Awake()
    {
        _arduinos = new Arduino_ManualInputs[arduinoNum];
        if (instance == null)
        {
            instance = this;
            for (int i = 0; i < arduinoNum; i++)
            {
                arduinoPrefab.GetComponent<Arduino_ManualInputs>().portName=portNames[i];
                _arduinos[i] = Instantiate(arduinoPrefab, transform).GetComponent<Arduino_ManualInputs>();
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

   public Arduino_ManualInputs[] arduinos
    {
        get
        {
            return _arduinos;
        }
    }
}
