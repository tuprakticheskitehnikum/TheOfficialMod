using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Steamworks;
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
    class ForbiddenTome : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Call forth a sandstorm tornado.");
		}

		public override void SetDefaults()
		{
			item.damage = 22;
			item.noMelee = true;
			item.magic = true;
			item.channel = true; //Channel so that you can hold the weapon
			item.mana = 20;
			item.rare = ItemRarityID.LightRed;
			item.width = 28;
			item.height = 30;
			item.useTime = 20;
			item.UseSound = SoundID.Item13;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 14f;
			item.useAnimation = 20;
			item.shoot = ProjectileID.SandnadoFriendly;
			item.value = Item.sellPrice(gold: 5);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.SoulofMight, 6);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			float x = Terraria.Main.MouseWorld.X;
			float y = Terraria.Main.MouseWorld.Y;
			Vector2 pos = new Vector2(x, y);
			position = pos;
			speedX = 0f;
			speedY = 0f;
			//bool lineOfSight = Collision.CanHitLine(pos, (int)x +20, (int)y+30, player.position, player.width, player.height);
			//if (lineOfSight != true)
			//{ }
			Projectile.NewProjectile(x, y, speedX, speedY, ProjectileID.SandnadoFriendly, item.damage, item.knockBack, Terraria.Main.myPlayer, 0f, 0f);
			return true;
		}
	}
}
