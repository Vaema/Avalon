using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Players;
using Terraria.Localization;
using System.Collections.Generic;
using Avalon.Items.Material.Bars;
using Avalon.Buffs;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
class CaesiumGalea : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Caesium Galea");
        //Tooltip.SetDefault("8% increased melee damage");
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 31;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.height = dims.Height;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Headgear;
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<CaesiumPlateMail>() && legs.type == ModContent.ItemType<CaesiumGreaves>();
    }
    public override void ArmorSetShadows(Player player)
    {
        if (player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
            player.armorEffectDrawOutlinesForbidden = true;
        else
            player.armorEffectDrawShadow = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<CaesiumBar>(), 30)
            .AddIngredient(ItemID.HellstoneBar, 10)
            .AddIngredient(ItemID.SoulofSight, 5)
            .AddTile(TileID.MythrilAnvil).Register();
    }
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Increased stats\nDouble tap " + Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN") +
            " to activate Caesium Boosting stance\n" +
            "This stance reduces movement speed and increases damage reduction";
        player.GetDamage(DamageClass.Melee) += 0.05f;
        player.statDefense += 4;
        player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoost = true;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 0.08f;
    }
}

public class CaesiumBoostingStancePlayer : ModPlayer
{
    public bool CaesiumBoost;
    public bool CaesiumBoostActive;
    public override void ResetEffects()
    {
        CaesiumBoost = false;
    }
    public override void PostUpdateEquips()
    {
        if (!CaesiumBoost)
            CaesiumBoostActive = false;
        //if (Player.doubleTapCardinalTimer[Main.ReversedUpDownArmorSetBonuses ? 1 : 0] < 15 && ((Player.releaseUp && Main.ReversedUpDownArmorSetBonuses && Player.controlUp) || (Player.releaseDown && !Main.ReversedUpDownArmorSetBonuses && Player.controlDown)))
        //{
            if (CaesiumBoost && !Player.mount.Active && Player.PlayerDoublePressedSetBonusActivateKey())
            {
                CaesiumBoostActive = !CaesiumBoostActive;
            }
        //}
    }
    public override void PostUpdate()
    {
        if (CaesiumBoostActive)
        {
            Player.AddBuff(ModContent.BuffType<CaesiumBoostingStance>(), 2);
        }
    }
}
