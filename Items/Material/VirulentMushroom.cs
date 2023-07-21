using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;

namespace Avalon.Items.Material;

class VirulentMushroom : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Material;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 50;
        Item.height = dims.Height;
    }
}
