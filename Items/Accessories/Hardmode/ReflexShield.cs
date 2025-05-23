using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Shield)]
public class ReflexShield : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.defense = 6;
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 10);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<ReflexCharm>())
			.AddIngredient(ItemID.AnkhShield)
			.AddIngredient(ModContent.ItemType<MidasShoe>())
			.AddIngredient(ModContent.ItemType<SpaceHelmet>())
			.AddIngredient(ModContent.ItemType<Antivenom>())
			.AddIngredient(ModContent.ItemType<FaceShield>())
			.AddIngredient(ModContent.ItemType<NuclearExtinguisher>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.noKnockback = true;
		player.fireWalk = true;
		player.buffImmune[BuffID.Weak] = true;
		player.buffImmune[BuffID.BrokenArmor] = true;
		player.buffImmune[BuffID.Bleeding] = true;
		player.buffImmune[BuffID.Poisoned] = true;
		player.buffImmune[BuffID.Slow] = true;
		player.buffImmune[BuffID.Confused] = true;
		player.buffImmune[BuffID.Silenced] = true;
		player.buffImmune[BuffID.Cursed] = true;
		player.buffImmune[BuffID.Darkness] = true;
		player.buffImmune[BuffID.Chilled] = true;
		player.buffImmune[BuffID.Frozen] = true;
		player.buffImmune[BuffID.Suffocation] = true;
		player.buffImmune[BuffID.Ichor] = true;
		player.buffImmune[BuffID.OnFire] = true;
		player.buffImmune[BuffID.Blackout] = true;
		player.buffImmune[BuffID.CursedInferno] = true;
		player.buffImmune[BuffID.Stoned] = true;
		player.buffImmune[BuffID.WindPushed] = true;
		player.buffImmune[BuffID.Venom] = true;
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Pathogen>()] = true;
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Unloaded>()] = true;
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.BrokenWeaponry>()] = true;
		player.GetModPlayer<AvalonPlayer>().ReflexShield = true;
	}
}
