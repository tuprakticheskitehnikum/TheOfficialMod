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
	class DestructionRing : ModItem
	{
		public override string Texture => "Terraria/Item_" + ItemID.Grenade;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Destruction Ring");
			Tooltip.SetDefault("Increases damage by 25% while below 50% health.");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Red;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.statLife >= player.statLifeMax * 0.9)
			{
				player.allDamage += 25;
			}
		}
	}
}