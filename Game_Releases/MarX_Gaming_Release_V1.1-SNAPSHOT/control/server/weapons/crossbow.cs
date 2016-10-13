//============================================================================
// control/players/crossbow.cs
//
// Copyright (c) 2003 Kenneth C. Finney
// Portions Copyright (c) 2001 GarageGames.Com
// Portions Copyright (c) 2001 by Sierra Online, Inc.
//============================================================================

//-----------------------------------------------------------------------------
// Crossbow weapon. This file contains all the items related to this weapon
// including explosions, ammo, the item and the weapon item image.
// These objects rely on the item & inventory support system defined
// in item.cs and inventory.cs
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Sounds profiles

datablock AudioProfile(CrossbowReloadSound)
{
   filename = "~/data/sound/crossbow/crossbow_reload.wav";
   description = CrossbowAudio;
	preload = true;
};

datablock AudioProfile(CrossbowFireSound)
{
   filename = "~/data/sound/crossbow/crossbow_firing.wav";
   description = CrossbowAudio;
	preload = true;
};

datablock AudioProfile(CrossbowFireEmptySound)
{
   filename = "~/data/sound/crossbow/crossbow_firing_empty.wav";
   description = CrossbowAudio;
	preload = true;
};

datablock AudioProfile(CrossbowExplosionSound)
{
   filename = "~/data/sound/crossbow/crossbow_explosion.wav";
   description = CrossbowAudio;
	preload = true;
};


//-----------------------------------------------------------------------------
// Crossbow bolt projectile particles

