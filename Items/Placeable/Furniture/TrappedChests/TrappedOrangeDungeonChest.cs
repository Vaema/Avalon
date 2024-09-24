using Avalon.Items.Placeable.Furniture.OrangeDungeon;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.TrappedChests;

public class TrappedOrangeDungeonChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 5;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<OrangeDungeonChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
			.Register();
    }
}
