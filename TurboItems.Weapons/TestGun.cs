using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;

namespace TurboItems
{

    public class TestGun : GunBehaviour
    {


        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Test Gun", "testd");
            Game.Items.Rename("outdated_gun_mods:test_gun", "turbo:test_gun");
            gun.gameObject.AddComponent<TestGun>();
            gun.SetShortDescription("Impressionable");
            gun.SetLongDescription("A gun left unfinished and abandoned by its creator. It still has great potential.");
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.AddProjectileModuleFrom("ak-47", true, false);
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 1.1f;
            gun.DefaultModule.cooldownTime = 0.1f;
            gun.DefaultModule.numberOfShotsInClip = 6;
            gun.SetBaseMaxAmmo(250);
            gun.quality = PickupObject.ItemQuality.D;
            gun.encounterTrackable.EncounterGuid = "WIP2";

            //test stuff

            //gun.blankDuringReload = true;
            //gun.blankDamageScalingOnEmptyClip = 1f;
            //gun.blankDamageToEnemies = 10f;
            //gun.blankKnockbackPower = 10f;
            //gun.blankReloadRadius = 5f; 

            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 5f;
            projectile.baseData.speed = 1.7f;
            projectile.transform.parent = gun.barrelOffset;
            ETGMod.Databases.Items.Add(gun, null, "ANY");
        }

        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
            player.ForceDropGun(gun);
            base.OnReloadPressed(player, gun, bSOMETHING);
        }
    }
}