
//-----------------------------------------------------------------------------
// Callback Handlers
//-----------------------------------------------------------------------------

//$MINIMUM_SCAN_INTERVAL = 2000;
//$MAXIMUM_SCAN_INTERVAL = 10000;
$ai_players = 0;
$MIN_SCAN_GAP = 1000;
$MAX_SCAN_GAP = 2000;
$MIN_TRIGGER_HOLD = 50;	// the next four varaibles are the important for gun aggressiveness
$MAX_TRIGGER_HOLD = 100;
$MIN_ITCHY_FINGER = 50;
$MAX_ITCHY_FINGER = 100;
$MAX_THREAT_ENGAGE_RANGE = 100000;
$MAX_AGGRESSIVENESS = 100000;
$MAX_ATTENTION_LEVEL = 10000;
$MAX_ALERTNESS = 10000;
$MAX_ROVER_SPEED = 0.5;
$MIN_ROVER_SPEED = 0.5;
$MAX_PATROL_SPEED = 0.1;
$MIN_PATROL_SPEED = 0.2;
$STATIONARY = 0.0;

$X_MOVE = 23;
$Y_MOVE = 12;

$cardinalDirection[0] = "0 10000 0";
$cardinalDirection[1] = "10000 0 0";
$cardinalDirection[2] = "0 -10000 0";
$cardinalDirection[3] = "-10000 0 0";

function AIBeast::onStuck(%this,%obj)
{
   // Invoked if the player is stuck while moving
   // This method is currently not being invoked correctly.
   if(!isObject(%obj))
      return;
}

function AIBeast::unBlock(%this,%obj)
{

   if(!isObject(%obj))
      return;
   cancel(%obj.nextBlockCheck);
   %this.onReachDestination(%obj);

}

function AIBeast::onReachDestination(%this,%obj)
{
   // Invoked when the player arrives at his destination point.
//   //echo( "onReachDestination" );
   if(!isObject(%obj))
      return;
    cancel(%obj.nextBlockCheck);
   %theRole = %obj.role;

  // //echo( "finding new dest !!! (from:"@%obj@")");
    %obj.setMoveSpeed($MIN_ROVER_SPEED);
    %victim = %obj.getAimObject();
    %this.setRandomDestination(%obj);
    %obj.nextBlockCheck = %this.schedule($MAX_SCAN_GAP*2, "unBlock", %obj);
//     //echo( "----%this.scheduledCheck" @ %this.scheduledCheck);

}

function AIBeast::checkForThreat(%this,%obj)
{
//   //echo( "checkForThreat...");
   if(!isObject(%obj))
      return;
      getWords($Human.getPosition(),0,0);
	if ($Health_Left <= 0 ) {
		%random1 = getRandom(1);
		if ( %random1 == 0 ) {
  		%obj.setMoveDestination(""@getWords($ReSpawn,0,0)-$X_MOVE@" "@getWords($ReSpawn,1,1)-$Y_MOVE@" "@getWords($ReSpawn,2,2));
  	}
  	else if ( %random1 == 1) {
  		%obj.setMoveDestination(""@getWords($ReSpawn,0,0)+$X_MOVE@" "@getWords($ReSpawn,1,1)+$Y_MOVE@" "@getWords($ReSpawn,2,2));
  	}
  	echo("i have told them to go to the RESPAWN POINT");
  }
  else {
  	%random2 = getRandom(1);
		if ( %random2 == 0 ) {
  		%obj.setMoveDestination(""@getWords($Human.getPosition(),0,0)-$X_MOVE@" "@getWords($Human.getPosition(),1,1)-$Y_MOVE@" "@getWords($Human.getPosition(),2,2));
  	}
  	else if ( %random2 == 1) {
  		%obj.setMoveDestination(""@getWords($Human.getPosition(),0,0)+$X_MOVE@" "@getWords($Human.getPosition(),1,1)+$Y_MOVE@" "@getWords($Human.getPosition(),2,2));
  	}
	  echo("i have told them to CHASE AFTER ME");
	}
   if (isObject( %obj) )
   {
      %idx = %obj.getClosestHuman();
      if (%obj.attentionLevel>0)  // if attentionLevel is non-zero, keep looking at max range
      {
         %testRange = %obj.range * (%obj.aggression*200);
      }
      else
      {
         %testRange = %obj.range;
      }
   }
   else
   {
      return 0;
   }
   if (%idx < 0)
      return 0;
   %target = ClientGroup.getObject( %idx );

   if (%target.player == %obj.currentTarget)
   {
      if (isObject( %obj) )
      {
         if ( %obj.GetTargetRange(%target.player) <  %testRange)
         {
            return %target.player;
         }
      }
      else
         return 0;
   }
   else     /// new threat
   {
      if (isObject( %obj) )
      {
         if ( %obj.GetTargetRange(%target.player) <  %testRange*20)
         {
            return %target.player;
         }
      }
      else
         return 0;
   }
 //  //echo( "...no threat");
   return 0;
}

