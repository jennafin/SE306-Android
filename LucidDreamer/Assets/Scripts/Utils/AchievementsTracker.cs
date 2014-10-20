using UnityEngine;
using System.Collections;

[System.Serializable]
public class AchievementsTracker {
	
	public bool ran100Meters = false;
	public bool ran1000Meters = false;
	public bool ran10000Meters = false;
	public bool ran20000Meters = false;
	public bool score10000Total = false;
	public bool score20000Total = false;
	public bool score50000Total = false;
	public bool score80000Total = false;
	public bool score100000Total = false;
	

	public void SetRan100Meters(bool val) {
		this.ran100Meters = val;
	}
	
	public void SetRan1000Meters(bool val) {
		this.ran1000Meters = val;
	}
	
	public void SetRan10000Meters(bool val) {
		this.ran10000Meters = val;
	}
	
	public void SetRan20000Meters(bool val) {
		this.ran20000Meters = val;
	}
	
	public void SetScore10000Total(bool val) {
		this.score10000Total = val;
	}
	
	public void SetSore20000Total(bool val) {
		this.score20000Total = val;
	}
	
	public void SetScore50000Total(bool val) {
		this.score50000Total = val;
	}
	
	public void SetScore80000Total(bool val) {
		this.score80000Total = val;
	}
	
	public void SetScore100000Total(bool val) {
		this.score100000Total = val;
	}
}
