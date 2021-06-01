using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace TheOfficialMod.Items.Weapons.Ammo
{
    class ExampleBulletA : ModItem
    {
		//public override string Texture => "Terraria/Item_" + ItemID.MusketBall;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Example Bullet A"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Example Bullet A");
		}
		public override void SetDefaults()
		{
			item.damage = 10; //This is added with the weapon's damage
			item.ranged = true;
			item.width = 14;
			item.height = 32;
			item.maxStack = 999;
			item.consumable = true; //Tells the game that this should be used up once fired
			item.knockBack = 1f; //Added with the weapon's knockback
			item.value = 500;
			item.rare = ItemRarityID.Green;

			//item.shoot = ProjectileType<Projectiles.FirstBullet>();   //The projectile shoot when your weapon using this ammo
			//item.shootSpeed = 6f;                  //The speed of the projectile
			//item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.

			item.shoot = mod.ProjectileType("ExampleBulletA");
			item.shootSpeed = 7f; //Added to the weapon's shoot speed
			item.ammo = mod.ItemType("ExampleBulletA"); //Tells the game that it should be considered the same type of ammo as this item
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
