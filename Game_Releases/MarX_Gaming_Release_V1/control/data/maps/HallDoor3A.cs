$DOORSOTHER::RADIANS_PER_DEGREE = -0.017444;
$DOORSOTHER::XY_UNIT_VECTORS = "0 0";
$DOORSOTHER::PLAYER_REACH = 6.5;
$DOORSOTHER::OPEN_INCREMENT = -0.02;
$DOORSOTHER::CLOSE_INCREMENT = -0.04;
$DOORSOTHER::OPEN_TIMER = 20;
$DOORSOTHER::CLOSE_TIMER = 10;
$DOORSOTHER::HOLD_TIMER = 2000;
$DOORSOTHER::MAX_ANGLE = 68; //  degrees

datablock StaticShapeData(DoorHall2A)
{
   category = "Doors";
   shapeFile = "~/data/doors/hall_door_2_a/door.dts";
};
function DoorHall2A::OnAdd(%theDatablock, %whichDoor)
{
  if(!%whichDoor.doorOpenFlag)
      %whichDoor.doorOpenFlag = false;
  if(!%whichDoor.rotationDirection)
      %whichDoor.rotationDirection = 1;
  if(!%whichDoor.maxOpenAngle)
      %whichDoor.maxOpenAngle = $DOORSOTHER::MAX_ANGLE;

  %whichDoor.currentRotation = 0;
  %whichDoor.originalRotation = 0;
  %whichDoor.partialTransform = "";
}

function serverCmdOperate(%client)
{
   %player = %client.player;
   %eye = %player.getEyeVector();
   %vec = vectorScale(%eye, $DOORSOTHER::PLAYER_REACH);
   %start = %player.getEyeTransform();
   %end = VectorAdd(%start,%vec);
   %found = ContainerRayCast (%start, %end, $TypeMasks::StaticObjectType, %player);
   if(%found)
      %found.getDataBlock().Operate(%found);
}
// need to modify function below for locking rooms etc
function DoorHall2A::Operate(%theDatablock, %whichDoor) {
	%can_open = 0;

	if (%whichDoor.getPosition() $= "85.7213 89.4079 68.5926" ) {
		if ( $Tokens_Collected > 2) {
			%can_open = 1;
		}
		else {
			%display = 3;
		}
	}
	else if (%whichDoor.getPosition() $= "44.8119 90.2499 68.5926" ) {
		if ( $Tokens_Collected > 2) {
			%can_open = 1;
		}
		else {
			%display = 3;
		}
	}
  if (%whichDoor.doorOpenFlag == false && %can_open == 1) {
    %theDatablock.StartOpenSwing(%whichDoor);
     echo("whichdoor is "@%whichDoor@" and datablock is "@%theDatablock);
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

function DoorHall2A::StartOpenSwing(%theDatablock, %whichDoor)
{
  %whichDoor.doorOpenFlag=true;
  %whichDoor.currentRotation = 0;
  %whichDoor.originalRotation = getword(%whichDoor.GetTransform(),6);
  %z_unit_vector = getword(%whichDoor.GetTransform(),5);
  if ( %z_unit_vector == 0 )
    %z_unit_vector = "1";
  %whichDoor.partialTransform = getwords(%whichDoor.GetTransform(),0,2) SPC $DOORSOTHER::XY_UNIT_VECTORS SPC %z_unit_vector;
  %theDatablock.IncrementSwing(%whichDoor);
  PlayMusic(DoorOpening);
}

function DoorHall2A::IncrementSwing(%theDatablock, %whichDoor)
{
  if ( %whichDoor.currentRotation > (%whichDoor.maxOpenAngle*$DOORSOTHER::RADIANS_PER_DEGREE))
  {
      %whichDoor.currentRotation  += ( $DOORSOTHER::OPEN_INCREMENT * %whichDoor.rotationDirection);
      %newrot = %whichDoor.originalRotation + %whichDoor.currentRotation;
      %whichDoor.settransform(%whichDoor.partialTransform SPC %newrot);
      %theDatablock.schedule($DOORSOTHER::OPEN_TIMER,"IncrementSwing", %whichDoor);
   }
  else
  {
    %theDatablock.schedule($DOORSOTHER::HOLD_TIMER,"StartCloseSwing",%whichDoor);
  }
}

function DoorHall2A::StartCloseSwing(%theDatablock, %whichDoor)
{
  %theDatablock.schedule($DOORSOTHER::CLOSE_TIMER,"DecrementSwing", %whichDoor);
  PlayMusic(DoorClosing);
}

function DoorHall2A::DecrementSwing(%theDatablock, %whichDoor)
{
  if ( %whichDoor.currentRotation < 0)
  {
    %whichDoor.currentRotation  -= ($DOORSOTHER::CLOSE_INCREMENT * %whichDoor.rotationDirection);
    %newrot = %whichDoor.originalRotation + %whichDoor.currentRotation;
    %whichDoor.settransform(%whichDoor.partialTransform SPC %newrot);
    %theDatablock.schedule($DOORSOTHER::CLOSE_TIMER,"DecrementSwing", %whichDoor);
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
