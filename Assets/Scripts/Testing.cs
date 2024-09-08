using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI mainText;
    public Transform spawnPoint;
    private void Start(){
        ScriptableObject newCodeItem = ScriptableObject.CreateInstance<So_Missions>();
        
        FunctionTimer.Create(TestingAction, 3f, "Timer");
        //FunctionTimer.Create(TestingAction2, 4f, "Timer2");
        //FunctionTimer.StopTimer("Timer");
    }
    private void TestingAction(){
        SetScreenInfo();
    }
    private void TestingAction2(){
        Debug.Log("Code Red on Patient 8");
    }
 
    public string SetScreenInfo(){
        int randomNumber = UnityEngine.Random.Range(1, 8);
        String text = "Code Blue on Patient " + randomNumber;
        Debug.Log(text);
        return text;
    }
}
