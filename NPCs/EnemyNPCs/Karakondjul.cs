using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheOfficialMod.NPCs.EnemyNPCs
{
    class Karakondjul : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 6; // make sure to set this for your modnpcs. Main.npcFrameCount[NPCID.Zombie];
        }

        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 48;
            //npc.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            npc.aiStyle = 26;
            npc.damage = 20;
            npc.defense = 20;
            npc.lifeMax = 800;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/KarakondjulHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/KarakondjulKillSound");
            npc.knockBackResist = 0f;
            //npc.alpha = 175;
            //npc.color = new Color(0, 80, 255, 100);
            npc.noTileCollide = false;
            npc.noGravity = false;

            //aiType = NPCID.Harpy;
            animationType = NPCID.Harpy;

            //banner = Item.NPCtoBanner(NPCID.Zombie);
            //bannerItem = Item.BannerToItem(banner);
            npc.value = 500f;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Confused] = false; // npc default to being immune to the Confused debuff. Allowing confused could be a little more work depending on the AI. npc.confused is true while the npc is confused.
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // we would like this npc to spawn in the overworld.
            return SpawnCondition.Overworld.Chance * 0.5f;
        }

        public override void NPCLoot()
        {
            //guaranteed drop
            Item.NewItem(npc.position, mod.ItemType("CoinBag"));
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem(npc.position, ItemID.SuperHealingPotion, 5);
            }
        }
    }
}
