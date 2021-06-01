using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using TheOfficialMod.Items.Placeable;
using TheOfficialMod.Tiles;
using Microsoft.Xna.Framework;

namespace TheOfficialMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	class Frogmouth : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Helmet specialized for jousting.");
		}

		public override void SetDefaults()
		{
			item.width = 46;
			item.height = 46;
			item.value = 100000;
			item.rare = ItemRarityID.Red;
			item.defense = 30;
			item.wornArmor = true;
		}

		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type != ItemID.None && legs.type != ItemID.None;
		}

		public override void UpdateEquip(Player player)
		{
			player.statManaMax2 += 20;
			player.jumpSpeedBoost += 1.33f;
			//player.doubleJumpFart = true;
			//player.fallStart += 1000;
			/*
			Color color = new Color(254, 105, 47);
			int glow = 0;
			switch (Main.GameUpdateCount / 60 % 4)
			{
				case 0:
					DrawArmorColor(player, 0, ref color, ref glow, ref color);
					break;
				case 1:
					color = new Color(34, 221, 151);
					DrawArmorColor(player, 0, ref color, ref glow, ref color);
					break;
				case 2:
					color = new Color(190, 30, 209);
					DrawArmorColor(player, 0, ref color, ref glow, ref color);
					break;
				case 3:
					color = new Color(0, 106, 185);
					DrawArmorColor(player, 0, ref color, ref glow, ref color);
					break;
			}
			base.UpdateArmorSet(player);
			*/
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "20% increased melee damage";
			player.meleeDamage += 0.2f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 30);
			recipe.AddIngredient(ItemID.IronBar, 10);
			recipe.AddTile(TileType<Tiles.SoulForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
