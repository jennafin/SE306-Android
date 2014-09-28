using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Linq;

public class HighScoresGUIController : MonoBehaviour {


	public GUISkin customSkin;

	private List<ParseObject> highScores = new List<ParseObject>() ;
	// Use this for initialization
	void Start () {
		var query = ParseObject.GetQuery("GameScore")
			.WhereEqualTo("user", ParseUser.CurrentUser);
		query.FindAsync().ContinueWith(t =>
		                               {

			highScores = Enumerable.ToList(t.Result);
		});
	}
	
	void OnGUI()
	{
		GUI.skin = customSkin;

		GUI.Box (new Rect (40, 40, (Screen.width - 60), (Screen.height - 60)), "Your High Scores");
		int iter = 0;
		foreach (ParseObject score in highScores)
		{
			GUI.TextArea(new Rect(100,100+iter,100,30), "Score");
			GUI.TextArea(new Rect(200,100+iter,100,30), (score.Get<float>("score")).ToString("0.00"));
			iter += 50;
		}

		//List high scores
		if (GUI.Button (new Rect ((Screen.width / 2 - 70), (Screen.height - 100), 140, 60), "Back")) {
			Application.LoadLevel("Login");
		}

	}
}
