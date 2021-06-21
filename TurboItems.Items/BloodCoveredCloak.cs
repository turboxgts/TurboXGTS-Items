using UnityEngine;
using ItemAPI;

namespace TurboItems
{
	public class BloodCoveredCloak : PassiveItem
	{
		public static void Register()
		{
			string itemName = "Blood Covered Cloak";
			string resourceName = "TurboItems/Resources/blood_cloak";
			GameObject obj = new GameObject(itemName);
			var item = obj.AddComponent<BloodCoveredCloak>();
			ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
			string shortDesc = "Bat's curse";
			string longDesc = "A cloak, stained with blood. Grants the curse of the vampire upon whoever wears it.";
			ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
			ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 2, StatModifier.ModifyMethod.ADDITIVE);
			item.quality = PickupObject.ItemQuality.EXCLUDED;
			item.CanBeDropped = false;
		}
		public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
			LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(436).gameObject, base.Owner);
			LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(595).gameObject, base.Owner);
			LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(285).gameObject, base.Owner);
			player.GiveItem("turbo:wooden_stake");
		}

	}
}