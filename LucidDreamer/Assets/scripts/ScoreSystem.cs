using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ScoreSystem : MonoBehaviour {

	public int currentScorePoints;
	public int currentMultiplier =1;

	private int pointsToBeAdded = 0;

	// Use this for initialization
	void Start () {
		currentScorePoints = 0;
	}

	public void ResetScore()
	{
		currentScorePoints = 0;
	}

	public void AddPoints(int addMe)
	{
		pointsToBeAdded = pointsToBeAdded + addMe;
	}

	public void AddMultiplier(int multi, int time)
	{
		currentMultiplier = currentMultiplier * multi;
	}

	public void ResetMultiplier()
	{
		currentMultiplier = 1;
	}

	public int UpdateScore(int distance)
	{
		//TODO: have a timer for multiplier to stop
		currentScorePoints = currentScorePoints + (pointsToBeAdded * currentMultiplier);
		return distance + currentScorePoints;
	}

	public int GetScore()
	{
		return currentScorePoints;
	}

	public int gameOver()
	{
		int finalScore = currentScorePoints + (pointsToBeAdded * currentMultiplier);
		Social.ReportScore (finalScore, "CgkIj8PyxKwKEAIQAQ", (bool success) => {
			//TODO: handle success or failure
		});

	}
}
