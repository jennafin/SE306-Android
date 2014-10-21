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

/**
 * Example of OpenIAB usage
 */
public class OpenIABTest : MonoBehaviour
{
    const string LIVES_4 = "4_lives";
    const string LIVES_5 = "5_lives";

    private string lastAttempedPurchase = "";

    string _label = "";
    bool _isInitialized = false;

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
        _column = 0;
        _row = 0;

        GUI.skin.button.fontSize = (Screen.width >= SMALL_SCREEN_SIZE || Screen.height >= SMALL_SCREEN_SIZE) ? LARGE_FONT_SIZE : SMALL_FONT_SIZE;

        if (Button("Initialize OpenIAB"))
        {
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

        if (!_isInitialized)
            return;

        if (Button("Purchase 4 lives"))
        {
            purchaseProduct(LIVES_4);
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
        } else if (productId.Equals(LIVES_5)) {
          //Enable 5 lives
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
}
