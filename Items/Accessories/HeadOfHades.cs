using Microsoft.Xna.Framework.Input;
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
	public class HeadOfHades : ModItem
	{

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Double tap DOWN to toggle stealth");
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
			player.shroomiteStealth = true;
			//player.stealth += 10000000000f;
			player.accRunSpeed *= 0;
			player.AddBuff(10, 1, true);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MedusaHead);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}