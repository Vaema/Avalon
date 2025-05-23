using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Heartstone;

public class HeartstoneSofa : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Heartstone.HeartstoneSofa>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.Ores.Heartstone>(), 5)
			.AddIngredient(ItemID.Silk, 2)
			.AddTile(TileID.Sawmill)
			.Register();
	}
}
