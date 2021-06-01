using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace TheOfficialMod.Items.Weapons
{
    class TestGun:ModItem
    {
		//public override string Texture => "Terraria/Item_" + ItemID.Handgun;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Example Gun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("I GOT A GUN.");
		}
		public override void SetDefaults()
		{
			item.damage = 20; //The damage
			item.ranged = true; //Whether or not it is a ranged weapon
			item.width = 60; //Item width
			item.height = 60; //Item height
			item.maxStack = 1; //How many of this item you can stack
			item.useTime = 20; //How long it takes for the item to be used | 1
			item.useAnimation = 20; //How long the animation of the item takes | 2
			item.knockBack = 7f; //How much knockback the item produces
			item.UseSound = SoundID.Item11; //The soundeffect played when used 
			item.useStyle = ItemUseStyleID.HoldingOut; //How the weapon is held, 5 is the gun hold style
			item.value = 10000; //How much the item is worth
			item.rare = ItemRarityID.Blue; //The rarity of the item
										   //item.shoot = mod.ProjectileType("FirstBullet"); //What the item shoots, retains an int value | 3
			item.shoot = mod.ProjectileType("ExampleBulletA"); //What the item shoots, retains an int value | *
			item.shootSpeed = 1f; //How fast the projectile fires	
			item.useAmmo = mod.ItemType("ExampleBulletA"); //Tells the game what type of ammo to use
											//item.useAmmo = AmmoID.Bullet;
			item.autoReuse = true; //Whether it automatically uses the item again after its done being used/animated
			item.shootSpeed = 25;
			item.noMelee = true;
		}
		public override void AddRecipes() //Adding recipes
		{
			ModRecipe recipe = new ModRecipe(mod); //Creating a new recipe to be added to 
			recipe.AddIngredient(ItemID.DirtBlock); //Setting the ingredient to the item as a Dirt Block
			recipe.SetResult(this); //Set the result of the recipe to this item (this refers to the class itself)
			recipe.AddRecipe(); //Add this recipe
		}

		public override bool ConsumeAmmo(Player p) //Tells the game whether the item consumes ammo or not
		{
			int rand = Terraria.Main.rand.Next(9); //A random chance... once again
			if (p.itemAnimation < p.inventory[p.selectedItem].useAnimation - 25) //Consumes ammo near the end of the item's animation
			{
				return true; //Ammo is consumed
			}
			else
			{
				return false; //Ammo is not consumed before animation is finished
			}
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) //This lets you modify the firing of the item
		{
			/*Code is made by berberborscing*/
			int spread = 30; //The angle of random spread.
			float spreadMult = 0.1f; //Multiplier for bullet spread, set it higher and it will make for some outrageous spread.
			for (int i = 0; i < 3; i++)
			{
				float vX = speedX + (float)Terraria.Main.rand.Next(-spread, spread + 1) * spreadMult;
				float vY = speedY + (float)Terraria.Main.rand.Next(-spread, spread + 1) * spreadMult;
                Projectile.NewProjectile(position.X, position.Y, vX, vY, type, damage, knockBack, Terraria.Main.myPlayer);
			}
			return false; //Makes sure to not spawn the original projectile
		}
	}
}
