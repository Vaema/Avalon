using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Utilities;

namespace Avalon.ModSupport.Thorium.Buffs;

public class AdvBloodRush : ModBuff
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
    public override void SetStaticDefaults()
    {
        Data.Sets.BuffSets.Elixir[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.pickSpeed -= 0.2f;
        player.moveSpeed += 0.2f;
    }
}
