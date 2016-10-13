$DOORS::RADIANS_PER_DEGREE = 0.017444;
$DOORS::XY_UNIT_VECTORS = "0 0";
$DOORS::PLAYER_REACH = 4.5;
$DOORS::OPEN_INCREMENT = 0.02;
$DOORS::CLOSE_INCREMENT = 0.04;
$DOORS::OPEN_TIMER = 20;
$DOORS::CLOSE_TIMER = 10;
$DOORS::HOLD_TIMER = 2000;
$DOORS::MAX_ANGLE = 90; //  degrees

datablock StaticShapeData(DoorInBank)
{
   category = "Doors";
   shapeFile = "~/data/doors/door_in_bank/door.dts";
};
function DoorInBank::OnAdd(%theDatablock, %whichDoor)
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
function DoorInBank::Operate(%theDatablock, %whichDoor) {
	%can_open = 0;
	
 // INSIDE THE BANK FIRST DOOR ON LEFT
	if (%whichDoor.getPosition() $= "-231.978 18.684 68.7" ) {
		if ( $Tokens_Collected > 13) {
			%can_open = 1;
		}
		else {
			%display = 14;
		}
	}
	// INSIDE THE BANK SECOND DOOR ON LEFT
	else if (%whichDoor.getPosition() $= "-234.778 18.684 68.7" ) {
		if ( $Tokens_Collected > 7) {
			%can_open = 1;
		}
		else {
			%display = 8;
		}
	}
	// INSIDE THE BANK DOOR BEHIND COUNTER
	else if (%whichDoor.getPosition() $= "-221.278 31.084 68.7" ) {
		if ( $Tokens_Collected > 16) {
			%can_open = 1;
		}
		else {
			%display = 17;
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

function DoorInBank::StartOpenSwing(%theDatablock, %whichDoor)
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

function DoorInBank::IncrementSwing(%theDatablock, %whichDoor)
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

function DoorInBank::StartCloseSwing(%theDatablock, %whichDoor)
{
  %theDatablock.schedule($DOORS::CLOSE_TIMER,"DecrementSwing", %whichDoor);
  PlayMusic(DoorClosing);
}

function DoorInBank::DecrementSwing(%theDatablock, %whichDoor)
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
