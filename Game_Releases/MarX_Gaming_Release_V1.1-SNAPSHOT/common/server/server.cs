//-----------------------------------------------------------------------------
// Torque Game Engine
//
// Copyright (c) 2001 GarageGames.Com
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------

function portInit(%port)
{
   %failCount = 0;
   while(%failCount < 10 && !SetNetPort(%port)) {
      Echo("Port init failed on port " @ %port @ " trying next port.");
      %port++; %failCount++;
   }
}

function CreateServer(%serverType, %mission)
{
   if (%mission $= "") {
      Error("CreateServer: mission name unspecified");
      return;
   }

   destroyServer();

   //
   $missionSequence = 0;
   $Server::PlayerCount = 0;
   $Server::ServerType = %serverType;

   // Setup for multi-player, the network must have been
   // initialized before now.
   if (%serverType $= "MultiPlayer") {
      Echo("Starting multiplayer mode");

      // Make sure the network port is set to the correct pref.
      portInit($Pref::Server::Port);
      allowConnections(true);

      if ($pref::Net::DisplayOnMaster !$= "Never" )
         schedule(0,0,startHeartbeat);
   }

   // Load the mission
   $ServerGroup = new SimGroup(ServerGroup);
   onServerCreated();
   loadMission(%mission, true);
}


//-----------------------------------------------------------------------------

function destroyServer()
{
   $Server::ServerType = "";
   allowConnections(false);
   stopHeartbeat();
   $missionRunning = false;

   // End any running mission
   endMission();
   onServerDestroyed();

   // Delete all the server objects
   if (IsObject(MissionGroup))
      MissionGroup.delete();
   if (IsObject(MissionCleanup))
      MissionCleanup.delete();
   if (IsObject($ServerGroup))
      $ServerGroup.delete();

   // Delete all the Connections:
   while (ClientGroup.getCount())
   {
      %client = ClientGroup.getObject(0);
      %client.delete();
   }

   $Server::GuidList = "";

   // Delete all the data blocks...
   deleteDataBlocks();


   // Dump anything we're not using
   purgeResources();
}


//--------------------------------------------------------------------------

function resetServerDefaults()
{
   Echo( "Resetting server defaults..." );

   // Override server defaults with prefs:
   Exec( "~/presets.cs" );

   loadMission( $Server::MissionFile );
}


//------------------------------------------------------------------------------
// Guid list maintenance functions:
function addToServerGuidList( %guid )
{
   %count = getFieldCount( $Server::GuidList );
   for ( %i = 0; %i < %count; %i++ )
   {
      if ( getField( $Server::GuidList, %i ) == %guid )
         return;
   }

   $Server::GuidList = $Server::GuidList $= "" ? %guid : $Server::GuidList TAB %guid;
}

function removeFromServerGuidList( %guid )
{
   %count = getFieldCount( $Server::GuidList );
   for ( %i = 0; %i < %count; %i++ )
   {
      if ( getField( $Server::GuidList, %i ) == %guid )
      {
         $Server::GuidList = removeField( $Server::GuidList, %i );
         return;
      }
   }

   // Huh, didn't find it.
}


//-----------------------------------------------------------------------------

function onServerInfoQuery()
{
   // When the server is queried for information, the value
   // of this function is returned as the status field of
   // the query packet.  This information is accessible as
   // the ServerInfo::State variable.
   return "Doing Ok";
}

