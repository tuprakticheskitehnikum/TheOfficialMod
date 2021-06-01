using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheOfficialMod.Items.Accessories
{
	public abstract class Mammoth : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(silver: 50);
			item.rare = ItemRarityID.Orange;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.noKnockback = true;
			player.endurance += (0.05f * (player.aggro / 50));
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DesertFossil, 20);
			recipe.AddIngredient(ItemID.SoulofMight, 8);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	
	public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int index = FindDifferentEquippedExclusiveAccessory().index;
				if (index != -1)
				{
					return slot == index;
				}
			}
			return base.CanEquipAccessory(player, slot);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
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
			int maxAccessoryIndex = 5 + Terraria.Main.LocalPlayer.extraAccessorySlots;
			for (int i = 13; i < 13 + maxAccessoryIndex; i++)
			{
				if (Terraria.Main.LocalPlayer.armor[i].type == item.type) return false;
			}

			if (FindDifferentEquippedExclusiveAccessory().accessory != null)
			{
				return true;
			}
			return base.CanRightClick();
		}

		public override void RightClick(Player player)
		{
			var (index, accessory) = FindDifferentEquippedExclusiveAccessory();
			if (accessory != null)
			{
				Terraria.Main.LocalPlayer.QuickSpawnClonedItem(accessory);
				Terraria.Main.LocalPlayer.armor[index] = item.Clone();
			}
		}

		protected (int index, Item accessory) FindDifferentEquippedExclusiveAccessory()
		{
			int maxAccessoryIndex = 5 + Terraria.Main.LocalPlayer.extraAccessorySlots;
			for (int i = 3; i < 3 + maxAccessoryIndex; i++)
			{
				Item otherAccessory = Terraria.Main.LocalPlayer.armor[i];
				if (!otherAccessory.IsAir &&
					!item.IsTheSameAs(otherAccessory) &&
					otherAccessory.modItem is Scarabs)
				{
					return (i, otherAccessory);
				}
			}
			return (-1, null);
		}
	}


	public class MammothSkullShield : Mammoth
	{		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants immunity to knockback\nIncreases damage reduction the more you are targeted by enemies");
		}
	}

	public class MammothFleshShield : Mammoth
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("7 defense\nGrants immunity to knockback\nIncreases damage reduction the more likely you are to be targeted by enemies");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Red;
		}
		public override void UpdateAccessory(Player player, bool hideVisual){
			player.aggro += 400;
			base.UpdateAccessory(player, hideVisual);
			player.statDefense += 7;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "MammothSkullShield");
			recipe.AddIngredient(ItemID.FleshKnuckles);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}