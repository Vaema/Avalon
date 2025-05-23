using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class ZincShortsword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToShortsword(ModContent.ProjectileType<Projectiles.Melee.ZincShortsword>(), 11, 4f, 12, 2.1f, scale: 0.95f, width: 50, height: 18);
		Item.value = Item.sellPrice(silver: 9);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 6)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
