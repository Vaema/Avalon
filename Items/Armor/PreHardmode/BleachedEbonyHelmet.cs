using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class BleachedEbonyHelmet : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(1);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<BleachedEbonyBreastplate>() && legs.type == ModContent.ItemType<BleachedEbonyGreaves>();
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Placeable.Tile.BleachedEbony>(), 20)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.OneDef");
		player.statDefense++;
	}
}
