using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
	// References
	public static GameManager instance;

	InfoPopulator infoPopulator = new InfoPopulator();

	// public variables
	#region Private Vars
	[Tooltip("The total time the player has to play the game in minutes (e.g. 1 = 60 seconds, 0.5 = 30 seconds)")]
	[SerializeField] private float totalTime = 1.0f;
	[Tooltip("Put the Mission Scriptable Objects in here")]
	[SerializeField] private List<So_Missions> missions;

	/// <summary>
	/// Bool for if the game is active
	/// </summary>
	private bool isGameActive = false;
	/// <summary>
	/// Bool for if the mission is active
	/// </summary>
	private bool isMissionActive = false;
	/// <summary>
	/// Players Current Score
	/// </summary>
	private int score = 0;
	/// <summary>
	/// Players High Score
	/// </summary>
	private int highScore = 0;
	/// <summary>
	/// Amount of time the player has been playing
	/// </summary>
	private float time = 0.0f;
	#endregion

	#region Unity Methods
	// Singleton
	private void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this.gameObject);
		}
	}

	// Start is called before the first frame update
	void Start() {
		infoPopulator.Populate();
	}

	// Update is called once per frame
	void Update() {
		if (isGameActive) {
			time += Time.deltaTime;
			if (isMissionActive) {

			} else {
				StartMission();
			}
			if (time >= totalTime * 60) {
				EndGame();
			}
		}
	}
	#endregion

	#region Methods
	#region Game Methods
	[Tooltip("Start the game")]
	public void StartGame() {
		isGameActive = true;
		score = 0;
		time = 0.0f;
	}

	private void EndGame() {
		isGameActive = false;
		if (score > highScore) {
			highScore = score;
		}
	}

	[Tooltip("Edit the players score. Negative numbers subtract score.")]
	public void AddScore(int value) {
		score += value;
	}

	[Tooltip("Pause the game timer")]
	public void PauseTimer() {
		isGameActive = false;
	}

	[Tooltip("Resume the game timer")]
	public void ResumeTimer() {
		isGameActive = true;
	}
	#endregion

	#region Mission Methods
	[Tooltip("Begins the current mission")]
	public void StartMission() {
		isMissionActive = true;
	}
	#endregion

	#region Getters
	[Tooltip("Gets the current time")]
	public float GetTime() => time;

	public int GetScore() => score;

	public bool GetPatientSex() => infoPopulator.sex;
	public string GetPatientName() => infoPopulator.patientName;
	public int GetBloodPressure() => infoPopulator.bloodPressure;
	public bool GetIV() => infoPopulator.iv;
	public int GetMedication() => infoPopulator.medication;
	public int GetAssessment() => infoPopulator.assessment;
	#endregion
	#endregion
}
