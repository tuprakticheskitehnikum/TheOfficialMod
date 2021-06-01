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
	public class TargetDummyShield : ModItem
	{

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("4 defense\nKnockback immunity\nEnemies are more likely to target you");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(silver: 50);
			item.rare = ItemRarityID.Blue;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 4;
			player.aggro += 200;
			player.noKnockback = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "Dartboard");
			recipe.AddIngredient(ItemID.CobaltShield);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}