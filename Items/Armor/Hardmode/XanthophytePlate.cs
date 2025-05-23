using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
public class XanthophytePlate : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(19);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 4, 80);
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Generic) += 0.07f;
		player.GetCritChance(DamageClass.Generic) += 8;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 24)
			.AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
