//-----------------------------------------------------------------------------
// Torque Engine
//
// Copyright (c) 2001 GarageGames.Com
//-----------------------------------------------------------------------------

function OpenALInit()
{
   OpenALShutdownDriver();

   Echo("");
   Echo("OpenAL Driver Init:");

   Echo ($pref::Audio::driver);

   if($pref::Audio::driver $= "OpenAL")
   {
      if(!OpenALInitDriver())
      {
         Error("   Failed to initialize driver.");
         $Audio::initFailed = true;
      } else {
         // this should go here
         Echo("   Vendor: " @ alGetString("AL_VENDOR"));
         Echo("   Version: " @ alGetString("AL_VERSION"));
         Echo("   Renderer: " @ alGetString("AL_RENDERER"));
         Echo("   Extensions: " @ alGetString("AL_EXTENSIONS"));

         alxListenerf( AL_GAIN_LINEAR, $pref::Audio::masterVolume );

         for (%channel=1; %channel <= 8; %channel++)
            alxSetChannelVolume(%channel, $pref::Audio::channelVolume[%channel]);

         Echo("");
      }
   }

}


//--------------------------------------------------------------------------

function OpenALShutdown()
{
   OpenALShutdownDriver();
   alxStopAll();
   AudioGui.delete();
   soundButtonOver.delete();
}
