using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeWave : MonoBehaviour
{
    public SineWaves sineWaves;

    public Button generateButton;

    public ValueData amplitudeData = new ValueData();
    public ValueData speedData= new ValueData();
    public ValueData frequencyData = new ValueData();

    void Update()
    {
        amplitudeData.value = sineWaves.Octaves[0].amplitude;
        amplitudeData.dataText.text = amplitudeData.value.ToString();
    }

    public void AddAmplitude()
    {
        Debug.Log("Added Amplitude");
    }

    public void SubAmplitude()
    {

    }

    public void AddSpeed()
    {

    }

    public void SubSpeed()
    {

    }

    public void AddFrequency()
    {

    }

    public void SubFrequency()
    {

    }

    [System.Serializable]
    public struct ValueData
    {
        public float value;
        public Slider slider;
        public Text dataText;
        public float offset;
        public float min;
        public float max;
    }

}
