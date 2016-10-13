//============================================================================
// control/client/profiles.cs
//
// Copyright (c) 2003 Kenneth C. Finney
//============================================================================

new GuiControlProfile (GuiDefaultProfile)
{
   tab = false;
   canKeyFocus = false;
   hasBitmapArray = false;
   mouseOverSelected = false;

   // fill color
   opaque = false;
   fillColor = "201 182 153";
   fillColorHL = "221 202 173";
   fillColorNA = "221 202 173";

   // border color
   border = false;
   borderColor   = "0 0 0";
   borderColorHL = "179 134 94";
   borderColorNA = "126 79 37";

   // font
   fontType = "Arial";
   fontSize = 14;

   fontColor = "0 0 0";
   fontColorHL = "32 100 100";
   fontColorNA = "0 0 0";
   fontColorSEL= "200 200 200";

   // bitmap information
   bitmap = "./interfaces/emaga_windowwidgets";
   bitmapBase = "";
   textOffset = "0 0";

   // used by guiTextControl
   modal = true;
   justify = "left";
   autoSizeWidth = false;
   autoSizeHeight = false;
   returnTab = false;
   numbersOnly = false;
   cursorColor = "0 0 0 255";

   // sounds
   soundButtonDown = "";
   soundButtonOver = "";
};

new GuiControlProfile (GuiWindowProfile)
{
   opaque = true;
   border = 2;
   fillColor = "151 152 250";
   fillColorHL = "151 152 173";
   fillColorNA = "151 152 173";
   fontColor = "255 255 255";
   fontColorHL = "255 255 255";
   bitmap = "./interfaces/emaga_windowwidgets";
   textOffset = "6 6";
   hasBitmapArray = true;
   justify = "center";
};

new GuiControlProfile (GuiScrollProfile)
{
   opaque = true;
   fillColor = "255 255 255";
   border = 3;
   borderThickness = 2;
   borderColor = "0 0 0";
   bitmap = "./interfaces/emaga_scrollwidgets";
   hasBitmapArray = true;
};

new GuiControlProfile (GuiCheckBoxProfile)
{
   opaque = false;
   fillColor = "232 232 232";
   border = false;
   borderColor = "0 0 0";
   fontSize = 14;
   fontColor = "0 0 0";
   fontColorHL = "32 100 100";
   fixedExtent = true;
   justify = "left";
   bitmap = "./interfaces/emaga_chkboxwidgets";
   hasBitmapArray = true;
};

new GuiControlProfile (GuiButtonProfile)
{
   opaque = false;
   fillColor = ($platform $= "macos") ? "11 11 211" : "0 0 0";
   fillColorHL = ($platform $= "macos") ? "144 44 244" : "20 20 250";
   fillColorNA = ($platform $= "macos") ? "144 44 244" : "20 20 250";
   border = true;
   fontColor = "250 0 0";
   fontColorHL = "255 250 250";
   fixedExtent = true;
   justify = "center";
	canKeyFocus = false;
};
new GuiControlProfile (GuiButtonProfile2)
{
   opaque = false;
   fillColor = ($platform $= "macos") ? "11 11 211" : "0 0 0";
   fillColorHL = ($platform $= "macos") ? "144 44 244" : "250 0 250";
   fillColorNA = ($platform $= "macos") ? "144 44 244" : "0 250 250";
   border = true;
   fontColor = "255 250 250";
   fontColorHL = "255 0 0";
   fixedExtent = true;
   justify = "center";
	canKeyFocus = false;
};

new GuiControlProfile (GuiRadioProfile)
{
   fontSize = 14;
   fillColor = "232 232 232";
   fontColorHL = "32 100 100";
   fixedExtent = true;
   bitmap = "./interfaces/emaga_radiowidgets";
   hasBitmapArray = true;
};

