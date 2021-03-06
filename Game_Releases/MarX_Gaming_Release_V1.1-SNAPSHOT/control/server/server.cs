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
$Enemy = 0;
function info(%show, %needed) { 
	if ( %show == 1) {
		// not enough tokens
		if ( %needed == 1) {
			Infobox.SetValue("You Need 1 Token To Open this Door");
		}
		else {
			Infobox.SetValue("You Need "@%needed@" Tokens To Open this Door");
		}
		PlayMusic(Duke_Door_Closed);
	}
	else if ( %show == 0 ) {
		// take the text off the screen
		Infobox.SetValue("");
	}
}
function OnServerCreated()
//----------------------------------------------------------------------------
// Once the engine has fired up the server, this function is called
//----------------------------------------------------------------------------
{
	 //Exec("./misc/sound_profile.cs"); // dont need this yet.
	 Exec("./doorsInit.cs");
   Exec("./misc/camera.cs");
   //Exec("./misc/server_audio.cs");
   Exec("./misc/shapeBase.cs");
   Exec("./misc/item.cs");
   Exec("./weapons/weapon.cs");
   Exec("./tommygun.cs");
   Exec("./M16.cs");
   //Exec("./MP5.cs");
   Exec("./weapons/crossbow.cs");
   Exec("./weather.cs");
   Exec("./players/player.cs"); // Load the player datablocks and methods
   Exec("./players/AI_Player.cs"); // Load the player datablocks and methods   
   Exec("./players/beast.cs"); // Load the player datablocks and methods
   Exec("./players/goblin.cs");
   Exec("./Spawning.cs");

   //Exec("./players/skeleton.cs");
   //Exec("./players/ai_goblin.cs"); // i dont need this anymore
   
}
function Can_Speak() {
	$Allowed_to_Speak++;
	//echo(" ******** the duke can talk **********");
}

function startGame() {
		$Lives_Left = 11;
		$Enter_Game = 1;
		$Picking = 1;
		$ReSpawn = "290 25.2 68.99 7.27498e-005 7.04974e-005 -1 1.53935";
		$Allowed_to_Speak = 0;
		$SpawningTriggerCount = 0;
		//$SpawningTriggerCount = 5;
		$duke_sound = 0;
		$YSpawnSpread = 3;
		$XSpawnSpread = 0;
		$Duke_Spawn = getRandom(7);
		$Duke_Pain = getRandom(5);
		$Tokens_Collected = 0; // need 21 to leave the concourse
		$Which_Game++;
		Livesbox.SetValue($Lives_Left);
		Coinbox.SetValue($Tokens_Collected);
	   if ($Game::Duration) // Start the game timer
	      $Game::Schedule = schedule($Game::Duration * 1000, 0, "onGameDurationEnd" );
	   $Game::Running = true;
	   $ai_players = 0;
}

function onMissionLoaded()
{
   // Called by loadMission() once the mission is finished loading.
   // Nothing special for now, just start up the game play.
   //StopMusic(Music_MainMenu);
   alxStopAll();
   //PlayMusic(MarX_Pro);
   schedule(1200, 0, "startGame");
   	PlayMusic(In_Game_Music);
   	$TommyGun_inventory = 0;
	 	$CrossBow_inventory = 0;
}

function onMissionEnded()
{
	
   cancel($Game::Schedule);
   $Game::Running = false;
   endGame();
   //alxStop($in_game_music_handle);
   alxStopAll();
   $info_screen_showing = 0;
   $Start_Up_Wait = 0;
   $Playing = 0;
   $Menu_Location = 0;
   ShowMenuScreen(1);
   %looped = 0;
   while ( %looped < $Enemy ) {
   	//echo("deleteing all enemies");
   	$Enemy_Array[%looped].schedule(10, "delete");
		%looped++;
	}

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

   schedule(800, 0, "Can_Speak");
   schedule(300, 0, "SpawnTriggers");

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
	  if ( $Enter_Game == 1 ) {
	  	PlayMusic(Duke_Lets_Rock);
	  	$Enter_Game = 2;
	  	}
	 	else {
	 		PlayMusic(Duke_Kick_Ass);
	 	}

}

function GameConnection::CreatePlayer(%this, %spawnPoint)
//----------------------------------------------------------------------------
// Create the player's avatar object, set it up, and give the player control
// of it.
//----------------------------------------------------------------------------
{
	

	
	   if (%this.player > 0)//The player should NOT already have an avatar object.
	   {                     // if he does, that's a Bad Thing.
	      Error( "Attempting to create an angus ghost!" );
	   }
	   if ( $Human > 200 ) {	// fool proof to kill the ghost in the game world and stop errors.
	   	$Human.schedule(10, "delete");
	   	//echo(" %%%%% i have deleted myself ");
	  }
	   $Lives_Left = $Lives_Left - 1;
		 Livesbox.SetValue($Lives_Left);
	   if ( $Lives_Left > 0 ) {
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
	   %player.setEnergyLevel(100);
	   $Health_Left = 100;
	   Scorebox.SetValue($Health_Left);
	  //echo("debug 1");
	   %weapon = new Item() {
	      dataBlock = M16;
	   };
	   //echo("debug 2");
	   %ammo = new Item() {
	      dataBlock = M16ammo;
	   };
	   //echo("debug 3");
	   %player.pickup(%weapon, 1);
	   %player.pickup(%ammo, 2);
	   
	   if ( $CrossBow_inventory == 1 ) {
			%weapon = new Item() {
	      dataBlock = Crossbow;
	   	};
	   	%ammo = new Item() {
	      dataBlock = CrossbowAmmo;
	   	};
	   	%player.pickup(%weapon, 3);
	   	%player.pickup(%ammo, 4);
	  	}
	   if ( $TommyGun_inventory == 1 ) {
	     %weapon = new Item() {
	      dataBlock = TommyGun;
	   		};
	   	%ammo = new Item() {
	      dataBlock = TommyGunAmmo;
	   	};
	   	%player.pickup(%weapon, 5);
	   	%player.pickup(%ammo, 6);
	  	}
	  	
	   Gunbox.SetValue(M16);	// SET THIS TO WHATEVER IS IN SLOT 1 OF THE WEAPONS
	   Ammobox.SetValue(200); // SET THIS TO WHATEVER IS IN SLOT 1 OF THE WEAPONS to the max it gives in ammo when mounted
	   // Give the client control of the player
	   %this.player = %player;
	   $Human = %player;
	   %this.setControlObject(%player);
	   //echo("**************** I SPAWNED INTO HERE  "@$Human.getPosition());
	   }                          // owner's connection
   else {
   	// go back to the menu screen and delete current game	
   	onMissionEnded();
  	}
		
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

function checkme() {
	
	echo("human position is 1- "@getField($Human.getPosition(), 1));
	
	echo("human position 2 is - "@getField($Human.getPosition(), 2));
	echo("human position  3 is - "@getField($Human.getPosition(), 3));
	%quick = $Human.getPosition();
	echo("human position + 5 is - "@getField(%quick),3);
	
	%this_one = getField(%quick, 1);
	
	echo("human position + 6 is - "@%this_one);
	
	%yer = getField(""@$Human.getPosition(), 3);
	
	echo("yer is "@%yer);
	
	getWords($Human.getPosition(),0,0); // gets x
	getWords($Human.getPosition(),1,1); // gets y
	getWords($Human.getPosition(),2,2); // gets Z
}
