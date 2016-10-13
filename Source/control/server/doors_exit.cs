$DOORS::RADIANS_PER_DEGREE = 0.017444;
$DOORS::XY_UNIT_VECTORS = "0 0";
$DOORS::PLAYER_REACH = 4.5;
$DOORS::OPEN_INCREMENT = 0.02;
$DOORS::CLOSE_INCREMENT = 0.04;
$DOORS::OPEN_TIMER = 20;
$DOORS::CLOSE_TIMER = 10;
$DOORS::HOLD_TIMER = 2000;
$DOORS::MAX_ANGLE = 90; //  degrees

datablock StaticShapeData(DoorExit)
{
   category = "Doors";
   shapeFile = "~/data/doors/door_exit/door.dts";
};
function DoorExit::OnAdd(%theDatablock, %whichDoor)
{
  if(!%whichDoor.doorOpenFlag)
      %whichDoor.doorOpenFlag = false;
  if(!%whichDoor.rotationDirection)
      %whichDoor.rotationDirection = 1;
  if(!%whichDoor.maxOpenAngle)
      %whichDoor.maxOpenAngle = $DOORS::MAX_ANGLE;

  %whichDoor.currentRotation = 0;
  %whichDoor.originalRotation = 0;
  %whichDoor.partialTransform = "";
}

function serverCmdOperate(%client)
{
   %player = %client.player;
   %eye = %player.getEyeVector();
   %vec = vectorScale(%eye, $DOORS::PLAYER_REACH);
   %start = %player.getEyeTransform();
   %end = VectorAdd(%start,%vec);
   %found = ContainerRayCast (%start, %end, $TypeMasks::StaticObjectType, %player);
   if(%found)
      %found.getDataBlock().Operate(%found);
}
// need to modify function below for locking rooms etc
function DoorExit::Operate(%theDatablock, %whichDoor) {
	%can_open = 0;
   	// MAIN ENTRANCE FAR LEFT DOOR LOOKING FROM YELLOW THING SIDE
	if (%whichDoor.getPosition() $= "18.2793 9.84639 68.7" ) {
		if ( $Tokens_Collected > 20) {
			if ( $Tokens_Collected != 30 ) {
				%can_open = 1;
				schedule(2000, 0, "End_Game");
			}
		}
		else {
			%display = 21;
		}
	}
 	// MAIN ENTRANCE MIDDLE LEFT DOOR LOOKING FROM YELLOW THING SIDE
	else if (%whichDoor.getPosition() $= "22.82 9.946 68.7" ) {
		if ( $Tokens_Collected > 20) {
			if ( $Tokens_Collected != 30 ) {
				%can_open = 1;
				schedule(2000, 0, "End_Game");
			}
		}
		else {
			%display = 21;
		}
	}
   	// MAIN ENTRANCE MIDDLE RIGHT DOOR LOOKING FROM YELLOW THING SIDE
	else if (%whichDoor.getPosition() $= "27.22 9.946 68.7" ) {
		if ( $Tokens_Collected > 20) {
			if ( $Tokens_Collected != 30 ) {
				%can_open = 1;
				schedule(2000, 0, "End_Game");
			}
		}
		else {
			%display = 21;
		}
	}   	
   	// MAIN ENTRANCE FAR RIGHT DOOR LOOKING FROM YELLOW THING SIDE
	else if (%whichDoor.getPosition() $= "31.62 9.946 68.7" ) {
		if ( $Tokens_Collected > 20) {
			if ( $Tokens_Collected != 30 ) {
				%can_open = 1;
				schedule(2000, 0, "End_Game");
			}
		}
		else {
			%display = 21;
		}
	}
   	// FAR RIGHT MAIN ENTRANCE DOOR ON LEFT FROM LOOKING OUTSIDE
	else if (%whichDoor.getPosition() $= "276.196 9.923 68.7" ) {
		if ( $Tokens_Collected > 20) {
			if ( $Tokens_Collected != 30 ) {
				%can_open = 1;
				schedule(2000, 0, "End_Game");
			}
		}
		else {
			%display = 21;
		}
	}
   	// FAR RIGHT MAIN ENTRANCE DOOR ON RIGHT FROM LOOKING OUTSIDE
	else if (%whichDoor.getPosition() $= "283.996 9.923 68.7" ) {
		if ( $Tokens_Collected > 20) {
			if ( $Tokens_Collected != 30 ) {
				%can_open = 1;
				schedule(2000, 0, "End_Game");
			}
		}
		else {
			%display = 21;
		}
	}
   	// FAR LEFT MAIN ENTRANCE BESIDE BANK
	else if (%whichDoor.getPosition() $= "-180.604 9.923 68.7" ) {
		if ( $Tokens_Collected > 20) {
			%can_open = 1;
			schedule(2000, 0, "End_Game");
		}
		else {
			%display = 21;
		}
	}
   	
   	
  if (%whichDoor.doorOpenFlag == false && %can_open == 1) {
    %theDatablock.StartOpenSwing(%whichDoor);
  }
  if ( %can_open == 0 ) {
  	// tell the player he needs more tokens
  	if ( $Tokens_Collected == 30 ) {
  		info(3,0);
  		schedule (1300, 0, "info", 0,0);
  	}
		else {
	  	info(1,%display);
	  	schedule (1500, 0, "info", 0,0);
	  	//echo("showing info to the screen");
	  	}
  	}
}

