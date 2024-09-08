using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodedMissionSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform tf;
    public TextMeshProUGUI text;
    public So_Missions scriptableObject;
    public GameObject currentGameObj;
    void Start()
    {
        FunctionTimer.Create(SpawnAtPoint, 3f, "Timer");
        text.text = scriptableObject.GetCompletionScore().ToString();
    }
    void SpawnAtPoint(){
        currentGameObj = scriptableObject.SpawnMissionPrefab();
        Instantiate(currentGameObj, tf);
    }
}
