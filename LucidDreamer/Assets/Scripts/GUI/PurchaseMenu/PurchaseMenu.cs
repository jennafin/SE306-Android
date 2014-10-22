/*******************************************************************************
 * Copyright 2012-2014 One Platform Foundation
 *
 *       Licensed under the Apache License, Version 2.0 (the "License");
 *       you may not use this file except in compliance with the License.
 *       You may obtain a copy of the License at
 *
 *           http://www.apache.org/licenses/LICENSE-2.0
 *
 *       Unless required by applicable law or agreed to in writing, software
 *       distributed under the License is distributed on an "AS IS" BASIS,
 *       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *       See the License for the specific language governing permissions and
 *       limitations under the License.
 ******************************************************************************/

using UnityEngine;
using OnePF;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


/**
 * Example of OpenIAB usage
 */
[ExecuteInEditMode()]
public class PurchaseMenu : MonoBehaviour
{
    // Style stuff
    public GUIStyle buttonStyle;
    public GUIStyle titleTextStyle;
    private int screenHeight;
    private int screenWidth;
    private int buttonWidth;
    private int buttonHeight;
    
	public Language language = Language.English;

    const string LIVES_4 = "4_lives";
    const string LIVES_5 = "5_lives";

    private string lastAttempedPurchase = "";

    string _label = "";
    bool _isInitialized = false;

    PurchaseManager purchaseManager = new PurchaseManager();

    private void OnEnable()
    {
        // Listen to all events for illustration purposes
        OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
        OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
        OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
        OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
    }
    private void OnDisable()
    {
        // Remove all event handlers
        OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
        OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
        OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
        OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
    }

    private void Start()
    {
        // Map skus for different stores
        OpenIAB.mapSku(LIVES_4, OpenIAB_Android.STORE_GOOGLE, LIVES_4);
        OpenIAB.mapSku(LIVES_5, OpenIAB_Android.STORE_GOOGLE, LIVES_5);

        // Style stuff
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        buttonWidth = screenWidth / 5;
        buttonHeight = screenHeight / 10;

        buttonStyle.fontSize =  screenHeight / 13;
        buttonStyle.alignment = TextAnchor.MiddleCenter;
		
		titleTextStyle.fontSize = (int)(0.16 * screenHeight);
		titleTextStyle.alignment = TextAnchor.MiddleCenter;
        
		// Load language
		if(File.Exists(Application.persistentDataPath + "/language.dat"))
		{
			//Binary formatter for loading back
			BinaryFormatter bf = new BinaryFormatter();
			//Get the file
			FileStream f = File.Open(Application.persistentDataPath + "/language.dat", FileMode.Open);
			//Load the language
			language = (Language)bf.Deserialize(f);
			f.Close();
		}
		LanguageManager.LoadLanguageFile(language);
		
		// Initialise IAP
		// Application public key
		var publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAi+blQ3BwJyNiPj6sVScE8z2kb4SQHr5yQ38fgd8VRpUvCRsiw2ZScKMt2kg5F3/elUvfqdoM9rmEfBAL57hCrSXreAOnYHTh0kSUvsFcj7Ezo6DSlVOVpPKKI+iON36PZslT0VhY61Y894XycdGeA5A3nu0E+EzCAeWhtpxu34z6Ev+HgXXeDSoYOW2sqPPHQ2BZobEm3Qyq2siFt9PmE6O41miVQ/AmfzR9A0cxKrn+JoDhkTIl4MbboM6TWtz8rUGmNH33JUhmfU+uiJ4YWlZZJHUlwNMWUOdcIcFBe2sIUAGsiN7uXv1w5FFL4aVOKCqjtx9rP875TKsIAa/bxwIDAQAB";
		
		var options = new Options();
		options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
		options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
		options.checkInventory = false;
		options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
		options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE, OpenIAB_Android.STORE_AMAZON, OpenIAB_Android.STORE_YANDEX };
		options.storeKeys = new Dictionary<string, string> { {OpenIAB_Android.STORE_GOOGLE, publicKey} };
		
