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
    class ExampleEnemy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Example Enemy");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
        }

        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 40;
            npc.lifeMax = 750;
            npc.damage = 40;
            npc.defense = 10;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 100;
            npc.knockBackResist = .8f;
            npc.aiStyle = 14;
            aiType = NPCID.Zombie;
            animationType = NPCID.Zombie;
            banner = Item.NPCtoBanner(NPCID.Zombie);
            bannerItem = Item.BannerToItem(banner);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNightMonster.Chance*0.4f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustType = Main.rand.Next(139, 143);
                int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }

        public override void NPCLoot()
        {
            //guaranteed drop
            Item.NewItem(npc.position, mod.ItemType("TestGun"));
            if(Main.rand.Next(4) == 0)
            {
                Item.NewItem(npc.position, ItemID.Vertebrae, 2);
            }
            if(Main.hardMode)
            {
                Item.NewItem(npc.position, ItemID.RottenChunk, 2);
            }

        }
    }
}
