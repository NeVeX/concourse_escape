//============================================================================
// control/client/misc/transfer.cs
//
// Copyright (c) 2003 Kenneth C. Finney
//============================================================================

//----------------------------------------------------------------------------
// Mission Loading & Mission Info
// The mission loading server handshaking is handled by the
// common/client/missingLoading.cs.  This portion handles the interface
// with the game GUI.
//----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Loading Phases:
// Phase 1: Download Datablocks
// Phase 2: Download Ghost Objects
// Phase 3: Scene Lighting

//----------------------------------------------------------------------------
// Phase 1
//----------------------------------------------------------------------------

function onMissionDownloadPhase1(%missionName, %musicTrack)
{
   // Reset the loading progress controls:
   LoadingProgress.setValue(0);
   LoadingProgressTxt.setValue("LOADING DATABLOCKS");
}

function onPhase1Progress(%progress)
{
   LoadingProgress.setValue(%progress);
   Canvas.repaint();
}

function onPhase1Complete()
{
}

//----------------------------------------------------------------------------
// Phase 2
//----------------------------------------------------------------------------

function onMissionDownloadPhase2()
{
   // Reset the loading progress controls:
   LoadingProgress.setValue(0);
   LoadingProgressTxt.setValue("LOADING OBJECTS");
   Canvas.repaint();
}

function onPhase2Progress(%progress)
{
   LoadingProgress.setValue(%progress);
   Canvas.repaint();
}

function onPhase2Complete()
{
}

function onFileChunkReceived(%fileName, %ofs, %size)
{
   LoadingProgress.setValue(%ofs / %size);
   LoadingProgressTxt.setValue("Downloading " @ %fileName @ "...");
}

//----------------------------------------------------------------------------
// Phase 3
//----------------------------------------------------------------------------

function onMissionDownloadPhase3()
{
   LoadingProgress.setValue(0);
   LoadingProgressTxt.setValue("LIGHTING MISSION");
   Canvas.repaint();
}

function onPhase3Progress(%progress)
{
   LoadingProgress.setValue(%progress);
}

function onPhase3Complete()
{
   LoadingProgress.setValue( 1 );
   $lightingMission = false;
}

//----------------------------------------------------------------------------
// Mission loading done!
//----------------------------------------------------------------------------

function onMissionDownloadComplete()
{
   // Client will shortly be dropped into the game, so this is
   // good place for any last minute gui cleanup.
}


//------------------------------------------------------------------------------
// Before downloading a mission, the server transmits the mission
// information through these messages.
//------------------------------------------------------------------------------

addMessageCallback( 'MsgLoadInfo', handleLoadInfoMessage );
addMessageCallback( 'MsgLoadDescripition', handleLoadDescriptionMessage );
addMessageCallback( 'MsgLoadInfoDone', handleLoadInfoDoneMessage );

//------------------------------------------------------------------------------

function handleLoadInfoMessage( %msgType, %msgString, %mapName ) {

	// Need to pop up the loading gui to display this stuff.
	Canvas.setContent("LoadScreen");

	// Clear all of the loading info lines:
	for( %line = 0; %line < LoadScreen.qLineCount; %line++ )
		LoadScreen.qLine[%line] = "";
	LoadScreen.qLineCount = 0;

   //
	LOAD_MapName.setText( %mapName );
}

//------------------------------------------------------------------------------

function handleLoadDescriptionMessage( %msgType, %msgString, %line )
{
	LoadScreen.qLine[LoadScreen.qLineCount] = %line;
	LoadScreen.qLineCount++;

   // Gather up all the previous lines, append the current one
   // and stuff it into the control
	%text = "<spush><font:Arial:16>";

	for( %line = 0; %line < LoadScreen.qLineCount - 1; %line++ )
		%text = %text @ LoadScreen.qLine[%line] @ " ";
   %text = %text @ LoadScreen.qLine[%line] @ "<spop>";

	LOAD_MapDescription.setText( %text );
}

//------------------------------------------------------------------------------

function handleLoadInfoDoneMessage( %msgType, %msgString )
{
   // This will get called after the last description line is sent.
}
