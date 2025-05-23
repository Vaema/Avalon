using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.Material.OreChunks;

public class BacciliteChunk : ModItem
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
        Item.rare = ItemRarityID.Blue;
    }
    public override void AddRecipes()
    {
        Recipe.Create(ModContent.ItemType<Material.Bars.BacciliteBar>())
            .AddIngredient(Type, 3)
            .AddTile(TileID.Furnaces)
			.SortAfterFirstRecipesOf(ModContent.ItemType<Material.Bars.BacciliteBar>())
			.Register();
    }
}
