using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{
    private static List<FunctionTimer> activeTimerList;
    private static GameObject initGameObject;
    private static void InitIfNeeded() {
        if(initGameObject == null){
            initGameObject = new GameObject("FunctionTimer_InitGameObject");
            activeTimerList = new List<FunctionTimer>();
        }
    }
    public static FunctionTimer Create(Action action, float timer, string timerName = null){

        InitIfNeeded();

        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));

        FunctionTimer functionTimer = new FunctionTimer(action, timer, timerName, gameObject);

        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

        activeTimerList.Add(functionTimer);

        return functionTimer;
    }
    // DUmmy class to have access to MonoBehavior Function
    private class MonoBehaviourHook : MonoBehaviour {
        public Action onUpdate;
        private void Update() {
            if (onUpdate != null) onUpdate();
        }
    }

    private static void RemoveTimer(FunctionTimer functionTimer){
        InitIfNeeded();
        activeTimerList.Remove(functionTimer);
    }

    private static void StopTimer(string timerName){
        for(int i = 0; i < activeTimerList.Count; i++){
            if(activeTimerList[i].timerName == timerName){
                //Stop the timer
                //Debug.Log()
                activeTimerList[i].DestroySelf();
                i--;
            }
        }
    }
    private Action action;
    private float timer;
    private bool isDestroyed;
    private GameObject gameObject;
    private string timerName;
   private FunctionTimer(Action action, float timer, string timerName, GameObject gameObject ){
    this.action = action;
    this.timer = timer;
    this.timerName = timerName;
    this.gameObject = gameObject;
    isDestroyed = false;
   }
   public void Update(){
    if(!isDestroyed){
            timer -= Time.deltaTime;
            //Debug.Log(timer);
    if(timer < 0){
        //Trigger the action
        action();
        DestroySelf();
    }
    }

   }

   private void DestroySelf(){
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
        RemoveTimer(this);
   }
}
