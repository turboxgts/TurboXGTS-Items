using System.Collections.Generic;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    public class TrankGunPack : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Trank Gun pack";
            string resourceName = "TurboItems/Resources/default_item_bundle";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<TrankGunPack>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Tranquilizers uwu";
            string longDesc = "Comes pre-equipped with the Trank Gun in its tranquilizer form and Angry Bullets.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            item.quality = PickupObject.ItemQuality.EXCLUDED;
            item.CanBeDropped = false;
            item.AddItemToSynergy("#NEEDSCISSORS", true);
        }
        protected override void Update()
        {
            if (base.Owner.CurrentGun.PickupObjectId == 42)
            {
                Owner.InfiniteAmmo.SetOverride("lplplp", true, null);
            }
            else
            {
                Owner.InfiniteAmmo.SetOverride("lplplp", false, null);
            }
            base.Update();
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(42).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(323).gameObject, base.Owner);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}