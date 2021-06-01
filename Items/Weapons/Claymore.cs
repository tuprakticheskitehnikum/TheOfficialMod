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
    class Claymore : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("OPSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			//Tooltip.SetDefault("insert tooltip");
		}

		public override void SetDefaults()
		{
			item.damage = 29;
			item.melee = true;
			item.width = 54;
			item.height = 54;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 5;
			item.value = 10000;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			//item.shoot = mod.ProjectileType("GreatArrow");
			//item.shoot = ProjectileID.CannonballFriendly; //- this makes the sword shoot wooden arrows for free
			//item.shoot = mod.ProjectileType("MagicSpear");
			//item.shootSpeed = 15;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 30);
			recipe.AddIngredient(ItemID.IronBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
