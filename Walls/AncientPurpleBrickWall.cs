using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class AncientPurpleBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.AncientPurpleBrickWall>();
        AddMapEntry(new Color(40, 28, 69));
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
    }
}
