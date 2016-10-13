//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Crossbow weapon. This file contains all the items related to this weapon
// including explosions, ammo, the item and the weapon item image.
// These objects rely on the item & inventory support system defined
// in item.cs and inventory.cs
//-----------------------------------------------------------------------------

datablock AudioProfile(M16FireSound)
{
   filename = "~/data/sound/crossbow_firing.wav";
   description = M16_Audio;
	preload = true;
};

//-----------------------------------------------------------------------------
// M16 ammo projectile splash

datablock ParticleData(M16SplashMist)
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
   textureName          = "~/data/soldier/splash";
   
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

datablock ParticleEmitterData(M16SplashMistEmitter)
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
   particles = "M16SplashMist";
};

datablock ParticleData( M16SplashParticle )
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

datablock ParticleEmitterData( M16SplashEmitter )
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
   particles = "M16SplashParticle";
};

datablock SplashData(M16Splash)
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

   texture = "~/data/soldier/splash";

   emitter[0] = M16SplashEmitter;
   emitter[1] = M16SplashMistEmitter;

   colors[0] = "0.7 0.8 1.0 0.0";
   colors[1] = "0.7 0.8 1.0 0.3";
   colors[2] = "0.7 0.8 1.0 0.7";
   colors[3] = "0.7 0.8 1.0 0.0";
   times[0] = 0.0;
   times[1] = 0.4;
   times[2] = 0.8;
   times[3] = 1.0;
};

datablock ParticleData(M16ExplosionSparks)
{
   textureName          = "~/data/soldier/spark";
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 250;
   lifetimeVarianceMS   = 50;

   sizes[0]      = 0.6;
   sizes[1]      = 0.5;
   sizes[2]      = 0.3;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(M16ExplosionSparkEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "M16ExplosionSparks";

   useEmitterSizes = false;
};


datablock ExplosionData(M16Explosion)
{
   //soundProfile = M16ExplosionSound;
   //lifeTimeMS = 1200;

   // Volume particles
   particleEmitter = M16ExplosionSparkEmitter;
   particleDensity = 15;
   particleRadius = 0.1;
   
   // Camera Shaking
   shakeCamera = true;

   // Impulse
   impulseRadius = 0.1;
   impulseForce = 15;

   // Dynamic light
   lightStartRadius = 1;
   lightEndRadius = 0.2;
   lightStartColor = "1.0 1.0 1.0";
   lightEndColor = "0 0 0";
};

datablock ExplosionData(M16WaterExplosion)
{
   //soundProfile = M16ExplosionSound;

   // Volume particles
   particleEmitter = M16ExplosionBubbleEmitter;
   particleDensity = 375;
   particleRadius = 1;


   // Point emission
   emitter[0] = M16ExplosionBubbleEmitter;
   emitter[1] = M16ExplosionWaterSparkEmitter;


   
   // Camera Shaking
   shakeCamera = false;

   // Impulse
   impulseRadius = 0.5;
   impulseForce = 15;

   // Dynamic light
   lightStartRadius = 1;
   lightEndRadius = 1;
   lightStartColor = "0 0.5 0.5";
   lightEndColor = "0 0 0";
};

//-----------------------------------------------------------------------------
// Projectile Object

datablock ProjectileData(M16Projectile)
{
   projectileShapeName = "~/data/soldier/tracer.dts";
   directDamage        = 10;
   radiusDamage        = 5;
   explosion           = M16Explosion;
   waterExplosion      = M16WaterExplosion;

   particleWaterEmitter= M16AmmoBubbleEmitter;

   splash              = M16Splash;

   muzzleVelocity      = 150;
   velInheritFactor    = 0.3;

   armingDelay         = 0;
   lifetime            = 5000;
   fadeDelay           = 5000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.10;

   hasLight    = false;
   lightRadius = 0.2;
   lightColor  = "1.5 1.5 1.25";

   hasWaterLight     = false;
   waterLightColor   = "0 0.5 0.5";
};

function M16Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
   // Apply damage to the object all shape base objects
   if (%col.getType() & $TypeMasks::ShapeBaseObjectType)
      %col.damage(%obj,%pos,%this.directDamage,"M16Ammo");

   // Radius damage is a support scripts defined in radiusDamage.cs
   // Push the contact point away from the contact surface slightly
   // along the contact normal to derive the explosion center. -dbs
   radiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %this.damageRadius,%this.radiusDamage,"Radius",20);
}


