using Avalon.Common;
using Avalon.ModSupport.Thorium.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;

namespace Avalon.ModSupport.Thorium.Items.Potions;

[ExtendsFromMod("ThoriumMod")]
class AdvKineticPotion : KineticPotion
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ExxoAvalonOrigins.ThoriumContentEnabled;
	}
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Item.ResearchUnlockCount = 30;
		Data.Sets.ItemSets.ElixirToPotionBuffID.Add(Type, ModContent.BuffType<KineticPotionBuff>());
		Data.Sets.ItemSets.PotionToElixirBuffID.Add(ModContent.ItemType<KineticPotion>(), ModContent.BuffType<AdvKinetic>());
	}
	public override void AddRecipes()
	{
	}
	public override void SetDefaults()
	{
		base.SetDefaults();
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 0, 4, 0);
		Item.useTime = 15;
		Item.useAnimation = 15;
		Item.buffTime = TimeUtils.MinutesToTicks(5);
		Item.buffType = ModContent.BuffType<AdvKinetic>();
		Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
	}
}
