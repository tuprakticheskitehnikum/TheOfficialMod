using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheOfficialMod.Items.Accessories
{
	class RingOfDestruction : ModItem
	{
		public override string Texture => "Terraria/Item_" + ItemID.GhostMask;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases damage by 25% while at full health.");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 5);
			item.rare = ItemRarityID.Yellow;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.statLife == player.statLifeMax)
			{
				player.allDamage += 25;
			}
		}
	}
}