using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    Rect startButton;
    float buttonWidth;
    float buttonHeight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        buttonHeight = Screen.height / 6;
        buttonWidth = Screen.width / 6;
        startButton = new Rect((Screen.width / 2) - (buttonWidth / 2), (Screen.height / 2) - (buttonHeight / 2), buttonWidth, buttonHeight);
        if (GUI.Button(startButton, "Become a single mother with eight children!"))
        {
            Application.LoadLevel("test");
        }
    }
}
