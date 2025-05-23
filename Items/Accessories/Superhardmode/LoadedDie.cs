using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;
public class LoadedDie : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 6);
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().LoadedDie = true;
		player.GetModPlayer<AvalonPlayer>().CrystalEdge = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Hardmode.SixSidedDie>())
			.AddIngredient(ModContent.ItemType<CrystalEdge>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
