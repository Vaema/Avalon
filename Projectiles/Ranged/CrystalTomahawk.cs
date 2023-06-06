using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged
{
    public class CrystalTomahawk : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(36);
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.usesLocalNPCImmunity= true;
            Projectile.localNPCHitCooldown = 60;
        }
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation += Projectile.velocity.Length() * 0.02f * Projectile.direction;

            Color[] Colors = { Color.LightSkyBlue, Color.Magenta, Color.White, Color.Magenta };
            Color Color1 = ClassExtensions.CycleThroughColors(Colors, 60) * 0.5f;

            Lighting.AddLight(Projectile.Center, new Vector3(Color1.R / 255f,Color1.G / 255f, Color1.B / 255f));

            //int[] Dusts = { DustID.IceTorch, DustID.HallowedTorch, DustID.WhiteTorch };
            int[] Dusts = { DustID.BlueCrystalShard, DustID.PinkCrystalShard, DustID.PurpleCrystalShard };
            if (Main.rand.NextBool(3))
            {
                Dust dust2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(18, 18), Dusts[Main.rand.Next(3)], Projectile.velocity, 64, default, 1f);
                dust2.fadeIn = 0.6f + Main.rand.NextFloat() * 0.5f;
                dust2.noGravity = true;
                dust2.noLightEmittence= true;
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] > 40)
            {
                Projectile.velocity.X *= 0.99f;
                if (Projectile.velocity.Y < 24)
                {
                    Projectile.velocity.Y += 0.3f;
                }
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = Projectile.width - 16;
            height = Projectile.height - 16;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            int[] Dusts = { DustID.BlueCrystalShard, DustID.PinkCrystalShard, DustID.PurpleCrystalShard, DustID.IceTorch, DustID.HallowedTorch, DustID.WhiteTorch };
            for (int i = 0; i < 20; i++)
            {
                Dust dust2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(18, 18), Dusts[Main.rand.Next(6)], Main.rand.NextVector2Circular(12, 12), 64, default, 1f);
                dust2.fadeIn = Main.rand.NextFloat(0, 1);
                dust2.noGravity = true;
                //dust2.velocity += -Projectile.oldVelocity;
            }
            if(Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AeonExplosion>(), Projectile.damage / 2, 0, Projectile.owner);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int[] Dusts = { DustID.BlueCrystalShard, DustID.PinkCrystalShard, DustID.PurpleCrystalShard, DustID.IceTorch, DustID.HallowedTorch, DustID.WhiteTorch };
            for (int i = 0; i < 10; i++)
            {
                Dust dust2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(18, 18), Dusts[Main.rand.Next(6)], Main.rand.NextVector2Circular(9, 9), 64, default, 1f);
                dust2.fadeIn = Main.rand.NextFloat(0, 1);
                dust2.noGravity = true;
                dust2.velocity += -Projectile.oldVelocity * 0.5f;
            }
            //SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            //if (Projectile.velocity.X != oldVelocity.X)
            //{
            //    Projectile.velocity.X = -oldVelocity.X * 0.7f;
            //}
            //if (Projectile.velocity.Y != oldVelocity.Y)
            //{
            //    Projectile.velocity.Y = -oldVelocity.Y * 0.7f;
            //}

            //Projectile.penetrate--;
            return true;
        }
    }
}
