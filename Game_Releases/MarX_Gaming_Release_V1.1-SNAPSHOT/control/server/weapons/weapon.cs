//============================================================================
// control/players/weapon.cs
//
// Copyright (c) 2003 Kenneth C. Finney
// Portions Copyright (c) 2001 GarageGames.Com
// Portions Copyright (c) 2001 by Sierra Online, Inc.
//============================================================================

// This file contains Weapon and Ammo Class/"namespace" helper methods
// as well as hooks into the inventory system. These functions are not
// attached to a specific C++ class or datablock, but define a set of
// methods which are part of dynamic namespaces "class". The Items
// include these namespaces into their scope using the  ItemData and
// ItemImageData "className" variable.

// All ShapeBase images are mounted into one of 8 slots on a shape.
// This weapon system assumes all primary weapons are mounted into
// this specified slot:
$WeaponSlot = 0;

//-----------------------------------------------------------------------------
// Weapon Class
//-----------------------------------------------------------------------------

function Weapon::onUse(%data,%obj)
{
   // Default behavoir for all weapons is to mount it into the
   // this object's weapon slot, which is currently assumed
   // to be slot 0
   if (%obj.getMountedImage($WeaponSlot) != %data.image.getId())
   {
      //serverPlay3D(WeaponUseSound,%obj.getTransform());
      
      //echo("Human is "@$Human @" and object is "@%obj);
      if ($Human == %obj ) {
      	Gunbox.SetValue(%data);
      }
      $Ammo_Left_now = %obj.getInventory(%this.ammo);
      %obj.mountImage(%data.image, $WeaponSlot);
   }
}

function Weapon::onPickup(%this, %obj, %shape, %amount)
{
   // The parent Item method performs the actual pickup.
   // For player's we automatically use the weapon if the
   // player does not already have one in hand.
   if (Parent::onPickup(%this, %obj, %shape, %amount))
      //serverPlay3D(WeaponPickupSound,%obj.getTransform());
      if ( (%shape.getClassName() $= "Player" ||
            %shape.getClassName() $= "AIPlayer"  )  &&
            %shape.getMountedImage($WeaponSlot) == 0)
      {
         %shape.use(%this);
      }
   if ( %shape.getClassName() $= "Player" ) {
   	if ( %this.getName() $= "Crossbow" ) {
   		$CrossBow_inventory = 1;
   	}
   	else if ( %this.getName() $= "TommyGun" ) {
   		$TommyGun_inventory = 1;
   	}
  }
	//echo("marko1 %this is "@%this.getName()@" and obj is "@%obj@" and shape is "@%shape);   	
   	  
   
}


function Weapon::onInventory(%this,%obj,%amount)
{
   // Weapon inventory has changed, make sure there are no weapons
   // of this type mounted if there are none left in inventory.
   if (!%amount && (%slot = %obj.getMountSlot(%this.image)) != -1)
      %obj.unmountImage(%slot);
}


//-----------------------------------------------------------------------------
// Weapon Image Class
//-----------------------------------------------------------------------------

function WeaponImage::onMount(%this,%obj,%slot)
{
   // Images assume a false ammo state on load.  We need to
   // set the state according to the current inventory.
   if (%obj.getInventory(%this.ammo))
      %obj.setImageAmmo(%slot,true);
   if ( %obj == $Human ) {
   	Ammobox.SetValue(%obj.getInventory(%this.ammo));
  }
}


//-----------------------------------------------------------------------------
// Ammmo Class
//-----------------------------------------------------------------------------

function Ammo::onPickup(%this, %obj, %shape, %amount)
{
   // The parent Item method performs the actual pickup.
   if (Parent::onPickup(%this, %obj, %shape, %amount))
   {
		
   }
}

function Ammo::onInventory(%this,%obj,%amount)
{
   // The ammo inventory state has changed, we need to update any
   // mounted images using this ammo to reflect the new state.
  if ( %this $= "M16ammo" ) {
   	PlayMusic(M16_Fire);
  }
  else if ( %this $= "CrossbowAmmo" ) {
   	PlayMusic(CrossBow_Fire);
  }
  else if ( %this $= "TommyGunAmmo" ) {
  	//echo("++++++++++++++++++++++ i was compared ok");
   	PlayMusic(TommyGun_Fire);
  }
   //echo("-------------- this is the obj = "@%this);
   for (%i = 0; %i < 8; %i++)
   {
      if ((%image = %obj.getMountedImage(%i)) > 0)
         if (IsObject(%image.ammo) && %image.ammo.getId() == %this.getId())
            %obj.setImageAmmo(%i,%amount != 0);
   }
}

// Support function which applies damage to objects within the radius of
// some effect, usually an explosion.  This function will also optionally
// apply an impulse to each object.

function radiusDamage(%sourceObject, %position, %radius, %damage, %damageType, %impulse)
{
   // Use the container system to iterate through all the objects
   // within our explosion radius.  We'll apply damage to all ShapeBase
   // objects.
     // PlayMusic(Duke_Kill_1);	// use this for when it hits something
   InitContainerRadiusSearch(%position, %radius, $TypeMasks::ShapeBaseObjectType);

   %halfRadius = %radius / 2;
   while ((%targetObject = containerSearchNext()) != 0) {

      // Calculate how much exposure the current object has to
      // the explosive force.  The object types listed are objects
      // that will block an explosion.  If the object is totally blocked,
      // then no damage is applied.
      %coverage = calcExplosionCoverage(%position, %targetObject,
         $TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
         $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
      if (%coverage == 0)
         continue;

      // Radius distance subtracts out the length of smallest bounding
      // box axis to return an appriximate distance to the edge of the
      // object's bounds, as opposed to the distance to it's center.
      %dist = containerSearchCurrRadiusDist();

      // Calculate a distance scale for the damage and the impulse.
      // Full damage is applied to anything less than half the radius away,
      // linear scale from there.
      %distScale = (%dist < %halfRadius)? 1.0:
         1.0 - ((%dist - %halfRadius) / %halfRadius);

      // Apply the damage
      %targetObject.damage(%sourceObject, %position,
         %damage * %coverage * %distScale, %damageType);

      // Apply the impulse
      if (%impulse) {
         %impulseVec = VectorSub(%targetObject.getWorldBoxCenter(), %position);
         %impulseVec = VectorNormalize(%impulseVec);
         %impulseVec = VectorScale(%impulseVec, %impulse * %distScale);
         %targetObject.applyImpulse(%position, %impulseVec);
      }
   }
}
