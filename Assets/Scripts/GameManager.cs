using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
	// References
	public static GameManager instance;

	InfoPopulator infoPopulator;

	// public variables
	#region Private Vars
	[Tooltip("The total time the player has to play the game in minutes (e.g. 1 = 60 seconds, 0.5 = 30 seconds)")]
	[SerializeField] private float totalTime = 1.0f;

	/// <summary>
	/// Bool for if the game is active
	/// </summary>
	private bool isGameActive = false;
	/// <summary>
	/// Players Current Score
	/// </summary>
	private int score = 0;
	/// <summary>
	/// Amount of time the player has been playing
	/// </summary>
	private float time = 0.0f;
	private int roomNumber = 0;
	#endregion

	public static event Action<int> OnMissionStarted;

	#region Unity Methods
	// Singleton
	private void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this.gameObject);
		}
		infoPopulator = new InfoPopulator();
	}

	// Start is called before the first frame update
	void Start() {
		StartTheGame();
	}

	// Update is called once per frame
	void Update() {
		if (isGameActive) {
			time += Time.deltaTime;
			if (time >= totalTime * 60.0f) {
				EndGame();
			}
		}
	}
	#endregion

	#region Methods
	#region Game Methods
	[Tooltip("Start the game")]
	public void StartTheGame() {
		StartCoroutine(WaitToStartGame());
	}
	private void StartGame() {
		isGameActive = true;
		roomNumber = Random.Range(1, 13); // 13 is exclusive, so this gives numbers 1-12
		OnMissionStarted?.Invoke(roomNumber);
		score = 0;
		time = 0.0f;
		PlayerController.instance.PulseController.SetAlarm((GameManager.instance.GetTotalTime * 60) - GameManager.instance.GetTime);
		PlayerController.instance.HeartbeatSound.SetHeartbeatAlarm((GameManager.instance.GetTotalTime * 60) - GameManager.instance.GetTime);
	}

	public string EndGame() {
		isGameActive = false;
		// calculate the final score for the player in percentage
		return score * 25 + "%";
	}

	[Tooltip("Edit the players score.")]
	public void BloodPressureScore(int value) {
		if (value == GetBloodPressure) {
			score += 1;
		}
	}
	public void IVScore(bool value) {
		if (value == GetIV) {
			score += 1;
		}
	}
	public void MedicationScore(int value) {
		if (value == GetMedication) {
			score += 1;
		}
	}
	public void AssessmentScore(int value) {
		if (value == GetAssessment) {
			score += 1;
		}
	}
	#endregion

	#region Mission Methods
	[Tooltip("A function in case we need it to trigger the difference between the info screen and the game's ui")]
	public void RoomEntered() {
		UIManager.instance.ActivateContinueButton();
	}
	#endregion

	#region Getters
	[Tooltip("Gets the current time")]
	public float GetTime => time;
	[Tooltip("Gets the time for how long the game will last total")]
	public float GetTotalTime => totalTime;

	public int GetScore => score;
	public int GetRoom => roomNumber;
	
	public bool GetPatientSex => infoPopulator.sex;
	public string GetPatientName => infoPopulator.patientName;
	public int GetBloodPressure => infoPopulator.bloodPressure;
	public bool GetIV => infoPopulator.iv;
	public int GetMedication => infoPopulator.medication;
	public int GetAssessment => infoPopulator.assessment;
	#endregion
	#endregion

	private IEnumerator WaitToStartGame() {
		yield return new WaitForSeconds(Random.Range(1f, 6f));
		StartGame();
	}
}
