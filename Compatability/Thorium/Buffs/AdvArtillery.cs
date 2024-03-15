using Terraria;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Buffs;

[ExtendsFromMod("ThoriumMod")]
public class AdvArtillery : ModBuff
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.maxTurrets += 2;
    }
}
