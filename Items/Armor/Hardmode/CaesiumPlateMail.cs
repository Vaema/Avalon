using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
public class CaesiumPlateMail : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(25);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 9);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<CaesiumBar>(), 40)
			.AddIngredient(ItemID.HellstoneBar, 12)
			.AddIngredient(ItemID.SoulofMight, 5)
			.AddTile(TileID.MythrilAnvil).Register();
	}
	public override void UpdateEquip(Player player)
	{
		player.GetCritChance(DamageClass.Melee) += 5;
		player.magmaStone = true;
	}
}