function AIBeast::DoScan(%this,%obj)
{
   ////echo( "doScan... (from:"@%obj@")");
   if(!isObject(%obj))
      return;
   cancel(%this.scheduledCheck);
   
   
  if ($Health_Left <= 0 ) {
  	%random3 = getRandom(1);
		if ( %random3 == 0 ) {
  		%obj.setMoveDestination(""@getWords($ReSpawn,0,0)-$X_MOVE@" "@getWords($ReSpawn,1,1)-$Y_MOVE@" "@getWords($ReSpawn,2,2));
  	}
  	else if ( %random3 == 1) {
  		%obj.setMoveDestination(""@getWords($ReSpawn,0,0)+$X_MOVE@" "@getWords($ReSpawn,1,1)+$Y_MOVE@" "@getWords($ReSpawn,2,2));
  	}
  	echo("i have told them to go to the RESPAWN POINT");
  }
  else {
  	%random4 = getRandom(1);
		if ( %random4 == 0 ) {
  		%obj.setMoveDestination(""@getWords($Human.getPosition(),0,0)-$X_MOVE@" "@getWords($Human.getPosition(),1,1)-$Y_MOVE@" "@getWords($Human.getPosition(),2,2));
  	}
  	else if ( %random4 == 1) {
  		%obj.setMoveDestination(""@getWords($Human.getPosition(),0,0)+$X_MOVE@" "@getWords($Human.getPosition(),1,1)+$Y_MOVE@" "@getWords($Human.getPosition(),2,2));
  	}
	  echo("i have told them to CHASE AFTER ME");
	}
   %theRole = %obj.role;

   if (%obj.attentionLevel<=0)  // if attentionLevel is non-zero, keep looking in same direction
      %obj.attentionLevel=0;
   else
      %obj.attentionLevel--;

  if (%obj.attentionLevel==0)  // if attentionLevel is non-zero, keep looking in same direction
  {
     %look = getRandom(3);
     if (%this.look != %look)
        %this.look = %look;
     else
     {
        %this.look = %look + 1;
        if (%this.look < 3)
           %this.look++;
        else
           %this.look = 0;
     }
  }

  %t = $cardinalDirection[%this.look];
  %obj.setAimLocation( %t);

   if ( (%tgtPlayer = %this.checkForThreat(%obj)) != 0)
   {
   	//echo("1111111111111111111111111 the target playerr is "@%tgtPlayer);
      if (%obj.currentTarget)
      {
         if (%obj.currentTarget==%tgtPlayer)
         {
           // //echo( "STILL A THREAT (from:"@%obj@")");
            %obj.setAimObject( %tgtPlayer );
            //echo("**************** is this where i am "@%tgtPlayer); // this is my object number
            //echo("**************** this is where i am NEW  "@%tgtPlayer.getPosition());
            %obj.attentionLevel = %obj.attention;
            if (%theRole !$= "Guard")
            {
               %obj.setMoveSpeed($MAX_ROVER_SPEED);
               %random5 = getRandom(1);
							if ( %random5 == 0 ) {
					  		%obj.setMoveDestination(""@getWords(%tgtPlayer.getPosition(),0,0)-$X_MOVE@" "@getWords(%tgtPlayer.getPosition(),1,1)-$Y_MOVE@" "@getWords(%tgtPlayer.getPosition(),2,2));
					  	}
					  	else if ( %random5 == 1) {
					  		%obj.setMoveDestination(""@getWords(%tgtPlayer.getPosition(),0,0)+$X_MOVE@" "@getWords(%tgtPlayer.getPosition(),1,1)+$Y_MOVE@" "@getWords(%tgtPlayer.getPosition(),2,2));
					  	}
               %obj.nextBlockCheck = %this.schedule($MAX_SCAN_GAP*2, "unBlock", %obj);
            }
         }
         else
         {
           // //echo( "CHANGED THREAT (from:"@%obj@")");
            %obj.currentTarget =  %tgtPlayer;
            %obj.setAimObject( %obj.currentTarget );
            if (%theRole !$= "Guard")
            {
               %obj.setMoveSpeed($MAX_ROVER_SPEED);
               %random6 = getRandom(1);
							if ( %random6 == 0 ) {
					  		%obj.setMoveDestination(""@getWords(%tgtPlayer.getPosition(),0,0)-$X_MOVE@" "@getWords(%tgtPlayer.getPosition(),1,1)-$Y_MOVE@" "@getWords(%tgtPlayer.getPosition(),2,2));
					  	}
					  	else if ( %random6 == 1) {
					  		%obj.setMoveDestination(""@getWords(%tgtPlayer.getPosition(),0,0)+$X_MOVE@" "@getWords(%tgtPlayer.getPosition(),1,1)+$Y_MOVE@" "@getWords(%tgtPlayer.getPosition(),2,2));
					  	}
               %obj.nextBlockCheck = %this.schedule($MAX_SCAN_GAP*2, "unBlock", %obj);
            }
         }
       }
       else
       {
         ////echo( "NEW THREAT!! (from:"@%obj@")");
         %obj.setAimObject( %tgtPlayer );
         %obj.currentTarget =  %tgtPlayer;
         %obj.attentionLevel = %obj.attention;
         if (%theRole !$= "Guard")
         {
            %obj.setMoveSpeed($MAX_ROVER_SPEED);
            %random7 = getRandom(1);
							if ( %random7 == 0 ) {
					  		%obj.setMoveDestination(""@getWords(%tgtPlayer.getPosition(),0,0)-$X_MOVE@" "@getWords(%tgtPlayer.getPosition(),1,1)-$Y_MOVE@" "@getWords(%tgtPlayer.getPosition(),2,2));
					  	}
					  	else if ( %random7 == 1) {
					  		%obj.setMoveDestination(""@getWords(%tgtPlayer.getPosition(),0,0)+$X_MOVE@" "@getWords(%tgtPlayer.getPosition(),1,1)+$Y_MOVE@" "@getWords(%tgtPlayer.getPosition(),2,2));
					  	}
            %obj.nextBlockCheck = %this.schedule($MAX_SCAN_GAP*2, "unBlock", %obj);
         }
       }
   }
   else
   {
      if (%obj.getAimObject)
      {
         %obj.clearAim();
        // //echo( "doScan >>> %obj.clearAim (from:"@%obj@")");
         %obj.currentTarget = 0; // forget this target
      }
      %this.nextScan = %this.schedule($MIN_SCAN_GAP+getRandom($MAX_SCAN_GAP/%this.alertness), "doScan", %obj);
   }
  
   
   
}

