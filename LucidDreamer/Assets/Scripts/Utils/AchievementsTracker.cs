using UnityEngine;
using System.Collections;

[System.Serializable]
public class AchievementsTracker {
	
	public bool ran10Meters = false;
	public bool ran20Meters = false;
	public bool ran30Meters = false;
	public bool ran40Meters = false;
	public bool ran50Meters = false;
	public bool ran250Meters = false;
	public bool ran500Meters = false;
	public bool ran750Meters = false;
	public bool ran1000Meters = false;
	public bool ran1250Meters = false;
	public bool ran1500Meters = false;
	public bool score2000Total = false;
	public bool score5000Total = false;
	public bool score10000Total = false;
	public bool score20000Total = false;
	public bool score50000Total = false;
	

	public void SetRan10Meters(bool val) {
		this.ran10Meters = val;
	}
	
	public void SetRan20Meters(bool val) {
		this.ran20Meters = val;
	}
	
	public void SetRan30Meters(bool val) {
		this.ran30Meters = val;
	}
	
	public void SetRan40Meters(bool val) {
		this.ran40Meters = val;
	}
	
	public void SetRan50Meters(bool val) {
		this.ran50Meters = val;
	}
	
	public void SetRan250Meters(bool val) {
		this.ran250Meters = val;
	}
	
	public void SetRan500Meters(bool val) {
		this.ran500Meters = val;
	}
	
	public void SetRan750Meters(bool val) {
		this.ran750Meters = val;
	}
	
	public void SetRan1000Meters(bool val) {
		this.ran1000Meters = val;
	}
	
	public void SetRan1250Meters(bool val) {
		this.ran1250Meters = val;
	}
	
	public void SetRan1500Meters(bool val) {
		this.ran1500Meters = val;
	}
	
	public void SetScore2000Total(bool val) {
		this.score2000Total = val;
	}
	
	public void SetScore5000Total(bool val) {
		this.score5000Total = val;
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
}
