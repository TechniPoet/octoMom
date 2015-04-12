using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoseScripts : MonoBehaviour {

	public Text LoseText;
	public GameObject game;
	public PhaseManager pm;
	public Button mainMenuBtn;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Lose()
	{
		game.SetActive(false);
		mainMenuBtn.gameObject.SetActive(true);
		LoseText.text = string.Format("Results:\nYou made it through {0} Phases and made ${1}.", pm.phaseNum, pm.totalWork);
	}

	public void BackToMainMenu()
	{
		Application.LoadLevel("newMainMenu");
	}
}
