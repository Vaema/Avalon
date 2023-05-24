using Avalon.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.Pets;
public class SnotOrb : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoTimeDisplay[Type] = true;
        Main.lightPet[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        bool unused = false;
        player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<Projectiles.SnotOrb>());
    }
}
