//============================================================================
// control/server/server.cs
//
//  server-side game specific module for 3DGPAI1 emaga5 sample game
//  provides client connection management and and player/avatar spawning
//
//  Copyright (c) 2003 by Kenneth C.  Finney.
//============================================================================

//============================================================================
// GameConnection Methods
// Extensions to the GameConnection class. Here we add some methods
// to handle player spawning and creation.
//============================================================================
$Human = 0;

function OnServerCreated()
//----------------------------------------------------------------------------
// Once the engine has fired up the server, this function is called
//----------------------------------------------------------------------------
{
	 //Exec("./misc/sound_profile.cs"); // dont need this yet.
	 Exec("./doorsInit.cs");
   Exec("./misc/camera.cs");
   //Exec("./misc/server_audio.cs");
   //Exec("./misc/shapeBase.cs");
   Exec("./misc/item.cs");
   Exec("./weapons/weapon.cs");
   Exec("./tommygun.cs");
   Exec("./M16.cs");
   //Exec("./MP5.cs");
   Exec("./weapons/crossbow.cs");
   Exec("./weather.cs");
   Exec("./players/player.cs"); // Load the player datablocks and methods
   //Exec("./players/AI_Player.cs"); // Load the player datablocks and methods   
   //Exec("./players/beast.cs"); // Load the player datablocks and methods
   //Exec("./players/goblin.cs");
	
   
}
function Can_Speak() {
	$Allowed_to_Speak++;
	//echo(" ******** the duke can talk **********");
}

function startGame() {
		alxStopAll();
		PlayMusic(WalkAround);
		$Picking = 0;
		$ReSpawn = "27 15 70.5 1 0 0 0";
		$Which_Game++;
		$Allowed_to_Speak = 1;
		$Tokens_Collected = 22; // need 21 to leave the concourse
		$ai_players = 0;
		Livesbox.SetValue(0);
		Coinbox.SetValue(0);
		Healthbox.SetValue(0);
		Scorebox.SetValue(0);
		gunbox.SetValue(0);
		ammobox.SetValue(0);
	   if ($Game::Duration) // Start the game timer
	      $Game::Schedule = schedule($Game::Duration * 1000, 0, "onGameDurationEnd" );
	   $Game::Running = true; 
	   
}

function onMissionLoaded()
{
   // Called by loadMission() once the mission is finished loading.
   // Nothing special for now, just start up the game play.
   //StopMusic(Music_MainMenu);
   //alxStopAll();
   //PlayMusic(MarX_Pro);
   startGame();
   //PlayMusic(In_Game_Music);
}

function onMissionEnded()
{
   cancel($Game::Schedule);
   $Game::Running = false;
   alxStopAll();
   $info_screen_showing = 0;
   $Start_Up_Wait = 0;
   $Playing = 0;
   $Menu_Location = 0;
   ShowMenuScreen(1);
   

}

function GameConnection::OnClientEnterGame(%this)
//----------------------------------------------------------------------------
// Called when the client has been accepted into the game by the server.
//----------------------------------------------------------------------------
{
   // Create a new camera object.
   %this.camera = new Camera() {
      dataBlock = Observer;
   };
   MissionCleanup.add( %this.camera );
   %this.camera.scopeToClient(%this);
	
   // Create a player object.
   %this.spawnPlayer();


}

function GameConnection::SpawnPlayer(%this)
//----------------------------------------------------------------------------
// This is where we place the player spawn decision code.
// It might also call a function that would figure out the spawn
// point transforms by looking up spawn markers.
// Once we know where the player will spawn, then we create the avatar.
//----------------------------------------------------------------------------
{

  //%this.createPlayer("0 0 72 1 0 0 0");
  %this.createPlayer(""@$ReSpawn); // spawn and look down the concourse

}

