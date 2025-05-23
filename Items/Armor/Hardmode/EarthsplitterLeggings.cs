using Avalon.Common.Extensions;
using Avalon.Items.Armor.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Legs)]
public class EarthsplitterLeggings : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(8);
		Item.rare = ModContent.RarityType<Rarities.CrispyRarity>();
		Item.value = Item.sellPrice(0, 3);
	}
	public override void UpdateEquip(Player player)
	{
		player.frogLegJumpBoost = true;
		Player.jumpHeight = 30;
		player.pickSpeed -= 0.15f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 6)
			.AddIngredient(ItemID.ShadowGreaves)
			.AddIngredient(ItemID.MiningPants)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 6)
			.AddIngredient(ItemID.AncientShadowGreaves)
			.AddIngredient(ItemID.MiningPants)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 6)
			.AddIngredient(ItemID.CrimsonGreaves)
			.AddIngredient(ItemID.MiningPants)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 6)
			.AddIngredient(ModContent.ItemType<ViruthornGreaves>())
			.AddIngredient(ItemID.MiningPants)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
