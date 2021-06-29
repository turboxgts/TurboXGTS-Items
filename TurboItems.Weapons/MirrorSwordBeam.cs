using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Gungeon;
using MonoMod;
using ItemAPI;
using UnityEngine;
using System.Reflection;


namespace TurboItems
{
    class MirrorSwordBeam : AdvancedGunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Mirror Sword Beam", "mirror_sword_beam");
            Game.Items.Rename("outdated_gun_mods:mirror_sword_beam", "turbo:mirror_sword_beam");
            var behav = gun.gameObject.AddComponent<MirrorSwordBeam>();
            behav.overrideNormalFireAudio = "Play_ENM_shelleton_beam_01";
            behav.preventNormalFireAudio = true;
            gun.SetShortDescription("SHWING");
            gun.SetLongDescription("A dull sword, presumably for use in an old ceremony before the Great Bullet struck the Gungeon. Can deflect bullets. Slightly angers the Jammed.");
            gun.SetupSprite(null, "mirror_sword_beam_idle_001", 8);
            gun.SetAnimationFPS(gun.reloadAnimation, 24);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.isAudioLoop = true;
            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(86) as Gun, true, false);

            //GUN STATS
            gun.doesScreenShake = false;
            gun.DefaultModule.ammoCost = 5;
            gun.DefaultModule.angleVariance = 0;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Beam;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.encounterTrackable.EncounterGuid = "thiseeshanideatwokeursdc eginbeuyxiknhvskyudxbgsduyigxberuixbybi7ewrycgnie";
            gun.reloadTime = 0f;
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.DefaultModule.cooldownTime = 0.001f;
            gun.InfiniteAmmo = true;
            gun.DefaultModule.numberOfShotsInClip = 100;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.BEAM;
            gun.barrelOffset.transform.localPosition = new Vector3(0.875f, 0.5f, 0f);


            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).loopStart = 1;

            List<string> BeamAnimPaths = new List<string>()
            {
                "TurboItems/Resources/BeamSprites/laser_disk_mid_001",
            };
            List<string> BeamEndPaths = new List<string>()
            {
                "TurboItems/Resources/BeamSprites/laser_disk_end_001",
            };

            //BULLET STATS
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(86) as Gun).DefaultModule.projectiles[0]);

            BasicBeamController beamComp = projectile.GenerateBeamPrefab(
                "TurboItems/Resources/BeamSprites/laser_disk_mid_001",
                new Vector2(5, 3),
                new Vector2(0, 1),
                BeamAnimPaths,
                9,
                //Impact
                null,
                -1,
                null,
                null,
                //End
                BeamEndPaths,
                9,
                new Vector2(5, 3),
                new Vector2(0, 1),
                //Beginning
                null,
                -1,
                null,
                null
                );

            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            projectile.baseData.damage = 4f;
            projectile.baseData.force *= 0.5f;
            projectile.baseData.range = 7.5f;
            projectile.baseData.speed *= 3f;

            beamComp.penetration = 0;
            beamComp.boneType = BasicBeamController.BeamBoneType.Straight;

            gun.DefaultModule.projectiles[0] = projectile;

            gun.quality = PickupObject.ItemQuality.EXCLUDED;
            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        private bool HasReloaded;
        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            if (gun.IsReloading && this.HasReloaded)
            {
                HasReloaded = false;
                AkSoundEngine.PostEvent("Stop_WPN_ALL", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);

            }
        }
        public MirrorSwordBeam()
        {

        }
    }
}