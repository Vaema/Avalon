using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;
public class SixSidedDie : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Pink;
        Item.Size = new Vector2(16);
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 6);
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().SixSidedDie = true;
        //player.GetModPlayer<AvalonPlayer>().MaxMeleeCrit = 20; // test
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddIngredient(ModContent.ItemType<BlackWhetstone>())
    //        .AddIngredient(ModContent.ItemType<BloodyWhetstone>())
    //        .AddIngredient(ItemID.SharkToothNecklace)
    //        .AddIngredient(ItemID.SoulofFright, 10)
    //        .AddTile(TileID.MythrilAnvil)
    //        .Register();
    //}
}