function AIBeast::openFire(%this,%obj)
{
   if(!isObject(%obj))
      return;
   %obj.setImageTrigger(0,false);
   %this.schedule($MIN_TRIGGER_HOLD+getRandom($MAX_TRIGGER_HOLD), "ceaseFire", %obj);
	//echo("123456 i am still firiring my gun");
}

function AIBeast::ceaseFire(%this,%obj)
{
   ////echo( "--------ceaseFire... (from:"@%obj@")");
   if(!isObject(%obj))
      return;
   %obj.setImageTrigger(0,false);
   %obj.clearAim();
   %obj.currentTarget = 0; // forget this target
}

function AIBeast::onTargetEnterLOS(%this,%obj)
{
   // If an aim target object is set, this method is invoked when
   // that object becomes visible.
  //  //echo( "LOS TARGET ! (from:"@%obj@")");
   if(!isObject(%obj))
      return;
   %theRole = %obj.role;

   %obj.attentionLevel = %this.attention;
   %this.schedule($MIN_ITCHY_FINGER+getRandom($MAX_ITCHY_FINGER), "openFire", %obj);
   %this.schedule($MIN_SCAN_GAP+getRandom($MAX_SCAN_GAP/%this.alertness), "doScan", %obj);
   %obj.setImageTrigger(0,true);
}

