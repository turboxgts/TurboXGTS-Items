using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;

namespace TurboItems
{

    public class ReloadForm1 : GunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("wip1", "wip1");
            Game.Items.Rename("outdated_gun_mods:wip1", "turbo:wip1");
            gun.gameObject.AddComponent<ReloadForm1>();
            gun.SetShortDescription("wip");
            gun.SetLongDescription("wip.");
            gun.SetupSprite(null, "wip1_idle_001", 12);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.SetAnimationFPS(gun.reloadAnimation, 24);
            gun.AddProjectileModuleFrom("ak-47", true, false);
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0.33f;
            gun.DefaultModule.cooldownTime = 0.1f;
            gun.DefaultModule.numberOfShotsInClip = 6;
            gun.SetBaseMaxAmmo(250);
            gun.quality = PickupObject.ItemQuality.D;
            gun.encounterTrackable.EncounterGuid = "WIP1";
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


        private IEnumerator DelayedTransform()
        {
            yield return new WaitForSeconds(0.33f);
                //true means the original form, false means the 2nd form            
                if (GunForm == true)
                {
                    gun.TransformToTargetGun(Gungeon.Game.Items["turbo:wip2"] as Gun);
                    GunForm = false;
                }
                else
                {
                    gun.TransformToTargetGun(Gungeon.Game.Items["turbo:wip1"] as Gun);
                    GunForm = true;
                }
            yield break;
        }
        private bool GunForm = true;
        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
            if (gun.ClipCapacity != gun.ClipShotsRemaining)
            {
                GameManager.Instance.StartCoroutine(DelayedTransform());
            }
            base.OnReloadPressed(player, gun, bSOMETHING);
        }
    }
}