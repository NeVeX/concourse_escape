//-----------------------------------------------------------------------------
// Torque Game Engine
//
// Copyright (c) 2001 GarageGames.Com
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Function to construct and initialize the default canvas window
// used by the games

function InitCanvas(%windowName)
{
   videoSetGammaCorrection($pref::OpenGL::gammaCorrection);
   if (!createCanvas(%windowName)) {
      quit();
      return;
   }


   setOpenGLTextureCompressionHint( $pref::OpenGL::compressionHint );
   setOpenGLAnisotropy( $pref::OpenGL::anisotropy );
   setOpenGLMipReduction( $pref::OpenGL::mipReduction );
   setOpenGLInteriorMipReduction( $pref::OpenGL::interiorMipReduction );
   setOpenGLSkyMipReduction( $pref::OpenGL::skyMipReduction );

   // Declare default GUI Profiles.
   Exec("~/ui/defaultProfiles.cs");
   // Common GUI's
   Exec("~/ui/GuiEditorGui.gui");
   Exec("~/ui/ConsoleDlg.gui");
   Exec("~/ui/InspectDlg.gui");
   Exec("~/ui/LoadFileDlg.gui");
   Exec("~/ui/SaveFileDlg.gui");
   Exec("~/ui/MessageBoxOkDlg.gui");
   Exec("~/ui/MessageBoxYesNoDlg.gui");
   Exec("~/ui/MessageBoxOKCancelDlg.gui");
   Exec("~/ui/MessagePopupDlg.gui");
   Exec("~/ui/HelpDlg.gui");
   Exec("~/ui/RecordingsDlg.gui");

   // Commonly used helper scripts
   Exec("./metrics.cs");
   Exec("./messageBox.cs");
   Exec("./screenshot.cs");
   Exec("./cursor.cs");
   Exec("./help.cs");
   Exec("./recordings.cs");

   // Init the audio system
   OpenALInit();
}

function resetCanvas()
{
   if (IsObject(Canvas))
   {
      Canvas.repaint();
   }
}
