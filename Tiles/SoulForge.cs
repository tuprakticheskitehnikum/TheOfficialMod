using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;


namespace TheOfficialMod.Tiles
{
    class SoulForge : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Soul Forge");
			AddMapEntry(new Color(200, 200, 200), name);

			dustType = DustID.PurpleCrystalShard;
			disableSmartCursor = true;
			adjTiles = new int[] { TileID.WorkBenches, TileID.Hellforge, TileID.Anvils };
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
		}
		/*
        public override void RandomUpdate(int i, int j)
        {
			if (Main.GameUpdateCount % 60 == 0)
			{
				Dust.NewDust(i * 16, j * 16, 56, 54, 5, -10);
			}
			base.RandomUpdate(i, j);
        }
		*/

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 56, 54, ItemType<Items.Placeable.SoulForge>());
		}
	}
}
