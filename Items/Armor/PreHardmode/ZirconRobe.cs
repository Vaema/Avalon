using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class ZirconRobe : ModItem
{
	public override void Load()
	{
		if (Main.netMode == NetmodeID.Server) return;
		EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this);
	}
	public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
	{
		robes = true;
		equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
	}
	public override void SetDefaults()
	{
		Item.DefaultToArmor(4);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 3, 25);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ItemID.Robe).AddIngredient(ModContent.ItemType<Material.Ores.Zircon>(), 10).AddTile(TileID.Loom).Register();
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<ZirconRobe>() && (head.type == ItemID.WizardHat || head.type == ItemID.MagicHat);
	}
	public override void UpdateArmorSet(Player player)
	{
		if (player.head == 14)
		{
			player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Robe1");
			player.GetCritChance(DamageClass.Magic) += 10;
		}
		else if (player.head == 159)
		{
			player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Robe2");
			player.statManaMax2 += 60;
		}
	}
	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += 80;
		player.manaCost -= 0.18f;
	}
}
