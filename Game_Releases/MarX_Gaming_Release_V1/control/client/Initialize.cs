//============================================================================
// control/client/initialize.cs
//
//  client control initialization module for 3DGPAI1 emaga5 sample game
//
//  Copyright (c) 2003 by Kenneth C.  Finney.
//============================================================================

$Which_Game = 0;

function InitializeClient()
//----------------------------------------------------------------------------
// Prepare some global client information, fire up the graphics engine,
// and then connect to the server code that is already running in another
// thread.
//----------------------------------------------------------------------------
{
   
	 $pref::Video::fullScreen = 1;
   InitBaseClient(); // basic client features defined in the common modules
	
   // Make sure a canvas has been built before any interface definitions are
   // loaded because most controls depend on the canvas to already exist when
   // they are loaded.

   InitCanvas("MarX Gaming - The Ultimate Experience."); // Start the graphics system.
   Exec("./misc/sound_profile.cs");
   Exec("./interfaces/splashscreen.gui");
   // Interface definitions
   Exec("./profiles.cs");
   Exec("./default_profiles.cs");
   
   Exec("./interfaces/MenuScreen.gui");
   Exec("./interfaces/loadscreen.gui");
   Exec("./interfaces/Information.gui");
   Exec("./interfaces/Elisha.gui");
   Exec("./interfaces/playerinterface.gui");
   Exec("./interfaces/Missions_Selecting.gui");
   Exec("./interfaces/GameSetupInfo.gui");
   Exec("./interfaces/ConcourseInfo.gui");
   Exec("./interfaces/WalkAroundInfo.gui");
   Exec("./interfaces/SurvivalInfo.gui");

   // Interface scripts
   Exec("./misc/Screens.cs");
   Exec("./misc/presetkeys.cs");
   Exec("./client.cs");

   
   
   // these modules rely on things defined in the common code
   // that are activated in the InitBaseClient() function above
   // so must be located after it has been called
   Exec("./misc/transfer.cs");
   Exec("./misc/connection.cs");
   //Canvas.setContent(LoadingStartScreen);

   Canvas.setContent(SplashScreen);
   schedule(1100, 0, "PlayMusic", Viewer_Discretion);
   schedule(5400, 0, "showWarning");
   $Start_Up_Wait = 1; // use this in the final game
   //$Start_Up_Wait = 0;
   
   
}

function showWarning() {
	Canvas.setContent(WarningScreen);
	schedule(5200, 0, "ShowMenuScreen",1);
}
