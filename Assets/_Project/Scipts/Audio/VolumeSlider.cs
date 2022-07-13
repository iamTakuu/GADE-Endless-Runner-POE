using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volSlider;

    private void Start()
    {
        volSlider.onValueChanged.AddListener(val => AudioManager.Instance.AdjustMasterVolume(val));
    }
}
