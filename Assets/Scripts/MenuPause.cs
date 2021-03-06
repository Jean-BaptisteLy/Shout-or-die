using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
using System;


public class MenuPause : MonoBehaviour
{
	//public Object sceneToLoad;
	private bool isPaused = false;
	public bool timeIsUp = false;
	private bool feedback = true;
	private TMPro.TextMeshProUGUI text;
	private Timer timer;
	private LevelManager lm;
	private PlayerStats playerStats;
	private GameObject player;
	// Start is called before the first frame update
	void Start()
	{
		TMPro.TextMeshProUGUI text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
		timer = text.GetComponent<Timer>();
		lm = gameObject.GetComponent<LevelManager>();
		playerStats = gameObject.GetComponent<PlayerStats>();
		player = GameObject.Find("Player");

	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = !isPaused;
		}
		/*
		if ( (timer.timeLeft <= 0f || timer.timeStay <= 0f) && (lm.currentLevel != 0) && (playerStats.currentCategory != 0) && timeIsUp == false ) {
			timeIsUp = !timeIsUp;
		}
		*/
		if (Input.GetKeyDown(KeyCode.F)) {
			feedback = !feedback;
		}
		if(isPaused || timeIsUp || feedback)
			Time.timeScale = 0f;
		else
			Time.timeScale = 1f;

	}

	void OnGUI () 
	{
		string feedbackMessage = "";

		if(isPaused)
		{
			// Si on clique sur le bouton alors isPaused devient faux donc le jeu reprend
			if(GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 20, 80, 40), "Resume"))
			{
				isPaused = false;
			}
			// Si on clique sur le bouton alors on ferme completment le jeu ou on charge la scene Menu Principal
			// Dans le cas du bouton Quitter, il faut augmenter sa position Y pour qu'il soit plus bas.
			if(GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), "Main menu"))
			{
				//Application.LoadLevel(sceneToLoad.name); // Charge le menu principal
				isPaused = false;
				SceneManager.LoadScene("MainMenu"); // Charge le menu principal
				Destroy(player);
			}
			if(GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 100, 80, 40), "Exit game"))
			{
				Application.Quit(); // Ferme le jeu
			}
		}
		else if (timeIsUp) {
			if(GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 20, 80, 40), "Time's up !"))
			{
				timeIsUp = false;
				lm.restartCurrentLevel();
			}
		}
		else if (feedback) {
			if (playerStats.currentLevel == 0){
				feedback = false;
			}
			else if (playerStats.currentCategory == 0) {
				feedbackMessage = "Focus on producing noise constantly.\n Click here to continue.";
			}
			else if (playerStats.currentCategory == 1) {
				feedbackMessage = "Try to better modulate your voice to collect more coins. \n Click here to continue.";
			}
			else if (playerStats.currentCategory == 2) {
				feedbackMessage = "Try not to stop producing noise so you finish the level \n on time and keep on modulating your voice to collect coins. \n Click here to continue.";
			}
			else if (playerStats.currentCategory == 3) {
				feedbackMessage = "You are doing well, keep on like that! \n Click here to continue.";
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 20, 480, 80), feedbackMessage) && feedback) {
				feedback = false;
			}
		}
	}

	public void inverseFeedbackBool(){
		// inverses the value of the feedback variable
		feedback = !feedback;
	}
}