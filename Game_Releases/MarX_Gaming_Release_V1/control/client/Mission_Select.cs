

//----------------------------------------
function PlaySolo()
{
   %id = SoloMissionList.getSelectedId();
   %mission = getField(SoloMissionList.getRowTextById(%id), 1);
   
   createServer("SinglePlayer", %mission);
   %conn = new GameConnection(ServerConnection);
   RootGroup.add(ServerConnection);
   %conn.setConnectArgs("Reader");
 //  %conn.setConnectArgs($pref::Player::Name);
 //  %conn.setJoinPassword($Client::Password);
   %conn.connectLocal();
}


//----------------------------------------
function SoloScreen::onWake()
{
   SoloMissionList.clear();
   %i = 0;
   for(%file = findFirstFile($Server::MissionFileSpec);
         %file !$= ""; %file = findNextFile($Server::MissionFileSpec))
      if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
         SoloMissionList.addRow(%i++, getMissionDisplayName(%file) @ "\t" @ %file );
   SoloMissionList.sort(0);
   SoloMissionList.setSelectedRow(0);
   SoloMissionList.scrollVisible(0);
}


//----------------------------------------
function getMissionDisplayName( %missionFile )
{
   %file = new FileObject();

   %MissionInfoObject = "";

   if ( %file.openForRead( %missionFile ) ) {
		%inInfoBlock = false;

		while ( !%file.isEOF() ) {
			%line = %file.readLine();
			%line = trim( %line );

			if( %line $= "new ScriptObject(MissionInfo) {" )
				%inInfoBlock = true;
			else if( %inInfoBlock && %line $= "};" ) {
				%inInfoBlock = false;
				%MissionInfoObject = %MissionInfoObject @ %line;
				break;
			}

			if( %inInfoBlock )
			   %MissionInfoObject = %MissionInfoObject @ %line @ " ";
		}

		%file.close();
	}
	%MissionInfoObject = "%MissionInfoObject = " @ %MissionInfoObject;
	eval( %MissionInfoObject );

   %file.delete();

   if( %MissionInfoObject.name !$= "" ) {
   %MissionInfoObject.name = "mARK";
      return %MissionInfoObject.name;
    }
   else
      return fileBase(%missionFile);
}

