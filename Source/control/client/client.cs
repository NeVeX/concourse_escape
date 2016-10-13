//============================================================================
// control/client/client.cs
//
// This module contains client specific code for handling
// the set up and operation of the player's in-game interface.
//
//  Copyright (c) 2003 by Kenneth C.  Finney.
//============================================================================

$PICTURE_SHOW = 1;

function LaunchGame(%mission) {
   createServer("SinglePlayer", "control/data/maps/flat_surface445.mis");
   $Picked_Mission = %mission;
   %conn = new GameConnection(ServerConnection);
   %conn.setConnectArgs("MarX");
   %conn.connectLocal();
}

function What_Mission() {
	Game_Setup($Picked_Mission);
}


function MyEndGame()
{
   destroyServer();
	ShowMenuScreen();
}

function clientCmdStartGame(%seq)
{
}

function clientCmdSyncClock(%time)
// called from within the common code
{
   // Store the base time in the hud control it will automatically increment.
   HudClock.setTime(%time);
}

//============================================================================
// the following functions are called from the client common code modules
// these stubs are added here to prevent warning messages from cluttering
// up the log file.
//============================================================================
function onServerMessage()
{
}
function onMissionDownloadPhase1()
{
}
function onPhase1Progress()
{
}
function onPhase1Complete()
{
}
function onMissionDownloadPhase2()
{
}
function onPhase2Progress()
{
}
function onPhase2Complete()
{
}
function onPhase3Complete()
{
}
function onMissionDownloadComplete()
{
}

function ShowMenuScreen(%which)
{
	  if ( %which == 1 ) {
		 PlayMusic(Music_MainMenu);
		}
		%choose_pic = $PICTURE_SHOW % 4;
		if ( %choose_pic == 0) {
	   Canvas.setContent( MenuScreenMarX );
	  }
	  else if ( %choose_pic == 1) {
	   Canvas.setContent( MenuScreenConcourse );
	  }
	  else if ( %choose_pic == 2) {
	   Canvas.setContent( MenuScreenWalk );
	  }
	  else if ( %choose_pic == 3) {
	   Canvas.setContent( MenuScreenSurvive);
	  }
   	Canvas.setCursor("DefaultCursor");
   	schedule(4500, 0, "Change_Background", %choose_pic);
   $info_screen_showing = 0;
   $Start_Up_Wait = 0;
   $Playing = 0;
   $Menu_Location = 0;

}

function SplashScreenInputCtrl::onInputEvent(%this, %dev, %evt, %make) {
   if(%make && $Start_Up_Wait == 0) {
     ShowMenuScreen();
   }
}


function Change_Background(%next_one) {
	
	if ( $info_screen_showing == 0 && $Playing == 0 && $Menu_Location == 0) {
		%next_pic = (%next_one + 1) % 4;
		if ( %next_pic == 0) {
	   Canvas.setContent( MenuScreenMarX );
	  }
	  else if ( %next_pic == 1) {
	   Canvas.setContent( MenuScreenConcourse );
	  }
	  else if ( %next_pic == 2) {
	   Canvas.setContent( MenuScreenWalk );
	  }
	  else if ( %next_pic == 3) {
	   Canvas.setContent( MenuScreenSurvive);
	  }
		schedule(4500, 0, "Change_Background", %next_pic);
		$PICTURE_SHOW = %next_pic;
	}
}



function changingScreens(%screen) {
	$info_screen_showing = 1;
	$Menu_Location = 1;
	if ( %screen == 1) {
		Canvas.setContent(Mission_Select);
	}
	else if ( %screen == 2) {
		Canvas.setContent(Information);
	}
	else if ( %screen == 25) {
		Canvas.setContent(Elisha);
	}
	else if ( %screen == 100) {
		$Menu_Location = 2;
		Canvas.setContent(ConcourseInfo);
	}
	else if ( %screen == 101) {
		$Menu_Location = 2;
		Canvas.setContent(WalkAroundInfo);
	}
	else if ( %screen == 102) {
		$Menu_Location = 2;
		Canvas.setContent(SurvivalInfo);
	}
	else if ( %screen == 200) {
		$Menu_Location = 2;
		Canvas.setContent(GameSetupInfo);
	}
}




