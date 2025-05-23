using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.MusicBoxes;

public class MusicBoxTuhrtlOutpost : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        if (ExxoAvalonOrigins.MusicMod != null)
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/TuhrtlOutpost"), ModContent.ItemType<MusicBoxTuhrtlOutpost>(), ModContent.TileType<Tiles.MusicBoxes>(), 324);
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBoxes>(), 9);
    }
}
