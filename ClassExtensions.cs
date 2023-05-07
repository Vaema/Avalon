using System;
using System.Collections.Generic;
using System.Linq;
using Avalon.Common;
using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon;

public static class ClassExtensions
{
    /// <summary>
    ///     Helper method for checking if the current item is an armor piece - used for armor prefixes.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Whether or not the item is an armor piece.</returns>
    public static bool IsArmor(this Item item) =>
        (item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1) && !item.vanity;

    public static void Active(this Tile t, bool a) => t.HasTile = a;

    public static Item HasItemInArmorFindIt(this Player p, int type)
    {
        for (int i = 0; i < p.armor.Length; i++)
        {
            if (p.armor[i].type == type) return p.armor[i];
        }
        return null;
    }

    /// <summary>
    ///     Checks if the current player has an item in their armor/accessory slots.
    /// </summary>
    /// <param name="p">The player.</param>
    /// <param name="type">The item ID to check.</param>
    /// <returns>Whether or not the item is found.</returns>
    public static bool HasItemInArmor(this Player p, int type) => p.armor.Any(t => type == t.type);
    public static bool InPillarZone(this Player p)
    {
        if (!p.ZoneTowerStardust && !p.ZoneTowerVortex && !p.ZoneTowerSolar)
        {
            return p.ZoneTowerNebula;
        }

        return true;
    }

    /// <summary>
    ///     Used to draw float coordinates to rounded coordinates to avoid blurry rendering of textures.
    /// </summary>
    /// <param name="vector">The vector to convert.</param>
    /// <returns>The rounded vector.</returns>
    public static Vector2 ToNearestPixel(this Vector2 vector) => new((int)(vector.X + 0.5f), (int)(vector.Y + 0.5f));

    public static Asset<T> VanillaLoad<T>(this Asset<T> asset) where T : class
    {
        try
        {
            if (asset.State == AssetState.NotLoaded)
            {
                Main.Assets.Request<Texture2D>(asset.Name, AssetRequestMode.ImmediateLoad);
            }
        }
        catch (AssetLoadException e)
        {
        }

        return asset;
    }
    public static int GetRhodiumVariantItemOre(this AvalonWorld.RhodiumVariant? rhodiumVariant)
    {
        return rhodiumVariant switch
        {
            AvalonWorld.RhodiumVariant.Osmium => ModContent.ItemType<OsmiumOre>(),
            AvalonWorld.RhodiumVariant.Rhodium => ModContent.ItemType<RhodiumOre>(),
            AvalonWorld.RhodiumVariant.Iridium => ModContent.ItemType<IridiumOre>(),
            _ => -1,
        };
    }

    /// <summary>
    ///     A helper method to check if the given Player is touching the ground.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>True if the player is touching the ground, false otherwise.</returns>
    public static bool IsOnGround(this Player player) =>
        (Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].HasTile &&
         Main.tileSolid[
             Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].TileType]) ||
        (Main.tile[(int)(player.position.X / 16f) + 1, (int)(player.position.Y / 16f) + 3].HasTile &&
         Main.tileSolid[
             Main.tile[(int)(player.position.X / 16f) + 1, (int)(player.position.Y / 16f) + 3].TileType] &&
         player.velocity.Y == 0f);
    public static void GetPointOnSwungItemPath(float spriteWidth, float spriteHeight, float normalizedPointOnPath, float itemScale, out Vector2 location, out Vector2 outwardDirection, Player player)
    {
        float num = (float)Math.Sqrt(spriteWidth * spriteWidth + spriteHeight * spriteHeight);
        float num2 = (float)(player.direction == 1).ToInt() * ((float)Math.PI / 2f);
        if (player.gravDir == -1f)
        {
            num2 += (float)Math.PI / 2f * (float)player.direction;
        }
        outwardDirection = player.itemRotation.ToRotationVector2().RotatedBy(3.926991f + num2);
        location = player.RotatedRelativePoint(player.itemLocation + outwardDirection * num * normalizedPointOnPath * itemScale);
    }
    public static int FindClosestNPC(this Entity entity, float maxDistance, Func<NPC, bool> invalidNPCPredicate)
    {
        int closest = -1;
        float lastDistance = maxDistance;
        for (int i = 0; i < Main.npc.Length; i++)
        {
            NPC npc = Main.npc[i];
            if (invalidNPCPredicate.Invoke(npc))
            {
                continue;
            }

            if (Vector2.Distance(entity.Center, npc.Center) < lastDistance)
            {
                lastDistance = Vector2.Distance(entity.Center, npc.Center);
                closest = i;
            }
        }

        return closest;
    }

    public static void DrawGas(String Texture, Color color, Projectile projectile, float spread, int iterations)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        int frameHeight = texture.Height / Main.projFrames[projectile.type];
        Rectangle frame = new Rectangle(0, frameHeight * projectile.frame, texture.Width, frameHeight);
        Vector2 drawPos = projectile.Center - Main.screenPosition;
        Main.EntitySpriteDraw(texture, drawPos, frame, color * projectile.Opacity, projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, projectile.scale, SpriteEffects.None, 0);

        for (int i = 0; i < iterations; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(0, projectile.width / spread * ((float)projectile.alpha) / 128).RotatedBy(i * (MathHelper.TwoPi) / iterations), frame, color * projectile.Opacity * 0.4f, projectile.rotation + ((float)projectile.alpha / 128) * (i / 128), new Vector2(texture.Width, frameHeight) / 2, projectile.scale, SpriteEffects.FlipVertically, 0);
        }
    }
    public static void Load<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TagCompound tag)
        where TKey : notnull
    {
        if (tag.ContainsKey("keys") && tag.ContainsKey("values"))
        {
            TKey[] keys = tag.Get<TKey[]>("keys");
            TValue[] values = tag.Get<TValue[]>("values");

            for (int i = 0; i < keys.Length; i++)
            {
                dictionary[keys[i]] = values[i];
            }
        }
    }

    public static TagCompound Save<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
       where TKey : notnull
    {
        TKey[] keys = dictionary.Keys.ToArray();
        TValue[] values = dictionary.Values.ToArray();
        var tag = new TagCompound();
        tag.Set("keys", keys);
        tag.Set("values", values);
        return tag;
    }

    public static Rectangle GetDims(this ModTexturedType texturedType) =>
        Main.netMode == NetmodeID.Server ? Rectangle.Empty : texturedType.GetTexture().Frame();

    public static Rectangle GetDims(this ModItem modItem) =>
        Main.netMode == NetmodeID.Server ? Rectangle.Empty : modItem.GetTexture().Frame();

    public static Rectangle GetDims(this ModProjectile modProjectile) =>
        Main.netMode == NetmodeID.Server ? Rectangle.Empty : modProjectile.GetTexture().Frame();

    public static Asset<Texture2D> GetTexture(this ModTexturedType texturedType) =>
        ModContent.Request<Texture2D>(texturedType.Texture);

    public static Asset<Texture2D> GetTexture(this ModItem modItem) =>
        ModContent.Request<Texture2D>(modItem.Texture);

    public static Asset<Texture2D> GetTexture(this ModProjectile modProjectile) =>
        ModContent.Request<Texture2D>(modProjectile.Texture);
}
