using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheOfficialMod.Items.Grabbags
{
    class BagOfSouls : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Dropped by a rare frost creature\n<right> for goodies!");
		}

			
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Purple;
		}

			
		public override bool CanRightClick()
		{
			return true;
		}


		public override void RightClick(Player player)
		{
			if (Main.expertMode)
			{
				player.QuickSpawnItem(ItemID.SoulofLight, Main.rand.Next(10, 31));
			}
			else
			{
				player.QuickSpawnItem(ItemID.SoulofLight, Main.rand.Next(5, 16));
			}
			
		}
	}
}
