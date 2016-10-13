
new AudioProfile(HeavyRainSound)
{
   fileName = "~/data/sound/rain.wav";
   description = "Rain";
};
new AudioProfile(Thunder1)
{
   fileName = "~/data/sound/thunder1.wav";
   description = "Thunder";
};
new AudioProfile(Thunder2)
{
   fileName = "~/data/sound/thunder2.wav";
   description = "Thunder";
};
new AudioProfile(Thunder3)
{
   fileName = "~/data/sound/thunder3.wav";
   description = "Thunder";
};
new AudioProfile(Thunder4)
{
   fileName = "~/data/sound/thunder4.wav";
   description = "Thunder";
};

datablock LightningData(LightningStorm)
{
   strikeTextures[0]  = "~/data/maps/lightning.dml";
   ThunderSounds[0] = Thunder1;
   ThunderSounds[1] = Thunder2;
   ThunderSounds[2] = Thunder3;
   ThunderSounds[3] = Thunder4;
};
// i dont like the rain for now
//datablock PrecipitationData(HeavyRain)
//{
   //type = 1;
   //materialList = "~/data/maps/rain.dml";
   //soundProfile = "HeavyRainSound";
   //sizeX = 0.1;
   //sizeY = 0.1;

   ///movingBoxPer = 0.35;
   //divHeightVal = 1.5;
  // sizeBigBox = 1;
   //topBoxSpeed = 20;
   //frontBoxSpeed = 30;
   //topBoxDrawPer = 0.5;
   //bottomDrawHeight = 40;
   //skipIfPer = -0.3;
   //bottomSpeedPer = 1.0;
   //frontSpeedPer = 1.5;
   //frontRadiusPer = 0.5;
//};

