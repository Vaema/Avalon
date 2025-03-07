using Avalon.Buffs;
using Avalon.Buffs.AdvancedBuffs;
using Avalon.Buffs.Debuffs;
using Avalon.Common;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class BuffEffects : ModHook
{
    protected override void Apply()
    {
        //On_Projectile.FishingCheck_RollDropLevels += OnFishingCheckRollDropLevels;
        //CommonCode.DropItemForEachInteractingPlayerOnThePlayer += OnDropItemForEachInteractingPlayerOnThePlayer;
        On_Player.AddBuff += OnAddBuff;
        On_NPC.AddBuff += OnAddBuffNPC;
    }

    private static void OnAddBuff(On_Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet = true, bool foodHack = false)
    {
        for (int j = 0; j < 44; j++)
        {
            if (self.buffType[j] == type)
            {
                if (type == ModContent.BuffType<StaminaDrain>())
                {
                    self.buffTime[j] += timeToAdd;
                    if (self.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks < 5)
                    {
                        self.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks++;
                    }
                    if (self.buffTime[j] > AvalonStaminaPlayer.StaminaDrainTime)
                    {
                        self.buffTime[j] = AvalonStaminaPlayer.StaminaDrainTime;
                        return;
                    }
                }
                else if (self.buffTime[j] < timeToAdd)
                {
                    self.buffTime[j] = timeToAdd;
                }
                if (type == ModContent.BuffType<SkyBlessing>())
                {
                    self.buffTime[j] += timeToAdd;
                    if (self.GetModPlayer<AvalonPlayer>().SkyStacks < 10)
                    {
                        self.GetModPlayer<AvalonPlayer>().SkyStacks++;
                    }
                    else self.GetModPlayer<AvalonPlayer>().SkyStacks = 10;
                    if (self.buffTime[j] > 60 * 7)
                    {
                        self.buffTime[j] = 60 * 7;
                        return;
                    }
                }
                else if (self.buffTime[j] < timeToAdd)
                {
                    self.buffTime[j] = timeToAdd;
                }
				if (self.GetModPlayer<AvalonPlayer>().Pathogen && Main.debuff[self.buffType[j]])
				{
					if (self.buffTime[j] < timeToAdd * 2)
						self.buffTime[j] = timeToAdd * 2;
				}
				else if (self.GetModPlayer<AvalonPlayer>().ThePill && Main.debuff[self.buffType[j]])
				{
					if (self.buffTime[j] > timeToAdd * 0.8f)
						self.buffTime[j] = (int)(timeToAdd * 0.8f);
				}
				else if (type == ModContent.BuffType<Leaping>() || type == ModContent.BuffType<AdvLeaping>())
				{
					self.GetModPlayer<AvalonPlayer>().IcarusTimer = 0;
				}
				/*if (type == ModContent.BuffType<Buffs.SkyBlessing>())
				{
					self.buffTime[j] += timeToAdd;
					if (self.GetModPlayer<ExxoBuffPlayer>().SkyStacks < 10)
					{
						self.GetModPlayer<ExxoBuffPlayer>().SkyStacks++;
					}
					else self.GetModPlayer<ExxoBuffPlayer>().SkyStacks = 10;
					if (self.buffTime[j] > 60 * 7)
					{
						self.buffTime[j] = 60 * 7;
						return;
					}
				}
				else if (self.buffTime[j] < timeToAdd)
				{
					self.buffTime[j] = timeToAdd;
				}
				if (type == ModContent.BuffType<Buffs.Reckoning>())
				{
					self.buffTime[j] += timeToAdd;
					if (self.GetModPlayer<ExxoPlayer>().reckoningLevel < 10)
					{
						self.GetModPlayer<ExxoPlayer>().reckoningLevel++;
					}
					else
						self.GetModPlayer<ExxoPlayer>().reckoningLevel = 10;
					if (self.buffTime[j] > 60 * 8)
					{
						self.buffTime[j] = 60 * 8;
						return;
					}
				}
				else if (self.buffTime[j] < timeToAdd)
				{
					self.buffTime[j] = timeToAdd;
				}*/
				return;
            }
        }
        orig(self, type, timeToAdd, quiet, foodHack);
    }

    private static void OnAddBuffNPC(On_NPC.orig_AddBuff orig, NPC self, int type, int time, bool quiet = false)
    {
		if (ExxoAvalonOrigins.Achievements != null)
		{
			if (((self.HasBuff(BuffID.OnFire) || self.HasBuff(BuffID.OnFire3)) && self.HasBuff(BuffID.CursedInferno) && (self.HasBuff(BuffID.Frostburn) || self.HasBuff(BuffID.Frostburn2)) && type == BuffID.ShadowFlame) ||
			(self.HasBuff(BuffID.ShadowFlame) && self.HasBuff(BuffID.CursedInferno) && (self.HasBuff(BuffID.Frostburn) || self.HasBuff(BuffID.Frostburn2)) && (type == BuffID.OnFire || type == BuffID.OnFire3)) ||
			(self.HasBuff(BuffID.ShadowFlame) && self.HasBuff(BuffID.CursedInferno) && (self.HasBuff(BuffID.OnFire) || self.HasBuff(BuffID.OnFire3)) && (type == BuffID.Frostburn || type == BuffID.Frostburn2)) ||
			((self.HasBuff(BuffID.OnFire) || self.HasBuff(BuffID.OnFire3)) && self.HasBuff(BuffID.ShadowFlame) && (self.HasBuff(BuffID.Frostburn) || self.HasBuff(BuffID.Frostburn2)) && type == BuffID.CursedInferno))
			{
				ExxoAvalonOrigins.Achievements.Call("Event", "ItBurnsBurnsBurnsBurns");
			}
		}
        for (int j = 0; j < 5; j++)
        {
            if (self.buffType[j] == type)
            {
                if (type == ModContent.BuffType<Lacerated>())
                {
                    self.buffTime[j] += time;
                    if (self.GetGlobalNPC<AvalonGlobalNPCInstance>().LacerateStacks < 3)
                    {
                        self.GetGlobalNPC<AvalonGlobalNPCInstance>().LacerateStacks++;
                    }
                    if (self.buffTime[j] > AvalonGlobalNPC.BleedTime)
                    {
                        self.buffTime[j] = AvalonGlobalNPC.BleedTime;
                        return;
                    }
                }
                else if (self.buffTime[j] < time)
                {
                    self.buffTime[j] = time;
                }
                return;
            }
            if (self.GetGlobalNPC<AvalonGlobalNPCInstance>().Pathogen && Main.debuff[self.buffType[j]])
            {
                if(self.buffTime[j] < time * 2)
                    self.buffTime[j] = time * 2;
            }
    }
        orig(self, type, time, quiet);
    }

    //private static void OnFishingCheckRollDropLevels(
    //    On.Terraria.Projectile.orig_FishingCheck_RollDropLevels orig,
    //    Projectile self, int fishingLevel, out bool common,
    //    out bool uncommon, out bool rare, out bool veryrare,
    //    out bool legendary, out bool crate)
    //{
    //    orig(self, fishingLevel, out common, out uncommon, out rare, out veryrare, out legendary, out crate);
    //    if (!crate && Main.player[self.owner].HasBuff<AdvCrate>() && Main.rand.NextFloat() < AdvCrate.Chance)
    //    {
    //        crate = true;
    //    }
    //}

    //private static void OnDropItemForEachInteractingPlayerOnThePlayer(
    //    CommonCode.orig_DropItemForEachInteractingPlayerOnThePlayer orig, NPC npc, int itemId, UnifiedRandom rng,
    //    int chanceNumerator, int chanceDenominator, int stack, bool interactionRequired)
    //{
    //    if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<ExxoBuffPlayer>().Lucky)
    //    {
    //        chanceNumerator += (int)(chanceNumerator * AdvLuck.PercentIncrease);
    //    }

    //    orig(npc, itemId, rng, chanceNumerator, chanceDenominator, stack, interactionRequired);
    //}
}
