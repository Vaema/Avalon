using Avalon.Dusts;
using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Critters;

public class PeridotBunny : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 7;
        Main.npcCatchable[Type] = true;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;
        NPCID.Sets.TownCritter[Type] = true;
        NPCID.Sets.CountsAsCritter[Type] = true;
        NPCID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Peridot>();
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.GemBunnyAmethyst);
        AnimationType = NPCID.GemBunnyAmethyst;
        AIType = NPCID.GemBunnyAmethyst;
        NPC.friendly = false;
        NPC.catchItem = ModContent.ItemType<Items.Consumables.Critters.PeridotBunny>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("CommonBestiaryFlavor.GemBunny"))
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo) //possibly incomplete, needs special rules for special seeds?
    {
        int spawnRangeX = (int)(NPC.sWidth / 16 * 0.7);
        int spawnRangeY = (int)(NPC.sHeight / 16 * 0.7);
        for (int l = 0; l < 255; l++)
        {
            bool spawnThing = false;
            if (Main.player[l].Center.Y / 16f > (float)Main.UnderworldLayer && (!Main.remixWorld || !((double)(Main.player[l].Center.X / 16f) > (double)Main.maxTilesX * 0.39 + 50.0) || !((double)(Main.player[l].Center.X / 16f) < (double)Main.maxTilesX * 0.61)))
            {
                int chance = 10;
                if (Main.player[l].townNPCs == 1f)
                {
                    chance = 10;
                }
                else if (Main.player[l].townNPCs == 2f)
                {
                    chance = 5;
                }
                else if (Main.player[l].townNPCs >= 3f)
                {
                    chance = 3;
                }
                if (Main.rand.NextBool(chance))
                {
                    spawnThing = true;
                }
            }


            int num16 = (int)(Main.player[l].position.Y / 16f) - spawnRangeX;
            int num17 = (int)(Main.player[l].position.Y / 16f) + spawnRangeY;
            int num4 = Main.rand.Next(num16, num17);
            if (spawnThing)
            {
                if (Main.raining && num4 <= Main.UnderworldLayer && num4 >= Main.rockLayer)
                {
                    if (num4 >= Main.rockLayer && Main.rand.NextBool(35))
                    {
                        return 1f;
                    }
                }
                else if (num4 > Main.UnderworldLayer)
                {
                    if (Main.remixWorld && (double)(Main.player[l].Center.X / 16f) > Main.maxTilesX * 0.39 + 50.0 && (double)(Main.player[l].Center.X / 16f) < Main.maxTilesX * 0.61)
                    {
                        if (Main.rand.NextBool(28))
                        {
                            return 1f;
                        }
                    }
                }
                else if (Main.remixWorld)
                {
                    if (num4 < Main.rockLayer && num4 > Main.worldSurface)
                    {
                        if (num4 >= Main.rockLayer && num4 <= Main.UnderworldLayer)
                        {
                            if (Main.rand.NextBool(28))
                            {
                                return 1f;
                            }
                        }
                    }
                }
                else if (num4 >= Main.rockLayer && num4 <= Main.UnderworldLayer)
                {
                    if (Main.rand.NextBool(28))
                    {
                        return 1f;
                    }
                }
            }
        }
        return 0;
    }
    public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (NPC.direction == 1)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }
        Rectangle frame6 = NPC.frame;
        float num35 = 0f;
        float num36 = Main.NPCAddHeight(NPC);
        Vector2 halfSize = new Vector2(TextureAssets.Npc[Type].Width() / 2, TextureAssets.Npc[Type].Height() / Main.npcFrameCount[Type] / 2);
        Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Avalon/NPCs/Critters/GemBunny_Glow").Value, new Vector2(NPC.position.X - screenPos.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[Type].Width() * NPC.scale / 2f + halfSize.X * NPC.scale, NPC.position.Y - screenPos.Y + (float)NPC.height - (float)TextureAssets.Npc[Type].Height() * NPC.scale / (float)Main.npcFrameCount[Type] + 4f + halfSize.Y * NPC.scale + num36 + num35 + NPC.gfxOffY), frame6, NPC.GetAlpha(Color.White), NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        Player player = Main.player[NPC.target];
        if (NPC.life > 0)
        {
            for (int num461 = 0; (double)num461 < (double)10.0 / (double)NPC.lifeMax * 20.0; num461++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PeridotDust>(), hit.HitDirection, -1f);
            }
            return;
        }
        for (int num462 = 0; num462 < 10; num462++)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PeridotDust>(), 2 * hit.HitDirection, -2f);
        }
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("PeridotBunny1").Type);
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("PeridotBunny2").Type);
    }
}
public class TourmalineBunny : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 7;
        Main.npcCatchable[Type] = true;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;
        NPCID.Sets.TownCritter[Type] = true;
        NPCID.Sets.CountsAsCritter[Type] = true;
        NPCID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Tourmaline>();
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.GemBunnyAmethyst);
        AnimationType = NPCID.GemBunnyAmethyst;
        AIType = NPCID.GemBunnyAmethyst;
        NPC.friendly = false;
        NPC.catchItem = ModContent.ItemType<Items.Consumables.Critters.TourmalineBunny>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("CommonBestiaryFlavor.GemBunny"))
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo) //possibly incomplete, needs special rules for special seeds?
    {
        int spawnRangeX = (int)(NPC.sWidth / 16 * 0.7);
        int spawnRangeY = (int)(NPC.sHeight / 16 * 0.7);
        for (int l = 0; l < 255; l++)
        {
            bool spawnThing = false;
            if (Main.player[l].Center.Y / 16f > (float)Main.UnderworldLayer && (!Main.remixWorld || !((double)(Main.player[l].Center.X / 16f) > (double)Main.maxTilesX * 0.39 + 50.0) || !((double)(Main.player[l].Center.X / 16f) < (double)Main.maxTilesX * 0.61)))
            {
                int chance = 10;
                if (Main.player[l].townNPCs == 1f)
                {
                    chance = 10;
                }
                else if (Main.player[l].townNPCs == 2f)
                {
                    chance = 5;
                }
                else if (Main.player[l].townNPCs >= 3f)
                {
                    chance = 3;
                }
                if (Main.rand.NextBool(chance))
                {
                    spawnThing = true;
                }
            }


            int num16 = (int)(Main.player[l].position.Y / 16f) - spawnRangeX;
            int num17 = (int)(Main.player[l].position.Y / 16f) + spawnRangeY;
            int num4 = Main.rand.Next(num16, num17);
            if (spawnThing)
            {
                if (Main.raining && num4 <= Main.UnderworldLayer && num4 >= Main.rockLayer)
                {
                    if (num4 >= Main.rockLayer && Main.rand.NextBool(35))
                    {
                        return 1f;
                    }
                }
                else if (num4 > Main.UnderworldLayer)
                {
                    if (Main.remixWorld && (double)(Main.player[l].Center.X / 16f) > Main.maxTilesX * 0.39 + 50.0 && (double)(Main.player[l].Center.X / 16f) < Main.maxTilesX * 0.61)
                    {
                        if (Main.rand.NextBool(28))
                        {
                            return 1f;
                        }
                    }
                }
                else if (Main.remixWorld)
                {
                    if (num4 < Main.rockLayer && num4 > Main.worldSurface)
                    {
                        if (num4 >= Main.rockLayer && num4 <= Main.UnderworldLayer)
                        {
                            if (Main.rand.NextBool(28))
                            {
                                return 1f;
                            }
                        }
                    }
                }
                else if (num4 >= Main.rockLayer && num4 <= Main.UnderworldLayer)
                {
                    if (Main.rand.NextBool(28))
                    {
                        return 1f;
                    }
                }
            }
        }
        return 0;
    }
    public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (NPC.direction == 1)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }
        Rectangle frame6 = NPC.frame;
        float num35 = 0f;
        float num36 = Main.NPCAddHeight(NPC);
        Vector2 halfSize = new Vector2(TextureAssets.Npc[Type].Width() / 2, TextureAssets.Npc[Type].Height() / Main.npcFrameCount[Type] / 2);
        Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Avalon/NPCs/Critters/GemBunny_Glow").Value, new Vector2(NPC.position.X - screenPos.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[Type].Width() * NPC.scale / 2f + halfSize.X * NPC.scale, NPC.position.Y - screenPos.Y + (float)NPC.height - (float)TextureAssets.Npc[Type].Height() * NPC.scale / (float)Main.npcFrameCount[Type] + 4f + halfSize.Y * NPC.scale + num36 + num35 + NPC.gfxOffY), frame6, NPC.GetAlpha(Color.White), NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        Player player = Main.player[NPC.target];
        if (NPC.life > 0)
        {
            for (int num461 = 0; (double)num461 < (double)10.0 / (double)NPC.lifeMax * 20.0; num461++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<TourmalineDust>(), hit.HitDirection, -1f);
            }
            return;
        }
        for (int num462 = 0; num462 < 10; num462++)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<TourmalineDust>(), 2 * hit.HitDirection, -2f);
        }
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("TourmalineBunny1").Type);
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("TourmalineBunny2").Type);
    }
}
public class ZirconBunny : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 7;
        Main.npcCatchable[Type] = true;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;
        NPCID.Sets.TownCritter[Type] = true;
        NPCID.Sets.CountsAsCritter[Type] = true;
        NPCID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Zircon>();
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.GemBunnyAmethyst);
        AnimationType = NPCID.GemBunnyAmethyst;
        AIType = NPCID.GemBunnyAmethyst;
        NPC.friendly = false;
        NPC.catchItem = ModContent.ItemType<Items.Consumables.Critters.ZirconBunny>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("CommonBestiaryFlavor.GemBunny"))
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo) //possibly incomplete, needs special rules for special seeds?
    {
        int spawnRangeX = (int)(NPC.sWidth / 16 * 0.7);
        int spawnRangeY = (int)(NPC.sHeight / 16 * 0.7);
        
        for (int l = 0; l < 255; l++)
        {
            bool spawnThing = false;
            if (Main.player[l].Center.Y / 16f > (float)Main.UnderworldLayer && (!Main.remixWorld || !((double)(Main.player[l].Center.X / 16f) > (double)Main.maxTilesX * 0.39 + 50.0) || !((double)(Main.player[l].Center.X / 16f) < (double)Main.maxTilesX * 0.61)))
            {
                int chance = 10;
                if (Main.player[l].townNPCs == 1f)
                {
                    chance = 10;
                }
                else if (Main.player[l].townNPCs == 2f)
                {
                    chance = 5;
                }
                else if (Main.player[l].townNPCs >= 3f)
                {
                    chance = 3;
                }
                if (Main.rand.NextBool(chance))
                {
                    spawnThing = true;
                }
            }


            int num16 = (int)(Main.player[l].position.Y / 16f) - spawnRangeX;
            int num17 = (int)(Main.player[l].position.Y / 16f) + spawnRangeY;
            int num4 = Main.rand.Next(num16, num17);
            if (spawnThing)
            {
                if (Main.raining && num4 <= Main.UnderworldLayer && num4 >= Main.rockLayer)
                {
                    if (num4 >= Main.rockLayer && Main.rand.NextBool(35))
                    {
                        return 1f;
                    }
                }
                else if (num4 > Main.UnderworldLayer)
                {
                    if (Main.remixWorld && (double)(Main.player[l].Center.X / 16f) > Main.maxTilesX * 0.39 + 50.0 && (double)(Main.player[l].Center.X / 16f) < Main.maxTilesX * 0.61)
                    {
                        if (Main.rand.NextBool(28))
                        {
                            return 1f;
                        }
                    }
                }
                else if (Main.remixWorld)
                {
                    if (num4 < Main.rockLayer && num4 > Main.worldSurface)
                    {
                        if (num4 >= Main.rockLayer && num4 <= Main.UnderworldLayer)
                        {
                            if (Main.rand.NextBool(28))
                            {
                                return 1f;
                            }
                        }
                    }
                }
                else if (num4 >= Main.rockLayer && num4 <= Main.UnderworldLayer)
                {
                    if (Main.rand.NextBool(28))
                    {
                        return 1f;
                    }
                }
            }
        }
        return 0;
    }
    public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (NPC.direction == 1)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }
        Rectangle frame6 = NPC.frame;
        float num35 = 0f;
        float num36 = Main.NPCAddHeight(NPC);
        Vector2 halfSize = new Vector2(TextureAssets.Npc[Type].Width() / 2, TextureAssets.Npc[Type].Height() / Main.npcFrameCount[Type] / 2);
        Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Avalon/NPCs/Critters/GemBunny_Glow").Value, new Vector2(NPC.position.X - screenPos.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[Type].Width() * NPC.scale / 2f + halfSize.X * NPC.scale, NPC.position.Y - screenPos.Y + (float)NPC.height - (float)TextureAssets.Npc[Type].Height() * NPC.scale / (float)Main.npcFrameCount[Type] + 4f + halfSize.Y * NPC.scale + num36 + num35 + NPC.gfxOffY), frame6, NPC.GetAlpha(Color.White), NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        Player player = Main.player[NPC.target];
        if (NPC.life > 0)
        {
            for (int num461 = 0; (double)num461 < (double)10.0 / (double)NPC.lifeMax * 20.0; num461++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<ZirconDust>(), hit.HitDirection, -1f);
            }
            return;
        }
        for (int num462 = 0; num462 < 10; num462++)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<ZirconDust>(), 2 * hit.HitDirection, -2f);
        }
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("ZirconBunny1").Type);
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("ZirconBunny2").Type);
    }
}
