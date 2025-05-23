using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class BronzeHammer : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.TinHammer);
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 8)
			.AddRecipeGroup(RecipeGroupID.Wood, 3)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
