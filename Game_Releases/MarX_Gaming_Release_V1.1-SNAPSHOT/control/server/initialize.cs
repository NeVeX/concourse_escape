//============================================================================
// control/server/initialize.cs
//
//  server control initialization module for 3DGPAI1 emaga5 sample game
//
//  Copyright (c) 2003 by Kenneth C.  Finney.
//============================================================================




function InitializeServer(%option)
//----------------------------------------------------------------------------
// Prepare some global server information & load the game-specific module
//----------------------------------------------------------------------------
{
   //echo("\n++++++++++++ Initializing module: emaga5 server ++++++++++++");
	 
   // Specify where the mission files are.
   //$Server::MissionFileSpec = "*/missions/flat_surface445.mis";
   
   InitBaseServer(); // basic server features defined in the common modules
	 $Playing = 1;
   // Load up game server support script
	if ( %option == 1) {
   Exec("./server.cs");
   $Picking = 1;
   }
	else if ( %option == 2) {
   Exec("./server_walk.cs");
   $Picking = 0;
	}
	else if ( %option == 3) {
   Exec("./server_survive.cs");
   $Picking = 1;
	}
	LaunchGame(%option);
	$Playing = 1;
}