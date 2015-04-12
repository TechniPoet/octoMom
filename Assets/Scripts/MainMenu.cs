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

	public void LaunchLevel()
	{
		Application.LoadLevel("test");
	}
}
