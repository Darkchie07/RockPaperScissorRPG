using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_Text bgmVolText;
    [SerializeField] private TMP_Text sfxVolText;
    void OnEnable()
    {
        if (audioManager.IsMuted())
        {
            muteToggle.SetIsOnWithoutNotify(true);
        }
        bgmSlider.value = audioManager.BgmVolume;
        sfxSlider.value = audioManager.SfxVolume;
        SetBgmVolText(bgmSlider.value);
        SetSfxVolText(sfxSlider.value);
    }

    public void UpdateUI()
    {
        if (audioManager.IsMuted())
        {
            bgmSlider.SetValueWithoutNotify(0);
            SetBgmVolText(bgmSlider.value);
            bgmSlider.interactable = false;
            sfxSlider.SetValueWithoutNotify(0); 
            SetSfxVolText(sfxSlider.value);
            sfxSlider.interactable = false;
        }else{
            bgmSlider.value = 1;
            SetBgmVolText(bgmSlider.value);
            bgmSlider.interactable = true;
            sfxSlider.value = 1;
            SetSfxVolText(sfxSlider.value);
            sfxSlider.interactable = true;
        }
    }

    public void SetBgmVolText(float value)
    {
        bgmVolText.text = Mathf.RoundToInt(bgmSlider.value * 100).ToString();
    }
    
    public void SetSfxVolText(float value)
    {
        sfxVolText.text = Mathf.RoundToInt(sfxSlider.value * 100).ToString();
    }
}
