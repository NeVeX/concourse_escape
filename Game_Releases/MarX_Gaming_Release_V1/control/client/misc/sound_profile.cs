

$GuiAudioType     = 1;
$SimAudioType     = 2;
$MessageAudioType = 3;
$MusicAudioType = 4;
$DukeAudioType1 = 5;
$InGameMusicType = 6;
$M16 = 7;
$Crossbow = 8;
$CrossBowExp = 11;
$TommyGun = 9;
$Door_Sounds = 10;
$BeastDeathAudioType = 12;
$MusicAudioTypeIntro = 14;
$SoundWeather = 16;
$SoundWeather2 = 18;
$MessageAudioType2 = 23;
$DukeAudioType22 = 35;

new AudioDescription(Duke)
{
   volume   = 1.3;
   isLooping= false;
   is3D     = false;
   type     = $DukeAudioType;
};
new AudioDescription(Duke5)
{
   volume   = 13.3;
   isLooping= false;
   is3D     = false;
   type     = $DukeAudioType22;
};
new AudioDescription(Rain)
{
   volume   = 1.0;
   isLooping= true;
   is3D     = true;
   type     = $SoundWeather2;
};
new AudioDescription(Thunder)
{
   volume   = 1.5;
   isLooping= false;
   is3D     = true;
   type     = $SoundWeather;
};
new AudioDescription(Beast_Death)
{
   volume   = 6.2;
   isLooping= false;
   is3D     = false;
   type     = $BeastDeathAudioType;
};
new AudioDescription(CrossbowExplosion)
{
   volume   = 20.2;
   isLooping= false;
   is3D     = false;
   type     = $CrossBowExp;
};
new AudioDescription(M16_Audio)
{
   volume   = 0.7;
   isLooping= false;
   is3D     = false;
   type     = $M16;
};
new AudioDescription(Crossbow_Audio)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = $Crossbow;
};
new AudioDescription(TommyGun_Audio)
{
   volume   = 1.4;
   isLooping= false;
   is3D     = false;
   type     = $TommyGun;
};
new AudioDescription(GameMusic)
{
   volume   = 1.0;
   isLooping= true;
   is3D     = false;
   type     = $InGameMusicType;
};
new AudioDescription(AudioGui)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = $GuiAudioType;
};

new AudioDescription(AudioMessage)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = $MessageAudioType;
};
new AudioDescription(Doors)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = $Door_Sounds;
};
new AudioProfile(DoorOpening)
{
   fileName = "~/data/sound/doors/DoorOpening.wav";
   description = "Doors";
   preload = true;
};

new AudioProfile(DoorClosing)
{
   fileName = "~/data/sound/doors/DoorClosing.wav";
   description = "Doors";
   preload = true;
};

new AudioProfile(DoorShut)
{
   fileName = "~/data/sound/doors/DoorShut.wav";
   description = "Doors";
   preload = true;
};
new AudioProfile(CrossbowExplosion2)
{
   fileName = "~/data/sound/Crossbow/CrossbowExplosion.wav";
   description = "CrossbowExplosion";
   preload = true;
};
new AudioProfile(M16_Fire)
{
   filename = "~/data/sound/M16/M16_Fire.wav";
   description = "M16_Audio";
	 preload = true;
};
new AudioProfile(Crossbow_Fire)
{
   filename = "~/data/sound/Crossbow/Crossbow_Fire.wav";
   description = "Crossbow_Audio";
	 preload = true;
};
new AudioProfile(TommyGun_Fire)
{
   filename = "~/data/sound/TommyGun/TommyGun_Fire.wav";
   description = "TommyGun_Audio";
	 preload = true;
};
new AudioProfile(MarX_Pro)
{
   filename = "~/data/sound/MarX_Production.wav";
   description = "AudioGui";
	 preload = true;
};

