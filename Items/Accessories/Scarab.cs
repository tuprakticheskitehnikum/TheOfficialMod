using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace TheOfficialMod.Items.Accessories
{
	public abstract class Scarabs : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Orange;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FossilOre, 15);
			recipe.AddIngredient(ItemID.Amber, 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.armorPenetration += player.maxMinions * 2;
			player.minionDamage += player.maxMinions * 0.02f;
			player.statDefense += player.maxMinions * 2;
			player.endurance += player.maxMinions * 0.01f;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			// To prevent the accessory from being equipped, we need to return false if there is one already in another slot
			// Therefore we go through each accessory slot ignoring vanity slots using FindDifferentEquippedExclusiveAccessory()
			// which we declared in this class below
			if (slot < 10) // This allows the accessory to equip in vanity slots with no reservations
			{
				// Here we use named ValueTuples and retrieve the index of the item, since this is what we need here
				int index = FindDifferentEquippedExclusiveAccessory().index;
				if (index != -1)
				{
					return slot == index;
				}
			}
			// Here we want to respect individual items having custom conditions for equipability
			return base.CanEquipAccessory(player, slot);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Here we want to add a tooltip to the item if it can be swapped with another one of its kind
			// Therefore we retrieve the accessory from the ValueTuple, because the index isn't needed here
			Item accessory = FindDifferentEquippedExclusiveAccessory().accessory;
			if (accessory != null)
			{
				tooltips.Add(new TooltipLine(mod, "Swap", "Right click to swap with '" + accessory.Name + "'!")
				{
					overrideColor = Color.OrangeRed
				});
			}
		}

		public override bool CanRightClick()
		{
			// An intricacy of vanilla is that it directly swaps the items on right click if the items are the same and just their prefixes differ,
			// even in vanity slots. For this, FindDifferentEquippedExclusiveAccessory() doesn't find these items
			// That means, if for whatever reason you have Green equipped, Yellow in a vanity slot, and then right click a Yellow item in your inventory
			// that has a different prefix than the vanity Yellow, it will swap with the vanity Yellow instead of the equipped Green
			// Therefore we need to reimplement this behavior by doing the following check

			// Check vanity accessory slots for the same item type equipped and return false (so vanilla handles it)
			int maxAccessoryIndex = 5 + Terraria.Main.LocalPlayer.extraAccessorySlots;
			for (int i = 13; i < 13 + maxAccessoryIndex; i++)
			{
				if (Terraria.Main.LocalPlayer.armor[i].type == item.type) return false;
			}

			// Only allow right clicking if there is a different ExclusiveAccessory equipped
			if (FindDifferentEquippedExclusiveAccessory().accessory != null)
			{
				return true;
			}
			// If this hook returns true, the item is consumed (just like crates and boss bags)
			return base.CanRightClick();
		}

		public override void RightClick(Player player)
		{
			// Here we implement the "swapping" when right clicked to equip this item inplace of another one
			// Because we need both index and accessory, we "unpack" this ValueTuple like this:
			var (index, accessory) = FindDifferentEquippedExclusiveAccessory();
			if (accessory != null)
			{
                Terraria.Main.LocalPlayer.QuickSpawnClonedItem(accessory);
                // We need to use index instead of accessory because we directly want to alter the equipped accessory
                Terraria.Main.LocalPlayer.armor[index] = item.Clone();
			}
		}

		// We make our own method for compacting the code because we will need to check equipped accessories often
		// This method returns a named ValueTuple, indicated by the (Type name1, Type name2, ...) as the return type
		// This allows us to return more than one value from a method
		protected (int index, Item accessory) FindDifferentEquippedExclusiveAccessory()
		{
			int maxAccessoryIndex = 5 + Terraria.Main.LocalPlayer.extraAccessorySlots;
			for (int i = 3; i < 3 + maxAccessoryIndex; i++)
			{
                Item otherAccessory = Terraria.Main.LocalPlayer.armor[i];
				// IsAir makes sure we don't check for "empty" slots
				// IsTheSameAs() compares two items and returns true if their types match
				// "is ExclusiveAccessory" is a way of performing pattern matching
				// Here, inheritance helps us determine if the given item is indeed one of our ExclusiveAccessory ones
				if (!otherAccessory.IsAir &&
					!item.IsTheSameAs(otherAccessory) &&
					otherAccessory.modItem is Scarabs)
				{
					// If we find an item that matches these criteria, return both the index and the item itself
					// The second argument is just for convenience, technically we don't need it since we can get the item from just i
					return (i, otherAccessory);
				}
			}
			// If no item is found, we return default values for index and item, always check one of them with this default when you call this method!
			return (-1, null);
		}
	}


	public class SandstormScarab : Scarabs
	{
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants minor improvements for each minion summoned\nLong ago helped Ra defeat the minions of Seth. Imbued with divine strength.");
		}

		/*
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 32;
            item.accessory = true;
			item.value = Item.sellPrice(silver: 30);
			item.rare = ItemRarityID.Orange;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FossilOre, 15);
			recipe.AddIngredient(ItemID.Amber, 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.armorPenetration += player.maxMinions * 2;
			player.minionDamage += player.maxMinions * 0.02f;
			player.statDefense += player.maxMinions * 2;
			player.endurance += player.maxMinions * 0.01f;
		}

		*/

	}

	public class DemonContract : Scarabs
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases your max number of minions by 1\nGrants minor improvements for each minion summoned");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Red;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxMinions++;
			base.UpdateAccessory(player, hideVisual);
			player.armorPenetration += player.maxMinions * 3;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "SandstormScarab");
			recipe.AddIngredient(mod, "CerberusLeash");
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
