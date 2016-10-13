//============================================================================
// control/misc/items.cs
//
//
//  Copyright (c) 2003 by Kenneth C.  Finney.
//============================================================================

// These scripts make use of dynamic attribute values on Item datablocks,
// these are as follows:
//
//    maxInventory      Max inventory per object (100 bullets per box, etc.)
//    pickupName        Name to display when client pickups item
//
// Item objects can have:
//
//    count             The # of inventory items in the object.  This
//                      defaults to maxInventory if not set.

// Respawntime is the amount of time it takes for a static "auto-respawn"
// object, such as an ammo box or weapon, to re-appear after it's been
// picked up.  Any item marked as "static" is automaticlly respawned.
$Item::RespawnTime = 20 * 1000;

// Poptime represents how long dynamic items (those that are thrown or
// dropped) will last in the world before being deleted.
$Item::PopTime = 10 * 1000;
$Duke_Pickup = getRandom(7);

//-----------------------------------------------------------------------------
// ItemData base class methods used by all items
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

function Item::respawn(%this)
{
   // This method is used to respawn static ammo and weapon items
   // and is usually called when the item is picked up.
   // Instant fade...
   %this.startFade(0, 0, true);
   %this.hide(true);

   // Shedule a reapearance
   %this.schedule($Item::RespawnTime, "hide", false);
   %this.schedule($Item::RespawnTime + 100, "startFade", 1000, 0, false);
}

function Item::schedulePop(%this)
{
   // This method deletes the object after a default duration. Dynamic
   // items such as thrown or drop weapons are usually popped to avoid
   // world clutter.
   %this.schedule($Item::PopTime - 1000, "startFade", 1000, 0, true);
   %this.schedule($Item::PopTime, "delete");
}


//-----------------------------------------------------------------------------
// Callbacks to hook items into the inventory system

function ItemData::onThrow(%this,%user,%amount)
{
   // Remove the object from the inventory
   if (%amount $= "")
      %amount = 1;
   if (%this.maxInventory !$= "")
      if (%amount > %this.maxInventory)
         %amount = %this.maxInventory;
   if (!%amount)
      return 0;
   %user.decInventory(%this,%amount);

   // Construct the actual object in the world, and add it to
   // the mission group so it's cleaned up when the mission is
   // done.  The object is given a random z rotation.
   %obj = new Item() {
      datablock = %this;
      rotation = "0 0 1 " @ (getRandom() * 360);
      count = %amount;
   };
   MissionGroup.add(%obj);
   %obj.schedulePop();
   return %obj;
}

function ItemData::onPickup(%this,%obj,%user,%amount)
{
   // Add it to the inventory, this currently ignores the request
   // amount, you get what you get.  If the object doesn't have
   // a count or the datablock doesn't have maxIventory set, the
   // object cannot be picked up.
   
   if ( $Picking == 1 ) {
	   %count = %obj.count;
	   if (%count $= "")
	      if (%this.maxInventory !$= "") {
	         if (!(%count = %this.maxInventory))
	            return;
	      }
	      else
	         %count = 1;
	   %user.incInventory(%this,%count);
	   if ( $Allowed_to_Speak > 0 ) {
			if ( $Duke_Pickup % 7 == 0 && %user == $Human ) {
				PlayMusic(Duke_Pickup1);
				$Duke_Pickup++;
			}
			else if ( $Duke_Pickup % 7 == 1 && %user == $Human) {
				PlayMusic(Duke_Pickup2);
				$Duke_Pickup++;
			}
			else if ( $Duke_Pickup % 7 == 2 && %user == $Human) {
				PlayMusic(Duke_Pickup3);
				$Duke_Pickup++;
			}
			else if ( $Duke_Pickup % 7 == 3 && %user == $Human) {
				PlayMusic(Duke_Pickup4);
				$Duke_Pickup++;
			}
			else if ( $Duke_Pickup % 7 == 4 && %user == $Human) {
				PlayMusic(Duke_Pickup5);
				$Duke_Pickup++;
			}
			else if ( $Duke_Pickup % 7 == 5 && %user == $Human) {
				PlayMusic(Duke_Pickup6);
				$Duke_Pickup++;
			}
			else if ( $Duke_Pickup % 7 == 6 && %user == $Human) {
				PlayMusic(Duke_Pickup7);
				$Duke_Pickup++;
			}
		}
	   // Inform the client what they got.
	
	   // If the item is a static respawn item, then go ahead and
	   // respawn it, otherwise remove it from the world.
	   // Anything not taken up by inventory is lost.
	   if (%obj.isStatic()) {
	   	//echo("picked up static item");
	      %obj.delete();
	    }
	   else {
	   	//echo("picked up NON static item");
	      %obj.respawn(); // my method
	    }
	   return true;
	  }
}