function AIBeast::onTargetExitLOS(%this,%obj)
{
   // If an aim target object is set, this method is invoked when
   // the object is not longer visible.
  // //echo( ".......Fuhgetaboutit (from:"@%obj@")");
  
  if ($Health_Left <= 0 ) {
  	%random11 = getRandom(1);
		if ( %random11 == 0 ) {
  		%obj.setMoveDestination(""@getWords($ReSpawn,0,0)-$X_MOVE@" "@getWords($ReSpawn,1,1)-$Y_MOVE@" "@getWords($ReSpawn,2,2));
  	}
  	else if ( %random11 == 1) {
  		%obj.setMoveDestination(""@getWords($ReSpawn,0,0)+$X_MOVE@" "@getWords($ReSpawn,1,1)+$Y_MOVE@" "@getWords($ReSpawn,2,2));
  	}
  	echo("i have told them to go to the RESPAWN POINT");
  }
  else {
  	%random41 = getRandom(1);
		if ( %random41 == 0 ) {
  		%obj.setMoveDestination(""@getWords($Human.getPosition(),0,0)-$X_MOVE@" "@getWords($Human.getPosition(),1,1)-$Y_MOVE@" "@getWords($Human.getPosition(),2,2));
  	}
  	else if ( %random41 == 1) {
  		%obj.setMoveDestination(""@getWords($Human.getPosition(),0,0)+$X_MOVE@" "@getWords($Human.getPosition(),1,1)+$Y_MOVE@" "@getWords($Human.getPosition(),2,2));
  	}
	  echo("i have told them to CHASE AFTER ME");
	}
   if(!isObject(%obj))
      return;
   %obj.setImageTrigger(0,false);
   %obj.clearAim();
  // //echo( "onTargetExitLOS >>> %obj.clearAim (from:"@%obj@")");
   %obj.currentTarget = 0;  // forget this target
   %this.schedule($MIN_SCAN_GAP, "doScan", %obj);
}

function AIBeast::setRandomDestination(%this,%obj)
{
   if(!isObject(%obj))
      return;
   //%pos = %obj.getTransform();
   //%x = getWord(%pos, 0); %y = getWord(%pos, 1); %z = getWord(%pos, 2);
   //%rnd1 = getrandom(20); %rnd2 = getrandom(20); %rnd3 = getrandom(20);

   //if( %rnd1 < 10 )
   //   %rnd01 = %x + getrandom(30);
   //else
   //   %rnd01 = %x - getrandom(30);

   //if( %rnd2 < 10 )
   //   %rnd02 = %y + getrandom(30);
   //else
   //   %rnd02 = %y - getrandom(30);

   //if( %rnd3 < 10 )
   //   %rnd03 = %z + getrandom(30);
   //else
    //  %rnd03 = %z - getrandom(30);
	/// I OVERRIDE THE RANDOM AND SET IT TO MY POSITION I.E THEY WILL CHASE AFTER ME!!
  //%obj.setMoveDestination(%rnd01 SPC %rnd02 SPC %rnd03);
  if ($Health_Left <= 0 ) {
  	%random112 = getRandom(1);
		if ( %random112 == 0 ) {
  		%obj.setMoveDestination(""@getWords($ReSpawn,0,0)-$X_MOVE@" "@getWords($ReSpawn,1,1)-$Y_MOVE@" "@getWords($ReSpawn,2,2));
  	}
  	else if ( %random112 == 1) {
  		%obj.setMoveDestination(""@getWords($ReSpawn,0,0)+$X_MOVE@" "@getWords($ReSpawn,1,1)+$Y_MOVE@" "@getWords($ReSpawn,2,2));
  	}
  	echo("i have told them to go to the RESPAWN POINT");
  }
  else {
  	%random42 = getRandom(1);
		if ( %random42 == 0 ) {
  		%obj.setMoveDestination(""@getWords($Human.getPosition(),0,0)-$X_MOVE@" "@getWords($Human.getPosition(),1,1)-$Y_MOVE@" "@getWords($Human.getPosition(),2,2));
  	}
  	else if ( %random42 == 1) {
  		%obj.setMoveDestination(""@getWords($Human.getPosition(),0,0)+$X_MOVE@" "@getWords($Human.getPosition(),1,1)+$Y_MOVE@" "@getWords($Human.getPosition(),2,2));
  	}
	  echo("i have told them to CHASE AFTER ME");
	}
  // //echo("**************** this is where i am NEW  "@$Human.getPosition());
}

