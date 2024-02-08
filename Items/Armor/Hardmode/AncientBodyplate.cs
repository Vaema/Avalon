using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
class AncientBodyplate : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 35;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 20);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.SolarFlareBreastplate)
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.FragmentVortex, 10)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
            .Register();

        CreateRecipe(1).AddIngredient(ItemID.NebulaBreastplate).AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.FragmentVortex, 10)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
            .Register();

        CreateRecipe(1).AddIngredient(ItemID.StardustBreastplate)
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.FragmentVortex, 10)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
            .Register();

        CreateRecipe(1).AddIngredient(ItemID.VortexBreastplate)
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
            .Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.aggro += 500;
        player.GetKnockback(DamageClass.Summon) += 0.1f;
    }
}
