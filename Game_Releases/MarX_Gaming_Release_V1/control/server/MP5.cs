//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Mp5 Weapon Script
// Copyright (C) DrewFX 2006
// Uses some ports of GameBeavers MP5 weapon script

//-----------------------------------------------------------------------------
// MP5 weapon. This file contains all the items related to this weapon
// including explosions, ammo, the item and the weapon item image.
// These objects rely on the item & inventory support system defined
// in item.cs and inventory.cs
//-----------------------------------------------------------------------------


//-----------------------------------------------------------------------------
// Sounds profiles

datablock AudioProfile(MP5ReloadSound)
{
filename = "~/data/sound/mp5/WeaponReload.wav"; 
description = AudioClose3d;
preload = true;
};

datablock AudioProfile(MP5FireSound)
{
filename = "~/data/sound/mp5/MP5_Fire.wav";
description = AudioClose3d;
preload = true;
};

datablock AudioProfile(MP5FireEmptySound)
{
filename = "~/data/sound/mp5/crossbow_firing_empty.wav";
description = AudioClose3d;
preload = true;
};

datablock AudioProfile(MP5ExplosionSound)
{
filename = "~/data/sound/mp5/MetalThud.wav";
description = AudioDefault3d;
preload = true;
};
//-----------------------------------------------------------------------------
// Weapon fire emitter

