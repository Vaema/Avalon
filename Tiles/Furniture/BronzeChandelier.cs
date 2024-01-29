using Avalon.Common.Templates;
using Avalon.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture;

public class BronzeChandelier : ChandelierTemplate
{
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f;
            g = 0.95f;
            b = 0.65f;
        }
    }
    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        bool intoRenderTargets = true;
        bool flag = intoRenderTargets || Main.LightingEveryFrame;

        if (Main.tile[i, j].TileFrameX % 54 == 0 && Main.tile[i, j].TileFrameY % 54 == 0 && flag)
        {
            Main.instance.TilesRenderer.AddSpecialPoint(i, j, 5);
        }
        if (flag)
        {
            ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
            Color color = new Color(224, 104, 147, 0);
            int frameX = Main.tile[i, j].TileFrameX;
            int frameY = Main.tile[i, j].TileFrameY;
            int width = 18;
            int offsetY = 0;
            int height = 18;
            int offsetX = 1;
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            for (int k = 0; k < 7; k++)
            {
                float x = (float)Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
                float y = (float)Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Flame").Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }

        return false;
    }
    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        //ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        //Color color = new Color(224, 104, 147, 0);
        //int frameX = Main.tile[i, j].TileFrameX;
        //int frameY = Main.tile[i, j].TileFrameY;
        //int width = 18;
        //int offsetY = 0;
        //int height = 18;
        //int offsetX = 1;
        //Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        //if (Main.drawToScreen)
        //{
        //    zero = Vector2.Zero;
        //}
        //for (int k = 0; k < 7; k++)
        //{
        //    float x = (float)Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
        //    float y = (float)Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
        //    Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Flame").Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //}
    }
}
