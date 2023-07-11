using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class PrimordialOre : ModTile
{
    public static int[] NoRequirement = new int[]
    {
        ItemID.CopperOre,
        ItemID.TinOre,
        ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
        ItemID.IronOre,
        ItemID.LeadOre,
        ModContent.ItemType<Items.Material.Ores.NickelOre>(),
        ItemID.SilverOre,
        ItemID.TungstenOre,
        ModContent.ItemType<Items.Material.Ores.ZincOre>(),
        ItemID.GoldOre,
        ItemID.PlatinumOre,
        ModContent.ItemType<Items.Material.Ores.BismuthOre>()
    };
    public static int[] Power55 = new int[]
    {
        ItemID.CopperOre,
        ItemID.TinOre,
        ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
        ItemID.IronOre,
        ItemID.LeadOre,
        ModContent.ItemType<Items.Material.Ores.NickelOre>(),
        ItemID.SilverOre,
        ItemID.TungstenOre,
        ModContent.ItemType<Items.Material.Ores.ZincOre>(),
        ItemID.GoldOre,
        ItemID.PlatinumOre,
        ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
        ItemID.Meteorite,
        ItemID.DemoniteOre,
        ItemID.CrimtaneOre,
        ItemID.Obsidian,
        ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
    };
    public static int[] Power60 = new int[]
    {
        ItemID.CopperOre,
        ItemID.TinOre,
        ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
        ItemID.IronOre,
        ItemID.LeadOre,
        ModContent.ItemType<Items.Material.Ores.NickelOre>(),
        ItemID.SilverOre,
        ItemID.TungstenOre,
        ModContent.ItemType<Items.Material.Ores.ZincOre>(),
        ItemID.GoldOre,
        ItemID.PlatinumOre,
        ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
        ItemID.Meteorite,
        ItemID.DemoniteOre,
        ItemID.CrimtaneOre,
        ItemID.Obsidian,
        ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
        ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
        ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
        ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
    };
    public static int[] Power70 = new int[]
    {
        ItemID.CopperOre,
        ItemID.TinOre,
        ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
        ItemID.IronOre,
        ItemID.LeadOre,
        ModContent.ItemType<Items.Material.Ores.NickelOre>(),
        ItemID.SilverOre,
        ItemID.TungstenOre,
        ModContent.ItemType<Items.Material.Ores.ZincOre>(),
        ItemID.GoldOre,
        ItemID.PlatinumOre,
        ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
        ItemID.Meteorite,
        ItemID.DemoniteOre,
        ItemID.CrimtaneOre,
        ItemID.Obsidian,
        ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
        ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
        ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
        ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
        ItemID.Hellstone
    };
    public static int[] Power100 = new int[]
    {
        ItemID.CopperOre,
        ItemID.TinOre,
        ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
        ItemID.IronOre,
        ItemID.LeadOre,
        ModContent.ItemType<Items.Material.Ores.NickelOre>(),
        ItemID.SilverOre,
        ItemID.TungstenOre,
        ModContent.ItemType<Items.Material.Ores.ZincOre>(),
        ItemID.GoldOre,
        ItemID.PlatinumOre,
        ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
        ItemID.Meteorite,
        ItemID.DemoniteOre,
        ItemID.CrimtaneOre,
        ItemID.Obsidian,
        ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
        ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
        ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
        ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
        ItemID.Hellstone,
        ItemID.CobaltOre,
        ItemID.PalladiumOre,
        //ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
    };
    public static int[] Power110 = new int[]
    {
        ItemID.CopperOre,
        ItemID.TinOre,
        ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
        ItemID.IronOre,
        ItemID.LeadOre,
        ModContent.ItemType<Items.Material.Ores.NickelOre>(),
        ItemID.SilverOre,
        ItemID.TungstenOre,
        ModContent.ItemType<Items.Material.Ores.ZincOre>(),
        ItemID.GoldOre,
        ItemID.PlatinumOre,
        ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
        ItemID.Meteorite,
        ItemID.DemoniteOre,
        ItemID.CrimtaneOre,
        ItemID.Obsidian,
        ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
        ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
        ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
        ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
        ItemID.Hellstone,
        ItemID.CobaltOre,
        ItemID.PalladiumOre,
        //ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
        ItemID.MythrilOre,
        ItemID.OrichalcumOre,
        //ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
    };
    public static int[] Power150 = new int[]
    {
        ItemID.CopperOre,
        ItemID.TinOre,
        ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
        ItemID.IronOre,
        ItemID.LeadOre,
        ModContent.ItemType<Items.Material.Ores.NickelOre>(),
        ItemID.SilverOre,
        ItemID.TungstenOre,
        ModContent.ItemType<Items.Material.Ores.ZincOre>(),
        ItemID.GoldOre,
        ItemID.PlatinumOre,
        ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
        ItemID.Meteorite,
        ItemID.DemoniteOre,
        ItemID.CrimtaneOre,
        ItemID.Obsidian,
        ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
        ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
        ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
        ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
        ItemID.Hellstone,
        ItemID.CobaltOre,
        ItemID.PalladiumOre,
        //ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
        ItemID.MythrilOre,
        ItemID.OrichalcumOre,
        //ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
        ItemID.AdamantiteOre,
        ItemID.TitaniumOre,
        //ModContent.ItemType<Items.Material.Ores.TroxiniumOre>(),
    };
    public static int[] Power200 = new int[]
    {
        ItemID.CopperOre,
        ItemID.TinOre,
        ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
        ItemID.IronOre,
        ItemID.LeadOre,
        ModContent.ItemType<Items.Material.Ores.NickelOre>(),
        ItemID.SilverOre,
        ItemID.TungstenOre,
        ModContent.ItemType<Items.Material.Ores.ZincOre>(),
        ItemID.GoldOre,
        ItemID.PlatinumOre,
        ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
        ItemID.Meteorite,
        ItemID.DemoniteOre,
        ItemID.CrimtaneOre,
        ItemID.Obsidian,
        ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
        ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
        ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
        ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
        ItemID.Hellstone,
        ItemID.CobaltOre,
        ItemID.PalladiumOre,
        //ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
        ItemID.MythrilOre,
        ItemID.OrichalcumOre,
        //ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
        ItemID.AdamantiteOre,
        ItemID.TitaniumOre,
        //ModContent.ItemType<Items.Material.Ores.TroxiniumOre>(),
        ItemID.ChlorophyteOre,
        //ModContent.ItemType<Items.Material.Ores.XanthophyteOre>(),
        ModContent.ItemType<Items.Material.Ores.CaesiumOre>(),
    };
    // add more tiers later

    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), LanguageManager.Instance.GetText("Primordial Ore"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 815;
        HitSound = SoundID.Tink;
        DustType = DustID.Stone;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        Asset<Texture2D> texture = TextureAssets.Tile[Type];

        // if (Main.canDrawColorTile(i, j))
        // {
        //     texture = Main.tileAltTexture[Type, tile.color()];
        // }
        // else
        // {
        //     texture = Main.tileTexture[Type];
        // }
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
        spriteBatch.Draw(texture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
    }

    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        int itemType = 0;
        int stackSize = 1;

        Player p = Main.player[Player.FindClosest(new(i * 16, j * 16), 16, 16)];

        int power = 0;
        for (int x = 0; x < 58; x++)
        {
            if (p.inventory[x].pick > 0 || p.inventory[x].pick > power)
            {
                power = p.inventory[x].pick;
            }
        }

        if (power < 55)
        {
            stackSize = 10;
            itemType = Main.rand.NextFromList(NoRequirement);
        }
        else if (power < 60)
        {
            stackSize = 8;
            itemType = Main.rand.NextFromList(Power55);
        }
        else if (power < 70)
        {
            stackSize = 7;
            itemType = Main.rand.NextFromList(Power60);
        }
        else if (power < 100)
        {
            stackSize = 6;
            itemType = Main.rand.NextFromList(Power70);
        }
        else if (power < 110)
        {
            stackSize = 5;
            itemType = Main.rand.NextFromList(Power100);
        }
        else if (power < 150)
        {
            stackSize = 5;
            itemType = Main.rand.NextFromList(Power110);
        }
        else if (power < 200)
        {
            stackSize = 5;
            itemType = Main.rand.NextFromList(Power150);
        }
        else // if
        {
            stackSize = 4;
            itemType = Main.rand.NextFromList(Power200);
        }
        yield return new Item(itemType, stackSize);
    }
}
