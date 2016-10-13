//============================================================================
// control/client/misc/presetkeys.cs
//
// Copyright (c) 2003 Kenneth C. Finney
//============================================================================

if ( IsObject(PlayerKeymap) )  // If we already have a player key map,
   PlayerKeymap.delete();        // delete it so that we can make a new one
new ActionMap(PlayerKeymap);

$movementSpeed = 1;             // m/s   for use by movement functions


//------------------------------------------------------------------------------
// Non-remapable binds
//------------------------------------------------------------------------------

function DoExitGame(%val) {
	if ( $info_screen_showing == 0 && %val && $Playing == 0 && $Menu_Location == 0) {
   MessageBoxYesNo( "Quit Game", "Exit From The Game?", "Quit();", "");
  }
  else if ( $Playing == 1 && %val ) {
  	MessageBoxYesNo( "Quit Mission", "Exit from this Mission\nAnd Return To The Menu?", "onMissionEnded();", "");
  }
  else if ( $Menu_Location == 1 && $Playing == 0 && %val) {
  	ShowMenuScreen();
  }
  else if ( $Menu_Location == 2 && $Playing == 0 && %val) {
		changingScreens(2);
  }
  else if ( %val )  {
  	ShowMenuScreen();
  }
}


//============================================================================
// Motion Functions
//============================================================================

function GoLeft(%val)
//----------------------------------------------------------------------------
// "strafing"
//----------------------------------------------------------------------------
{
   $mvLeftAction = %val;
}

function GoRight(%val)
//----------------------------------------------------------------------------
// "strafing"
//----------------------------------------------------------------------------
{
   $mvRightAction = %val;
}

function GoAhead(%val)
//----------------------------------------------------------------------------
// running forward
//----------------------------------------------------------------------------
{
   $mvForwardAction = %val;
}

function BackUp(%val)
//----------------------------------------------------------------------------
// running backwards
//----------------------------------------------------------------------------
{
   $mvBackwardAction = %val;
}

function DoYaw(%val)
//----------------------------------------------------------------------------
// looking, spinning or aiming horizontally by mouse or joystick control
//----------------------------------------------------------------------------
{
   $mvYaw += %val * ($cameraFov / 90) * 0.02;
}

function DoPitch(%val)
//----------------------------------------------------------------------------
// looking vertically by mouse or joystick control
//----------------------------------------------------------------------------
{
   $mvPitch += %val * ($cameraFov / 90) * 0.02;
}

function DoJump(%val)
//----------------------------------------------------------------------------
// momentary upward movement, with character animation
//----------------------------------------------------------------------------
{
	echo("i am jumping");
   $mvTriggerCount2++;
}

//============================================================================
// View Functions
//============================================================================

function Toggle3rdPPOVLook( %val )
//----------------------------------------------------------------------------
// enable the "free look" feature. As long as the mapped key is pressed,
// the player can view his avatar by moving the mouse around.
//----------------------------------------------------------------------------
{
   if ( %val )
      $mvFreeLook = true;
   else
      $mvFreeLook = false;
}

function MouseAction(%val)
{
   $mvTriggerCount0++;
}

function Toggle1stPPOV(%val)
//----------------------------------------------------------------------------
// switch between 1st and 3rd person point-of-views.
//----------------------------------------------------------------------------
{
   if (%val)
   {
      $firstPerson = !$firstPerson;
   }
}


//============================================================================
// keyboard control mappings
//============================================================================
// these ones available when player is in game
PlayerKeymap.Bind( mouse, button0, MouseAction ); // left mouse button
PlayerKeymap.Bind(keyboard, up, GoAhead);
PlayerKeymap.Bind(keyboard, down, BackUp);
PlayerKeymap.Bind(keyboard, left, GoLeft);
PlayerKeymap.Bind(keyboard, right, GoRight);
PlayerKeymap.Bind(keyboard, numpad0, DoJump );
PlayerKeymap.Bind(mouse, button1, DoJump );
PlayerKeymap.Bind(keyboard, z, Toggle3rdPPOVLook );
PlayerKeymap.Bind(keyboard, tab, Toggle1stPPOV );
PlayerKeymap.Bind(mouse, xaxis, DoYaw );
PlayerKeymap.Bind(mouse, yaxis, DoPitch );
PlayerKeyMap.Bind(keyboard, enter, Operate );
PlayerKeyMap.Bind(keyboard, 1, weapons1);
PlayerKeyMap.Bind(keyboard, 2, weapons2);
PlayerKeyMap.Bind(keyboard, 3, weapons3);


// these ones are always available

GlobalActionMap.Bind(keyboard, escape, DoExitGame);
GlobalActionMap.Bind(keyboard, tilde, ToggleConsole);


function Operate() {
	commandToServer('Operate');
}

function weapons1( %val ) {
	echo("Changing Weapons1 ");
	if ( %val )
		commandToServer('use',"M16");
}
function weapons2( %val ) {
	echo("Changing Weapons2 ");
	if ( %val )
		commandToServer('use',"TommyGun");
}
function weapons3( %val ) {
	echo("Changing Weapons3 ");
	if ( %val )
		commandToServer('use',"CrossBow");
}

function dropCameraAtPlayer(%val)
{
   if (%val)
      commandToServer('dropCameraAtPlayer');
}

function dropPlayerAtCamera(%val)
{
   if (%val)
      commandToServer('DropPlayerAtCamera');
}
function toggleCamera(%val)
{
   if (%val)
   {
      commandToServer('ToggleCamera');
      }
}

PlayerKeymap.bind(keyboard, "F8", dropCameraAtPlayer);
PlayerKeymap.bind(keyboard, "F7", dropPlayerAtCamera);

PlayerKeymap.bind(keyboard, "F6", toggleCamera);