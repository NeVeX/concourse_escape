$DOORS::RADIANS_PER_DEGREE = -0.017444;
$DOORS::XY_UNIT_VECTORS = "0 0";
$DOORS::PLAYER_REACH = 6.5;
$DOORS::OPEN_INCREMENT = -0.02;
$DOORS::CLOSE_INCREMENT = -0.04;
$DOORS::OPEN_TIMER = 20;
$DOORS::CLOSE_TIMER = 10;
$DOORS::HOLD_TIMER = 2000;
$DOORS::MAX_ANGLE = 80; //  degrees

datablock StaticShapeData(DoorHall1A)
{
   category = "Doors";
   shapeFile = "~/data/doors/hall_door_1_a/door.dts";
};
function DoorHall1A::OnAdd(%theDatablock, %whichDoor)
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
function DoorHall1A::Operate(%theDatablock, %whichDoor) {
	%can_open = 0;
	
 	// BLUE BLOCK DOOR
	if (%whichDoor.getPosition() $= "288.498 70.0719 68.6091" ) {
		if ( $Tokens_Collected > 2) {
			%can_open = 1;
		}
		else {
			%display = 3;
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

function DoorHall1A::StartOpenSwing(%theDatablock, %whichDoor)
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

function DoorHall1A::IncrementSwing(%theDatablock, %whichDoor)
{
  if ( %whichDoor.currentRotation > (%whichDoor.maxOpenAngle*$DOORS::RADIANS_PER_DEGREE))
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

function DoorHall1A::StartCloseSwing(%theDatablock, %whichDoor)
{
  %theDatablock.schedule($DOORS::CLOSE_TIMER,"DecrementSwing", %whichDoor);
  PlayMusic(DoorClosing);
}

function DoorHall1A::DecrementSwing(%theDatablock, %whichDoor)
{
  if ( %whichDoor.currentRotation < 0)
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
