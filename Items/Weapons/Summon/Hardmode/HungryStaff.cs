using Avalon.Buffs.Minions;
using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode;

public class HungryStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
		ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

		ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
	}

	public override void SetDefaults()
	{
		Item.DefaultToMinionWeapon(ModContent.ProjectileType<Projectiles.Summon.HungrySummon>(), ModContent.BuffType<Hungry>(), 21, 1.5f, 30);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1);
		Item.UseSound = SoundID.Item44;
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
		position = Main.MouseWorld;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 14)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
		player.AddBuff(Item.buffType, 2);

		// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
		var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
		projectile.originalDamage = Item.damage;
		if (player.GetModPlayer<AvalonPlayer>().FleshArmor)
		{
			projectile.minionSlots = 0.5f;
		}

		// Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
		return false;
	}
}
