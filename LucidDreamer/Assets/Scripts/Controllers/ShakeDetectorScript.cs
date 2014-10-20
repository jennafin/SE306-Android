using UnityEngine;
using System;
using System.Collections.Generic;

public class ShakeDetectorScript : MonoBehaviour {

  private List<Shakeable> subscribers = new List<Shakeable>();

  int currentShake = Environment.TickCount;

	private float previousDistance = 0;


  //Lucid power level
	private  float lucidPower =1;

	//HUD
	public GameObject LucidHUDObject;

	//GameController
	public GameObject gameControllerObject;

	//GameController Script
	private GameControllerScript gameController;

	//ludic hud script
	private HUDLucidGUI LucidHUD;

	//Raio of score to lucid power
	public float distancePowerRatio = 0.01f;

	//Decrement speed
	public float PowerDownRatio = 0.999f;

	//If lucid power has just been used
	private bool PowerDown = false;

  public float avrgTime = 0.5f;
  public float peakLevel = 0.6f;
  public float endCountTime = 0.6f;
  public int shakeDir;
  public int shakeCount;

  Vector3 avrgAcc = Vector3.zero;
  int countPos;
  int countNeg;
  int lastPeak;
  int firstPeak;
  bool counting;
  float timer;


	void Start(){
		gameController = gameControllerObject.GetComponent<GameControllerScript> ();
		LucidHUD = LucidHUDObject.GetComponent<HUDLucidGUI> ();
		}

  /* This method comes from unity forums
   * http://answers.unity3d.com/questions/427191/how-to-shake-phone-using-c.html
   */
  bool ShakeDetector(){
    // read acceleration:
    Vector3 curAcc = Input.acceleration;
    // update average value:
    avrgAcc = Vector3.Lerp(avrgAcc, curAcc, avrgTime * Time.deltaTime);
    // calculate peak size:
    curAcc -= avrgAcc;
    // variable peak is zero when no peak detected...
    int peak = 0;
    // or +/- 1 according to the peak polarity:
    if (curAcc.y > peakLevel) peak = 1;
    if (curAcc.y < -peakLevel) peak = -1;
    // do nothing if peak is the same of previous frame:
    if (peak == lastPeak)
      return false;
    // peak changed state: process it
    lastPeak = peak; // update lastPeak
    if (peak != 0){ // if a peak was detected...
      timer = 0; // clear end count timer...
      if (peak > 0) // and increment corresponding count
        countPos++;
      else
        countNeg++;
      if (!counting){ // if it's the first peak...
        counting = true; // start shake counting
        firstPeak = peak; // save the first peak direction
      }
    }
    else // but if no peak detected...
    if (counting){ // and it was counting...
      timer += Time.deltaTime; // increment timer
      if (timer > endCountTime){ // if endCountTime reached...
        counting = false; // finish counting...
        shakeDir = firstPeak; // inform direction of first shake...
        if (countPos > countNeg) // and return the higher count
          shakeCount = countPos;
        else
          shakeCount = countNeg;
        // zero counters and become ready for next shake count
        countPos = 0;
        countNeg = 0;
        return true; // count finished
      }
    }
    return false;
  }

  void Update() {
    if (ShakeDetector()){ // call ShakeDetector every Update!
      // the device was shaken up and the count is in shakeCount
      // the direction of the first shake is in shakeDir (1 or -1)
    }
    // the variable counting tells when the device is being shaken:
    if (counting){
      if (Math.Abs(Environment.TickCount - currentShake) > 500) {
        phoneShake();
        counting = false;
        currentShake = Environment.TickCount;
      }
    }
		if (!PowerDown) {
			//Get the change in score to update lucid power
			float current = gameController.GetDistance ();

			lucidPower += (current - previousDistance) * distancePowerRatio;
			previousDistance = current;
//			
//			
			LucidHUD.UpdateDisplay (lucidPower);
		} else {
			lucidPower = lucidPower * PowerDownRatio;
			LucidHUD.UpdateDisplay (lucidPower);
			if (lucidPower < 0.1){
				lucidPower = 0;
				PowerDown = false;
			}
		}

  }
	//for testing
	void OnGUI(){

		if (GUI.Button (new Rect (100,100,80,20), "Use Lucid")) {
						phoneShake ();
				}
		}

  // Notify subscribers
  private void phoneShake() {
		//if there is enough lucid power
		if (lucidPower > 0.99) {
//			foreach (Shakeable obj in subscribers) {
//				//if the object is visible on screen
//				//Note isVisible might  be triggered by scene view camera
//				if(obj.isOnScreen()){
//					obj.doShakeAction ();
//				}
//			}
			PowerDown = true;
//			Debug.Log ("hahaha");
			gameController.CollectAllCollectables();

		}
  }

  public void Subscribe(Shakeable obj) {
    subscribers.Add(obj);
  }
}
