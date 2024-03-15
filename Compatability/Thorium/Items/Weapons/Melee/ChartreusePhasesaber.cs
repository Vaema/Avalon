using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Items.Weapons.Melee;

[ExtendsFromMod("ThoriumMod")]
public class ChartreusePhasesaber : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.RedPhasesaber);
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<ChartreusePhaseblade>())
            .AddIngredient(ItemID.CrystalShard, 50)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        Lighting.AddLight((int)((player.itemLocation.X + 6f + player.velocity.X) / 16f), (int)((player.itemLocation.Y - 14f) / 16f), 0.909f, 0.909f, 0.478f);
    }
}
