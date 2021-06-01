using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;

namespace TheOfficialMod.Projectiles
{
    class SecondBullet : ModProjectile
	{
		public override string Texture => "Terraria/Item_" + ItemID.SilverBullet;
		public override void SetDefaults()
		{
			projectile.Name = "Example Bullet B";
			projectile.width = 16;
			projectile.height = 16;
			projectile.timeLeft = 180;
			projectile.penetrate = 8;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true; ;
			projectile.ignoreWater = true;
			projectile.ranged = true;
			projectile.aiStyle = 18; //18 is the demon scythe style
			projectile.noDropItem = true;
		}
		public override void AI()
		{
			projectile.type = 45; //This is the demon scythe projectile ID
		}
	}
}
