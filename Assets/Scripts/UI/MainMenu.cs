using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GUISkin mainMenuSkin;
    public GUIStyle muteButton, playButton, optionsButton, exitButton, returnButton;
    public Texture2D muteTex, unmuteTex;
    public bool showOP;
    public bool mute;
    public float audioSlider;
    public float dirSlider;
    public float unmuteVolume;
    public AudioSource audi;
    public Light dirLight;

    void Start()
    {
        // Added the feature to mute the volume inside the game
        audi = GameObject.Find("Audio Source").GetComponent <AudioSource>();
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();

        // Lighting and the Volume for the game
        if (PlayerPrefs.HasKey("Volume"))
        {
            dirLight.intensity = PlayerPrefs.GetFloat("Directional Light");
            audi.volume = PlayerPrefs.GetFloat("Volume");
            if (PlayerPrefs.GetInt("mute") == 0)
            {
                // Muting the volume of the player checks the mute button
                mute = true;
                unmuteVolume = PlayerPrefs.GetFloat("Volume");
                audi.volume = 0;
                muteButton.normal.background = muteTex;
            }
            else
            {
                //Unmuting the volume if the player checks the unmute button
                mute = false;
                audioSlider = PlayerPrefs.GetFloat("Volume");
                muteButton.normal.background = unmuteTex;
            }
        }
            // Sliders for the Options menu to change the value of the certain settings
            audioSlider = audi.volume;
            dirSlider = dirLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the volume of game to match the volume that the user set
        if (audi.volume != audioSlider)
        {
            audi.volume = audioSlider;
        }

        // Changes the directional light of game to match the directional light that the user set
        if (dirLight.intensity != dirSlider)
        {
            dirLight.intensity = dirSlider;
        }
    }

    void OnGUI()
    {
        // Setting the screen resolution for the game
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        GUI.skin = mainMenuSkin;

        // Setting the values for the buttons inside the main menu
        //GUI.Box(new Rect(0f * scrW, 6.4f * scrH, 1.7f * scrW, 0.5f * scrH), "Play");
        //GUI.Box(new Rect(1.7f * scrW, 6.4f * scrH, 4.2f * scrW, 0.5f * scrH), "Options");
        //GUI.Box(new Rect(5.9f * scrW, 6.4f * scrH, 4.2f * scrW, 0.5f * scrH), "Quit");
        if (!showOP)
        {
            if (GUI.Button(new Rect (2f * scrW, 5.5f * scrH, 4f * scrW, 2f * scrH), "", playButton))
            {
                SceneManager.LoadScene(1);
            }
            if (GUI.Button(new Rect (6f * scrW, 5.5f * scrH, 4f * scrW, 2f * scrH), "", optionsButton))
            {
                showOP = true;
            }
            if (GUI.Button(new Rect (10f * scrW, 5.5f * scrH, 4f * scrW, 2f * scrH), "", exitButton))
            {
                Application.Quit();
            }
        }

        else // else we are in the options menu
        {
            // Setting the values for the buttons inside the settings menu
            if (!mute)
            {
                audioSlider = GUI.HorizontalSlider(new Rect(5.1f * scrW, 5.8f * scrH, 6 * scrW, 1 * scrH), audioSlider, 0f, 1f);
            }    
            else
            {
                GUI.HorizontalSlider(new Rect(5.1f * scrW, 5.8f * scrH, 6 * scrW, 1 * scrH), audioSlider, 0f, 1f);
            }          
            GUI.Label (new Rect (7.5f * scrW, 5.25f * scrH, 3 * scrW, 0.8f * scrH), "Volume!");


            dirSlider = GUI.HorizontalSlider (new Rect(5.1f * scrW, 7.1f * scrH, 6 * scrW, 0.25f * scrH), dirSlider, 0f, 1f);

            GUI.Label(new Rect(7.35f * scrW, 6.45f * scrH, 2 * scrW, 0.8f * scrH), "Brightness!");


            if (GUI.Button (new Rect (7.65f * scrW, 8 * scrH, 2 * scrW, 1 * scrH), "Return", returnButton))
            {
                SaveOptions();
                showOP = false;
            }

            GUI.Label(new Rect(1.5f * scrW, 5.4f * scrH, 1.5f * scrW, 0.6f * scrH), "Mute!");

            if (GUI.Button(new Rect(1.2f * scrW, 6.1f * scrH, 1.5f * scrW, 0.6f * scrH), "", muteButton))
            {
                Mute(); 
            }
            GUI.skin = mainMenuSkin;
        }
    }

    // Saves the player's settings
    void SaveOptions()
    {
        PlayerPrefs.SetFloat("Volume", audioSlider);
        PlayerPrefs.SetFloat("Directional Light", dirSlider);
        if (mute)
        {
            PlayerPrefs.SetInt("mute", 0);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 1);
        }
   }

    // Toggling the mute of the in game sounds if the player presses the mute button
    public bool Mute()
    {
        // If the player presses the mute button, it will mute   
        if (mute == true)
        {
            audioSlider = unmuteVolume;
            mute = false;
            muteButton.normal.background = unmuteTex;
            return false;
        }
        else
        {
            // If the player presses the unmute button, it will unmute
            unmuteVolume = audioSlider;
            audioSlider = 0;
            mute = true;
            muteButton.normal.background = muteTex;
            return true;
        }
    }
}
