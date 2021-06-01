using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TheOfficialMod.NPCs.EnemyNPCs
{
    
    class Protodactyl : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Protodactyl"); // Automatic from .lang files
            Main.npcFrameCount[npc.type] = 6; // make sure to set this for your modnpcs. Main.npcFrameCount[NPCID.Zombie];
		}

		public override void SetDefaults()
		{
			npc.width = 100;
			npc.height = 84;
            npc.aiStyle = -1;
            npc.damage = 50;
			npc.defense = 20;
			npc.lifeMax = 1500;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/PterodactylHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/PterodactylKill");
            npc.knockBackResist = 0f;
            //npc.alpha = 175;
            //npc.color = new Color(0, 80, 255, 100);
            npc.noTileCollide = true;
            npc.noGravity = true;

            //aiType = NPCID.Harpy;
            animationType = NPCID.Harpy;
            
            //banner = Item.NPCtoBanner(NPCID.Zombie);
            //bannerItem = Item.BannerToItem(banner);
            npc.value = 5000f;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Underworld.Chance * 0.5f;
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax*bossLifeScale); // This scales the enemies health and damage if you are playing in an Expert World
            npc.damage = (int)(npc.damage * 1.5f);
        }

        // defining a few variables for the timers and statses of the NPC, just to make the code cleaner and easier to read
        private const int AI_State_Slot = 0;
        private const int AI_Timer_Slot = 1;
        private const int AI_Unused_Slot_2 = 2;
        private const int AI_Unused_Slot_3 = 3;


        private const int State_Passive = 0;
        private const int State_Notice = 1;
        private const int State_Flying_Attack = 2;
        private const int State_Stationary_Attack = 3;
        private const int State_Dash = 4;

        // instead of typing npc.ai[0] each type we can just ype AI_State
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
        //this is the code for its custom AI, behaviour, attacks and so on
        public override void AI()
        {
            //initializing some variables for movement, velocity etc. that will be used in attack patterns below
            Player player = Main.player[npc.target];
            Vector2 target = Main.player[npc.target].Center;
            float speed = 15f;
            Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float x = player.position.X + (float)(player.width / 2) - vector.X;
            float y = player.position.Y + (float)(player.height / 2) - vector.Y;
            float distance2 = (float)Math.Sqrt(x * x + y * y);
            float factor = speed / distance2;

            //npc.netAlways = true;
            if (AI_State == State_Passive) {
                npc.TargetClosest(true);
                //this checks if there's a valid player nearby to attack them, otherwise it ill be passive and move in 1 random direction horizontally
                if(npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 700f) {
                    AI_State = State_Notice;
                    AI_Timer = 0;
                    target = player.Center;
                }
                else {
                    npc.direction = Main.rand.NextBool() ? 1 : -1;
                    npc.velocity.X = npc.velocity.X + (1f* npc.direction);
                    npc.netUpdate = true;
                }
            }

            else if (AI_State == State_Notice)
            {
                if (Main.player[npc.target].Distance(npc.Center) < 500f)
                {
                    AI_Timer++;
                    // This code makes the NPC fly towards the player
                    if (AI_Timer == 1)
                    {
                        // We apply an initial velocity the first tick. -Y is up. 
                        npc.velocity = new Vector2(npc.direction * 3, -7f);
                    }
                    // after half a second, transition to the first attack
                    if (AI_Timer >= 30)
                    {
                        AI_State = State_Flying_Attack;
                        AI_Timer = 0;
                    }
                }
                else { // If the target is out of range go back to being passive
                    npc.TargetClosest(true);
                    if (!npc.HasValidTarget || Main.player[npc.target].Distance(npc.Center) > 700f) {
                        AI_State = State_Passive;
                        AI_Timer = 0;
                    }
                }
            }

            else if (AI_State == State_Flying_Attack) {
                //First of the three attacks, fly towards the player with moderate speed and creates a smokescreen from dust, if the velocity becomes low set it to Vector2(5f,5f), also checks if it's infront of blocks
                AI_Timer++;
                if (AI_Timer >= 20 && AI_Timer <90)
                {
                    npc.velocity.X = 8f * npc.DirectionTo(player.Center).X;
                    npc.velocity.Y = 12f * npc.DirectionTo(player.Center).Y;
                }
                else if (AI_Timer >120 && AI_Timer <150)
                {
                    if (Main.player[npc.target].Distance(npc.Center) < 64f && Math.Abs(npc.velocity.X) <= 5) {
                        npc.velocity.X = 0;// = new Vector2(0, 0);
                    }
                    else if (npc.velocity.X > 5f)
                    {
                        npc.velocity.X *= 0.99f * npc.DirectionTo(player.Center).X;
                        npc.velocity.Y *= 0.99f * npc.DirectionTo(player.Center).Y;
                    }
                    else
                    {
                        npc.velocity.X = 5 * npc.DirectionTo(player.Center).X;
                        npc.velocity.Y = 5 * npc.DirectionTo(player.Center).Y;
                    }
                }

                if (npc.collideX)
                {
                    npc.velocity += new Vector2(0, -64f);
                }

                //npc.velocity = npc.DirectionTo(player.Center + new Vector2(0, -320)) / 5f;
                //npc.velocity = npc.DirectionTo(player.Center + new Vector2(-64*npc.direction,0)) / 10f;
                //npc.velocity.X = x * 0.07f; //- 32*npc.DirectionTo(player.Center).X; 
                //npc.velocity.Y = y * 0.07f;
                //npc.velocity *= 0.99f; Gradually slows down the velocity each tick// * npc.DirectionTo(player.Center);

                if (AI_Timer >= 10)
                {
                    for (int i = 0; i < 90; i++) //set to 2 while testing, 90 makes the smokescreen
                    {
                        Dust.NewDust(npc.position + new Vector2(64 * npc.direction, Main.rand.Next(0, 2)), npc.width, npc.height, DustID.Smoke, npc.velocity.X + npc.direction * Main.rand.Next(-20, 21), npc.velocity.Y, 128, new Color(115, 130, 118), Main.rand.Next(10, 15));
                    }
                }

                if (AI_Timer >= 180) {
                    AI_State = State_Stationary_Attack; //transition to the next attack after 3 seconds. 180 are the ticks, 60 ticks make 1 second
                    AI_Timer = 0;
                }
            }
            
            else if (AI_State == State_Stationary_Attack) {
                // Stay still in one place and shoot bouncing fireballs towards the player for 2 seconds
                AI_Timer++;
                if (AI_Timer == 1 || !npc.collideX)
                {
                    npc.velocity.X = 0f;
                    npc.velocity.Y = 0f;
                }
                
                if (npc.collideX)
                {
                    npc.velocity.Y = -48f;
                }
                if (AI_Timer % (Main.expertMode ? 10 : 30) == 0)
                {
                    Vector2 shootPos = npc.Center;
                    float accuracy = 5f * (npc.lifeMax / npc.life);
                    Vector2 shootVel = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
                    shootVel.Normalize();
                    shootVel *= 7.5f;
                    float randX = 0;
                    float randY = 0;
                    float randomShotX = 0;
                    float randomShotY = 0;
                    for (int i = 0; i < (Main.expertMode ? 5 : 3); i++)
                    {
                        randX = (float)Main.rand.Next(-128, 128);
                        randY = (float)Main.rand.Next(-128, 128);
                        randomShotX = shootPos.X + randX;
                        randomShotY = shootPos.Y + randY;
                        if (Collision.CanHit(new Vector2(randomShotX, randomShotY), 32, 32, player.Center, player.width, player.height))
                        {
                        //if (Framing.GetTileSafely(i, j).active(, (int)randomShotY)) {
                            Projectile.NewProjectile(shootPos.X + randX, shootPos.Y - randY, shootVel.X, shootVel.Y, ProjectileID.Fireball, (npc.damage + (30 * (npc.lifeMax - npc.life) / npc.lifeMax)) / 4, 1f);
                            //Projectile.NewProjectile(shootPos.X + randX, shootPos.Y - randY, shootVel.X, shootVel.Y, ProjectileID custom meteor, (npc.damage + (30 * (npc.lifeMax - npc.life) / npc.lifeMax)) / 4, 1f);
                        }
                    }
                }
                if (AI_Timer >= 120) {
                    AI_State = State_Dash;
                    AI_Timer = 0;
                }
            }
            
            else if (AI_State == State_Dash)
            {
                AI_Timer++;
                //Creates dust as it moves in this phase, dashes 8 times towards the player, once every half second, after that it flies above the player in a straight line shooting 8 fireballs
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.FlameBurst);
                }
                if (AI_Timer %30 == 0 && AI_Timer<=240)
                {
                    npc.velocity.X = x * factor;
                    npc.velocity.Y = y * factor;
                }
                if (AI_Timer > 240 && AI_Timer < 260)
                {
                    npc.velocity.X = (player.position.X - npc.position.X - npc.DirectionTo(player.Center).X * 200) * 0.1f;
                    npc.velocity.Y = (player.position.Y - npc.position.Y - 384) * 0.1f;
                }
                if (AI_Timer == 260)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y = 0;
                }
                if (AI_Timer >= 261 && AI_Timer <275 && Math.Abs(npc.velocity.X) < 20f)
                {
                    npc.velocity = npc.DirectionTo(player.Center+new Vector2(0, -320))/10f;
                }
                else if(AI_Timer == 279)
                {
                    npc.velocity.X = 20f * npc.DirectionTo(player.Center).X;
                }
                if (AI_Timer > 280 && AI_Timer % 8==0)
                {
                    npc.velocity *= 1.01f; // gradually increases velocity each tick, OR npc.velocity.X = 20f * npc.DirectionTo(player.Center).X; makes the pterodactyls movement more reactive to the player, but wont rain the fireballs in an arc
                    Projectile.NewProjectile(npc.position + new Vector2(npc.DirectionTo(player.Center).X*npc.width*0.5f, 80), new Vector2(-npc.DirectionTo(player.Center).X*15f, 10f), ProjectileID.DD2BetsyFireball, 50, 2, Main.myPlayer, 0f, 0f);
                }
                if (AI_Timer >= 360) {
                    AI_State = State_Flying_Attack;
                    AI_Timer = 0;
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            //creates random red dust upon hitting the NPC
            for (int i = 0; i < 10; i++)
            {
                int dustType = Main.rand.Next(266, 272);
                int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {//we want to hide teh healthbar during the smokescreen and stationary attacks so it doesn't indicate its position
            if(AI_State == State_Flying_Attack || AI_State == State_Stationary_Attack)
            {
                return false;
            }
            return true;
        }

        public override void NPCLoot()
        {
            //guaranteed drop
            Item.NewItem(npc.position, mod.ItemType("SoulForge"));
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem(npc.position, ItemID.Vertebrae, 2);
            }
            if (Main.hardMode)
            {
                Item.NewItem(npc.position, ItemID.RottenChunk, 2);
            }
        }

    }
}


