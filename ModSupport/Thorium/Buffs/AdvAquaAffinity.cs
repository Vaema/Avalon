using Terraria;
using Terraria.ModLoader;

namespace Avalon.ModSupport.Thorium.Buffs;

public class AdvAquaAffinity : ModBuff
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
        player.ignoreWater = true;
        if (player.wet)
        {
            player.moveSpeed += 0.15f;
            player.runAcceleration += 0.12f;
        }
    }
}
