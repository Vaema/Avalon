using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ContagionCystWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.NewWall3[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ContagionCystWall>();
        AddMapEntry(new Color(61, 70, 64));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
