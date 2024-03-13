using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Walls;

[ExtendsFromMod("ThoriumMod")]
public class ChartreuseStainedGlass : ModWall
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void SetStaticDefaults()
    {
        WallID.Sets.Transparent[Type] = true;
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ChartreuseStainedGlass>();
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ChartreuseStainedGlass>();
        DustType = ModContent.DustType<Dusts.ChrysoberylDust>();
        HitSound = SoundID.Shatter;
    }
}