//npc.velocity.X = 14f * npc.direction;
//npc.velocity.X = 12f*(npc.position.X > player.Center.X ? 1:-1); // + (player.position.X - npc.position.X) * 0.05f
/*
                 * {
                 * Dust.NewDust(npc.position + new Vector2(64*npc.direction, 32*((int)npc.velocity.Y/Math.Abs(npc.velocity.Y))), npc.width, npc.height, DustID.Smoke, -npc.velocity.X + npc.direction*Main.rand.Next(-20,21), -npc.velocity.Y + Main.rand.Next(0,2), 128, new Color(115,130,118), Main.rand.Next(10,15));
                }
                npc.velocity = new Vector2(0, npc.direction * (Main.player[npc.target].position.Y - npc.Center.Y));
                if (AI_Timer >= 30)
                {
                    npc.velocity += new Vector2(npc.direction * (npc.Center.Y - Main.player[npc.target].position.Y) / 1.5f, 0);
                }
                */
/*
if (((Main.player[npc.target].position.Y > npc.Center.Y && Main.player[npc.target].position.X < npc.Center.X) || (Main.player[npc.target].position.Y < npc.Center.Y && Main.player[npc.target].position.X > npc.Center.X)))
{
    npc.velocity += new Vector2(npc.direction * (npc.Center.Y - Main.player[npc.target].position.Y)/1.5f, 0);
}
else if ((Main.player[npc.target].position.Y < npc.Center.Y && Main.player[npc.target].position.X < npc.Center.X) || (Main.player[npc.target].position.Y > npc.Center.Y && Main.player[npc.target].position.X > npc.Center.X)) {
    npc.velocity += new Vector2(npc.direction * (Main.player[npc.target].position.Y - npc.Center.Y)/1.5f, 0);
}
*/
//npc.velocity.X = 0f;
//npc.velocity.Y = 0f;

