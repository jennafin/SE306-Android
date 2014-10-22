/* This file is generated by make_achievements.sh */
using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public  class AchievementsList {
	
	// Yes the default value is false, http://msdn.microsoft.com/en-us/library/c8f5xwh7.aspx
	private bool[] executed = new bool[     17];
	
	public void GetUnityWorks() {
		if (!executed[0]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQAg", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[0] = true;
		}
	}
	
	public void GetRan100Meters() {
		if (!executed[1]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQAw", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[1] = true;
		}
	}
	
	public void GetRan1000Meters() {
		if (!executed[2]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQBA", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[2] = true;
		}
	}
	
	public void GetRan10000Meters() {
		if (!executed[3]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQBQ", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[3] = true;
		}
	}
	
	public void GetRan20000Meters() {
		if (!executed[4]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQBg", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[4] = true;
		}
	}
	
	public void GetPlayed1MinutesTotal() {
		if (!executed[5]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQCA", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[5] = true;
		}
	}
	
	public void GetPlayed10MinutesTotal() {
		if (!executed[6]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQBw", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[6] = true;
		}
	}
	
	public void GetPlayed60MinutesTotal() {
		if (!executed[7]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQCQ", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[7] = true;
		}
	}
	
	public void GetPlayedFor120MinutesTotal() {
		if (!executed[8]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQCg", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[8] = true;
		}
	}
	
	public void GetLucidRunner() {
		if (!executed[9]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQCw", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[9] = true;
		}
	}
	
	public void GetRan1250Meters() {
		if (!executed[10]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQDA", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[10] = true;
		}
	}
	
	public void GetRan1500Meters() {
		if (!executed[11]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQDQ", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[11] = true;
		}
	}
	
	public void GetCumulativeScoreOver10000() {
		if (!executed[12]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQEA", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[12] = true;
		}
	}
	
	public void GetCumulativeScoreOver20000() {
		if (!executed[13]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQEQ", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[13] = true;
		}
	}
	
	public void GetCumulativeScoreOver50000() {
		if (!executed[14]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQEg", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[14] = true;
		}
	}
	
	public void GetCumulativeScoreOver80000() {
		if (!executed[15]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQEw", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[15] = true;
		}
	}
	
	public void GetCumulativeScoreOver100000() {
		if (!executed[16]) {
			Social.ReportProgress ("CgkIj8PyxKwKEAIQDg", 100.0f, (bool success) => {
				// handle success or failure
			});
			executed[16] = true;
		}
	}
	
}
