using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;

namespace TurboItems
{
    class YarnBall : AdvancedGunBehaviour
    {
        public static void Add()
        {

            Gun gun = ETGMod.Databases.Items.NewGun("Ball of Yarn", "ball_of_yarn");
            Game.Items.Rename("outdated_gun_mods:ball_of_yarn", "turbo:ball_of_yarn");
            var behav = gun.gameObject.AddComponent<YarnBall>();
            behav.overrideNormalFireAudio = "Play_ENM_shelleton_beam_01";
            behav.preventNormalFireAudio = true;
            gun.SetShortDescription("Crochet Rocket");
            gun.SetLongDescription("Very fuzzy");

            gun.SetupSprite(null, "ball_of_yarn_idle_001", 8);
            gun.gunHandedness = GunHandedness.HiddenOneHanded;
            gun.SetAnimationFPS(gun.shootAnimation, 8);
            gun.isAudioLoop = true;
            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(86) as Gun, true, false);

            //GUN STATS
            gun.doesScreenShake = false;
            gun.DefaultModule.ammoCost = 10;
            gun.DefaultModule.angleVariance = 0;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Beam;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0f;
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.DefaultModule.cooldownTime = 0.001f;
            gun.DefaultModule.numberOfShotsInClip = 500;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.BEAM;
            gun.barrelOffset.transform.localPosition = new Vector3(0.93f, 0.18f, 0f);
            gun.InfiniteAmmo = true;

            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).loopStart = 1;

            List<string> BeamAnimPaths = new List<string>()
            {
                "TurboItems/Resources/BeamSprites/ball_of_yarn_mid_001",
                "TurboItems/Resources/BeamSprites/ball_of_yarn_mid_002",
                "TurboItems/Resources/BeamSprites/ball_of_yarn_mid_003",
                "TurboItems/Resources/BeamSprites/ball_of_yarn_mid_004",
                "TurboItems/Resources/BeamSprites/ball_of_yarn_mid_005",
                "TurboItems/Resources/BeamSprites/ball_of_yarn_mid_006",
            };
            List<string> BeamEndPaths = new List<string>()
            {
                "TurboItems/Resources/BeamSprites/ball_of_yarn_end_001",
            };

            //BULLET STATS
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(86) as Gun).DefaultModule.projectiles[0]);

            BasicBeamController beamComp = projectile.GenerateBeamPrefab(
                "TurboItems/Resources/BeamSprites/ball_of_yarn_mid_001",
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
            projectile.baseData.damage = 12f;
            projectile.baseData.force *= 0.5f;
            projectile.baseData.range = 15f;
            projectile.baseData.speed *= 3f;

            beamComp.penetration = 0;
            beamComp.boneType = BasicBeamController.BeamBoneType.Projectile;
            beamComp.interpolateStretchedBones = true;

            gun.DefaultModule.projectiles[0] = projectile;

            gun.quality = PickupObject.ItemQuality.C;
            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        public YarnBall()
        {

        }
    }
}