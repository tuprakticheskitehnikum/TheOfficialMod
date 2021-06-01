using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheOfficialMod.Items.Accessories
{
    class PhoenixFeather : ModItem
	{
		public override string Texture => "Terraria/Item_" + ItemID.FireFeather;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Makes movement precise");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 5, silver: 50);
			item.rare = ItemRarityID.Red;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.accRunSpeed *= 0;
			player.autoJump = true;
			player.statDefense += 500;
			player.moveSpeed += 1f;
			player.statLifeMax = 400;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FireFeather);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
