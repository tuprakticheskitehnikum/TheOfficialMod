using Microsoft.Xna.Framework;
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
    class Talasum : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 3; // make sure to set this for your modnpcs. Main.npcFrameCount[NPCID.Zombie];
        }

        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 48;
            //npc.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            npc.aiStyle = -1;
            npc.damage = 30;
            npc.defense = 20;
            npc.lifeMax = 600;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/KarakondjulHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/TalasumKill");
            npc.knockBackResist = 0f;
            npc.alpha = 96;
            //npc.color = new Color(0, 80, 255, 100);
            npc.noTileCollide = true;
            npc.noGravity = false;

            //aiType = NPCID.Harpy;
            //animationType = NPCID.Wraith;

            //banner = Item.NPCtoBanner(NPCID.Zombie);
            //bannerItem = Item.BannerToItem(banner);
            npc.value = 681f;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Confused] = false; // npc default to being immune to the Confused debuff. Allowing confused could be a little more work depending on the AI. npc.confused is true while the npc is confused.
        }

        private const int AI_State_Slot = 0;
        private const int AI_Timer_Slot = 1;

        private const int State_Passive = 0;
        private const int State_Notice = 1;
        private const int State_Attack = 2;

        public float AI_State
        {
            get => npc.ai[AI_State_Slot];
            set => npc.ai[AI_State_Slot] = value;
        }

        public float AI_Timer
        {
            get => npc.ai[AI_Timer_Slot];
            set => npc.ai[AI_Timer_Slot] = value;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            Vector2 target = Main.player[npc.target].Center;
            float speed = 5f;
            Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float x = player.position.X + (float)(player.width / 2) - vector.X;
            float y = player.position.Y + (float)(player.height / 2) - vector.Y;
            float distance2 = (float)Math.Sqrt(x * x + y * y);
            float factor = speed / distance2;

            if (AI_State == State_Passive)
            {
                npc.TargetClosest(true);

                if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 700f)
                {
                    AI_State = State_Notice;
                    AI_Timer = 0;
                    target = player.Center;
                }
                else
                {
                    npc.direction = Main.rand.NextBool() ? 1 : -1; // TODO: rotated 30 degrees?
                    npc.velocity.X = npc.velocity.X + (1f * npc.direction);
                    npc.netUpdate = true;
                }
            }

            else if (AI_State == State_Notice)
            {
                if (Main.player[npc.target].Distance(npc.Center) < 500f)
                {
                    AI_Timer++;
                    //fly towards the player
                    //MoveTowards(npc, target, (float)distance > 300f?13f :7f, 30f);

                    if (AI_Timer == 1)
                    {
                        // We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up. 
                        npc.velocity = new Vector2(npc.direction * 3, -7f);
                    }
                    // after half a second, transition to the first attack
                    if (AI_Timer >= 30)
                    {
                        AI_State = State_Attack;
                        AI_Timer = 0;
                    }
                }
                else
                {
                    npc.TargetClosest(true);
                    if (!npc.HasValidTarget || Main.player[npc.target].Distance(npc.Center) > 700f)
                    {
                        AI_State = State_Passive;
                        AI_Timer = 0;
                    }
                }
            }

            else if (AI_State == State_Attack)
            {
                npc.velocity.X = x * factor;
                npc.velocity.Y = y * factor;
                AI_Timer++;
                if (AI_Timer %40 == 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        //Dust.NewDust(npc.position, npc.width, npc.height, DustID.WhiteTorch, npc.velocity.X, npc.velocity.Y, 96);
                        //Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
                    }
                }
                if(AI_Timer == 99)
                {
                    AI_Timer = 0;
                }
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNight.Chance * 0.1f;
        }

        public override void NPCLoot()
        {
            //guaranteed drop
            Item.NewItem(npc.position, ItemID.Ectoplasm, 5);
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem(npc.position, mod.ItemType("BagOfSouls"));
            }
        }

        private const int Frame_Float_1 = 0;
        private const int Frame_Float_2 = 1;
        private const int Frame_Float_3 = 2;

        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[npc.target];
            npc.spriteDirection = (player.position.X > npc.position.X ? 1 : -1);
            npc.frameCounter++;
            if (npc.frameCounter < 20)
            {
                npc.frame.Y = Frame_Float_1 * frameHeight;
            }
            else if (npc.frameCounter < 40)
            {
                npc.frame.Y = Frame_Float_2 * frameHeight;
            }
            else if (npc.frameCounter < 60)
            {
                npc.frame.Y = Frame_Float_3 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }
    }
}
