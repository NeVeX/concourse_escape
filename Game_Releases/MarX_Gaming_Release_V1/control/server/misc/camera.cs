//============================================================================
// control/misc/camera.cs
//
//
//  Copyright (c) 2003 by Kenneth C.  Finney.
//============================================================================

// Global movement speed that affects all cameras.  This should be moved
// into the camera datablock.
$Camera::movementSpeed = 40;


datablock CameraData(Observer)
//-----------------------------------------------------------------------------
// Defining a datablock class for an observer camera
//-----------------------------------------------------------------------------
{
   mode = "Observer";
};


function Observer::onTrigger(%this,%obj,%trigger,%state)
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   // state = 0 means that a trigger key was released
   if (%state == 0)
      return;

   // Default player triggers: 0=fire 1=altFire 2=jump
   %client = %obj.getControllingClient();
   switch$ (%obj.mode)
   {
      case "Observer":
         // Do something interesting.

      case "Death":
         // Viewing dead avatar. The player may want to respawn.
         %client.spawnPlayer();

         // Set the camera back into observer mode, since in
         // debug mode we like to switch to it.
         %this.setMode(%obj,"Observer");
   }
}

function Observer::setMode(%this,%obj,%mode,%arg1,%arg2,%arg3)
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
	
   switch$ (%mode)
   {
      case "Observer":
         // Let the player fly around
         %obj.setFlyMode();

      case "Death":
         // Lock the camera down in orbit around the corpse,
         // which should be arg1
         %transform = %arg1.getTransform();
         %obj.setOrbitMode(%arg1, %transform, 0.5, 4.5, 4.5);

   }
   %obj.mode = %mode;
}


//=============================================================================
// Camera methods
//=============================================================================


function Camera::onAdd(%this,%obj)
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   // Default start mode
   %this.setMode(%this.mode);
}

function Camera::setMode(%this,%mode,%arg1,%arg2,%arg3)
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
{
   // Punt this one over to our datablock
   %this.getDatablock().setMode(%this,%mode,%arg1,%arg2,%arg3);
}