/*
if (AI_Timer % 30f == 0)
{
    npc.velocity = new Vector2(AI_Timer % 30 == 0 ? -5f : 5f, AI_Timer % 60 ==0?10f : -10f);                
    //npc.velocity = new Vector2(npc.direction * (Main.player[npc.target].position.Y - npc.Center.Y), 0);
}
*/

/*
if (AI_Timer >=90)
{
    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5*npc.direction, 5, ProjectileID.DD2BetsyFlameBreath, (npc.damage + (30 * (npc.lifeMax - npc.life) / npc.lifeMax)) / 4, 1f);
    Projectile.NewProjectile(player.position + new Vector2(0, -500), new Vector2(0, 0), ProjectileID.DD2BetsyFireball, 50, 2, Main.myPlayer, 0f, 0f);
}
*/

/*
for (int i = 0; i < 10; i++)
{
    Dust.NewDust(npc.position, npc.width, npc.height, DustID.SomethingRed);
}
*/

//npc.velocity = new Vector2(0, npc.direction * (Main.player[npc.target].position.Y - npc.Center.Y) / 2f);
//npc.velocity += new Vector2(npc.direction * (Main.player[npc.target].position.Y - npc.Center.Y)/2f, 0);

//npc.velocity.X = 3.5f * npc.direction;

/*
if (Main.rand.Next(5) == 0)
{
for (int i = 0; i < 30; i++)
{
Dust.NewDust(npc.position, npc.width, npc.height, DustID.Sandnado);
}
}
*/
