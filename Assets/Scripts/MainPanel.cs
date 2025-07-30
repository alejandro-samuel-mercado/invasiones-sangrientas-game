using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanel : MonoBehaviour
{
    [Header("Options")]
    public Slider VolumeFX;
    public Slider VolumeMaster;
    public Toggle Mute;
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;
    private float lastVolume;
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject levelSelectorPanel;
    private void Awake()
    {
        //VolumeFX.onValueChanged.AddListener(ChangerVolumeFX);
        VolumeMaster.onValueChanged.AddListener(ChangerVolumeMaster);
    }
    public void PlayLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetMute()
    {
        if (Mute.isOn)
        {
            mixer.GetFloat("VolMaster", out lastVolume);
            mixer.SetFloat("VolMaster", -80);
        }
        else
        {
            mixer.SetFloat("VolMaster", lastVolume);
        }
    }
    public void OpenPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        levelSelectorPanel.SetActive(false);
        optionsPanel.SetActive(false);

        panel.SetActive(true);
        PlaySoundButton();
    }
    public void ChangerVolumeMaster(float volume)
    {
        mixer.SetFloat("VolMaster", volume);
    }
    public void ChangerVolumeFX(float volume)
    {
        mixer.SetFloat("VolFX", volume);
    }
    public void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }
}