function GameConnection::CreatePlayer(%this, %spawnPoint)
//----------------------------------------------------------------------------
// Create the player's avatar object, set it up, and give the player control
// of it.
//----------------------------------------------------------------------------
{
	

		   // Create the player object
		%player = new Player() {
		    dataBlock = HumanMaleAvatar;   // defined in players/player.cs
		    client = %this;           // the avatar will have a pointer to its
		 }; 
	   // Player setup...
	   %player.setTransform(%spawnPoint); // where to put it
	   // Update the camera to start with the player
	   //%this.camera.setTransform(290 25.2041 71.1874 7.27498e-005 7.04974e-005 -1 1.53935);
	   %this.camera.setTransform(%player.getEyeTransform());
	   // Give the client control of the player
	   %this.player = %player;
	   %this.setControlObject(%player);
		
}

function GameConnection::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc)
{

   // Switch the client over to the death cam and unhook the player object.
   if (IsObject(%this.camera) && IsObject(%this.player))
   {
      %this.camera.setMode("Death",%this.player);
      %this.setControlObject(%this.camera);
   }
   %this.player = 0;

   // Doll out points and display an appropriate message
   if (%damageType $= "Suicide" || %sourceClient == %this)
   {

   }
   else
   {
   }
}

function serverCmdToggleCamera(%client)
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   %co = %client.getControlObject();
   if (%co == %client.player)
   {
      %co = %client.camera;
      %co.mode = toggleCameraFly;
   }
   else
   {
      %co = %client.player;
      %co.mode = observerFly;
   }
   %client.setControlObject(%co);
}

function serverCmdDropPlayerAtCamera(%client)
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   if ($Server::DevMode || IsObject(EditorGui))
   {
      %client.player.setTransform(%client.camera.getTransform());
      %client.player.setVelocity("0 0 0");
      %client.setControlObject(%client.player);
   }
}

function serverCmdDropCameraAtPlayer(%client)
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   %client.camera.setTransform(%client.player.getEyeTransform());
   %client.camera.setVelocity("0 0 0");
   %client.setControlObject(%client.camera);
}


function serverCmdUse(%client,%data)
//-----------------------------------------------------------------------------
// Server Item Use
//-----------------------------------------------------------------------------
{
   %client.getControlObject().use(%data);
}

function centerPrintAll( %message, %time, %lines )
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   if( %lines $= "" || ((%lines > 3) || (%lines < 1)) )
      %lines = 1;

   %count = ClientGroup.getCount();
   for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
      if( !%cl.isAIControlled() )
         commandToClient( %cl, 'centerPrint', %message, %time, %lines );
   }
}

function bottomPrintAll( %message, %time, %lines )
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   if( %lines $= "" || ((%lines > 3) || (%lines < 1)) )
      %lines = 1;

   %count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
      if( !%cl.isAIControlled() )
         commandToClient( %cl, 'bottomPrint', %message, %time, %lines );
   }
}

//-------------------------------------------------------------------------------------------------------

function centerPrint( %client, %message, %time, %lines )
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   if( %lines $= "" || ((%lines > 3) || (%lines < 1)) )
      %lines = 1;


   commandToClient( %client, 'CenterPrint', %message, %time, %lines );
}

function bottomPrint( %client, %message, %time, %lines )
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   if( %lines $= "" || ((%lines > 3) || (%lines < 1)) )
      %lines = 1;

   commandToClient( %client, 'BottomPrint', %message, %time, %lines );
}

//-------------------------------------------------------------------------------------------------------

function clearCenterPrint( %client )
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   commandToClient( %client, 'ClearCenterPrint');
}

function clearBottomPrint( %client )
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   commandToClient( %client, 'ClearBottomPrint');
}

//-------------------------------------------------------------------------------------------------------

function clearCenterPrintAll()
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
	%count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
      if( !%cl.isAIControlled() )
         commandToClient( %cl, 'ClearCenterPrint');
   }
}

function clearBottomPrintAll()
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
	%count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
      if( !%cl.isAIControlled() )
         commandToClient( %cl, 'ClearBottomPrint');
   }
}


