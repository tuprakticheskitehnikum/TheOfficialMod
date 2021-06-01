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
	/*
    public class DemonContract : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases your max number of minions\nGrants minor improvements for each minion summoned");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Red;
			item.material = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxMinions ++;
			player.armorPenetration += player.maxMinions * 3;
			player.minionDamage += player.maxMinions * 0.02f;
			player.statDefense += player.maxMinions * 2;
			player.endurance += player.maxMinions * 0.01f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "SandstormScarab", 1);
            recipe.AddIngredient(mod, "CerberusLeash", 1);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	*/
}
