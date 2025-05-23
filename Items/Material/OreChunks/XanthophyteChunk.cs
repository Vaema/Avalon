using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.Material.OreChunks;

public class XanthophyteChunk : ModItem
{
    // remove after this is added
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
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
        Item.rare = ItemRarityID.Yellow;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(ModContent.ItemType<Material.Bars.XanthophyteBar>())
    //        .AddIngredient(Type, 5)
    //        .AddTile(TileID.AdamantiteForge)
    //        .Register();
    //}
}
