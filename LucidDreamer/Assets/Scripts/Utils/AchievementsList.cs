using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public  class AchievementsList {

	private  bool one = true;
	private  bool two = true;
	private  bool three = true;
	private  bool four = true;
	private  bool five = true;
	private  bool six = true;

	public  void GetUnityWorks() {
		if (one) 
		{
			Social.ReportProgress ("CgkIj8PyxKwKEAIQAg", 100.0f, (bool success) => {
					// handle success or failure
			});
			one= false;
		}
	}
	
	public  void GetRan10Meters() {
		if (two) {
						Social.ReportProgress ("CgkIj8PyxKwKEAIQAw", 100.0f, (bool success) => {
								// handle success or failure
						});
						two = false;
				}
	}
	
	public  void GetRan20Meters() {
		if (three) {
						Social.ReportProgress ("CgkIj8PyxKwKEAIQBA", 100.0f, (bool success) => {
								// handle success or failure
						});
						three = false;
				}
	}
	
	public  void GetRan30Meters() {
		if (four) {

						Social.ReportProgress ("CgkIj8PyxKwKEAIQBQ", 100.0f, (bool success) => {
								// handle success or failure
						});
						four = false;
				}
	}
	
	public  void GetRan40Meters() {
		if (five) {
						Social.ReportProgress ("CgkIj8PyxKwKEAIQBg", 100.0f, (bool success) => {
								// handle success or failure
						});
						five = false;
				}
	}
	
	public  void GetRan50Meters() {
		if (six) {

						Social.ReportProgress ("CgkIj8PyxKwKEAIQBw", 100.0f, (bool success) => {
								// handle success or failure
						});
						six = false;
				}
	}
}