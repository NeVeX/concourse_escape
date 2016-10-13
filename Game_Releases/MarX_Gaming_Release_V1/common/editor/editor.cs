//-----------------------------------------------------------------------------
// Torque Game Engine
// 
// Copyright (c) 2001 GarageGames.Com
// Portions Copyright (c) 2001 by Sierra Online, Inc.
//-----------------------------------------------------------------------------


//------------------------------------------------------------------------------
// Hard coded images referenced from C++ code
//------------------------------------------------------------------------------

//   editor/SelectHandle.png
//   editor/DefaultHandle.png
//   editor/LockedHandle.png


//------------------------------------------------------------------------------
// Functions
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Mission Editor 
//------------------------------------------------------------------------------

function Editor::create()
{
   // Not much to do here, build it and they will come...
   // Only one thing... the editor is a gui control which
   // expect the Canvas to exist, so it must be constructed
   // before the editor.
   new EditManager(Editor)
   {
      profile = "GuiContentProfile";
      horizSizing = "right";
      vertSizing = "top";
      position = "0 0";
      extent = "640 480";
      minExtent = "8 8";
      visible = "1";
      setFirstResponder = "0";
      modal = "1";
      helpTag = "0";
      open = false;
   };
}


function Editor::onAdd(%this)
{
   // Basic stuff
   Exec("./cursors.cs");

   // Tools
   Exec("./editor.bind.cs");
   Exec("./ObjectBuilderGui.gui");

   // New World Editor
   Exec("./EditorGui.gui");
   Exec("./EditorGui.cs");

   // World Editor
   Exec("./WorldEditorSettingsDlg.gui");

   // Terrain Editor
   Exec("./TerrainEditorVSettingsGui.gui");

   // Ignore Replicated fxStatic Instances.
   EWorldEditor.ignoreObjClass("fxShapeReplicatedStatic");

   // do gui initialization...
   EditorGui.init();

   //
   Exec("./editorRender.cs");
}

function Editor::checkActiveLoadDone()
{
   if(IsObject(EditorGui) && EditorGui.loadingMission)
   {
      Canvas.setContent(EditorGui);
      EditorGui.loadingMission = false;
      return true;
   }
   return false;
}

//------------------------------------------------------------------------------
function toggleEditor(%make)
{
   if (%make)
   {
      if (!$missionRunning) 
      {
         MessageBoxOK("Mission Required", "You must load a mission before starting the Mission Editor.", "");
         return;
      }

      if (!IsObject(Editor))
      {
         Editor::create();
         MissionCleanup.add(Editor);
      }
      if (Canvas.getContent() == EditorGui.getId())
         Editor.close();
      else
         Editor.open();
   }
}

//------------------------------------------------------------------------------
//  The editor action maps are defined in editor.bind.cs
//GlobalActionMap.bind(keyboard, "f11", toggleEditor);
