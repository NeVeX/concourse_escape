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
   maxDamage = 1;
   maxEnergy =  1000000000;

   //runForce = 100;
   maxForwardSpeed = 20;
   maxBackwardSpeed = 10;
   maxSideSpeed = 12;

   jumpForce = 100;

   // Controls over slope of runnable/jumpable surfaces
   runSurfaceAngle  = 40;
   jumpSurfaceAngle = 30;

   minJumpSpeed = 20;
   maxJumpSpeed = 30;

   
   //drag = 0.1;
   //maxdrag = 0.5;
   //maxDamage = 100;
   //maxEnergy =  100;
   //maxForwardSpeed = 15;
   //maxBackwardSpeed = 10;
   //maxSideSpeed = 12;
   //minJumpSpeed = 20;
   //maxJumpSpeed = 30;
   //runForce = 1000;
   //jumpForce = 1000;
   //runSurfaceAngle  = 40;
   //jumpSurfaceAngle = 30;

   //runEnergyDrain = 0.05;
   //minRunEnergy = 1;
   //jumpEnergyDrain = 20;
   //minJumpEnergy = 20;
   //recoverDelay = 30;
   //recoverRunForceScale = 1.2;
   //minImpactSpeed = 15;
   //speedDamageScale = 3.0;
   //repairRate = 0.1;

   maxInv[Copper] = 9999;
   maxInv[Silver] = 99;
   maxInv[Gold] = 9;

   maxInv[Crossbow] = 1;
   maxInv[CrossbowAmmo] = 500;
   maxInv[m16] = 1;
   maxInv[m16ammo] = 500;
   
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

  if ( %obj_state $= "Dead")
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
   if (%obj.getState() $= "Dead")
      return;
   %obj.applyDamage(%damage);
   %location = "Body";

   // Deal with client callbacks here because we don't have this
   // information in the onDamage or onDisable methods
   %client = %obj.client;
   %sourceClient = %sourceObject ? %sourceObject.client : 0;

   if (%obj.getState() $= "Dead")
   {
      %client.onDeath(%sourceObject, %sourceClient, %damageType, %location);
   }
}

function HumanMaleAvatar::onDamage(%this, %obj, %delta)
//----------------------------------------------------------------------------
//
//----------------------------------------------------------------------------
{
   // This method is invoked by the ShapeBase code whenever the
   // object's damage level changes.
   if (%delta > 0 && %obj.getState() !$= "Dead")
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

   // Release the main weapon trigger
   %obj.setImageTrigger(0,false);

   // Schedule corpse removal.
   %obj.schedule(5000, "startFade", 5000, 0, true);
   %obj.schedule(10000, "delete");
}

