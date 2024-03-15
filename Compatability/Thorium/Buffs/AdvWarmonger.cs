using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Utilities;

namespace Avalon.Compatability.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvWarmonger : ModBuff
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetThoriumPlayer().buffWarmonger = true;
    }
}
