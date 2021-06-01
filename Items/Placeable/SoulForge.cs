using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TheOfficialMod.Items.Placeable
{
    class SoulForge : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Used to craft special items.");
		}

		public override void SetDefaults()
		{
			item.width = 48;
			item.height = 48;
			item.maxStack = 999;
			item.rare = ItemRarityID.Red;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 25000;
			item.createTile = TileType<Tiles.SoulForge>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WorkBench);
			recipe.AddIngredient(ItemID.IronAnvil);
			recipe.AddIngredient(ItemID.Hellforge);
			//recipe.AddIngredient(ItemType<>(), 10);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
