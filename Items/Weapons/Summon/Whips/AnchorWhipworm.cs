using Avalon.Projectiles.Summon;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Whips;

public class AnchorWhipworm : ModItem
{
    //public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ExampleWhipDebuff.TagDamage);

    public override void SetDefaults()
    {
        // This method quickly sets the whip's properties.
        // Mouse over to see its parameters.
        Item.DefaultToWhip(ModContent.ProjectileType<AnchorWhipwormProjectile>(), 20, 2, 4);
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(gold: 4);
    }
    // Makes the whip receive melee prefixes
    public override bool MeleePrefix()
    {
        return true;
    }
}