//-----------------------------------------------------------------------------
// Hook into the mission editor.

function ItemData::create(%data)
{
   // The mission editor invokes this method when it wants to create
   // an object of the given datablock type.  For the mission editor
   // we always create "static" re-spawnable rotating objects.
   %obj = new Item() {
      dataBlock = %data;
      static = true;
      rotate = true;
   };
   return %obj;
}

datablock ItemData(Copper)
{
   // Mission editor category, this datablock will show up in the
   // specified category under the "shapes" root category.
   category = "Coins";

   // Basic Item properties
   shapeFile = "~/data/models/items/kash1.dts";
   mass = 0.7;
   friction = 0.8;
   elasticity = 0.3;


   //respawnTime = 30 * 60000;
   //salvageTime = 15 * 60000;
   // Dynamic properties defined by the scripts
   pickupName = "a copper coin";
   value = 1;
};

datablock ItemData(Silver)
{
   // Mission editor category, this datablock will show up in the
   // specified category under the "shapes" root category.
   category = "Coins";

   // Basic Item properties
   shapeFile = "~/data/models/items/kash100.dts";
   mass = 0.7;
   friction = 0.8;
   elasticity = 0.3;


   //respawnTime = 30 * 60000;
   //salvageTime = 15 * 60000;
   // Dynamic properties defined by the scripts
   pickupName = "a silver coin";
   value = 100;
};

datablock ItemData(Gold)
{
   // Mission editor category, this datablock will show up in the
   // specified category under the "shapes" root category.
   category = "Coins";

   // Basic Item properties
   shapeFile = "~/data/models/items/kash1000.dts";
   mass = 0.7;
   friction = 0.8;
   elasticity = 0.3;


   //respawnTime = 30 * 60000;
   //salvageTime = 15 * 60000;
   // Dynamic properties defined by the scripts
   pickupName = "a gold coin";
   value = 1000;
};


//-----------------------------------------------------------------------------
// Health Patchs cannot be picked up and are not meant to be added to
// inventory.  Health is applied automatically when an objects collides
// with a patch.
//-----------------------------------------------------------------------------

datablock ItemData(FirstAidKit)
{
   // Mission editor category, this datablock will show up in the
   // specified category under the "shapes" root category.
   category = "Health";

   // Basic Item properties
   shapeFile = "~/data/models/items/healthPatch.dts";
   mass = 1;
   friction = 1;
   elasticity = 0.3;

   respawnTime = 600000;
   // Dynamic properties defined by the scripts
   repairAmount = 200;
   maxInventory = 0; // No pickup or throw
};

function FirstAidKit::onCollision(%this,%obj,%col)
{
	
	if ( $Picking == 1) {
	   // Apply health to colliding object if it needs it.
	   // Works for all shapebase objects.
	   if (%col.getDamageLevel() != 0 && %col.getState() !$= "Dead" )
	   {
	   //echo("!!@@@@@@@@@@@@@ col"@%col);
	   $Health_Left = $Health_Left + 50;
	   Scorebox.SetValue($Health_Left);
	      %col.applyRepair(%this.repairAmount);
	      //%obj.respawn();
	   }
	      //echo("I GOT AN EXTRA LIFE");
	   		$Lives_Left++;
	     	Livesbox.SetValue($Lives_Left);
	   		%obj.delete();
	   		PlayMusic(Duke_Groovy);
   }
}


