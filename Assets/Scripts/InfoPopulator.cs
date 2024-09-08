using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Spawns an interactable object at the location of the object respawner
/// </summary>
public class InfoPopulator {
	public bool sex;
	public string patientName;
	public int bloodPressure;
	public bool iv;
	public int medication;
	public int assessment;

	public InfoPopulator() {
		Sex();
		PatientName();
		BloodPressure();
		IV();
		Medication();
		AssessmentList();
	}

	//public void Populate() {
	//	
	//}

	private void Sex() {
		switch (Random.Range(0, 2)) {
			case 0: {
				// Male
				sex = false;
				break;
			}
			case 1: {
				// Female
				sex = true;
				break;
			}
		}
	}

	private void PatientName() {
		List<string> Malelist = new List<string>();
		List<string> Femalelist = new List<string>();

		Malelist.Add("John Doe");
		Malelist.Add("Danny Tapia");
		Malelist.Add("Timothy Forest");
		Malelist.Add("Delano Igbinoba");
		Malelist.Add("Kunal Patel");

		Femalelist.Add("Isabella Johnston");

		if (sex) { // female
			patientName = Femalelist[Random.Range(0, Femalelist.Count)];
		} else { // male
			patientName = Malelist[Random.Range(0, Malelist.Count)];
		}
	}

	private void BloodPressure() {
		bloodPressure = Random.Range(0, 3);
	}

	private void IV() {
		switch (Random.Range(0, 2)) {
			case 0: {
				// Male
				iv = false;
				break;
			}
			case 1: {
				// Female
				iv = true;
				break;
			}
		}
	}

	private void Medication() {
		medication = Random.Range(0, 3);
	}

	private void AssessmentList() {
		assessment = Random.Range(0, 6);
	}
}
