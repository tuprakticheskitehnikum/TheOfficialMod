using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TheOfficialMod.NPCs
{
    class GlobalNPCs : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Medusa)
            {
                //Item.NewItem(npc.getRect(), ItemID.Beenade, , Main.rand.Next(5, 8)); // 5, 6, or 7
                if (Main.rand.Next(7) == 0 && Main.expertMode)
                    Item.NewItem(npc.getRect(), mod.ItemType("HeadOfHades")); 
            }

            // Next add gold bags and soul bags!

            /*
             * if(Main.rand.Next(7) < 2) // a 2 in 7 chance
             * if (Main.rand.NextFloat() < .1323f) // 13.23% chance
            */
        }
    }
}
