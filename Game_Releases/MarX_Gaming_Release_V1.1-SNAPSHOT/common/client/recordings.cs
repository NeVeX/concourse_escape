//-----------------------------------------------------------------------------
// Torque Game Engine
// 
// Copyright (c) 2001 GarageGames.Com
// Portions Copyright (c) 2001 by Sierra Online, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// RecordingsGui is the main TSControl through which the a demo game recording
// is viewed. 
//-----------------------------------------------------------------------------

function recordingsDlg::onWake()
{
   RecordingsDlgList.clear();
   %i = 0;
   %filespec = $currentMod @ "/recordings/*.rec";
   Echo(%filespec);
   for(%file = findFirstFile(%filespec); %file !$= ""; %file = findNextFile(%filespec)) 
   { 
      %fileName = fileBase(%file);
      if (strStr(%file, "/CVS/") == -1) 
      {
         RecordingsDlgList.addRow(%i++, %fileName);
      }
   }
   RecordingsDlgList.sort(0);
   RecordingsDlgList.setSelectedRow(0);
   RecordingsDlgList.scrollVisible(0);
}

function StartSelectedDemo()
{
   // first unit is filename
   %sel = RecordingsDlgList.getSelectedId();
   %rowText = RecordingsDlgList.getRowTextById(%sel);

   %file = $currentMod @ "/recordings/" @ getField(%rowText, 0) @ ".rec";

   new GameConnection(ServerConnection);
   RootGroup.add(ServerConnection);

   if(ServerConnection.playDemo(%file))
   {
      Canvas.popDialog(RecordingsDlg);
      ServerConnection.prepDemoPlayback();
   }
   else 
   {
      MessageBoxOK("Playback Failed", "Demo playback failed for file '" @ %file @ "'.");
      if (IsObject(ServerConnection)) {
         ServerConnection.delete();
      }
   }
}

function startDemoRecord()
{
   // make sure that current recording stream is stopped
   ServerConnection.stopRecording();
   
   // make sure we aren't playing a demo
   if(ServerConnection.isDemoPlaying())
      return;
   
   for(%i = 0; %i < 1000; %i++)
   {
      %num = %i;
      if(%num < 10)
         %num = "0" @ %num;
      if(%num < 100)
         %num = "0" @ %num;

      %file = $currentMod @ "/recordings/demo" @ %num @ ".rec";
      if(!isfile(%file))
         break;
   }
   if(%i == 1000)
      return;

   $DemoFileName = %file;

   ChatBox.AddLine( "\c4Recording to file [\c2" @ $DemoFileName @ "\cr].");

   ServerConnection.prepDemoRecord();
   ServerConnection.startRecording($DemoFileName);

   // make sure start worked
   if(!ServerConnection.isDemoRecording())
   {
      deleteFile($DemoFileName);
      ChatBox.AddLine( "\c3 *** Failed to record to file [\c2" @ $DemoFileName @ "\cr].");
      $DemoFileName = "";
   }
}

function stopDemoRecord()
{
   // make sure we are recording
   if(ServerConnection.isDemoRecording())
   {
      ChatBox.AddLine( "\c4Recording file [\c2" @ $DemoFileName @ "\cr] finished.");
      ServerConnection.stopRecording();
   }
}

function demoPlaybackComplete()
{
   Disconnect();
   Canvas.setContent("MainMenuGui");
   Canvas.pushDialog(RecordingsDlg);
}
