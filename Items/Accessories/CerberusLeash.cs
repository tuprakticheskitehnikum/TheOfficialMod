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
    public class CerberusLeash : ModItem
    {

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases your max number of minions by 1");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 2, silver: 50);
			item.rare = ItemRarityID.Red;
            item.material = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxMinions ++;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Obsidian, 10);
			recipe.AddIngredient(ItemID.HellstoneBar, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
