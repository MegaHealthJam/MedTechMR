using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
	public static UIManager instance;

	public GameObject patientInfotext;
	public GameObject timertext;
	public GameObject ContinueButton;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this.gameObject);
		}
	}

	// Start is called before the first frame update
	void Start() {
		patientInfotext.GetComponent<TMP_Text>().text = GetInfo();
	}

	// Update is called once per frame
	void Update() {
		UpdateTimer();
	}

	private void UpdateTimer() {
		float timeleft = (GameManager.instance.GetTotalTime * 60) - GameManager.instance.GetTime;
		int minutes = (int) timeleft / 60;
		int seconds = (int) timeleft % 60;
		timertext.GetComponent<TMP_Text>().text = string.Format("{0:0}:{1:00}", minutes, seconds);
	}

	private string GetInfo() {
		string final = "";
		final += "Name: " + GameManager.instance.GetPatientName + "\n";
		
		if (GameManager.instance.GetPatientSex) {
			final += "Sex: " + "Female" + "\n";
		} else {
			final += "Sex: " + "Male" + "\n";
		}


		switch (GameManager.instance.GetBloodPressure) {
			case 0: {
				final += "Blood Pressre: " + "Low" + "\n";
				break;
			}
			case 1: {
				final += "Blood Pressre: " + "Medium" + "\n";
				break;
			}
			case 2: {
				final += "Blood Pressre: " + "High" + "\n";
				break;
			}
		}

		if (GameManager.instance.GetIV) {
			final += "Needs IV: " + "Yes" + "\n";
		} else {
			final += "Needs IV: " + "No" + "\n";
		}

		switch (GameManager.instance.GetMedication) {
			case 0: {
				final += "Medication for: " + "Sleep" + "\n";
				break;
			}
			case 1: {
				final += "Medication for: " + "Pain" + "\n";
				break;
			}
			case 2: {
				final += "Medication for: " + "Antibiotics" + "\n";
				break;
			}
		}

		switch (GameManager.instance.GetAssessment) {
			case 0: {
				final += "Assessment: " + "Ligma" + "\n";
				break;
			}
			case 1: {
				final += "Assessment: " + "Hypovolemia" + "\n";
				break;
			}
			case 2: {
				final += "Assessment: " + "Sepsis" + "\n";
				break;
			}
			case 3: {
				final += "Assessment: " + "Hemorrhage" + "\n";
				break;
			}
			case 4: {
				final += "Assessment: " + "Cardiogenic shock" + "\n";
				break;
			}
		}
		//Debug.Log(final);
		return final;
	}

	public void ActivateContinueButton() {
		ContinueButton.SetActive(true);
	}
}