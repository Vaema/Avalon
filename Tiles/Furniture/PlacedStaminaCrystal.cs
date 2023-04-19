using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Avalon.Items.Potions.Other;
using Avalon.Items.Consumables;
using System.Collections.Generic;

namespace Avalon.Tiles.Furniture
{
    public class PlacedStaminaCrystal : ModTile
    {
        public override void SetStaticDefaults()
        {
            AddMapEntry(Color.Green);
            AnimationFrameHeight = 36;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.DrawYOffset = 0;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 300;
            Main.tileFrameImportant[Type] = true;
            DustType = DustID.Grass;
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter > 6)
            {
                frameCounter = 0;
                frame++;
                if (frame >= 11) frame = 0;
            }
        }
        public override bool KillSound(int i, int j, bool fail)
        {
            if (!fail)
            {
                SoundEngine.PlaySound(SoundID.Shatter, new Vector2(i, j).ToWorldCoordinates());
                return false;
            }
            return base.KillSound(i, j, fail);
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ModContent.ItemType<StaminaCrystal>(), 1, -1);
        }
        //public override void KillMultiTile(int i, int j, int frameX, int frameY)
        //{
        //    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<StaminaCrystal>(), pfix: -1);
        //}
    }
}
