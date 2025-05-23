using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class NickelShortsword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToShortsword(ModContent.ProjectileType<Projectiles.Melee.NickelShortsword>(), 9, 4f, 12, 2.1f, width: 50, height: 18);
		Item.value = Item.sellPrice(silver: 3, copper: 60);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 6)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
