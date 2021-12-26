using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{

    public Slider[] volumeSliders;
    public static string code = "defaultQuestions";
    public InputField iField;
    public String odpoved;


    private void Start()
    {
       
        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;
 

    }

    public void NewGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OkGame()
    {
        odpoved = iField.text;
        Debug.Log(MainMenu.code);
        code = odpoved;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CustomGame()
    {
        odpoved = iField.text;
        SceneManager.LoadScene("CustomMenu");
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(MainMenu.code);
            code = odpoved;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

            
        }



    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Konec hry");
    }

    public void SetMasterVolume(float value) {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }
    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }

    public void OpenQuizCreator()
    {
        Application.OpenURL("https://hopesoft.eu/last-city/");
    }





}
