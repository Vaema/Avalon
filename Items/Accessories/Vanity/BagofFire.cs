using Avalon.Common;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

public class BagofFire : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.VanityBags;
	}
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Green;
		Item.vanity = true;
		Item.value = Item.sellPrice(0, 1);
		Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
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
		CreateRecipe()
			.AddIngredient(ItemID.Fireblossom, 15)
			.AddIngredient(ItemID.HellstoneBar, 10)
			.AddIngredient(ItemID.AshBlock, 50)
			.AddIngredient(ModContent.ItemType<FireShard>(), 5)
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
				player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 2f);
			Main.dust[num2].noGravity = true;
			Main.dust[num2].noLight = true;
			Main.dust[num2].velocity.X -= player.velocity.X * 0.5f;
			Main.dust[num2].velocity.Y -= player.velocity.Y * 0.5f;
			int t = player.HasItemInArmorReturnIndex(Type);
			if (t > 10) t -= 10;
			if (t > 0)
			{
				Main.dust[num2].shader = GameShaders.Armor.GetShaderFromItemId(player.dye[t].type);
			}
		}
	}
}
