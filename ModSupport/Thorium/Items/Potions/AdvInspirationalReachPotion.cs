using Avalon.ModSupport.Thorium.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using ThoriumMod.Buffs.Bard;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Depths;

namespace Avalon.ModSupport.Thorium.Items.Potions;

[ExtendsFromMod("ThoriumMod")]
class AdvInspirationalReachPotion : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
		return ExxoAvalonOrigins.ThoriumContentEnabled;
	}
	public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 30;
        Data.Sets.Item.ElixirToPotionBuffID.Add(Type, ModContent.BuffType<InspirationReachPotionBuff>());
        Data.Sets.Item.PotionToElixirBuffID.Add(ModContent.ItemType<InspirationReachPotion>(), ModContent.BuffType<AdvInspirationalReach>());
    }

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useTurn = true;
        Item.value = Item.sellPrice(0, 0, 4);
        Item.consumable = true;
        Item.maxStack = 9999;
        Item.UseSound = SoundID.Item3;
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.sellPrice(0, 0, 4, 0);
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.buffTime = 12 * 3600;
        Item.buffType = ModContent.BuffType<AdvInspirationalReach>();
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
}
