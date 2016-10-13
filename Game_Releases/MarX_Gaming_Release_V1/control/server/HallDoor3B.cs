$DOORSOTHER2::RADIANS_PER_DEGREE = 0.017444;
$DOORSOTHER2::XY_UNIT_VECTORS = "0 0";
$DOORSOTHER2::PLAYER_REACH = 4.5;
$DOORSOTHER2::OPEN_INCREMENT = 0.02;
$DOORSOTHER2::CLOSE_INCREMENT = 0.04;
$DOORSOTHER2::OPEN_TIMER = 20;
$DOORSOTHER2::CLOSE_TIMER = 10;
$DOORSOTHER2::HOLD_TIMER = 2000;
$DOORSOTHER2::MAX_ANGLE = 68; //  degrees

datablock StaticShapeData(DoorHall3B)
{
   category = "Doors";
   shapeFile = "~/data/doors/hall_door_3_b/door.dts";
};
function DoorHall3B::OnAdd(%theDatablock, %whichDoor)
{
  if(!%whichDoor.doorOpenFlag)
      %whichDoor.doorOpenFlag = false;
  if(!%whichDoor.rotationDirection)
      %whichDoor.rotationDirection = 1;
  if(!%whichDoor.maxOpenAngle)
      %whichDoor.maxOpenAngle = $DOORSOTHER2::MAX_ANGLE;

  %whichDoor.currentRotation = 0;
  %whichDoor.originalRotation = 0;
  %whichDoor.partialTransform = "";
}

function serverCmdOperate(%client)
{
   %player = %client.player;
   %eye = %player.getEyeVector();
   %vec = vectorScale(%eye, $DOORSOTHER2::PLAYER_REACH);
   %start = %player.getEyeTransform();
   %end = VectorAdd(%start,%vec);
   %found = ContainerRayCast (%start, %end, $TypeMasks::StaticObjectType, %player);
   if(%found)
      %found.getDataBlock().Operate(%found);
}
// need to modify function below for locking rooms etc
function DoorHall3B::Operate(%theDatablock, %whichDoor) {
	%can_open = 0;
	
	if (%whichDoor.getPosition() $= "16.4073 93.1032 68.5926" ) {
		if ( $Tokens_Collected > 12) {
			%can_open = 1;
		}
		else {
			%display = 13;
		}
	}
	else if (%whichDoor.getPosition() $= "-24.0689 84.408 68.5926" ) {
		if ( $Tokens_Collected > 14) {
			%can_open = 1;
		}
		else {
			%display = 15;
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

function DoorHall3B::StartOpenSwing(%theDatablock, %whichDoor)
{
  %whichDoor.doorOpenFlag=true;
  %whichDoor.currentRotation = 0;
  %whichDoor.originalRotation = getword(%whichDoor.GetTransform(),6);
  %z_unit_vector = getword(%whichDoor.GetTransform(),5);
  if ( %z_unit_vector == 0 )
    %z_unit_vector = "1";
  %whichDoor.partialTransform = getwords(%whichDoor.GetTransform(),0,2) SPC $DOORSOTHER2::XY_UNIT_VECTORS SPC %z_unit_vector;
  %theDatablock.IncrementSwing(%whichDoor);
  PlayMusic(DoorOpening);
}

function DoorHall3B::IncrementSwing(%theDatablock, %whichDoor)
{
  if ( %whichDoor.currentRotation < (%whichDoor.maxOpenAngle*$DOORSOTHER2::RADIANS_PER_DEGREE))
  {
      %whichDoor.currentRotation  += ( $DOORSOTHER2::OPEN_INCREMENT * %whichDoor.rotationDirection);
      %newrot = %whichDoor.originalRotation + %whichDoor.currentRotation;
      %whichDoor.settransform(%whichDoor.partialTransform SPC %newrot);
      %theDatablock.schedule($DOORSOTHER2::OPEN_TIMER,"IncrementSwing", %whichDoor);
   }
  else
  {
    %theDatablock.schedule($DOORSOTHER2::HOLD_TIMER,"StartCloseSwing",%whichDoor);
  }
}

function DoorHall3B::StartCloseSwing(%theDatablock, %whichDoor)
{
  %theDatablock.schedule($DOORSOTHER2::CLOSE_TIMER,"DecrementSwing", %whichDoor);
  PlayMusic(DoorClosing);
}

function DoorHall3B::DecrementSwing(%theDatablock, %whichDoor)
{
  if ( %whichDoor.currentRotation > 0)
  {
    %whichDoor.currentRotation  -= ($DOORSOTHER2::CLOSE_INCREMENT * %whichDoor.rotationDirection);
    %newrot = %whichDoor.originalRotation + %whichDoor.currentRotation;
    %whichDoor.settransform(%whichDoor.partialTransform SPC %newrot);
    %theDatablock.schedule($DOORSOTHER2::CLOSE_TIMER,"DecrementSwing", %whichDoor);
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
