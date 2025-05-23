using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.Superhardmode;

public class TheOasisRemembered : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTome(5, 3);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Summon) += 0.2f;
		player.GetKnockback(DamageClass.Summon) += 0.2f;
	}

	//public override void AddRecipes()
	//{
	//    CreateRecipe(1)
	//        .AddIngredient(ModContent.ItemType<DragonOrb>())
	//        .AddIngredient(ModContent.ItemType<HydrolythBar>(), 25)
	//        .AddIngredient(ModContent.ItemType<SoulofBlight>(), 10)
	//        .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3)
	//        .AddTile(ModContent.TileType<Tiles.TomeForge>())
	//        .Register();
	//}
}
