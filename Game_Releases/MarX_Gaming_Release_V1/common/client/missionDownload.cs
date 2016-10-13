//-----------------------------------------------------------------------------
// Torque Game Engine
//
// Copyright (c) 2001 GarageGames.Com
//-----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Mission Loading
// Server download handshaking.  This produces a number of onPhaseX
// calls so the game scripts can update the game's GUI.
//
// Loading Phases:
// Phase 1: Download Datablocks
// Phase 2: Download Ghost Objects
// Phase 3: Scene Lighting
//----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Phase 1
//----------------------------------------------------------------------------

function clientCmdMissiOnStartPhase1(%seq, %missionName, %musicTrack)
{
   // These need to come after the cls.
   Echo ("*** MarX Gaming Mission Load: " @ %missionName);
   Echo ("*** Phase 1: Download Datablocks & Targets");
   onMissionDownloadPhase1(%missionName, %musicTrack);
   commandToServer('MissionStartPhase1Ack', %seq);
}

function onDataBlockObjectReceived(%index, %total)
{
   onPhase1Progress(%index / %total);
}

//----------------------------------------------------------------------------
// Phase 2
//----------------------------------------------------------------------------

function clientCmdMissiOnStartPhase2(%seq,%missionName)
{
   onPhase1Complete();
   Echo ("*** Phase 2: Download Ghost Objects");
   purgeResources();
   onMissionDownloadPhase2(%missionName);
   commandToServer('MissionStartPhase2Ack', %seq);
}

function onGhostAlwaysStarted(%ghostCount)
{
   $ghostCount = %ghostCount;
   $ghostsRecvd = 0;
}

function onGhostAlwaysObjectReceived()
{
   $ghostsRecvd++;
   onPhase2Progress($ghostsRecvd / $ghostCount);
}

//----------------------------------------------------------------------------
// Phase 3
//----------------------------------------------------------------------------

function clientCmdMissiOnStartPhase3(%seq,%missionName)
{
   onPhase2Complete();
   StartClientReplication();
   StartFoliageReplication();
   Echo ("*** Phase 3: Mission Lighting");
   $MSeq = %seq;
   $Client::MissionFile = %missionName;

   // Need to light the mission before we are ready.
   // The sceneLightingComplete function will complete the handshake
   // once the scene lighting is done.
   if (lightScene("sceneLightingComplete", ""))
   {
      Error("Lighting mission....");
      schedule(1, 0, "updateLightingProgress");
      onMissionDownloadPhase3(%missionName);
      $lightingMission = true;
   }
}

function updateLightingProgress()
{
   onPhase3Progress($SceneLighting::lightingProgress);
   if ($lightingMission)
      $lightingProgressThread = schedule(1, 0, "updateLightingProgress");
}

function sceneLightingComplete()
{
   Echo("Mission lighting done");
   onPhase3Complete();

   // The is also the end of the mission load cycle.
   onMissionDownloadComplete();
   commandToServer('MissionStartPhase3Ack', $MSeq);
}

