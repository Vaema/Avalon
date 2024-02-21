using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

public class PriminiVice : ModProjectile
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
        Projectile.width = 30;
        Projectile.height = 30;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.timeLeft *= 5;
        Projectile.minion = true;
        Projectile.minionSlots = 0.25f;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        Main.projPet[Projectile.type] = true;
        DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
        DrawOriginOffsetY = -(int)((dims.Height / Main.projFrames[Projectile.type] / 2) - (Projectile.Size.Y / 2));
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

        if (Projectile.position.Y > Main.player[Projectile.owner].Center.Y + Main.rand.Next(-10, 0) + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
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
        else if (Projectile.position.Y < Main.player[Projectile.owner].Center.Y + Main.rand.Next(-10, 0) + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
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
        var num959 = Projectile.FindClosestNPC(480, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
        if (num959 == -1)
        {
            Projectile.rotation = -2.3561945f;
            return;
        }
        Projectile.rotation = Vector2.Normalize(Main.npc[num959].Center - Projectile.Center).ToRotation() + MathHelper.Pi;
        if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num959].position, Main.npc[num959].width, Main.npc[num959].height))
        {
            if (!Main.npc[num959].active)
            {
                Projectile.ai[1] = 0f;
                return;
            }
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] >= 50f)
            {
                Projectile.velocity = Vector2.Normalize(Main.npc[num959].Center - Projectile.Center) * 9f;
                return;
            }
            if (Projectile.ai[1] >= 100f)
            {
                Projectile.velocity = Vector2.Normalize(new Vector2(Main.npc[num959].Center.X - 50f, Main.npc[num959].Center.Y)) * 2.5f;
                return;
            }
            if (Projectile.ai[1] >= 150f)
            {
                Projectile.velocity = Vector2.Normalize(new Vector2(Main.npc[num959].Center.X + 50f, Main.npc[num959].Center.Y)) * 2.5f;
                return;
            }
        }
    }
}