datablock ParticleData(CrossbowBoltParticle)
{
   textureName          = "~/data/particles/smoke";
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;   // rises slowly
   inheritedVelFactor   = 0.00;
   lifetimeMS           = 500;  // lasts 0.7 second
   lifetimeVarianceMS   = 150;   // ...more or less
   useInvAlpha = false;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 1.0";
   colors[2]     = "0 0 0 0";

   sizes[0]      = 0.25;
   sizes[1]      = 0.5;
   sizes[2]      = 1.0;

   times[0]      = 0.0;
   times[1]      = 0.3;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(CrossbowBoltEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 5;

   ejectionVelocity = 0.25;
   velocityVariance = 0.10;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   particles = CrossbowBoltParticle;
};


//-----------------------------------------------------------------------------
// Projectile Explosion

datablock ParticleData(CrossbowExplosionParticle)
{
   textureName          = "~/data/particles/smoke";
   dragCoefficient      = 2;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 150;

   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 0.0";

   sizes[0]      = 0.5;
   sizes[1]      = 1.0;
};

datablock ParticleEmitterData(CrossbowExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "CrossbowExplosionParticle";
};

datablock ParticleData(CrossbowExplosionSmoke)
{
   textureName          = "~/data/particles/smoke";
   dragCoeffiecient     = 100.0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.25;
   constantAcceleration = -0.80;
   lifetimeMS           = 1200;
   lifetimeVarianceMS   = 300;
   useInvAlpha =  true;
   spinRandomMin = -80.0;
   spinRandomMax =  80.0;

   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.2 0.2 0.2 1.0";
   colors[2]     = "0.0 0.0 0.0 0.0";

   sizes[0]      = 1.0;
   sizes[1]      = 1.5;
   sizes[2]      = 2.0;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(CrossbowExplosionSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 4;
   velocityVariance = 0.5;
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   lifetimeMS       = 250;
   particles = "CrossbowExplosionSmoke";
};

datablock ParticleData(CrossbowExplosionSparks)
{
   textureName          = "~/data/particles/spark";
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 350;

   colors[0]     = "0.60 0.40 0.30 1.0";
   colors[1]     = "0.60 0.40 0.30 1.0";
   colors[2]     = "1.0 0.40 0.30 0.0";

   sizes[0]      = 0.5;
   sizes[1]      = 0.25;
   sizes[2]      = 0.25;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(CrossbowExplosionSparkEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 13;
   velocityVariance = 6.75;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "CrossbowExplosionSparks";
};

datablock ExplosionData(CrossbowSubExplosion1)
{
   offset = 1.0;
   emitter[0] = CrossbowExplosionSmokeEmitter;
   emitter[1] = CrossbowExplosionSparkEmitter;
};

datablock ExplosionData(CrossbowSubExplosion2)
{
   offset = 1.0;
   emitter[0] = CrossbowExplosionSmokeEmitter;
   emitter[1] = CrossbowExplosionSparkEmitter;
};

datablock ExplosionData(CrossbowExplosion)
{
   soundProfile   = CrossbowExplosionSound;
   lifeTimeMS = 1200;

   // Volume particles
   particleEmitter = CrossbowExplosionEmitter;
   particleDensity = 80;
   particleRadius = 1;

   // Point emission
   emitter[0] = CrossbowExplosionSmokeEmitter;
   emitter[1] = CrossbowExplosionSparkEmitter;

   // Sub explosion objects
   subExplosion[0] = CrossbowSubExplosion1;
   subExplosion[1] = CrossbowSubExplosion2;

   // Camera Shaking
   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 6;
   lightEndRadius = 3;
   lightStartColor = "0.5 0.5 0";
   lightEndColor = "0 0 0";
};


//-----------------------------------------------------------------------------
// Projectile Object

datablock ProjectileData(CrossbowProjectile)
{
   projectileShapeName = "~/data/models/weapons/bolt.dts";
   directDamage        = 44;
   radiusDamage        = 22;
   damageRadius        = 1.5;
   explosion           = CrossbowExplosion;
   particleEmitter     = CrossbowBoltEmitter;

   muzzleVelocity      = 100;
   velInheritFactor    = 0.3;

   armingDelay         = 0;
   lifetime            = 5000;
   fadeDelay           = 5000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.80;

   hasLight    = true;
   lightRadius = 4.0;
   lightColor  = "0.5 0.5 0";
};

function CrossbowProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
   // Apply damage to the object all shape base objects
   if (%col.getType() & $TypeMasks::ShapeBaseObjectType)
      %col.damage(%obj,%pos,%this.directDamage,"CrossbowBolt");

   // Radius damage is a support scripts defined in radiusDamage.cs
   radiusDamage(%obj,%pos,%this.damageRadius,%this.radiusDamage,"CrossbowBolt",0);
   PlayMusic(CrossbowExplosion2);
}


//-----------------------------------------------------------------------------
// Ammo Item

datablock ItemData(CrossbowAmmo)
{
   // Mission editor category
   category = "Ammo";

   // Add the Ammo namespace as a parent.  The ammo namespace provides
   // common ammo related functions and hooks into the inventory system.
   className = "Ammo";

   // Basic Item properties
   shapeFile = "~/data/models/weapons/boltclip.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;

	// Dynamic properties defined by the scripts
	pickUpName = "crossbow bolts";
   maxInventory = 20;
};


//--------------------------------------------------------------------------
// Weapon Item.  This is the item that exists in the world, i.e. when it's
// been dropped, thrown or is acting as re-spawnable item.  When the weapon
// is mounted onto a shape, the CrossbowImage is used.

datablock ItemData(Crossbow)
{
   // Mission editor category
   category = "Weapon";

   // Hook into Item Weapon class hierarchy. The weapon namespace
   // provides common weapon handling functions in addition to hooks
   // into the inventory system.
   className = "Weapon";

   // Basic Item properties
   shapeFile = "~/data/models/weapons/crossbow.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   emap = true;

	// Dynamic properties defined by the scripts
	pickUpName = "a crossbow";
	image = CrossbowImage;
};


//--------------------------------------------------------------------------
// Crossbow image which does all the work.  Images do not normally exist in
// the world, they can only be mounted on ShapeBase objects.

datablock ShapeBaseImageData(CrossbowImage)
{
   // Basic Item properties
   shapeFile = "~/data/models/weapons/crossbow.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   eyeOffset = "0.1 0.4 -0.6";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = Crossbow;
   ammo = CrossbowAmmo;
   projectile = CrossbowProjectile;
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
   stateTimeoutValue[3]             = 0.2;
   stateFire[3]                     = true;
   stateRecoil[3]                   = LightRecoil;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire";
   stateScript[3]                   = "onFire";
   stateSound[3]                    = CrossbowFireSound;

   // Play the relead animation, and transition into
   stateName[4]                     = "Reload";
   stateTransitionOnNoAmmo[4]       = "NoAmmo";
   stateTransitionOnTimeout[4]      = "Ready";
   stateTimeoutValue[4]             = 0.8;
   stateAllowImageChange[4]         = false;
   stateSequence[4]                 = "Reload";
   stateEjectShell[4]               = true;
   stateSound[4]                    = CrossbowReloadSound;

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
   stateSound[6]                    = CrossbowFireEmptySound;
};


//-----------------------------------------------------------------------------

function CrossbowImage::onFire(%this, %obj, %slot)
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
   if ( $Human == %obj ) {
   	Ammobox.SetValue(%obj.getInventory(%this.ammo));
   	$Ammo_Left_now = %obj.getInventory(%this.ammo);
  }
   MissionCleanup.add(%p);
   return %p;
}
