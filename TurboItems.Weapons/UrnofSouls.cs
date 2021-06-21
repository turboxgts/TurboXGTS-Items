using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;

namespace TurboItems
{
    public class UrnOfSouls : GunBehaviour
    {
        class Global
        {
            public static int SoulCharges;

        }
        public static void Add()
        {
            Global.SoulCharges = 20;
            Gun gun = ETGMod.Databases.Items.NewGun("Urn of Souls", "urn_of_souls");
            Game.Items.Rename("outdated_gun_mods:urn_of_souls", "turbo:urn_of_souls");
            gun.gameObject.AddComponent<UrnOfSouls>();
            gun.SetShortDescription("Unleash their sorrow");
            gun.SetLongDescription("A peculiar urn, taken from a basement filled with a child's drawings of themself and their mother.");
            gun.SetupSprite(null, "urn_of_souls_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.AddProjectileModuleFrom("flame_hand", true, false);
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Burst;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0;
            gun.DefaultModule.cooldownTime = 0.1f;
            gun.DefaultModule.numberOfShotsInClip = Global.SoulCharges;
            gun.SetBaseMaxAmmo(Global.SoulCharges);
            gun.quality = PickupObject.ItemQuality.B;
            gun.encounterTrackable.EncounterGuid = "heener neener I am uncreative";
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

        public override void OnInitializedWithOwner(GameActor actor)
        {
            base.OnInitializedWithOwner(actor);

            if (actor is PlayerController)
            {
                (actor as PlayerController).OnKilledEnemyContext += TestGun_OnKilledEnemyContext;
            }
        }

        private void TestGun_OnKilledEnemyContext(PlayerController player, HealthHaver enemy)
        {
            if (enemy.aiActor != null)
            {
                LootEngine.SpawnItem(PickupObjectDatabase.GetById(595).gameObject, enemy.aiActor.sprite.WorldCenter, Vector2.zero, 0);
            }
        }
        public override void OnPostFired(PlayerController player, Gun gun)
        {
            gun.PreventNormalFireAudio = true;
            AkSoundEngine.PostEvent("Play_ENM_flame_veil_01", gameObject);
            Global.SoulCharges -= 1;
        }

        private bool HasReloaded;
        protected void Update()
        {
            gun.DefaultModule.numberOfShotsInClip = Global.SoulCharges;
            gun.SetBaseMaxAmmo(Global.SoulCharges);
            if (gun.CurrentOwner)
            {

                if (!gun.PreventNormalFireAudio)
                {
                    this.gun.PreventNormalFireAudio = true;
                }
                if (!gun.IsReloading && !HasReloaded)
                {
                    this.HasReloaded = true;
                }
            }
        }
    }
}