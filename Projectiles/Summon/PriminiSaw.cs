using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

public class PriminiSaw : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        Main.projPet[Projectile.type] = true;
        Main.projFrames[Projectile.type] = 3;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = dims.Width * 30 / 18;
        Projectile.height = dims.Height * 30 / 18 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.timeLeft *= 5;
        Projectile.minion = true;
        Projectile.minionSlots = 0.25f;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        Main.projPet[Projectile.type] = true;
    }
    public override bool MinionContactDamage()
    {
        return true;
    }
    public override bool? CanHitNPC(NPC target)
    {
        if (target.type == NPCID.TargetDummy) return false;
        return base.CanHitNPC(target);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        readyToHome = false;
    }
    public float maxSpeed = 10f + Main.rand.NextFloat(10f);
    public float homeDistance = 300;
    public float homeStrength = 3f;
    public float homeDelay;
    public bool readyToHome = true;
    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];
        if (owner.dead)
        {
            owner.GetModPlayer<AvalonPlayer>().PrimeMinion = false;
        }
        if (owner.GetModPlayer<AvalonPlayer>().PrimeMinion)
        {
            Projectile.timeLeft = 2;
        }
        AvalonGlobalProjectile.ModifyProjectileStats(Projectile, ModContent.ProjectileType<PrimeArmsCounter>(), 50, 3, 1f, 0.1f);

        if (Projectile.position.Y > Main.player[Projectile.owner].Center.Y - Main.rand.Next(60, 80) - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
        {
            if (Projectile.velocity.Y > 0f)
            {
                Projectile.velocity.Y *= 0.96f;
            }
            Projectile.velocity.Y -= 0.3f;
            if (Projectile.velocity.Y > 6f)
            {
                Projectile.velocity.Y = 6f;
            }
        }
        else if (Projectile.position.Y < Main.player[Projectile.owner].Center.Y - Main.rand.Next(60, 80) - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
        {
            if (Projectile.velocity.Y < 0f)
            {
                Projectile.velocity.Y *= 0.96f;
            }
            Projectile.velocity.Y += 0.2f;
            if (Projectile.velocity.Y < -6f)
            {
                Projectile.velocity.Y = -6f;
            }
        }
        if (Projectile.Center.X > Main.player[Projectile.owner].Center.X + Main.rand.Next(45, 65) + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
        {
            if (Projectile.velocity.X > 0f)
            {
                Projectile.velocity.X *= 0.94f;
            }
            Projectile.velocity.X -= 0.3f;
            if (Projectile.velocity.X > 9f)
            {
                Projectile.velocity.X = 9f;
            }
        }
        if (Projectile.Center.X < Main.player[Projectile.owner].Center.X + Main.rand.Next(45, 65) + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
        {
            if (Projectile.velocity.X < 0f)
            {
                Projectile.velocity.X *= 0.94f;
            }
            Projectile.velocity.X += 0.2f;
            if (Projectile.velocity.X < -8f)
            {
                Projectile.velocity.X = -8f;
            }
        }

        if (!readyToHome)
        {
            homeDelay++;
            if (homeDelay >= 20)
            {
                readyToHome = true;
                homeDelay = 0;
            }
        }

        var closest = Projectile.FindClosestNPC(640, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
        if (closest == -1)
        {
            Projectile.rotation = 2.35619449f; // +
            return;
        }
        Vector2 startPosition = Projectile.Center;
        Projectile.rotation = Vector2.Normalize(Main.npc[closest].Center - Projectile.Center).ToRotation() + MathHelper.Pi;
        if (Collision.CanHit(Main.npc[closest], Projectile))
        {
            Vector2 target = Main.npc[closest].Center;
            float distance = Vector2.Distance(target, startPosition);
            Vector2 goTowards = Vector2.Normalize(target - startPosition) * ((homeDistance - distance) / (homeDistance / homeStrength));

            Projectile.velocity += goTowards;

            if (Projectile.velocity.Length() > maxSpeed)
            {
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * maxSpeed;
            }
        }

        //if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[closest].position, Main.npc[closest].width, Main.npc[closest].height))
        //{
        //    if (!Main.npc[closest].active)
        //    {
        //        Projectile.ai[1] = 0f;
        //        return;
        //    }
        //    Projectile.ai[1]++;
        //    if (Projectile.ai[1] >= 50f)
        //    {
        //        Projectile.velocity = Vector2.Normalize(Main.npc[closest].Center - Projectile.Center) * 9f;
        //        return;
        //    }
        //}
    }
}
