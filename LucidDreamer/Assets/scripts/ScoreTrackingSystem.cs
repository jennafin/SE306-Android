using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ScoreTrackingSystem {

	public int currentScorePoints;


	private List<Multiplier> multipliers = new List<Multiplier> ();
	private int pointsToBeAdded = 0;

	public ScoreTrackingSystem()
	{
		currentScorePoints = 0;
	}

	public void ResetScore()
	{
		currentScorePoints = 0;
		multipliers.Clear();
	}

	public void AddPoints(int addMe)
	{
		pointsToBeAdded = pointsToBeAdded + addMe;
		Debug.Log ("Added points");
	}

	public void AddMultiplier(int multi, int time)
	{
		multipliers.Add (new Multiplier (multi, time));
	}

	public void ResetMultiplier()
	{
		multipliers.Clear();
	}

	public int UpdateScore(int distance)
	{
		int totalMultiplier = 1;

		for (int i = multipliers.Count - 1; i >= 0; i--)
		{
			totalMultiplier = totalMultiplier * multipliers[i].getMultiplier();
			if (multipliers[i].checkExpired())
				multipliers.RemoveAt(i);
		}

		currentScorePoints = currentScorePoints + (pointsToBeAdded * totalMultiplier);
		pointsToBeAdded = 0;
		Debug.Log (currentScorePoints.ToString());
		return distance + currentScorePoints;
	}

	public int GetCurrentScore(int distance)
	{
		return UpdateScore(distance);
	}

	public int gameOver(int distance)
	{
		int finalScore = UpdateScore (distance);
		Social.ReportScore (finalScore, "CgkIj8PyxKwKEAIQAQ", (bool success) => {
			//TODO: handle success or failure
		});
		return finalScore;

	}
}

class Multiplier
{
	private int multiplier = 1;
	private int timer = 0;

	public Multiplier(int multi, int time)
	{
		this.multiplier = multi;
		this.timer = time;
	}

	public bool checkExpired()
	{
		if (timer < 0) 
		{
			return true;
		} 
		return false;
	}

	public int getMultiplier()
	{

		if (timer > 0)
		{
			timer = timer -1;
			return multiplier;
		} else {
			return 1;
		}


	}

}
