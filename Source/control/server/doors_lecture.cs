$DOORS::RADIANS_PER_DEGREE = 0.017444;
$DOORS::XY_UNIT_VECTORS = "0 0";
$DOORS::PLAYER_REACH = 4.5;
$DOORS::OPEN_INCREMENT = 0.02;
$DOORS::CLOSE_INCREMENT = 0.04;
$DOORS::OPEN_TIMER = 20;
$DOORS::CLOSE_TIMER = 10;
$DOORS::HOLD_TIMER = 2000;
$DOORS::MAX_ANGLE = 90; //  degrees

datablock StaticShapeData(DoorLecture)
{
   category = "Doors";
   shapeFile = "~/data/doors/doors_lecture/door.dts";
};
function DoorLecture::OnAdd(%theDatablock, %whichDoor)
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
function DoorLecture::Operate(%theDatablock, %whichDoor) {
	%can_open = 0;
   	// big lecture hall on right door RIGHT
	if ( %whichDoor.getPosition() $= "80.422 77.931 68.7" ) {
		if ( $Tokens_Collected > 15 ) {
			%can_open = 1;
		}
		else {
			%display = 16;
		}
	}
	 // big lecture hall on right door LEFT
	else if (%whichDoor.getPosition() $= "55.078 77.813 68.7" ) {
		if ( $Tokens_Collected > 15) {
			%can_open = 1;
		}
		else {
			%display = 16;
		}
	}
  // big lecture hall on LEFT door RIGHT
	else if (%whichDoor.getPosition() $= "2.908 77.781 68.7" ) {
		if ( $Tokens_Collected > 6) {
			%can_open = 1;
		}
		else {
			%display = 7;
		}
	}
   	// big lecture hall on LEFT door LEFT
	else if (%whichDoor.getPosition() $= "-22.271 77.791 68.7" ) {
		if ( $Tokens_Collected > 6) {
			%can_open = 1;
		}
		else {
			%display = 7;
		}
	}
   	// LECTURE HALL BESIDE AC104
	else if (%whichDoor.getPosition() $= "-137.743 51.918 68.7" ) {
		if ( $Tokens_Collected > 18) {
			%can_open = 1;
		}
		else {
			%display = 19;
		}
	}
   	 // GEOGRRAPHY LECTURE HALL
	else if (%whichDoor.getPosition() $= "-193.59 54.657 68.7" ) {
		if ( $Tokens_Collected > 14) {
			%can_open = 1;
		}
		else {
			%display = 15;
		}
	}
	// LECTURE HALL ON FAR RIGHT DOOR LEFT
	else if (%whichDoor.getPosition() $= "256.948 55.528 68.7" ) {
		if ( $Tokens_Collected > 1) {
			%can_open = 1;
		}
		else {
			%display = 2;
		}
	}
   	// LECTURE HALL ON FAR RIGHT DOOR RIGHT
	else if (%whichDoor.getPosition() $= "282.148 55.528 68.7" ) {
		if ( $Tokens_Collected > 1) {
			%can_open = 1;
		}
		else {
			%display = 2;
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

function DoorLecture::StartOpenSwing(%theDatablock, %whichDoor)
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

function DoorLecture::IncrementSwing(%theDatablock, %whichDoor)
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

function DoorLecture::StartCloseSwing(%theDatablock, %whichDoor)
{
  %theDatablock.schedule($DOORS::CLOSE_TIMER,"DecrementSwing", %whichDoor);
  PlayMusic(DoorClosing);
}

function DoorLecture::DecrementSwing(%theDatablock, %whichDoor)
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
