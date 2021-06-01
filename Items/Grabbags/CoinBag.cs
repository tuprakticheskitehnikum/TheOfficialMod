using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TheOfficialMod.Items.Grabbags
{
    class CoinBag : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Dropped by rare creatures\n<right> for goodies!");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Green;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			if (Main.expertMode)
			{
				player.QuickSpawnItem(ItemID.GoldCoin, Main.rand.Next(10, 31));
			}
			else
			{
				player.QuickSpawnItem(ItemID.GoldCoin, Main.rand.Next(5, 16));

			}
		}
	}
}
