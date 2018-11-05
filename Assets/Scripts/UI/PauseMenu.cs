using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GUISkin pauseMenuSkin;
    public GUIStyle muteButton;
    public Texture2D muteTex, unmuteTex;
    public bool showOP;
    public bool mute;
    public bool paused;
    public float audioSlider;
    public float dirSlider;
    public float unmuteVolume;
    public AudioSource audi;
    public Light dirLight;
    public float timer;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // Added the feature to mute the volume inside the game
        //audi = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        //dirLight = GameObject.Find("Directional Light").GetComponent<Light>();

        if (PlayerPrefs.HasKey("Volume"))
        {
            // Lighting and the Volume for the game
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void FixedUpdate()
    {
        // Pause the game if the escape key is pressed to go into the menu
        timer += Time.deltaTime;

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

    // If the game is paused, the time of the game will be paused
    public bool TogglePause()
    {
        if (paused)
        {
            player.GetComponent<AFPC.FPController>().enabled = true;
            player.GetComponent<Combat>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            paused = false;
            Time.timeScale = 1;
            return false;
        }
        else
        {
            player.GetComponent<AFPC.FPController>().enabled = false;
            player.GetComponent<Combat>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            paused = true;
            Time.timeScale = 0;
            return true;
        }
    }

    void OnGUI()
    {
        // Setting the screen resolution for the game
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        GUI.skin = pauseMenuSkin;

        // Setting the values for the buttons inside the pause menu
        if (paused)
        {
            if (!showOP)//showOp == false
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

                if (GUI.Button(new Rect (12f * scrW, 5.2f * scrH, 4f * scrW, 0.6f * scrH), "resume"))
                {
                    TogglePause();
                }
                if (GUI.Button(new Rect (12f * scrW, 6f * scrH, 4f * scrW, 0.6f * scrH), "options"))
                {
                    showOP = true;
                }
                if (GUI.Button(new Rect (12f * scrW, 6.8f * scrH, 4f * scrW, 0.6f * scrH), "credits"))
                {

                }
                if (GUI.Button(new Rect (12f * scrW, 7.6f * scrH, 4f * scrW, 0.6f * scrH), "quit"))
                {
                    Application.Quit();
                }
            }

            else // else we are in the options menu
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

                // Setting the values for the buttons inside the settings menu
                if (!mute)
                {
                    audioSlider = GUI.HorizontalSlider(new Rect(5.1f * scrW, 5.8f * scrH, 6 * scrW, 1 * scrH), audioSlider, 0f, 1f);
                }
                else
                {
                    GUI.HorizontalSlider(new Rect(5.1f * scrW, 5.8f * scrH, 6 * scrW, 1 * scrH), audioSlider, 0f, 1f);
                }
                GUI.Label(new Rect(7.5f * scrW, 5.25f * scrH, 3 * scrW, 0.8f * scrH), "volume");


                dirSlider = GUI.HorizontalSlider(new Rect(5.1f * scrW, 7.1f * scrH, 6 * scrW, 0.25f * scrH), dirSlider, 0f, 1f);

                GUI.Label(new Rect(7.35f * scrW, 6.45f * scrH, 2 * scrW, 0.8f * scrH), "brightness");


                if (GUI.Button(new Rect(7.65f * scrW, 8 * scrH, 2 * scrW, 1 * scrH), "return"))
                {
                    SaveOptions();
                    showOP = false;
                }

                GUI.Label(new Rect(1.5f * scrW, 5.4f * scrH, 1.5f * scrW, 0.6f * scrH), "mute");

                if (GUI.Button(new Rect(1.2f * scrW, 6.1f * scrH, 1.5f * scrW, 0.6f * scrH), "", muteButton))
                {
                    Mute();
                }
                GUI.skin = pauseMenuSkin;
            }
        }
    }

    // Saves the player's settings
    void SaveOptions()
    {
        PlayerPrefs.SetFloat("Volume", audioSlider);
        PlayerPrefs.SetFloat("Directional Light", dirSlider);
        if (!mute)
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
        // If the player presses the unmute button, it will unmute
        {
            unmuteVolume = audioSlider;
            audioSlider = 0;
            mute = true;
            muteButton.normal.background = muteTex;
            return true;
        }
    }
}
