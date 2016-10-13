
//------------------------------------------------------------------------
// control/main.cs
//  main control module for 3DGPAI1 emaga chapter4 sample game
//
//  Copyright (c) 2003 by Kenneth C.  Finney.
//------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
// Load up defaults console values.


// Pre-determined setting variables
Exec("./client/presets.cs");
Exec("./server/presets.cs");


// Defaults console values

//-----------------------------------------------------------------------------
// Package overrides to initialize the mod.
package control {

function OnStart()
//------------------------------------------------------------------------
// Called by root main when package is loaded
//------------------------------------------------------------------------
{
   Parent::OnStart();
   Echo("\n++++++++++++ Initializing control module ++++++++++++");

   // The following scripts contain the preparation code for
   // both the client and server code. A client can also host
   // games, so they need to be able to act as servers if the
   // user wants to host a game. That means we always prepare
   // to be a server at anytime, unless we are launched as a
   // dedicated server.
   Exec("./client/initialize.cs");
   Exec("./server/initialize.cs");
   echo(" this is "@$Picked_Mission);
   InitializeClient(); // Prepare the client-specific aspects
    // Prepare the server-specific aspects

}

function OnExit()
//------------------------------------------------------------------------
// Called by root main when package is unloaded
//------------------------------------------------------------------------
{

   Parent::onExit();
}

}; // Client package

ActivatePackage(control); // Tell TGE to make the client package active
