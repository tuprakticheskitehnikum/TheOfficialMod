using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework;

namespace TheOfficialMod.Projectiles
{
    class MagicSpear : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arrows made to tear down oversized threats");
		}

		public override void SetDefaults()
		{
			//ModProjectile greatArrow = this;
			projectile.magic = true;
			projectile.width = 25;
			projectile.height = 25;
			projectile.aiStyle = 1;
			aiType = ProjectileID.FrostBoltStaff;
			projectile.friendly = true;
			//aiType = ProjectileID.WoodenArrowFriendly;
			projectile.extraUpdates = 1;
			projectile.noDropItem = true;
			projectile.light = 1f;
		}


		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			base.OnHitNPC(target, damage, knockback, crit);
			/*
			target.teleporting = true;
			target.teleportStyle = 1;
			Vector2 vec = new Vector2();
			vec = target.position;
			target.Teleport(vec, (int)vec.X, (int)vec.Y - 200);
			*/
		}

		public override void AI()
		{
			//base.AI();
			projectile.ai[0] += 1f;
			if (projectile.ai[0] >= 30f)
			{
				// Half a second has passed. Reset timer, etc.
				projectile.ai[0] = 30f;
				projectile.velocity.Y = projectile.velocity.Y + 0.01f;
				//projectile.netUpdate = true;
				// Do something here, maybe change to a new state.
				//projectile.extraUpdates = 1;
			}


			//projectile.velocity.Y = projectile.velocity.Y + 0.1f;
			if (projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
			{
				projectile.velocity.Y = 16f;//change this
			}

			//projectile.velocity.X = projectile.velocity.X * 0.97f; // 0.99f for rolling grenade speed reduction. Try values between 0.9f and 0.99f

			if (Main.rand.Next(5) == 0) // only spawn 20% of the time
			{
				int choice = Main.rand.Next(3); // choose a random number: 0, 1, or 2
				if (choice == 0) // use that number to select dustID: 15, 57, or 58
				{
					choice = 15;
				}
				else if (choice == 1)
				{
					choice = 56;
				}
				else
				{
					choice = 226;
				}
				// Spawn the dust
				for (int i = 0; i<5;i++)
					Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, choice, -projectile.velocity.X * 0.5f, -projectile.velocity.Y * 0.5f, 150, default(Color), 0.7f);
			}

			//Lighting.AddLight(projectile.Center, 0.9f, 0.1f, 0.3f); -adds light, the same the crimson heart produces

			/*

			if (projectile.tileCollide == true && projectile.owner == Main.myPlayer)
			{
				for (int i = 0; i < 3; i++)
				{
					// Calculate new speeds for other projectiles.
					// Rebound at 40% to 70% speed, plus a random amount between -8 and 8
					float speedX = -projectile.velocity.X * Main.rand.NextFloat(.4f, .7f) + Main.rand.NextFloat(-8f, 8f);
					float speedY = -projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f; // This is Vanilla code, a little more obscure.
																															 // Spawn the Projectile.
					Projectile.NewProjectile(projectile.position.X + speedX, projectile.position.Y + speedY, speedX, speedY, ProjectileID.CrystalBullet, (int)(projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);
				}
			}

			*/

		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//Vector2 pos = projectile.position;
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 226, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, Color.White, 1f);
			return base.OnTileCollide(oldVelocity);
		}
	}
}
