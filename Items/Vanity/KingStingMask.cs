using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
class KingStingMask : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 2);
        Item.height = dims.Height;
    }
}
