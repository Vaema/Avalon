using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Armor.Hardmode;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Shoes, EquipType.Wings)]
class InertiaBoots : ModItem
{
    public override void SetStaticDefaults()
    {
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(1000, 9f, 1.2f, true);
    }

    public override void SetDefaults()
    {
        Item.defense = 4;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.width = 30;
        Item.value = Item.sellPrice(0, 16, 45, 0);
        Item.accessory = true;
        Item.height = 30;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup("Avalon:Wings")
            .AddIngredient(ItemID.FrostsparkBoots)
            .AddIngredient(ItemID.BlackBelt)
            .AddIngredient(ItemID.LunarBar, 2)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<InertiaBootsSlower>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
	public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
	{
		acceleration = 0.2f;
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().NoSticky = true;
        player.accRunSpeed = 10.29f;
        // ADD BACK AFTER CAESIUM ARMOR ADDED
        if (!player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
        {
            player.accRunSpeed = 10.29f;
        }
        else
        {
            player.accRunSpeed = 5f;
        }
        player.rocketBoots = 3;
        player.noFallDmg = true;
        player.blackBelt = true;
        player.iceSkate = true;
        player.wingTime = 1000;
        player.empressBrooch = true;
        player.GetModPlayer<AvalonPlayer>().InertiaBoots = true;
		player.wingsLogic = 45;
		if (!player.mount.Active && player.controlDown && !player.controlJump && player.velocity.Y != 0f)
		{
			player.velocity.Y += 0.6f * player.gravDir;
			player.maxFallSpeed = 20f;
		}
		//if (player.controlUp && player.controlJump)
		//{
		//    player.velocity.Y = player.velocity.Y - 0.3f * player.gravDir;
		//    if (player.gravDir == 1f)
		//    {
		//        if (player.velocity.Y > 0f)
		//        {
		//            player.velocity.Y = player.velocity.Y - 1f;
		//        }
		//        else if (player.velocity.Y > -Player.jumpSpeed)
		//        {
		//            player.velocity.Y = player.velocity.Y - 0.2f;
		//        }
		//        if (player.velocity.Y < -Player.jumpSpeed * 3f)
		//        {
		//            player.velocity.Y = -Player.jumpSpeed * 3f;
		//        }
		//    }
		//    else
		//    {
		//        if (player.velocity.Y < 0f)
		//        {
		//            player.velocity.Y = player.velocity.Y + 1f;
		//        }
		//        else if (player.velocity.Y < Player.jumpSpeed)
		//        {
		//            player.velocity.Y = player.velocity.Y + 0.2f;
		//        }
		//        if (player.velocity.Y > Player.jumpSpeed * 3f)
		//        {
		//            player.velocity.Y = Player.jumpSpeed * 3f;
		//        }
		//    }
		//}

		// ADD BACK AFTER CAESIUM ADDED
		if (!player.vortexStealthActive && !player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
        {
            if (player.controlLeft)
            {
                if (player.velocity.X > (player.vortexStealthActive ? -1f : -5f))
                {
                    player.velocity.X -= player.vortexStealthActive ? 0.06f : 0.31f;
                }
                if (player.velocity.X < (player.vortexStealthActive ? -1f : -5f) && player.velocity.X > (player.vortexStealthActive ? -2f : -10f))
                {
                    player.velocity.X -= player.vortexStealthActive ? 0.04f : 0.29f;
                }
            }
            if (player.controlRight)
            {
                if (player.velocity.X < (player.vortexStealthActive ? 1f : 5f))
                {
                    player.velocity.X += player.vortexStealthActive ? 0.06f : 0.31f;
                }
                if (player.velocity.X > (player.vortexStealthActive ? 1f : 5f) && player.velocity.X < (player.vortexStealthActive ? 2f : 10f))
                {
                    player.velocity.X += player.vortexStealthActive ? 0.04f : 0.29f;
                }
            }
        }
    }
}
