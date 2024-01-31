using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class FlightTimeScroll : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.accessory = true;
        Item.width = dims.Width;
        Item.rare = ItemRarityID.Green;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = new SoundStyle("Avalon/Sounds/Item/Scroll");
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().StaminaScroll = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Book)
            .AddIngredient(ItemID.Feather, 20)
            .AddIngredient(ItemID.SoulofFlight, 15)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>())
            .AddTile(TileID.Bookcases)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            player.GetModPlayer<AvalonStaminaPlayer>().FlightRestoreUnlocked = true;
        }
    }
}
