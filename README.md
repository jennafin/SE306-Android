![](http://jennafin.github.io/SE306-Android/presentations/images/logo-horizontal.png)

SE306-Android
=============

Second group project for SE306 paper - an Android based 'platformer' game.

# Building project with your own keystore

## 1. Setup a Google Play developer account
First you must register for a Google Play developers account and [setup your game] (https://developers.google.com/games/services/console/enabling) in the developers console. You may need to change the package name so it does not clash with the already existing game.

## 2. Creating a keystore

Run the following command in a unix based terminal:  

`keytool -genkey -v -keystore mykey.keystore -alias coffee -keyalg RSA -keysize 2048 -validity 10000`

Replacing “mykey” and “coffee” with your app names/key name.  
Answer the questions about your name etc.  
The .keystore text file is now on your computer.


## 3. Android Setup

To configure your Unity game to run with Google Play Games on Android, first
open the Android SDK manager and verify that you have downloaded the **Google
Play Services** package. The Android SDK manager is usually available in your
SDK installation directory, under the "tools" subdirectory, and is called
**android** (or **android.exe** on Windows). The **Google Play Services**
package is available under the **Extras** folder. If it is not installed
or is out of date, install or update it before proceeding.

Next, set up the path to your Android SDK installation in Unity. This is located in the
preferences menu, under the **External Tools** section. 

Next, configure your game's package name. To do this, click **File | Build Settings**, 
select the **Android** platform and click **Player Settings** to show Unity's 
Player Settings window. In that window, look for the **Bundle Identifier** setting
under **Other Settings**. Enter your package name there (for example
_com.example.my.awesome.game_).

Next, click the **File | Play Games - Android setup** menu item. This will display the Android setup screen, where you must input your Application ID (e.g. 12345678012).

After filling in the application ID, click the **Setup** button.

**Important:** The application ID and package name settings must match exactly
the values you used when setting up your project on the Developer Console.

### Additional instructions on building for Android on Windows

If you are using Windows, you must make sure that your Java SDK installation can be accessed by Unity. To do this:

1. Set the JAVA_HOME environment variable to your Java SDK installation path (for example, `C:\Program Files\Java\jdk1.7.0_45`).
2. Add the Java SDK's `bin` folder to your `PATH` environment variable (for example, `C:\Program Files\Java\jdk1.7.0_45\bin`)
3. Reboot.

**How to edit environment variables:** In Windows 2000/XP/Vista/7, 
right-click **My Computer**, then **Properties**, then go to **Advanced System Properties**
(or **System Properties** and then click the **Advanced** tab), then
click **Environment Variables**. On Windows 8, press **Windows Key + W** and
search for **environment variables**.
For more information, consult the documentation for your version of Windows. 

## Building for Android

To build your game for Android, do as you would normally do in Unity. Select 
**File | Build Settings**, then select the **Android** platform and build. If 
you are signing your APK file, please make sure that you are signing it with the 
correct certificate, that is, the one that corresponds to the SHA1 certificate 
fingerprint you entered in the Developer Console during the setup

A more in depth description can be found [here] (https://github.com/playgameservices/play-games-plugin-for-unity/blob/master/README.md)