		// Transmit options and start the service
		OpenIAB.init(options);
    }

    const float X_OFFSET = 10.0f;
    const float Y_OFFSET = 10.0f;
    const int SMALL_SCREEN_SIZE = 800;
    const int LARGE_FONT_SIZE = 34;
    const int SMALL_FONT_SIZE = 24;
    const int LARGE_WIDTH = 380;
    const int SMALL_WIDTH = 160;
    const int LARGE_HEIGHT = 100;
    const int SMALL_HEIGHT = 40;

    int _column = 0;
    int _row = 0;

    private bool Button(string text)
    {
        float width = Screen.width / 2.0f - X_OFFSET * 2;
        float height = (Screen.width >= SMALL_SCREEN_SIZE || Screen.height >= SMALL_SCREEN_SIZE) ? LARGE_HEIGHT : SMALL_HEIGHT;

        bool click = GUI.Button(new Rect(
            X_OFFSET + _column * X_OFFSET * 2 + _column * width,
            Y_OFFSET + _row * Y_OFFSET + _row * height,
            width, height),
            text);

        ++_column;
        if (_column > 1)
        {
            _column = 0;
            ++_row;
        }

        return click;
    }

    private void OnGUI()
    {
    
    	// title
		GUI.Label (new Rect (0, screenHeight / 10, screenWidth, 0)
		           , LanguageManager.GetText ("ViewProducts")
		           , titleTextStyle);
    
    	// back button
		if (GUI.Button (new Rect (screenWidth / 2 - buttonWidth/2, 7 * screenHeight / 8, buttonWidth, buttonHeight)
		                , LanguageManager.GetText ("Back")
		                , buttonStyle)) {
			LoadMainMenu();
		}
    		
    		
        _column = 0;
        _row = 0;

        GUI.skin.button.fontSize = (Screen.width >= SMALL_SCREEN_SIZE || Screen.height >= SMALL_SCREEN_SIZE) ? LARGE_FONT_SIZE : SMALL_FONT_SIZE;

        if (!_isInitialized)
            return;

        purchaseManager = new PurchaseManager();
        purchaseManager.Load();
        if (purchaseManager.Get4Lives()) {
          //if (Button(LanguageManager.GetText("Begin5Lives")))
          if (GUI.Button (new Rect ((screenWidth / 2 - screenWidth / 3f), 2.5f * screenHeight / 5, screenWidth / 1.5f, buttonHeight)
                      , LanguageManager.GetText ("Begin5Lives")
                      , buttonStyle))
          {
              purchaseProduct(LIVES_5);
          }
        } else {
          //if (Button(LanguageManager.GetText("Begin4Lives")))
          if (GUI.Button (new Rect ((screenWidth / 2 - screenWidth / 3f), 2.5f * screenHeight / 5, screenWidth / 1.5f, buttonHeight)
                      , LanguageManager.GetText ("Begin4Lives")
                      , buttonStyle))
          {
              purchaseProduct(LIVES_4);
          }
        }
    }

    private void purchaseProduct(string product) {
      OpenIAB.purchaseProduct(product);
      lastAttempedPurchase = product;
    }

    private void purchased(string productId) {
      if (productId != null)
        if (productId.Equals(LIVES_4)) {
          //Enable 4 lives
          PurchaseManager purchaseManager = new PurchaseManager();
          purchaseManager.Load();
          purchaseManager.Set4Lives(true);
          purchaseManager.Save();
        } else if (productId.Equals(LIVES_5)) {
          //Enable 5 lives
          PurchaseManager purchaseManager = new PurchaseManager();
          purchaseManager.Load();
          purchaseManager.Set5Lives(true);
          purchaseManager.Save();
        }
    }

    private void billingSupportedEvent()
    {
        _isInitialized = true;
        Debug.Log("billingSupportedEvent");
    }
    private void billingNotSupportedEvent(string error)
    {
        Debug.Log("billingNotSupportedEvent: " + error);
    }

    private void purchaseSucceededEvent(Purchase purchase)
    {
        Debug.Log("purchaseSucceededEvent: " + purchase);
        _label = "PURCHASED:" + purchase.ToString();

        // Handle product purchase
        purchased(purchase.Sku);
    }

    private void purchaseFailedEvent(int errorCode, string errorMessage)
    {
        Debug.Log("purchaseFailedEvent: " + errorMessage);
        Debug.Log("purchaseFailedEvent error code: " + errorCode);
        _label = "Purchase Failed: " + errorMessage;

        // If the error code is 7 then the user has already purchased the product
        if (errorCode == 7) {
          // Enable product purchase
          purchased(lastAttempedPurchase);
        }

    }

    void Update ()
    {
      // go to main menu on escape/back button
      if (Input.GetKeyDown (KeyCode.Escape)) {
          LoadMainMenu();
      }
    }
    
    private void LoadMainMenu() {
		GameObject.Find ("Main Camera").GetComponent<SceneFader> ().LoadScene("MainMenu");
    }
}
