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
    class TroupesEmblem : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Magic and ranged weapons gain armor penetration equal to their crit\n20% chance not to consume ammo\n8% mana reduction.");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 3, silver: 50);
			item.rare = ItemRarityID.Red;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if(item.magic || item.ranged)
            {
				player.armorPenetration += item.crit;
            }
			player.ammoBox = true;
			player.manaCost *= 0.92f;
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
