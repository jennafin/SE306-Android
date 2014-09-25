using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;

public class GuiLoginController : MonoBehaviour {
	public string stringToEditUsername = "Username";
	public string stringToEditNewUsername = "Username";
	public string stringToEditEmail = "Email";
	private string stringToEditPassword = "pass";
	private string stringToEditNewPassword = "pass";
	//Make private later after testing
	public bool loginBool = true;
	
	void OnGUI () {
		// Make a background box

		GUI.Box (new Rect (40, 40, (Screen.width - 60), (Screen.height - 60)), "Login Menu");

		if (loginBool) 
		{

			stringToEditUsername = GUI.TextField (new Rect ((Screen.width / 2 - ((Screen.width / 8) / 2)), (Screen.height / 2), (Screen.width / 8), 25), stringToEditUsername, 25);

			stringToEditPassword = GUI.PasswordField (new Rect ((Screen.width / 2 - ((Screen.width / 8) / 2)), ((Screen.height / 2) + 35), (Screen.width / 8), 25), stringToEditPassword, "*" [0], 25);

			// Make the second button.
			if (GUI.Button (new Rect ((Screen.width / 2 - 35), (Screen.height - 100), 70, 30), "Login")) {
					//respond
			}

			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if (GUI.Button (new Rect ((Screen.width / 2 - 35), (Screen.height - 140), 70, 30), "Sign Up")) {
					loginBool = false;
			}
		} else {

			stringToEditNewUsername = GUI.TextField (new Rect ((Screen.width / 2 - ((Screen.width / 8) / 2)), (Screen.height / 2 - 35), (Screen.width / 8), 25), stringToEditNewUsername, 25);
			stringToEditEmail = GUI.TextField (new Rect ((Screen.width / 2 - ((Screen.width / 8) / 2)), (Screen.height / 2), (Screen.width / 8), 25), stringToEditEmail, 25);
			stringToEditNewPassword = GUI.PasswordField (new Rect ((Screen.width / 2 - ((Screen.width / 8) / 2)), ((Screen.height / 2) + 35), (Screen.width / 8), 25), stringToEditPassword, "*" [0], 25);
				
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if (GUI.Button (new Rect ((Screen.width / 2 - 35), (Screen.height - 140), 70, 30), "Sign Up")) {
				var user = new ParseUser()
				{
					Username = stringToEditNewUsername,
					Password = stringToEditNewPassword,
					Email = stringToEditEmail
				};
				
				Task signUpTask = user.SignUpAsync();
			}

			// Make the second button.
			if (GUI.Button (new Rect ((Screen.width / 2 - 35), (Screen.height - 100), 70, 30), "Cancel")) {
				loginBool = true;
			}
		}

	}
}