//
//-----------------------------------------------------------------------------

function AIPlayer::spawnBot(%index,%location)
{
   // An example function which creates a new AIPlayer object
   // using the the example player datablock.
   //echo("which enemy is "@$which_enemy);
   if ( $which_enemy == 1) {
   	%me = new AIPlayer() {
      	dataBlock = Goblin;
      	aiPlayer = true;
   	};
  }
  if ( $which_enemy == 2) {
  	%me = new AIPlayer() {
      	dataBlock = BeastAvatar;
      	aiPlayer = true;
   	};
	}
	if ( $which_enemy == 3) {
  	%me = new AIPlayer() {
      	dataBlock = BeastAvatarWhite;
      	aiPlayer = true;
   	};
	}
	if ( $which_enemy == 4) {
  	%me = new AIPlayer() {
      	dataBlock = BeastAvatarSanta;
      	aiPlayer = true;
   	};
	}
   MissionCleanup.add(%me);
   AIGroup.add(%me);
   // player defaults
   $mebot = %me;
   %me.look = 0;
   %me.range = 100;
   %spawn=%location;

    %me.alertness = 75;
    %me.aggression = 75;
    %me.attention = 75;
    %me.range = 50;

   %me.index = %index;
   %me.setTransform(%spawn);
   %me.setEnergyLevel(60);
   %me.role = %role;
   if ( $which_enemy == 1 ) {
   	%me.setShapeName("Goblin");
   	%weapon = new Item() {
	      dataBlock = M16;
	   };
	 	%me.pickup(%weapon, 1); 
	 	%ammo = new Item() {
	      dataBlock = M16Ammo;
	   	};
		%me.pickup(%ammo, 2); 
  }
  if ( $which_enemy == 2 ) {
  	%me.setShapeName("Beast");
  	%weapon = new Item() {
	      dataBlock = Crossbow;
	   };
	 	%me.pickup(%weapon, 1); 
	 	%ammo = new Item() {
	      dataBlock = CrossbowAmmo;
	   	};
		%me.pickup(%ammo, 2); 
  }
  if ( $which_enemy == 3 ) {
  	%me.setShapeName("BeastWhite");
  	%weapon = new Item() {
	      dataBlock = TommyGun;
	   };
	 	%me.pickup(%weapon, 1); 
	 	%ammo = new Item() {
	      dataBlock = TommyGunAmmo;
	   	};
		%me.pickup(%ammo, 2); 
  }
  if ( $which_enemy == 4 ) {
  	%me.setShapeName("BeastSanta");
  	%weapon = new Item() {
	      dataBlock = Crossbow;
	   };
	 	%me.pickup(%weapon, 1); 
	 	%ammo = new Item() {
	      dataBlock = CrossbowAmmo;
	   	};
		%me.pickup(%ammo, 2); 
  }
   %me.attentionLevel = 0;
   %me.nextBlockCheck = 0;

	 
	   	
   MissionCleanup.add(%weapon);
   MissionCleanup.add(%ammo);
   
   %me.look = 0;
   %me.setMoveSpeed(0);

  //echo("Added Beast [" SPC %me SPC "] :" SPC %me.task SPC ":" SPC %me.role SPC "#" SPC  %me.index );
	//echo("me is "@%me);
	if ($Health_Left <= 0 ) {
		%me.setAimLocation( $ReSpawn );
		%random13 = getRandom(1);
		if ( %random13 == 0 ) {
  		%me.setMoveDestination(""@getWords($ReSpawn,0,0)-$X_MOVE@" "@getWords($ReSpawn,1,1)-$Y_MOVE@" "@getWords($ReSpawn,2,2));
  	}
  	else if ( %random13 == 1) {
  		%me.setMoveDestination(""@getWords($ReSpawn,0,0)+$X_MOVE@" "@getWords($ReSpawn,1,1)+$Y_MOVE@" "@getWords($ReSpawn,2,2));
  	}
  	echo("i have told them to go to the RESPAWN POINT");
  }
  else {
	  %me.setAimLocation( $Human.getPosition() );
	  %random43 = getRandom(1);
		if ( %random43 == 0 ) {
  		%me.setMoveDestination(""@getWords($Human.getPosition(),0,0)-$X_MOVE@" "@getWords($Human.getPosition(),1,1)-$Y_MOVE@" "@getWords($Human.getPosition(),2,2));
  	}
  	else if ( %random43 == 1) {
  		%me.setMoveDestination(""@getWords($Human.getPosition(),0,0)+$X_MOVE@" "@getWords($Human.getPosition(),1,1)+$Y_MOVE@" "@getWords($Human.getPosition(),2,2));
  	}
	  echo("i have told them to CHASE AFTER ME");
	}
  %me.getDataBlock().schedule(0800, "DoScan", %me);
  
  $Enemy_Array[$Enemy] = %me;
  $Enemy++;

   return %me;
}

