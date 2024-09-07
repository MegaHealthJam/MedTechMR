using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class GameManager : MonoBehaviour {
	// References
	public static GameManager instance;

	// public variables
	#region Private Vars
	[Tooltip("The total time the player has to play the game in minutes (e.g. 1 = 60 seconds, 0.5 = 30 seconds)")]
	[SerializeField] private float totalTime = 10.0f;
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
	/// Number of rooms in the game
	/// </summary>
	private int numRooms = 12;
	/// <summary>
	/// Var to keep track of the current mission type
	/// </summary>
	private int numCurrentMissionType = 0;
	/// <summary>
	/// Amount of time the player has been playing
	/// </summary>
	private float time = 0.0f;
	/// <summary>
	/// Amount of time the player has been doing the current mission
	/// </summary>
	private float missionTime = 0.0f;
	/// <summary>
	/// List of how much time the player took per missions
	/// </summary>
	private List<float> missionTimes;
	/// <summary>
	/// List of missions the player has done
	/// </summary>
	private List<int> missionList;
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

	}

	// Update is called once per frame
	void Update() {
		if (isGameActive) {
			time += Time.deltaTime;
			if (isMissionActive) {
				missionTime += Time.deltaTime;
				if (missionTime >= missions[numCurrentMissionType].GetMissionTime()) {
					EndMission();
				}
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
		GetMissionCount();
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
		numCurrentMissionType = Random.Range(1, missions.Count + 1);
		missionList.Add(numCurrentMissionType);
		isMissionActive = true;
		missionTime = 0.0f;
	}

	[Tooltip("Completes the current mission")]
	public void CompleteMission() {
		isMissionActive = false;
		missionTimes.Add(missionTime);
		missionTime = 0.0f;
	}

	[Tooltip("Ends the current mission without completion")]
	public void EndMission() {
		isMissionActive = false;
		missionTimes.Add(missions[numCurrentMissionType].GetMissionTime());
		missionTime = 0.0f;
	}
	#endregion

	#region Getters
	[Tooltip("Gets the current time")]
	public float GetTime() {
		return time;
	}

	public int GetScore() {
		return score;
	}

	public int GetMissionCount() {
		return missionList.Count;
	}
	public event Action onMissionStart;
	public void StartTheMission(){
		if(onMissionStart != null){
			onMissionStart();
		}
	}
	#endregion
	#endregion
}
