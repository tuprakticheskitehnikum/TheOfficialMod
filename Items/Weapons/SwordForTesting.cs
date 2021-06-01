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

namespace TheOfficialMod.Items.Weapons
{
    class SwordForTesting : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is a modded sword i test stuff on.");
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 1;
			item.value = 10000;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("MagicSpear");
			item.shootSpeed = 15;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.Stone);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}

// DisplayName.SetDefault("OPSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
//item.shoot = mod.ProjectileType("GreatArrow");
//item.shoot = ProjectileID.CannonballFriendly; //- this makes the sword shoot wooden arrows for free
/*
public override void UpdateArmorSet(Player player)
{
	switch (Main.GameUpdateCount / 60 % 4)
	{
		case 0:
			DrawArmorColor(player.whoAmI, 0, new Color(254, 105, 47), 0, 0);
			break;
		case 1:
			line.overrideColor = new Color(34, 221, 151);
			break;
		case 2:
			line.overrideColor = new Color(190, 30, 209);
			break;
		case 3:
			line.overrideColor = new Color(0, 106, 185);
			break;
	}
	base.UpdateArmorSet(player);
}
*/