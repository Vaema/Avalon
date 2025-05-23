using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class ViruthornHelmet : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(6);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 54);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 15)
			.AddIngredient(ModContent.ItemType<Material.Booger>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<ViruthornScalemail>() && legs.type == ModContent.ItemType<ViruthornGreaves>();
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Viruthorn");
		player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
	}
	public override void UpdateVanitySet(Player player)
	{
		if (Main.rand.NextBool(7))
		{
			Dust d = Dust.NewDustDirect(player.position, player.width, player.height, ModContent.DustType<ContagionWeapons>());
			d.noGravity = true;
			d.scale *= 0.1f;
			d.alpha = 128;
			d.velocity *= 0.1f;
			d.fadeIn = 1;
			d.velocity += player.velocity + new Vector2(0, 0.4f);
			d.shader = GameShaders.Armor.GetShaderFromItemId(player.dye[Main.rand.Next(3)].type);
		}
	}
	public override void UpdateEquip(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.07f);
	}
}
