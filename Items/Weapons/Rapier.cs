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
    class Rapier : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Backstab");
		}

		public override void SetDefaults()
		{
			item.damage = 26;
			item.melee = true;
			item.width = 46;
			item.height = 46;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.Stabbing;
			item.knockBack = 3;
			item.value = 10000;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.crit = 19;
			//item.shoot = mod.ProjectileType("GreatArrow");
			//item.shoot = ProjectileID.CannonballFriendly; //- this makes the sword shoot wooden arrows for free
			//item.shoot = mod.ProjectileType("MagicSpear");
			//item.shootSpeed = 15;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 13); //|| ItemID.TungstenBar
			recipe.AddIngredient(ItemID.TissueSample, 10); //|| ItemID.ShadowScale
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.useTime = 40;
				item.useAnimation = 40;
				item.damage = 14;
				item.knockBack = 0;
			}
			else
			{
				item.useStyle = ItemUseStyleID.Stabbing;
				item.useTime = 10;
				item.useAnimation = 10;
				item.damage = 26;
				item.knockBack = 3;
			}
			return base.CanUseItem(player);
		}
	}
}