new AudioDescription(MusicLooping) {
	volume = 1.0;
	isLooping= true;
	is3D = false;
	type = $MusicAudioType;
};
new AudioDescription(MusicLooping2) {
	volume = 1.4;
	isLooping= true;
	is3D = false;
	type = $MusicAudioType2;
};
new AudioDescription(MusicIntro) {
	volume = 1.0;
	isLooping = false;
	is3D = false;
	type = $MusicAudioTypeIntro;
};
new AudioProfile(Music_MainMenu) {
	filename = "~/data/sound/menu_music.wav";
	description = "MusicLooping";
	preload = true;
};
new AudioProfile(WalkAround) {
	filename = "~/data/sound/walk_around.wav";
	description = "MusicLooping2";
	preload = true;
};
new AudioProfile(Duke_Groovy) {
	filename = "~/data/sound/duke/Duke_Groovy.wav";
	description = "Duke";
	preload = true;
};
new AudioProfile(Duke_Lets_Rock) {
	filename = "~/data/sound/duke/Duke_Lets_Rock.wav";
	description = "Duke";
	preload = true;
};
new AudioProfile(Duke_Kill_1) {
	filename = "~/data/sound/duke/Kill_1.wav";
	description = "Duke";
	preload = true;
};
new AudioProfile(Duke_Kill_2) {
	filename = "~/data/sound/duke/Kill_2.wav";
	description = "Duke";
	preload = true;
};
new AudioProfile(Duke_Kill_3) {
	filename = "~/data/sound/duke/Kill_3.wav";
	description = "Duke";
	preload = true;
};
new AudioProfile(Duke_Death) {
	filename = "~/data/sound/duke/death.wav";
	description = "Duke";
	preload = true;
};
new AudioProfile(Duke_Pickup1)
{
   fileName = "~/data/sound/duke/pickup/pickup1.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Pickup2)
{
   fileName = "~/data/sound/duke/pickup/pickup2.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Pickup3)
{
   fileName = "~/data/sound/duke/pickup/pickup3.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Pickup4)
{
   fileName = "~/data/sound/duke/pickup/pickup4.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Pickup5)
{
   fileName = "~/data/sound/duke/pickup/pickup5.wav";
   description = "Duke5";
   preload = true;
};
new AudioProfile(Duke_Pickup6)
{
   fileName = "~/data/sound/duke/pickup/pickup6.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Pickup7)
{
   fileName = "~/data/sound/duke/pickup/pickup7.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Spawned_Enemies1)
{
   fileName = "~/data/sound/duke/enemy_spawn/SpawnEnemies1.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Spawned_Enemies2)
{
   fileName = "~/data/sound/duke/enemy_spawn/SpawnEnemies2.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Spawned_Enemies3)
{
   fileName = "~/data/sound/duke/enemy_spawn/SpawnEnemies3.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Spawned_Enemies4)
{
   fileName = "~/data/sound/duke/enemy_spawn/SpawnEnemies4.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Spawned_Enemies5)
{
   fileName = "~/data/sound/duke/enemy_spawn/SpawnEnemies5.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Spawned_Enemies6)
{
   fileName = "~/data/sound/duke/enemy_spawn/SpawnEnemies6.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Spawned_Enemies7)
{
   fileName = "~/data/sound/duke/enemy_spawn/SpawnEnemies7.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Pain1)
{
   fileName = "~/data/sound/duke/pain/pain1.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Pain2)
{
   fileName = "~/data/sound/duke/pain/pain2.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Pain3)
{
   fileName = "~/data/sound/duke/pain/pain3.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Pain4)
{
   fileName = "~/data/sound/duke/pain/pain4.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Pain5)
{
   fileName = "~/data/sound/duke/pain/pain5.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Door_Closed)
{
   fileName = "~/data/sound/duke/door_not_open.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Duke_Kick_Ass)
{
   fileName = "~/data/sound/duke/duke_kick_ass.wav";
   description = "Duke";
   preload = true;
};
new AudioProfile(Beast1_Death)
{
   fileName = "~/data/sound/deaths/Beast1_Death.wav";
   description = "Beast_Death";
   preload = true;
};
new AudioProfile(Beast2_Death)
{
   fileName = "~/data/sound/deaths/Beast2_Death.wav";
   description = "Beast_Death";
   preload = true;
};
new AudioProfile(Beast3_Death)
{
   fileName = "~/data/sound/deaths/Beast3_Death.wav";
   description = "Beast_Death";
   preload = true;
};
new AudioProfile(Beast4_Death)
{
   fileName = "~/data/sound/deaths/Beast4_Death.wav";
   description = "Beast_Death";
   preload = true;
};
new AudioProfile(In_Game_Music) {
	filename = "~/data/sound/MarX_Music.wav";
	description = "GameMusic";
	preload = true;
};
new AudioProfile(Viewer_Discretion) {
	filename = "~/data/sound/viewer_discretion.wav";
	description = "MusicIntro";
	preload = true;
};
new AudioProfile(Game_Over) {
	filename = "~/data/sound/game_over.wav";
	description = "MusicIntro";
	preload = true;
};

function PlayMusic(%handle) {
	if ( %handle $= "Music_MainMenu" || %handle $= "In_Game_Music" || %handle $= "Viewer_Discretion"
		|| %handle $= "WalkAround" || %handle $= "Game_Over" || %handle $= "Duke_Pickup5" ) {
		alxPlay(%handle);
	}
	else if ( $Playing == 1 ) {
	// this if is to make sure duke does not talk all over the place
  if ( %handle $= "Duke_Pickup1" || %handle $= "Duke_Pickup2" || %handle $= "Duke_Pickup3" || %handle $= "Duke_Pickup4" ||
  				%handle $= "Duke_Pickup5" || %handle $= "Duke_Pickup6" || %handle $= "Duke_Pickup7" || %handle $= "Duke_Kill_1" || 
  				%handle $= "Duke_Kill_2" || %handle $= "Duke_Kill_3" || %handle $= "Duke_Death" || %handle $= "Duke_Spawned_Enemies1"
  				|| %handle $= "Duke_Spawned_Enemies2" || %handle $= "Duke_Spawned_Enemies3" || %handle $= "Duke_Spawned_Enemies4" 
  				|| %handle $= "Duke_Spawned_Enemies5" || %handle $= "Duke_Spawned_Enemies6" || %handle $= "Duke_Spawned_Enemies7"
  				|| %handle $= "Duke_Pain1" || %handle $= "Duke_Pain2" || %handle $= "Duke_Pain3" || %handle $= "Duke_Pain4"
  				|| %handle $= "Duke_Pain5" || %handle $= "Duke_Door_Closed" || %handle $= "Duke_Groovy" || %handle $= "Duke_Kick_Ass" ) {
  	if (  $duke_sound == 0 && $Lives_Left > 0) {
  		$duke_sound = 1;
	     alxPlay(%handle);
	     schedule(2000, 0, "duke_sound_change");
	    }
	  
    }
  else if (!alxIsPlaying(%handle)) {
     alxPlay(%handle);
    }
  }
}

function duke_sound_change() {
	 $duke_sound = 0;
	}

function StopMusic(%handle) {
   if (alxIsPlaying(%handle))
     alxStop(%handle);
}