function AIPlayer::GetTargetRange(%this, %target)
{
   $tgt = %target;
   %tgtPos = %target.getPosition();
   %eyePoint = %this.getWorldBoxCenter();
   %distance = VectorDist(%tgtPos, %eyePoint);
///   //echo("Actual range to target: " @ %distance );
   return %distance;
}

function AIPlayer::getClosestHuman(%this) {
///      //echo( "getClosestHuman...");

   %index = -1;
   %botPos = %this.getPosition();
   %count = ClientGroup.getCount();
   for(%i = 0; %i < %count; %i++)
   {
      %client = ClientGroup.getObject(%i);
      if (%client.player $= "" || %client.player == 0 )
         return -1;
      %playPos = %client.player.getPosition();

      %tempDist = VectorDist(%playPos, %botPos);
      if(%i == 0) {
         %dist = %tempDist;
         %index = %i;
      }
      else {
         if(%dist > %tempDist) {
            %dist = %tempDist;
            %index = %i;
         }
      }
   }
   return %index;
}

function CreateEnemies()
{
  new SimSet(AIGroup);
  %this_game = $Which_Game;
  for (%i = 0; %i < $HowManyEnemies; %i++) {
  	schedule(%i * $SpawnInterval, 0, "Creating_Enemies",%i,%this_game); // give the human a chance to react
  	echo("making enemies");
	}
}

function Creating_Enemies(%i, %current_game) {
	
	if ( %current_game == $Which_Game ) {
		
		if ( $Many_Beast1 > 0 ) { $which_enemy = 1; $Many_Beast1--;}
		else if ( $Many_Beast2 > 0 ) { $which_enemy = 2; $Many_Beast2--;}
		else if ( $Many_Beast3 > 0 ) { $which_enemy = 3; $Many_Beast3--; }
		else if ( $Many_Beast4 > 0 ) { $which_enemy = 4; $Many_Beast4--;}
		//echo("Enemy has spawned here -- "@$XSpawn@" "@$YSpawn@" 70 1 0 0 0");
		AIPlayer::spawnBot(%i,""@$XSpawn@" "@$YSpawn@" 70 1 0 0 0");
		$YSpawn = $YSpawn + $YSpawnSpread;
		$XSpawn = $XSpawn + $XSpawnSpread;
	}
}



