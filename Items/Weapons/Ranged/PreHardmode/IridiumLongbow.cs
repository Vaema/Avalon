using Avalon.Common.Extensions;
using Avalon.Projectiles.Ranged.Held;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class IridiumLongbow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToLongbow(67, 2.3f, 24f, 83);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<IridiumLongbowHeld>(), damage, knockback, player.whoAmI, type);
		return false;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 13)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 2)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
