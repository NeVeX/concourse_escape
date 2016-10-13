//============================================================================
// control/players/player.cs
//
//  player definition module for 3DGPAI1 emaga5 sample game
//
//  Copyright (c) 2003 by Kenneth C.  Finney.
//============================================================================

datablock PlayerData(HumanMaleAvatar)
{
   className = MaleAvatar;
   shapeFile = "~/data/models/avatars/human/player.dts";
   emap = true;
   renderFirstPerson = false;
   cameraMaxDist = 3;


   maxdrag = 0.5;
   density = 10;
   maxDamage = 100;  // max damage to have - more is more life
   maxEnergy = 120;

   maxForwardSpeed = 20;
   maxBackwardSpeed = 10;
   maxSideSpeed = 12;

   jumpForce = 100;

   // Controls over slope of runnable/jumpable surfaces
   runSurfaceAngle  = 40;
   jumpSurfaceAngle = 30;

   minJumpSpeed = 20;
   maxJumpSpeed = 30;

   maxInv[Copper] = 9999;
   maxInv[Silver] = 99;
   maxInv[Gold] = 25;

   maxInv[Crossbow] = 1;
   maxInv[CrossbowAmmo] = 4000;
   maxInv[m16] = 1;
   maxInv[m16ammo] = 4000;
   maxInv[TommyGun] = 1;
   maxInv[TommygunAmmo] = 6000;
   
};


//============================================================================
// Avatar Datablock methods
//============================================================================

function MaleAvatar::onAdd(%this,%obj)
//----------------------------------------------------------------------------
//
//----------------------------------------------------------------------------
{
   %obj.mountVehicle = false;

   // Default dynamic Avatar stats
   %obj.setRechargeRate(0.01);
   %obj.setRepairRate(%this.repairRate);
}

function MaleAvatar::onRemove(%this, %obj)
//----------------------------------------------------------------------------
//
//----------------------------------------------------------------------------
{
   %client = %obj.client;
   if (%client.player == %obj)
   {
      %client.player = 0;
   }
}

function MaleAvatar::onNewDataBlock(%this,%obj)
//----------------------------------------------------------------------------
//
//----------------------------------------------------------------------------
{
}


function MaleAvatar::onCollision(%this,%obj,%col,%vec,%speed)
//----------------------------------------------------------------------------
//
//----------------------------------------------------------------------------
{
  %obj_state = %obj.getState();
  %col_className = %col.getClassName();
  %col_dblock_className = %col.getDataBlock().className;
  %colName = %col.getDataBlock().getName();

  if ( %obj_state $= "Dead" || $Health_Left < 1)
    return;


  if (%col_className $= "Item" || %col_className $= "Weapon" ) // Deal with all items
  {
    %obj.pickup(%col);                     // otherwise, pick the item up
  }
  // Mount vehicles
  %this = %col.getDataBlock();

  %pushForce = %obj.getDataBlock().pushForce;  // Try to push the object away
  if (!%pushForce)
     %pushForce = 20;
  %eye = %obj.getEyeVector();                   // Start with the shape's eye vector...
  %vec = vectorScale(%eye, %pushForce);
  %vec = vectorAdd(%vec,%obj.getVelocity());    // Add the shape's velocity
  %pos = %col.getPosition();                    // then push
  %vec    = getWords(%vec, 0, 1) @ " 0.0";
  %col.applyImpulse(%pos,%vec);
}



//============================================================================
// HumanMaleAvatar (ShapeBase) class methods
//============================================================================

function HumanMaleAvatar::onImpact(%this,%obj,%collidedObject,%vec,%vecLen)
//----------------------------------------------------------------------------
//
//----------------------------------------------------------------------------
{
   %obj.damage(0, VectorAdd(%obj.getPosition(),%vec),
      %vecLen * %this.speedDamageScale, "Impact");
}

function HumanMaleAvatar::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
//----------------------------------------------------------------------------
//
//----------------------------------------------------------------------------
{
   if (%obj.getState() $= "Dead" || $Health_Left < 1) {
      return;
    }
      
   %obj.applyDamage(%damage);
   //echo("************* DAMAGE TAKEN WAS "@%damage);
   %location = "Body";
   //echo("BEFORE HEALTH = "@$Health_Left);
   $Health_Left = $Health_Left - %damage;
   //echo("AFTER HEALTH = "@$Health_Left);
   Scorebox.SetValue($Health_Left);
   // Deal with client callbacks here because we don't have this
   // information in the onDamage or onDisable methods
   %client = %obj.client;
   %sourceClient = %sourceObject ? %sourceObject.client : 0;

	



   if (%obj.getState() $= "Dead" || $Health_Left < 1)
   {
      %client.onDeath(%sourceObject, %sourceClient, %damageType, %location);
      Scorebox.SetValue(0);
   }
   else { 	// not dead so play a random pain sound
   	if ( $Playing == 1 ) {
	   	if ( $Duke_Pain % 7 == 0 ) {
	   		PlayMusic(Duke_Pain1);
	   	}
	   	else if ( $Duke_Pain % 7 == 1 ) {
	   		PlayMusic(Duke_Pain2);
	   	}
	   	else if ( $Duke_Pain % 7 == 2 ) {
	   		PlayMusic(Duke_Pain3);
	   	}
	   	else if ( $Duke_Pain % 7 == 3 ) {
	   		PlayMusic(Duke_Pain4);
	   	}
	   	else if ( $Duke_Pain % 7 == 4 ) {
	   		PlayMusic(Duke_Pain5);
	   	}
	   	$Duke_Pain++;
	  }
	}
}

function HumanMaleAvatar::onDamage(%this, %obj, %delta)
//----------------------------------------------------------------------------
//
//----------------------------------------------------------------------------
{
   // This method is invoked by the ShapeBase code whenever the
   // object's damage level changes.
   if (%delta > 0 && %obj.getState() !$= "Dead" && $Health_Left > 0)
   {
      // Increment the flash based on the amount.
      %flash = %obj.getDamageFlash() + ((%delta / %this.maxDamage) * 2);
      if (%flash > 0.75)
         %flash = 0.75;

      if (%flash > 0.001)
      {
        %obj.setDamageFlash(%flash);
      }
      %obj.setRechargeRate(0.01);
      %obj.setRepairRate(0.01);
   }
}

function HumanMaleAvatar::onDisabled(%this,%obj,%state)
//----------------------------------------------------------------------------
//
//----------------------------------------------------------------------------
{
   // The player object sets the "disabled" state when damage exceeds
   // it's maxDamage value.  This is method is invoked by ShapeBase
   // state mangement code.

   // If we want to deal with the damage information that actually
   // caused this death, then we would have to move this code into
   // the script "damage" method.
   %obj.clearDamageDt();

   %obj.setRechargeRate(0);
   %obj.setRepairRate(0);

   %obj.setImageTrigger(0,false);
	 PlayMusic(Duke_Death);
	 
   // Schedule corpse removal.
   %obj.schedule(100, "startFade", 100, 0, true);
   %obj.schedule(200, "delete");
   
}

