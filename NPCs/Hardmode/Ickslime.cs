using Terraria.GameContent.Bestiary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Avalon.Items.Material;
using Avalon.Common.Players;
using System;
using Terraria.Localization;
using Avalon.Common;
using System.IO;

namespace Avalon.NPCs.Hardmode;

public class Ickslime : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 2;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        Data.Sets.NPCSets.Wicked[NPC.type] = true;
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1,5,10));
        npcLoot.Add(ItemDropRule.StatusImmunityItem(ItemID.Vitamins, 90));

        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsNotUp(), ItemID.PigPetItem, 1500));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsUp(), ItemID.PigPetItem, 500));
    }

    public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
        if (Main.rand.NextBool(3))
        {
            target.AddBuff(BuffID.Weak, 360);
        }
        else
        {
            target.AddBuff(BuffID.Weak, 120);
        }
    }
    public override void SetDefaults()
    {
        NPC.damage = 57;
        NPC.lifeMax = 186;
        NPC.defense = 30;
		if (NPC.IsABestiaryIconDummy)
		{
			NPC.alpha = 55;
		}
		else
		{
			NPC.alpha = 255;
		}
        NPC.width = 40;
        NPC.aiStyle = 1;
        NPC.scale = 1.1f;
        NPC.value = 400f;
        NPC.height = 30;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.IckslimeBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<Biomes.UndergroundContagion>().Type };
    }
    //public override void OnSpawn(IEntitySource source)
    //{
    //    int J = Main.rand.Next(0, 3);
    //    if (J == 1)
    //    {
    //        NPC.lifeMax = (int)(NPC.lifeMax * 0.9f);
    //        NPC.defense = (int)(NPC.defense * 0.8f);
    //        NPC.scale *= 0.85f;
    //        NPC.knockBackResist *= 1.2f;
    //        NPC.value *= 0.8f;
    //    }
    //    if (J == 2)
    //    {
    //        NPC.lifeMax = (int)(NPC.lifeMax * 1.2f);
    //        NPC.defense = (int)(NPC.defense * 1.1f);
    //        NPC.scale *= 1.15f;
    //        NPC.knockBackResist *= 0.9f;
    //        NPC.value *= 1.2f;
    //    }
    //    NPC.Size *= NPC.scale;
    //    NPC.life = NPC.lifeMax;
    //    NPC.netUpdate = true;
    //    if (Main.netMode == 2 && NPC.whoAmI < 200)
    //        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI);
    //}
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Ickslime"))
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return ((spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion || spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneUndergroundContagion) &&
            !spawnInfo.Player.InPillarZone() && Main.hardMode) ? 0.7f : 0f;
    }
    int timer;
    int J;
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        J = reader.ReadInt32();
        NPC.alpha = reader.ReadInt32();
        timer = reader.ReadInt32();
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(J);
        writer.Write(NPC.alpha);
        writer.Write(timer);
    }
    public override void AI()
    {
        timer++;
        if (timer == 1)
        {
            J = Main.rand.Next(3);
        }
        if (timer == 2)
        {
            NPC.alpha = 55;
            if (J == 1)
            {
                NPC.lifeMax = (int)(NPC.lifeMax * 0.9f);
                NPC.defense = (int)(NPC.defense * 0.8f);
                NPC.damage = (int)(NPC.damage * 0.8f);
                NPC.scale *= 0.85f;
                NPC.knockBackResist *= 1.2f;
                NPC.value *= 0.8f;
            }
            if (J == 2)
            {
                NPC.lifeMax = (int)(NPC.lifeMax * 1.2f);
                NPC.defense = (int)(NPC.defense * 1.1f);
                NPC.damage = (int)(NPC.damage * 1.2f);
                NPC.scale *= 1.15f;
                NPC.knockBackResist *= 0.9f;
                NPC.value *= 1.2f;
            }
            NPC.Size *= NPC.scale;
            NPC.life = NPC.lifeMax;
            NPC.netUpdate = true;
        }

        if (Main.expertMode)
        {
            VanillaNPCExpertChanges.SlimeJumpSpeedIncrease(NPC, 1.3f);
            NPC.ai[1]++;
        }
    }
    public override void FindFrame(int frameHeight)
    {
        var num2 = 0;
        if (NPC.aiAction == 0)
        {
            if (NPC.velocity.Y < 0f)
            {
                num2 = 2;
            }
            else if (NPC.velocity.Y > 0f)
            {
                num2 = 3;
            }
            else if (NPC.velocity.X != 0f)
            {
                num2 = 1;
            }
            else
            {
                num2 = 0;
            }
        }
        else if (NPC.aiAction == 1)
        {
            num2 = 4;
        }
        NPC.frameCounter += 1.0;
        if (num2 > 0)
        {
            NPC.frameCounter += 1.0;
        }
        if (num2 == 4)
        {
            NPC.frameCounter += 1.0;
        }
        if (NPC.frameCounter >= 8.0)
        {
            NPC.frame.Y = NPC.frame.Y + frameHeight;
            NPC.frameCounter = 0.0;
        }
        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            for (int i = 0; i < 30; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, 0, 175, default, Main.rand.NextFloat(1, 1.2f));
                Main.dust[d].color = new Color(215, 225, 162);
                Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1.5f, 5) * MathHelper.Clamp(NPC.velocity.X, -1, 1), Main.rand.NextFloat(-1, -5));
            }
        }
        else
        for (int i = 0; i < Math.Min(hit.Damage / 3,30) + 1; i++)
        {
            int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, 0, 175, default, Main.rand.NextFloat(1, 1.2f));
            Main.dust[d].color = new Color(215,225,162);
            Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1.3f, 4) * MathHelper.Clamp(NPC.velocity.X, -1, 1), Main.rand.NextFloat(-1, -3));
        }
    }
}
