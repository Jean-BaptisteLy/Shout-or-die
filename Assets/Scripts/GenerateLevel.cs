using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateLevel : MonoBehaviour
{
	private float tempsOptimal;
	private bool chrono;
	private float tempsInitial;
	private bool allowedSilence;
	private float timeStay;

	public float ratio_temps;
	private ProcessData processData;

	// Start is called before the first frame update
	void Start()
	{
		// var
		processData = gameObject.GetComponent<ProcessData>();
		ratio_temps = 1 - processData.timeRatioList[processData.timeRatioList.Count - 1];

		// Temps optimal pour chaque niveau
		if (SceneManager.GetActiveScene().name == "Level 0") {
			tempsOptimal = 0.0f;
		}
		else if (SceneManager.GetActiveScene().name == "Level 1") {
			tempsOptimal = 0.0f;
		}
		else if (SceneManager.GetActiveScene().name == "Level 2") {
			tempsOptimal = 0.0f;
		}
		else if (SceneManager.GetActiveScene().name == "Level 3") {
			tempsOptimal = 0.0f;
		}
		else {
			// LOL
		}

		// var algo
    	tempsOptimal = tempsOptimal + (tempsOptimal * (1 - processData.timeRatioList[processData.timeRatioList.Count - 1]));
    	chrono = false;
    	tempsInitial = (tempsOptimal / 10 ) / ratio_temps;
    	allowedSilence = true;
    	timeStay = (tempsOptimal / 10) / ratio_temps;

    	// Bon et rapide
		if (processData.playerCategory == 4) {
			chrono = true;
			allowedSilence = false;
		}
		// Bon mais lent
		else if (processData.playerCategory == 3) {
			allowedSilence = false;
		}
		// Mauvais mais rapide
		else if (processData.playerCategory == 2) {
			chrono = true;
		}
		// Mauvais et lent
		else if (processData.playerCategory == 1) {
			// Destruction de toutes les pièces, ou random

		}

	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
