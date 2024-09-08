using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	public GameObject patientSextext;
	public GameObject patientNametext;
	public GameObject bloodPressureText;
	public GameObject ivText;
	public GameObject medicationText;
	public GameObject assessmentText;
	public GameObject timertext;

	// Start is called before the first frame update
	void Start() {
		GetInfo();
	}

	// Update is called once per frame
	void Update() {
		UpdateTimer();
	}

	private void UpdateTimer() {
		float timeleft = GameManager.instance.GetTotalTime() - GameManager.instance.GetTime();
		int minutes = (int) timeleft / 60;
		int seconds = (int) timeleft % 60;
		timertext.GetComponent<TextMesh>().text = string.Format("{0:0}:{1:00}", minutes, seconds);
	}

	private void GetInfo() {
		if (GameManager.instance.GetPatientSex()) {
			patientSextext.GetComponent<TextMesh>().text = "Female";
		} else {
			patientSextext.GetComponent<TextMesh>().text = "Male";
		}

		patientNametext.GetComponent<TextMesh>().text = GameManager.instance.GetPatientName();

		switch (GameManager.instance.GetBloodPressure()) {
			case 0: {
				bloodPressureText.GetComponent<TextMesh>().text = "Low";
				break;
			}
			case 1: {
				bloodPressureText.GetComponent<TextMesh>().text = "Medium";
				break;
			}
			case 2: {
				bloodPressureText.GetComponent<TextMesh>().text = "High";
				break;
			}
		}

		if (GameManager.instance.GetIV()) {
			ivText.GetComponent<TextMesh>().text = "Yes";
		} else {
			ivText.GetComponent<TextMesh>().text = "No";
		}

		switch (GameManager.instance.GetMedication()) {
			case 0: {
				medicationText.GetComponent<TextMesh>().text = "Sleep";
				break;
			}
			case 1: {
				medicationText.GetComponent<TextMesh>().text = "Pain";
				break;
			}
			case 2: {
				medicationText.GetComponent<TextMesh>().text = "Antibiotics";
				break;
			}
		}

		switch (GameManager.instance.GetAssessment()) {
			case 0: {
				assessmentText.GetComponent<TextMesh>().text = "Ligma";
				break;
			}
			case 1: {
				assessmentText.GetComponent<TextMesh>().text = "Hypovolemia";
				break;
			}
			case 2: {
				assessmentText.GetComponent<TextMesh>().text = "Sepsis";
				break;
			}
			case 3: {
				assessmentText.GetComponent<TextMesh>().text = "Hemorrhage";
				break;
			}
			case 4: {
				assessmentText.GetComponent<TextMesh>().text = "Cardiogenic shock";
				break;
			}
		}
	}
}