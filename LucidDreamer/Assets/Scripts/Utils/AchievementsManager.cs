using UnityEngine;
using System.Collections;

public class AchievementsManager : MonoBehaviour {

	private AchievementsList achievementsList = new AchievementsList ();
	private TotalScoreManager totalScore = new TotalScoreManager();
	private AchievementsTrackerManager achievements = new AchievementsTrackerManager();
	private int score = 0;
	private int total;
	
	public void Load () {
		// Load total score
		totalScore.Load();
		total = totalScore.GetTotalScore();
	
		// Load achievements status
		achievements.Load ();
	}
	
	// check all the distance achievements
	public void CheckDistanceAchievements(float x) {
	
		if (x >= 100 && !achievements.achievementsTracker.ran100Meters) {
			achievementsList.GetRan100Meters ();
			achievements.achievementsTracker.SetRan100Meters(true);
		}
		
		if (x >= 20 && !achievements.achievementsTracker.ran1000Meters) {
			achievementsList.GetRan1000Meters ();
			achievements.achievementsTracker.SetRan1000Meters(true);
		}
		
		if (x >= 30 && !achievements.achievementsTracker.ran10000Meters) {
			achievementsList.GetRan10000Meters ();
			achievements.achievementsTracker.SetRan10000Meters(true);
		}
		
		if (x >= 40 && !achievements.achievementsTracker.ran20000Meters) {
			achievementsList.GetRan20000Meters ();
			achievements.achievementsTracker.SetRan20000Meters(true);
		}
	}
	
	// check all the score achievements
	public void CheckScoreAchievements(int x) {
	
		score = x;
		int totalTest = total + x;		
		
		if (totalTest >= 10000 && !achievements.achievementsTracker.score10000Total) {
			achievementsList.GetCumulativeScoreOver10000 ();
			achievements.achievementsTracker.SetScore10000Total(true);
		}
		
		if (totalTest >= 20000 && !achievements.achievementsTracker.score20000Total) {
			achievementsList.GetCumulativeScoreOver20000 ();
			achievements.achievementsTracker.SetSore20000Total(true);
		}
		
		if (totalTest >= 50000 && !achievements.achievementsTracker.score50000Total) {
			achievementsList.GetCumulativeScoreOver50000 ();
			achievements.achievementsTracker.SetScore50000Total(true);
		}
		
		if (totalTest >= 80000 && !achievements.achievementsTracker.score80000Total) {
			achievementsList.GetCumulativeScoreOver80000 ();
			achievements.achievementsTracker.SetScore80000Total(true);
		}
		
		if (totalTest >= 100000 && !achievements.achievementsTracker.score100000Total) {
			achievementsList.GetCumulativeScoreOver100000 ();
			achievements.achievementsTracker.SetScore100000Total(true);
		}	
	}
	
	//Save scores 
	public void SaveTotalScore()
	{
		totalScore.UpdateScore (score);
		totalScore.SaveTotalScore ();
	}
}
