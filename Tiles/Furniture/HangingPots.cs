using Avalon.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.GameContent.Drawing.TileDrawing;

namespace Avalon.Tiles.Furniture;

public class HangingPots : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true; // Any multitile requires this
        Main.tileLavaDeath[Type] = true;

        DustType = -1;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.Origin = new Point16(0, 0);
        TileObjectData.newTile.AnchorBottom = default(AnchorData);
        TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.newTile.DrawYOffset = 0;
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.PlatformNonHammered, TileObjectData.newTile.Width, 0);
        TileObjectData.newAlternate.DrawYOffset = -10;
        TileObjectData.addAlternate(0);

        // Register the tile data itself
        TileObjectData.addTile(Type);

        // Register map name and color
        AddMapEntry(new Color(147, 166, 42));
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        //Tile tile = Main.tile[i, j];
        //int topLeftX = i - tile.TileFrameX / 36 % 1;
        //int topLeftY = j - tile.TileFrameY / 18 % 3;
        //if (WorldGen.IsBelowANonHammeredPlatform(topLeftX, topLeftY))
        //{
        //    offsetY -= 8;
        //}
    }
    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        bool intoRenderTargets = true;
        bool flag = intoRenderTargets || Main.LightingEveryFrame;

        if (Main.tile[i, j].TileFrameX % 36 == 0 && Main.tile[i, j].TileFrameY % 54 == 0 && flag)
        {
            Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileCounterType.MultiTileVine);
            //Point p = new(i, j);
            //if (!Coordinates.Contains(p)) Coordinates.Add(p);
        }

        return false;
    }
}
