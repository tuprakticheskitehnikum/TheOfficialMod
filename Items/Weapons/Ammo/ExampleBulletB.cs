using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TheOfficialMod.Items.Weapons.Ammo
{
    class ExampleBulletB : ModItem
    {
		//public override string Texture => "Terraria/Item_" + ItemID.SilverBullet;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Example Bullet B"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Example Bullet B");
		}
		public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 14;
			item.height = 32;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 1f;
			item.value = 500;
			item.rare = ItemRarityID.Green;

			//item.shoot = ProjectileType<Projectiles.FirstBullet>();   //The projectile shoot when your weapon using this ammo
			//item.shootSpeed = 16f;                  //The speed of the projectile
			//item.ammo = AmmoID.Bullet;

			item.shoot = mod.ProjectileType("ExampleBulletB");
			item.shootSpeed = 9f;
			item.ammo = mod.ItemType("ExampleBulletA"); //Tells the game that it should be considered as the same ammo type as Example Ammo A
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 111);
			recipe.AddRecipe();
		}


	}
}
