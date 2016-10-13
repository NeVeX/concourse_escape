EnableWinConsole(true);// send logging output to a windows console window
//------------------------------------------------------------------------
//  ./main.cs
//
//  root main module for 3DGPAI1 emaga chapter4 sample game
//
//  Copyright (c) 2003 by Kenneth C.  Finney.
//------------------------------------------------------------------------

// ========================================================================
// ========================= Initializations ==============================
// ========================================================================


$pref::Audio::driver = "OpenAL";
$pref::Audio::forceMaxDistanceUpdate = 0;
$pref::Audio::environmentEnabled = 0;
$pref::Audio::masterVolume   = 1.0;
$pref::Audio::channelVolume1 = 1.0;
$pref::Audio::channelVolume2 = 1.0;
$pref::Audio::channelVolume3 = 1.0;
$pref::Audio::channelVolume4 = 1.0;
$pref::Audio::channelVolume5 = 1.0;
$pref::Audio::channelVolume6 = 1.0;
$pref::Audio::channelVolume7 = 1.0;
$pref::Audio::channelVolume8 = 1.0;



$usageFlag =  false;  //help won't be displayed unless the command line
                      //switch ( -h ) is used

$logModeEnabled =  true; //track the logging state we set in the next line.
SetLogMode(2);   // overwrites existing logfile & closes log file at exit.

// ========================================================================
// ======================= Function Definitions ===========================
// ========================================================================

function OnExit()
//------------------------------------------------------------------------
// This is called from the common code modules. Any last gasp exit
// acticities we might want to perform can be put in this function.
// We need to provide a stub to prevent warnings in the log file.
//------------------------------------------------------------------------
{
}

function ParseArgs()
//------------------------------------------------------------------------
//  handle the command line arguments
//
//  this function is called from the common code
//
//------------------------------------------------------------------------
{
  for($i = 1;  $i  < $Game::argc ; $i++) //loop thru all command line args
  {
    $currentarg    = $Game::argv[$i];   // get current arg from the list
    $nextArgument       = $Game::argv[$i+1]; // get arg after the current one
    $nextArgExists = $Game::argc-$i > 1;// if there *is* a next arg,note that
    $logModeEnabled =  false;           // turn this off-let the args dictate
                                // if logging should be enabled.

    switch$($currentarg)
    {
      case "-?":       // the user wants command line help, so this cause the
        $usageFlag = true;   // Usage function to be run, instead of the game
        $argumentFlag[$i] = true;                // adjust the argument count

      case "-h":         // exactly the same as "-?"
        $usageFlag = true;
        $argumentFlag[$i] = true;
    }
  }
}

function Usage()
//------------------------------------------------------------------------
// Display the command line usage help
//------------------------------------------------------------------------
{
//  NOTE: any logging entries are written to the file 'console.log'
  Echo("\n\nemaga command line options:\n\n" @
         " -h, -?              display this message\n" );
}

function  LoadAddOns(%list)
//------------------------------------------------------------------------
// Exec each of the startup scripts for add-ons.
//------------------------------------------------------------------------
{
  if (%list $= "")
    return;
  %list = NextToken(%list, token, ";");
  LoadAddOns(%list);
  Exec(%token @  "/main.cs");
}

// ========================================================================
// ================ Module Body - Inline Statements =======================
// ========================================================================
//  Parse the command line  arguments
ParseArgs();

//  Either  display the help message or start the program.
if  ($usageFlag)
{
  EnableWinConsole(true);// send logging output to a windows console window
  Usage();
  EnableWinConsole(false);
  Quit();
}
else
{

  //  scan argument list, and log an Error message for each unused argument
  for ($i = 1;  $i  < $Game::argc; $i++)
  {
     if (!$argumentFlag[$i])
        Error("Error: Unknown command line argument:  " @ $Game::argv[$i]);
  }

  if  (!$logModeEnabled)
  {
     SetLogMode(6);      //  Default to  a new logfile each session.
  }
  //  Set the add-on path list to specify the directories that will be
  //  available to the scripts and engine. note that *all* required
  //  directory trees are included: common and control as well as the
  //  user add-ons.
  $pathList = $addonList !$= "" ? $addonList @ ";control;common" : "control;common";
  SetModPaths($pathList);

  // Execute startup script for the common code modules
  Exec("common/main.cs");

  // Execute startup script for the control specific code modules
  Exec("control/main.cs");

  // Execute startup scripts for all user add-ons
  Echo("--------- Loading Add-ons ---------");
  LoadAddOns($addonList);
  Echo("Engine initialization complete.");

  OnStart();
}
