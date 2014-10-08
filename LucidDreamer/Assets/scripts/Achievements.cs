using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public static class Achievements {

	private bool one = false;
	private bool two = false;
	private bool three = false;
	private bool four = false;
	private bool five = false;
	private bool six = false;

	public static void GetUnityWorks() {
		Social.ReportProgress("CgkIj8PyxKwKEAIQAg", 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void GetRan10Meters() {
		Social.ReportProgress("CgkIj8PyxKwKEAIQAw", 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void GetRan20Meters() {
		Social.ReportProgress("CgkIj8PyxKwKEAIQBA", 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void GetRan30Meters() {
		Social.ReportProgress("CgkIj8PyxKwKEAIQBQ", 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void GetRan40Meters() {
		Social.ReportProgress("CgkIj8PyxKwKEAIQBg", 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void GetRan50Meters() {
		Social.ReportProgress("CgkIj8PyxKwKEAIQBw", 100.0f, (bool success) => {
			// handle success or failure
		});
	}
}