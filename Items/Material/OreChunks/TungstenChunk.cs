using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.Material.OreChunks;

public class TungstenChunk : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 200;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 100;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(ItemID.TungstenBar)
            .AddIngredient(Type, 4)
            .AddTile(TileID.Furnaces)
			.SortAfterFirstRecipesOf(ItemID.TungstenBar)
			.Register();
    }
}
