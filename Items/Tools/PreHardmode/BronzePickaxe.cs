using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class BronzePickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.TinPickaxe);
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 8)
			.AddRecipeGroup(RecipeGroupID.Wood, 4)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