function DoorExit::StartOpenSwing(%theDatablock, %whichDoor)
{
  %whichDoor.doorOpenFlag=true;
  %whichDoor.currentRotation = 0;
  %whichDoor.originalRotation = getword(%whichDoor.GetTransform(),6);
  %z_unit_vector = getword(%whichDoor.GetTransform(),5);
  if ( %z_unit_vector == 0 )
    %z_unit_vector = "1";
  %whichDoor.partialTransform = getwords(%whichDoor.GetTransform(),0,2) SPC $DOORS::XY_UNIT_VECTORS SPC %z_unit_vector;
  %theDatablock.IncrementSwing(%whichDoor);
  PlayMusic(DoorOpening);
}

function DoorExit::IncrementSwing(%theDatablock, %whichDoor)
{
  if ( %whichDoor.currentRotation < (%whichDoor.maxOpenAngle*$DOORS::RADIANS_PER_DEGREE))
  {
      %whichDoor.currentRotation  += ( $DOORS::OPEN_INCREMENT * %whichDoor.rotationDirection);
      %newrot = %whichDoor.originalRotation + %whichDoor.currentRotation;
      %whichDoor.settransform(%whichDoor.partialTransform SPC %newrot);
      %theDatablock.schedule($DOORS::OPEN_TIMER,"IncrementSwing", %whichDoor);
   }
  else
  {
    %theDatablock.schedule($DOORS::HOLD_TIMER,"StartCloseSwing",%whichDoor);
  }
}

function DoorExit::StartCloseSwing(%theDatablock, %whichDoor)
{
  %theDatablock.schedule($DOORS::CLOSE_TIMER,"DecrementSwing", %whichDoor);
  PlayMusic(DoorClosing);
}

function DoorExit::DecrementSwing(%theDatablock, %whichDoor)
{
  if ( %whichDoor.currentRotation > 0)
  {
    %whichDoor.currentRotation  -= ($DOORS::CLOSE_INCREMENT * %whichDoor.rotationDirection);
    %newrot = %whichDoor.originalRotation + %whichDoor.currentRotation;
    %whichDoor.settransform(%whichDoor.partialTransform SPC %newrot);
    %theDatablock.schedule($DOORS::CLOSE_TIMER,"DecrementSwing", %whichDoor);
  }
  else
  {
    %whichDoor.doorOpenFlag=false;
    %whichDoor.settransform(%whichDoor.partialTransform SPC %whichDoor.originalRotation);
    %whichDoor.currentRotation = 0;
    %whichDoor.dump();
    PlayMusic(DoorShut);
  }
}
