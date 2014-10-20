using UnityEngine;
using System.Collections;

public class AchievementsManager : MonoBehaviour {

	private AchievementsList achievementsList = new AchievementsList ();
	private TotalScoreManager totalScore = new TotalScoreManager();
	private AchievementsTrackerManager achievements = new AchievementsTrackerManager();
	private int score = 0;
	private int total;
	
	void Start () {
		totalScore.Load();
		total = totalScore.GetTotalScore();
		
		achievements.Load ();
	}
	
	void CheckDistanceAchievements(float x) {
				
		if (x >= 10 && achievements.achievementsTracker.ran10Meters) {
			achievementsList.GetRan10Meters ();
			achievements.achievementsTracker.SetRan10Meters(true);
		}
		if (x >= 20 && achievements.achievementsTracker.ran20Meters) {
			achievementsList.GetRan20Meters ();
			achievements.achievementsTracker.SetRan20Meters(true);
		}
		if (x >= 30 && achievements.achievementsTracker.ran30Meters) {
			achievementsList.GetRan30Meters ();
			achievements.achievementsTracker.SetRan30Meters(true);
		}
		if (x >= 40 && achievements.achievementsTracker.ran40Meters) {
			achievementsList.GetRan40Meters ();
			achievements.achievementsTracker.SetRan40Meters(true);
		}
		if (x >= 50 && achievements.achievementsTracker.ran50Meters) {
			achievementsList.GetRan50Meters ();
			achievements.achievementsTracker.SetRan50Meters(true);
		}
		if (x >= 250 && achievements.achievementsTracker.ran250Meters) {
			achievementsList.GetRan250Meters ();
			achievements.achievementsTracker.SetRan250Meters(true);
		}
		if (x >= 500 && achievements.achievementsTracker.ran500Meters) {
			achievementsList.GetRan500Meters ();
			achievements.achievementsTracker.SetRan500Meters(true);
		}
		if (x >= 750 && achievements.achievementsTracker.ran750Meters) {
			achievementsList.GetRan750Meters ();
			achievements.achievementsTracker.SetRan750Meters(true);
		}
		if (x >= 1000 && achievements.achievementsTracker.ran1000Meters) {
			achievementsList.GetRan1000Meters ();
			achievements.achievementsTracker.SetRan1000Meters(true);
		}
		if (x >= 1250 && achievements.achievementsTracker.ran1250Meters) {
			achievementsList.GetRan1250Meters ();
			achievements.achievementsTracker.SetRan1250Meters(true);
		}
		if (x >= 1500 && achievements.achievementsTracker.ran1500Meters) {
			achievementsList.GetRan1500Meters ();
			achievements.achievementsTracker.SetRan1500Meters(true);
		}
	}
	
	void CheckScoreAchievements(int x) {
		score += x;
		total += score;
		
		if (total >= 2000 && achievements.achievementsTracker.ran2000Total) {
			// TODO: Create achievement online, add it to AchievementsList
			achievements.achievementsTracker.SetRan2000Total(true);
		}
		
		if (total >= 5000 && achievements.achievementsTracker.ran5000Total) {
			// TODO: Create achievement online, add it to AchievementsList
			achievements.achievementsTracker.SetRan5000Total(true);
		}
		
		if (total >= 10000 && achievements.achievementsTracker.ran10000Total) {
			// TODO: Create achievement online, add it to AchievementsList
			achievements.achievementsTracker.SetRan10000Total(true);
		}
		
		if (total >= 20000 && achievements.achievementsTracker.ran20000Total) {
			// TODO: Create achievement online, add it to AchievementsList
			achievements.achievementsTracker.SetRan2000Total(true);
		}
		
		if (total >= 50000 && achievements.achievementsTracker.ran50000Total) {
			// TODO: Create achievement online, add it to AchievementsList
			achievements.achievementsTracker.SetRan50000Total(true);
		}
		
	}
	
	//Make sure this is working in develop when he dies
	void SaveTotalScore()
	{
		totalScore.UpdateScore (score);
		totalScore.SaveTotalScore ();
		
	}
}
