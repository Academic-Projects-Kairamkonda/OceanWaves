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

    private float amplitude;
    private float speed;
    private float frequency;

    void Update()
    {
        GetWaveData();
        SetTextData();
        SetCustomizationData();
    }

    private void SetTextData()
    {
        amplitudeData.dataText.text = amplitude.ToString();
        speedData.dataText.text = speed.ToString();
        frequencyData.dataText.text = frequency.ToString();
    }

    private void GetWaveData()
    {
        amplitude = sineWaves.Octaves[0].amplitude;
        speed = sineWaves.Octaves[1].speed.y;
        frequency = sineWaves.Octaves[1].frequency.y;
    }

    private void SetCustomizationData()
    {
        amplitudeData.slider.minValue = amplitudeData.min;
        amplitudeData.slider.maxValue = amplitudeData.max;

        speedData.slider.minValue = speedData.min;
        speedData.slider.maxValue = speedData.max;

        frequencyData.slider.minValue = frequencyData.min;
        frequencyData.slider.maxValue = frequencyData.max;
    }

    public void GetCustomizationData()
    {
        sineWaves.Octaves[0].amplitude = amplitudeData.slider.value;
        sineWaves.Octaves[1].speed.y = speedData.slider.value;
        sineWaves.Octaves[1].frequency.y = frequencyData.slider.value;
    }


    public void AddAmplitude()
    {
        if(amplitudeData.max> amplitude)
            sineWaves.Octaves[0].amplitude += amplitudeData.offset;
    }

    public void SubAmplitude()
    {
        if(amplitudeData.min<amplitude)
        sineWaves.Octaves[0].amplitude -= amplitudeData.offset;
    }

    public void AddSpeed()
    {
        if(speedData.max>speed)
        sineWaves.Octaves[1].speed.y += speedData.offset;
    }

    public void SubSpeed()
    {
        if (speedData.min < speed)
            sineWaves.Octaves[1].speed.y -= speedData.offset;
    }

    public void AddFrequency()
    {
        if(frequencyData.max>frequency)
        sineWaves.Octaves[1].frequency.y += frequencyData.offset;
    }

    public void SubFrequency()
    {
        if(frequencyData.min<frequency)
        sineWaves.Octaves[1].frequency.y -= frequencyData.offset;
    }

    [System.Serializable]
    public struct ValueData
    {
        //public float value;
        public Slider slider;
        public Text dataText;
        public float offset;
        public float min;
        public float max;
    }

}