//-----------------------------------------------------------------------------
// Ammo Item

datablock ItemData(M16Ammo)
{
   // Mission editor category
   category = "Ammo";

   // Add the Ammo namespace as a parent.  The ammo namespace provides
   // common ammo related functions and hooks into the inventory system.
   className = "Ammo";

   // Basic Item properties
   shapeFile = "~/data/m16/ammo.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;

	// Dynamic properties defined by the scripts
	pickUpName = "M16 bullets";
   maxInventory = 200;
   
};
//--------------------------------------------------------------------------
// Weapon Item.  This is the item that exists in the world, i.e. when it's
// been dropped, thrown or is acting as re-spawnable item.  When the weapon
// is mounted onto a shape, the M16Image is used.

datablock ItemData(M16)
{
   // Mission editor category
   category = "Weapon";
   // Hook into Item Weapon class hierarchy. The weapon namespace
   // provides common weapon handling functions in addition to hooks
   // into the inventory system.
   className = "Weapon";

   // Basic Item properties
   shapeFile = "~/data/soldier/M16.dts";
   mass = 10;
   scale = "2.0 2.0 2.0";
   elasticity = 0.2;
   friction = 0.6;
   emap = true;

	// Dynamic properties defined by the scripts
	pickUpName = "a M16";
	image = M16Image;
};


//--------------------------------------------------------------------------
// M16 image which does all the work.  Images do not normally exist in
// the world, they can only be mounted on ShapeBase objects.

datablock ShapeBaseImageData(M16Image)
{
   // Basic Item properties
   shapeFile = "~/data/soldier/M16.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   eyeOffset = "0.7 1.0 -0.8";
		
   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = M16;
   ammo = M16Ammo;
   projectile = M16Projectile;
   projectileType = Projectile;

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
   stateTimeoutValue[1]             = 0.6;
   stateSequence[1]                 = "Activate";

   // Ready to fire, just waiting for the trigger
   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   // Fire the weapon. Calls the fire script which does 
   // the actual work.
   stateName[3]                     = "Fire";
   stateTransitionOnTimeout[3]      = "Reload";
   stateTimeoutValue[3]             = 0.1;
   stateFire[3]                     = true;
   stateRecoil[3]                   = LightRecoil;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire";
   stateScript[3]                   = "onFire";
   stateSound[3]                    = M16FireSound;

   // Play the relead animation, and transition into
   stateName[4]                     = "Reload";
   stateTransitionOnNoAmmo[4]       = "NoAmmo";
   stateTransitionOnTimeout[4]      = "Ready";
   stateTimeoutValue[4]             = 0.05;
   stateAllowImageChange[4]         = false;
   stateSequence[4]                 = "Reload";
   stateEjectShell[4]               = true;
   //stateSound[4]                    = M16ReloadSound;

   // No ammo in the weapon, just idle until something
   // shows up. Play the dry fire sound if the trigger is
   // pulled.
   stateName[5]                     = "NoAmmo";
   stateTransitionOnAmmo[5]         = "Reload";
   stateSequence[5]                 = "NoAmmo";
   stateTransitionOnTriggerDown[5]  = "DryFire";

   // No ammo dry fire
   stateName[6]                     = "DryFire";
   stateTimeoutValue[6]             = 1.0;
   stateTransitionOnTimeout[6]      = "NoAmmo";
  // stateSound[6]                    = M16FireEmptySound;
};


//-----------------------------------------------------------------------------

function M16Image::onFire(%this, %obj, %slot)
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
   //echo("obj is "@%obj @" and human is "@$Human);
   if ( $Human == %obj ) {
   	Ammobox.SetValue(%obj.getInventory(%this.ammo));
   	$Ammo_Left_now = %obj.getInventory(%this.ammo);
  }
   MissionCleanup.add(%p);
   return %p;
}