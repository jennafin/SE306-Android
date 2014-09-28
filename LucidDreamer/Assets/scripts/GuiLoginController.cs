using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;

public class GuiLoginController : MonoBehaviour {
	public GUISkin customSkin;
	public string stringToEditUsername = "Username";
	public string stringToEditNewUsername = "Username";
	public string stringToEditEmail = "Email";
	private string stringToEditPassword = "pass";
	private string stringToEditNewPassword = "pass";
	private string stringToEditHighScore = "20";
	//Make private later after testing
	public bool loginBool = true;
	
	void OnGUI () {
		GUI.skin = customSkin;
		// Make a background box
		GUI.Box (new Rect (40, 40, (Screen.width - 60), (Screen.height - 60)), "Login Menu");
			
		if (ParseUser.CurrentUser != null) {
			//The following for the logged in page
			GUI.TextArea(new Rect(100,100,Screen.width-100,50), "You are logged in! Eventually customised high scores will be shown here! Wooop!");

			stringToEditHighScore = GUI.TextField (new Rect ((Screen.width / 2 - ((Screen.width / 4) / 2)), (Screen.height / 2), (Screen.width / 4), 45), stringToEditHighScore, 25);
			//Submit high score button
			if (GUI.Button (new Rect ((Screen.width / 2 - 70), (Screen.height/2 + 100), 140, 60), "Submit")) {
				ParseObject gameObject = new ParseObject("GameScore");
				gameObject["user"] = ParseUser.CurrentUser; 
				gameObject["score"] = float.Parse(stringToEditHighScore);
				gameObject.SaveAsync();
			}

			//List high scores
			if (GUI.Button (new Rect ((Screen.width / 2 - 70), (Screen.height - 100), 140, 60), "Scores")) {
				Application.LoadLevel("HighScores");
			}

			//Logout button
			if (GUI.Button (new Rect ((Screen.width / 2 - 70), (Screen.height - 100), 140, 60), "Logout")) {
				ParseUser.LogOut();
				var currentUser = ParseUser.CurrentUser;
			}
		} else 
		{
			if (loginBool) {
				//The following for the login page
				stringToEditUsername = GUI.TextField (new Rect ((Screen.width / 2 - ((Screen.width / 4) / 2)), (Screen.height / 2), (Screen.width / 4), 45), stringToEditUsername, 25);
				
				stringToEditPassword = GUI.PasswordField (new Rect ((Screen.width / 2 - ((Screen.width / 4) / 2)), ((Screen.height / 2) + 55), (Screen.width / 4), 45), stringToEditPassword, "*" [0], 25);


				
				// Make the second button.
				if (GUI.Button (new Rect ((Screen.width / 2 - 70), (Screen.height - 170), 140, 60), "Login")) {
					ParseUser.LogInAsync(stringToEditUsername, stringToEditPassword).ContinueWith(t =>
					{
						if (t.IsFaulted || t.IsCanceled)
						{
							// The login failed. Check the error to see why.
						}
						else
						{
							// Login was successful.
						}
					});
				}
				
				// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
				if (GUI.Button (new Rect ((Screen.width / 2 - 70), (Screen.height - 100), 140, 60), "Sign Up")) {
					loginBool = false;
				}
			} else {
				//The folloowing for a signup page
				
				stringToEditNewUsername = GUI.TextField (new Rect ((Screen.width / 2 - ((Screen.width / 4) / 2)), (Screen.height / 2 - 55), (Screen.width / 4), 45), stringToEditNewUsername, 25);
				stringToEditEmail = GUI.TextField (new Rect ((Screen.width / 2 - ((Screen.width / 4) / 2)), (Screen.height / 2), (Screen.width / 4), 45), stringToEditEmail, 25);
				stringToEditNewPassword = GUI.PasswordField (new Rect ((Screen.width / 2 - ((Screen.width / 4) / 2)), ((Screen.height / 2) + 55), (Screen.width / 4), 45), stringToEditNewPassword, "*" [0], 25);
				
				// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
				if (GUI.Button (new Rect ((Screen.width / 2 - 70), (Screen.height - 170), 140, 60), "Sign Up")) {
					var user = new ParseUser ()
					{
						Username = stringToEditNewUsername,
						Password = stringToEditNewPassword,
						Email = stringToEditEmail
					};
					
					Task signUpTask = user.SignUpAsync ();
				}
				
				// Make the second button.
				if (GUI.Button (new Rect ((Screen.width / 2 - 70), (Screen.height - 100), 140, 60), "Cancel")) {
					loginBool = true;
				}
			} 
		}



	}
}
