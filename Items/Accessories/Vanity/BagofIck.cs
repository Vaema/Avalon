using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

internal class BagofIck : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 1);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            UpdateVanity(player);
        }
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.YuckyBit>(), 15)
            //.AddIngredient(ModContent.ItemType<Material.Pathogen>(), 10)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.ChunkstoneBlock>(), 50)
            .AddIngredient(ModContent.ItemType<Material.Shards.CorruptShard>(), 5)
            .AddTile(TileID.Hellforge)
            .Register();
    }
    public override void UpdateVanity(Player player)
    {
        if (!(player.velocity.Length() > 0))
        {
            return;
        }

        for (int j = 0; j < 2; j++)
        {
            int num2 = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f),
                player.width, player.height, DustID.CorruptGibs, 0f, 0f, 100, default, 0.9f);
            Main.dust[num2].noGravity = true;
            Main.dust[num2].noLight = true;
            Main.dust[num2].velocity.X -= player.velocity.X * 0.5f;
            Main.dust[num2].velocity.Y -= player.velocity.Y * 0.5f;

            num2 = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f),
                player.width, player.height, DustID.CorruptGibs, 0f, 0f, 100, default, 1.5f);
            Main.dust[num2].noGravity = true;
            Main.dust[num2].noLight = true;
            Main.dust[num2].velocity.X -= player.velocity.X * 0.5f;
            Main.dust[num2].velocity.Y -= player.velocity.Y * 0.5f;
        }

        //int dust1 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.CorruptGibs, 0f, 0f, 100,
        //    Color.White, 0.9f);
        //Main.dust[dust1].noGravity = true;
        //int dust2 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.CorruptGibs, 0f, 0f, 100,
        //    Color.White, 1.5f);
        //Main.dust[dust2].noGravity = true;
    }
}
