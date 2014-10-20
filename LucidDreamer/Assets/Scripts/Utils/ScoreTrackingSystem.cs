using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ScoreTrackingSystem {

	public int currentScorePoints;


	private int multiplier = 1;
	private int pointsToBeAdded = 0;

	public ScoreTrackingSystem()
	{
		currentScorePoints = 0;
	}

	public void ResetScore()
	{
		currentScorePoints = 0;
		multiplier = 1;
	}

	public void AddPoints(int addMe)
	{
		pointsToBeAdded = pointsToBeAdded + addMe;
	}

	public void AddMultiplier(int multiplierToAdd)
	{
		multiplier *= multiplierToAdd;
	}

	public void RemoveMultiplier(int multiplierToRemove)
	{
		multiplier /= multiplierToRemove;
	}

	public void ResetMultiplier()
	{
		multiplier = 1;
	}

	public int UpdateScore(int distance)
	{
		currentScorePoints = currentScorePoints + (pointsToBeAdded * multiplier);
		pointsToBeAdded = 0;
		//
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
		PlayerPrefs.SetInt ("Score", finalScore);
		return finalScore;

	}
}