datablock ParticleData(mp5FireParticle)
{
   textureName          = "~/data/MP5/muz3";


   dragCoefficient     = 0.0;
   gravityCoefficient   = -0.1;  // rises
   inheritedVelFactor   = 0.3;

   lifetimeMS           = 75;   // Time in ms
   lifetimeVarianceMS   = 50;    // ...more or less

   useInvAlpha = false;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   colors[0]     = "0.7 0.8 0.1 0.5";
   colors[1]     = "0.7 0.8 0.3 0.5";
   colors[2]     = "0 0 0 0";

   sizes[0]      = 0.2;
   sizes[1]      = 0.5;
   sizes[2]      = 1;

   times[0]      = 0.0;
   times[1]      = 0.3;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(mp5FireEmitter)
{
   ejectionPeriodMS = 30;
   periodVarianceMS = 0;

   ejectionVelocity = 2;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 0.0;

   particles = mp5FireParticle;
};

//-----------------------------------------------------------------------------
// MP5 projectile splash

datablock ParticleData(MP5SplashMist)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "~/data/particles/splash";
   
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.8;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(MP5SplashMistEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 2.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   lifetimeMS       = 250;
   particles = "MP5SplashMist";
};

datablock ParticleData( MP5SplashParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( MP5SplashEmitter )
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 60;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "MP5SplashParticle";
};

datablock SplashData(MP5Splash)
{
   numSegments = 15;
   ejectionFreq = 15;
   ejectionAngle = 40;
   ringLifetime = 0.5;
   lifetimeMS = 300;
   velocity = 4.0;
   startRadius = 0.0;
   acceleration = -3.0;
   texWrap = 5.0;

   texture = "~/data/MP5/splash";

   emitter[0] = MP5SplashEmitter;
   emitter[1] = MP5SplashMistEmitter;

   colors[0] = "0.7 0.8 1.0 0.0";
   colors[1] = "0.7 0.8 1.0 0.3";
   colors[2] = "0.7 0.8 1.0 0.7";
   colors[3] = "0.7 0.8 1.0 0.0";
   times[0] = 0.0;
   times[1] = 0.4;
   times[2] = 0.8;
   times[3] = 1.0;
};

//-----------------------------------------------------------------------------
// MP5 projectile particles

datablock ParticleData(MP5BoltParticle)
{
   textureName          = "~/data/particles/smoke";
   dragCoefficient     = 0.0;
   gravityCoefficient   = -0.1;   // rises slowly
   inheritedVelFactor   = 0.0;
   lifetimeMS           = 150;
   lifetimeVarianceMS   = 10;   // ...more or less
   useInvAlpha = false;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   colors[0]     = "0.1 0.1 0.1 1.0";
   colors[1]     = "0.1 0.1 0.1 1.0";
   colors[2]     = "0.1 0.1 0.1 0";

   sizes[0]      = 0.15;
   sizes[1]      = 0.20;
   sizes[2]      = 0.25;

   times[0]      = 0.0;
   times[1]      = 0.3;
   times[2]      = 1.0;
};

datablock ParticleData(MP5BubbleParticle)
{
   textureName          = "~/data/particles/bubble";
   dragCoefficient      = 0.0;
   gravityCoefficient   = -0.25;   // rises slowly
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1500;
   lifetimeVarianceMS   = 600;    // ...more or less
   useInvAlpha          = false;
   spinRandomMin        = -100.0;
   spinRandomMax        = 100.0;

   colors[0]     = "0.7 0.8 1.0 0.4";
   colors[1]     = "0.7 0.8 1.0 1.0";
   colors[2]     = "0.7 0.8 1.0 0.0";

   sizes[0]      = 0.2;
   sizes[1]      = 0.2;
   sizes[2]      = 0.2;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(MP5BoltEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionVelocity = 0.0;
   velocityVariance = 0.10;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = MP5BoltParticle;
};

datablock ParticleEmitterData(MP5BoltBubbleEmitter)
{
   ejectionPeriodMS = 9;
   periodVarianceMS = 0;

   ejectionVelocity = 1.0;
   ejectionOffset   = 0.1;
   velocityVariance = 0.5;

   thetaMin         = 0.0;
   thetaMax         = 80.0;

   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;  

   particles = MP5BubbleParticle;
};

//-----------------------------------------------------------------------------
// Mp5 Explosion

datablock ParticleData(MP5ExplosionFire)
{
   textureName          = "~/data/particles/fire";
   dragCoeffiecient     = 100.0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.25;
   constantAcceleration = 0.1;
   lifetimeMS           = 1200;
   lifetimeVarianceMS   = 300;
   useInvAlpha =  false;
   spinRandomMin = -80.0;
   spinRandomMax =  80.0;

   colors[0]     = "0.8 0.4 0 0.8";
   colors[1]     = "0.2 0.0 0 0.8";
   colors[2]     = "0.0 0.0 0.0 0.0";

   sizes[0]      = 1.5;
   sizes[1]      = 0.9;
   sizes[2]      = 0.5;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(MP5ExplosionFireEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 0.8;
   velocityVariance = 0.5;
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   lifetimeMS       = 250;
   particles = "MP5ExplosionFire";
};

datablock ExplosionData(MP5Explosion)
{
   soundProfile = MP5ExplosionSound;

   lifeTimeMS = 1500;

   particleEmitter = MP5ExplosionFireEmitter;
   particleDensity = 50;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 6;
   lightEndRadius = 0;
   lightStartColor = "0.8 0.6 0.1";
   lightEndColor = "0 0 0";
 
};

//-----------------------------------------------------------------------------
// Projectile Object

datablock ProjectileData(MP5Projectile)
{
   projectileShapeName = "~/data/MP5/Mp5_Projectile_DTS.dts";
   directDamage        = 20;
   radiusDamage        = 20;
   damageRadius        = 1.5;
   areaImpulse         = 200;

   explosion           = MP5Explosion;
   
   particleEmitter     = mp5SmokeEmitter;
   

   splash              = MP5Splash;

   muzzleVelocity      = 100;
   velInheritFactor    = 0.3;

   armingDelay         = 0;
   lifetime            = 5000;
   fadeDelay           = 5000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.80;

   hasLight    = true;
   lightRadius = 4;
   lightColor  = "0.5 0.5 0.25";

   hasWaterLight     = true;
   waterLightColor   = "0 0.5 0.5";
};

function MP5Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
   // Apply damage to the object all shape base objects
   if (%col.getType() & $TypeMasks::ShapeBaseObjectType)
      %col.damage(%obj,%pos,%this.directDamage,"MP5Bolt");

   // Radius damage is a support scripts defined in radiusDamage.cs
   // Push the contact point away from the contact surface slightly
   // along the contact normal to derive the explosion center. -dbs
   radiusDamage(%obj, %pos, %this.damageRadius, %this.radiusDamage, "Radius", %this.areaImpulse);
}


//--------------------------------------------------------------------------
// mp5 shell that's ejected during reload.

datablock DebrisData(MP5Shell)
{
   shapeFile = "~/data/MP5/BulletCasing_DTS.dts";
   lifetime = 3.0;
   minSpinSpeed = 300.0;
   maxSpinSpeed = 400.0;
   elasticity = 0.5;
   friction = 0.2;
   numBounces = 4;
   staticOnMaxBounce = true;
   snapOnMaxBounce = false;
   fade = true;
};
//-----------------------------------------------------------------------------
// Ammo Item

datablock ItemData(MP5Ammo)
{
   // Mission editor category
   category = "Ammo";

   // Add the Ammo namespace as a parent.  The ammo namespace provides
   // common ammo related functions and hooks into the inventory system.
   className = "Ammo";

   // Basic Item properties
   shapeFile = "~/data/MP5/MP5_Clip_DTS.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;

   // Dynamic properties defined by the scripts
   pickUpName = "MP5 rounds";
   maxInventory = 35;
};


//--------------------------------------------------------------------------
// Weapon Item.  This is the item that exists in the world, i.e. when it's
// been dropped, thrown or is acting as re-spawnable item.  When the weapon
// is mounted onto a shape, the MP5Image is used.

datablock ItemData(MP5)
{
   // Mission editor category
   category = "Weapon";

   // Hook into Item Weapon class hierarchy. The weapon namespace
   // provides common weapon handling functions in addition to hooks
   // into the inventory system.
   className = "Weapon";

   // Basic Item properties
   shapeFile = "~/data/MP5/MP5_dts.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   emap = true;

	// Dynamic properties defined by the scripts
	pickUpName = "an MP5";
	image = MP5Image;
};


//--------------------------------------------------------------------------
// MP5 image which does all the work.  Images do not normally exist in
// the world, they can only be mounted on ShapeBase objects.

datablock ShapeBaseImageData(MP5Image)
{
   // Basic Item properties
   shapeFile = "~/data/MP5/MP5_dts.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   eyeOffset = "0.14 0.08 -0.25";
   offset = "0 0 0";
   
   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = MP5;
   ammo = MP5Ammo;
   projectile = MP5Projectile;
   projectileType = Projectile;
   //casing = MP5Shell;

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
   stateName[0]                     = "Preactivate";
   stateTransitionOnLoaded[0]       = "Activate";
   stateTransitionOnNoAmmo[0]       = "NoAmmo";

   // Activating the gun.  Called when the weapon is first
   // mounted and there is ammo.
   stateName[1]                     = "Activate";
   stateTransitionOnTimeout[1]      = "Ready";
   stateTimeoutValue[1]             = 0.5;
   stateSequence[1]                 = "Activate";

   // Ready to fire, just waiting for the trigger
   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   // Fire the weapon. Calls the fire script which does
   // the actual work.
   stateName[3]                     = "Fire";
   stateTransitionOnTimeout[3]      = "Reload";
   stateTimeoutValue[3]             = 0.05;
   stateFire[3]                     = true;
   stateRecoil[3]                   = LightRecoil;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire";
   stateScript[3]                   = "onFire";
   stateEmitter[3]                  = mp5FireEmitter;
   stateEmitterTime[3]              = 0.15;
   stateSound[3]                    = MP5FireSound;

   // Play the reload animation, and transition into
   stateName[4]                     = "Reload";
   stateTransitionOnNoAmmo[4]       = "NoAmmo";
   stateTransitionOnTimeout[4]      = "Ready";
   stateTimeoutValue[4]             = 0.05;
   stateAllowImageChange[4]         = false;
   //stateSequence[4]                 = "Reload";
   stateEjectShell[4]               = true;

   // No ammo in the weapon, just idle until something
   // shows up. Play the dry fire sound if the trigger is
   // pulled.
   stateName[5]                     = "NoAmmo";
   stateTransitionOnAmmo[5]         = "Reload";
   stateSequence[5]                 = "NoAmmo";
   stateTransitionOnTriggerDown[5]  = "DryFire";

   // No ammo dry fire
   stateName[6]                     = "DryFire";
   stateTimeoutValue[6]             = 0.5;
   stateTransitionOnTimeout[6]      = "NoAmmo";
   stateSound[6]                    = MP5FireEmptySound;
};


//-----------------------------------------------------------------------------

function MP5Image::onFire(%this, %obj, %slot)
{
   %projectile = %this.projectile;

   // Decrement inventory ammo. The image's ammo state is update
   // automatically by the ammo inventory hooks.
   %obj.decInventory(%this.ammo,1);

   // Determin initial projectile velocity based on the 
   // gun's muzzle point and the object's current velocity
   %muzzleVector = %obj.getMuzzleVector(%slot);
   %objectVelocity = %obj.getVelocity();
   %muzzleVelocity = VectorAdd(
      VectorScale(%muzzleVector, %projectile.muzzleVelocity),
      VectorScale(%objectVelocity, %projectile.velInheritFactor));

   // Create the projectile object
   %p = new (%this.projectileType)() {
      dataBlock        = %projectile;
      initialVelocity  = %muzzleVelocity;
      initialPosition  = %obj.getMuzzlePoint(%slot);
      sourceObject     = %obj;
      sourceSlot       = %slot;
      client           = %obj.client;
   };
   MissionCleanup.add(%p);
   return %p;
}
