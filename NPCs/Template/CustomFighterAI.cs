using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Template;

public abstract class CustomFighterAI : ModNPC
{
	public virtual float MaxMoveSpeed { get; set; } = 2.5f;
	public virtual float MaxAirSpeed{ get; set; } = 3.5f;
	public virtual float Acceleration { get; set; } = 0.1f;
	public virtual float AirAcceleration { get; set; } = 0.1f;
	public virtual float MaxJumpHeight{ get; set; } = 8f;
	public virtual float JumpRadius { get; set; } = 150;
	public virtual bool JumpOverDrop { get; set; } = true;
	public virtual int NumberOfJumpsAgainstWall { get; set; } = 4;
	public virtual int TimeBeforeTurningAround { get; set; } = 180;

	public float PreviousDirection;

	public float NumJumps
	{
		get => NPC.ai[0];
		set => NPC.ai[0] = value;
	}
	/// <summary>
	/// RunningMode 0 is Targeting player
	/// RunningMode 1 is Running away
	/// </summary>
	public float RunningMode
	{
		get => NPC.ai[1];
		set => NPC.ai[1] = value;
	}
	public float RunningModeTimer
	{
		get => NPC.ai[2];
		set => NPC.ai[2] = value;
	}

	public ref float SavePrevDir => ref NPC.ai[3];

	public override bool? CanFallThroughPlatforms()
	{
		Player player = Main.player[NPC.FindClosestPlayer()];
		float upOrDown = NPC.Center.Y - player.Center.Y;

		//if the player is under the npc then fall through the platform, should maybe check for canhitline but vanilla doesn't do that so idk
		return NPC.collideY && upOrDown < -15;
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(MaxMoveSpeed);
		writer.Write(MaxAirSpeed);
		writer.Write(MaxJumpHeight);
		writer.Write(JumpRadius);
		writer.Write(Acceleration);
		writer.Write(AirAcceleration);
		writer.Write(PreviousDirection);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		MaxMoveSpeed = reader.ReadSingle();
		MaxAirSpeed = reader.ReadSingle();
		MaxJumpHeight = reader.ReadSingle();
		JumpRadius = reader.ReadSingle();
		Acceleration = reader.ReadSingle();
		AirAcceleration = reader.ReadSingle();
		PreviousDirection = reader.ReadSingle();
	}
	public virtual void CustomBehavior()
	{
	}
	//private bool isHit;
	private bool isInJump;
	private float jumpdelay = 3;
	public override void AI()
	{
		Player player = Main.player[NPC.FindClosestPlayer()];
		float distanceBetweenPlayer = Vector2.Distance(player.Center, NPC.Center);
		float dir;

		// if confused and the buff has more than 1 tick left on it, set RunningMode to 1 (running away)
		// if confused and buff has exactly 1 tick left, set RunningMode to 0 (targeting the closest player)
		if (NPC.confused)
		{
			int bindex = NPC.FindBuffIndex(BuffID.Confused);
			if (bindex > -1)
			{
				if (NPC.buffTime[bindex] > 1)
				{
					RunningMode = 1;
					NumJumps = 0;
				}
				else if (NPC.buffTime[bindex] == 1)
				{
					RunningMode = 0;
					NumJumps = 0;
				}
			}
		}
		// set running mode to 0 if just hit
		if (!NPC.confused && NPC.justHit)
		{
			RunningMode = 0;
			NumJumps = 0;
		}
		// some annoying logic to allow the npc to save the direction it's going during running away mode
		// (so it doesn't always face away from the player if you happen to run to the other side of it while it's in this mode)
		if (RunningMode == 0)
		{
			dir = NPC.Center.X - player.Center.X;
		}
		else
		{
			if (SavePrevDir == 0)
			{
				dir = NPC.Center.X - player.Center.X;
				PreviousDirection = dir;
				SavePrevDir = 1;
			}
			else
			{
				dir = PreviousDirection;
			}
		}
		float upOrDown = NPC.Center.Y - player.Center.Y;

		dir = Math.Sign(dir);

		//movement stuff you don't need to worry about... unless you do then just figure it out lol
		float moveSpeedMulti;
		float airSpeedMulti;

		// set multipliers based on running mode
		if (RunningMode == 0)
		{
			moveSpeedMulti = NPC.velocity.X + (Acceleration * -dir);
			airSpeedMulti = NPC.velocity.X + (AirAcceleration * -dir);
		}
		else
		{
			moveSpeedMulti = -NPC.velocity.X + (Acceleration * -dir);
			airSpeedMulti = -NPC.velocity.X + (AirAcceleration * -dir);
		}
		moveSpeedMulti = Math.Clamp(moveSpeedMulti, -MaxMoveSpeed, MaxMoveSpeed);
		airSpeedMulti = Math.Clamp(airSpeedMulti, -MaxAirSpeed, MaxAirSpeed);

		// set X velocity based on running mode
		if (RunningMode == 0)
		{
			NPC.spriteDirection = -(int)dir;
			if (NPC.velocity.Y == 0)
			{
				NPC.velocity.X = moveSpeedMulti;
			}
			else
			{
				NPC.velocity.X = airSpeedMulti;
			}
		}
		else if (RunningMode == 1)
		{
			NPC.spriteDirection = (int)dir;
			if (NPC.velocity.Y == 0)
			{
				NPC.velocity.X = -moveSpeedMulti;
			}
			else
			{
				NPC.velocity.X = -airSpeedMulti;
			}
		}
		// increment running mode timer
		if (RunningMode == 1 && !NPC.confused)
		{
			RunningModeTimer++;
			if (RunningModeTimer > TimeBeforeTurningAround && NPC.collideY)
			{
				RunningMode = 0;
				RunningModeTimer = 0;
				SavePrevDir = 0;
				NumJumps = 0;
				return;
			}
		}
		//if the player is above the npc and in range (JumpRadius) and there is also a line between the player and the npc
		if (distanceBetweenPlayer < JumpRadius && NPC.collideY && upOrDown > 1 && Collision.CanHitLine(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
		{
			Jump(MaxJumpHeight);
			NumJumps = 0;
		}
		Point a = NPC.Bottom.ToTileCoordinates();
		float height = 0;
		
		// if its on the ground and touching a wall
		if ((NPC.collideY || Main.tileSolid[Main.tile[a.X, a.Y].TileType] && Main.tile[a.X, a.Y].HasTile && !Main.tileSolidTop[Main.tile[a.X, a.Y].TileType]) && NPC.collideX && NumJumps <= NumberOfJumpsAgainstWall)
		{
			//check for the height of the wall infront
			for (int i = 0; i < 10; i++)
			{
				int modifier = 1;
				if (RunningMode == 1) modifier = -1;
				if (Main.tile[a.X + 1 * -(int)dir * modifier, a.Y - i].HasTile && Main.tileSolid[Main.tile[a.X + 1 * -(int)dir * modifier, a.Y - i].TileType])
				{
					height = i + 1;
				}
			}
			//jumps with the right height
			if (NumJumps < NumberOfJumpsAgainstWall)
				Jump(height);
			else
			{
				// swap the running mode and set num jumps to 0
				if (RunningMode == 0) RunningMode = 1;
				else if (RunningMode == 1) RunningMode = 0;
				NumJumps = 0;
			}
		}

		//if its on the ground
		if (NPC.collideY || Main.tileSolid[Main.tile[a.X, a.Y].TileType] && Main.tile[a.X, a.Y].HasTile)
		{
			//enable stepup when on the ground
			Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
			isInJump = false;
			//isHit = false;
			if (jumpdelay != 0)
			{
				jumpdelay--;
			}
			//if the tile under and infront is air then jump
			if ((!Main.tileSolid[Main.tile[a.X + 1 * -(int)dir, a.Y].TileType] || !Main.tile[a.X + 1 * -(int)dir, a.Y].HasTile) && (!Main.tileSolid[Main.tile[a.X + 2 * -(int)dir, a.Y].TileType] || !Main.tile[a.X + 2 * -(int)dir, a.Y].HasTile) && upOrDown > -20 && JumpOverDrop)
			{
				Jump(MaxJumpHeight);
				NumJumps = 0;
			}
		}
		else
		{
			if (NPC.velocity.Y < 0)
			{
				isInJump = true;
			}
			//enable step up if at peak of the jump
			if (NPC.velocity.Y > 0)
			{
				Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
			}
		}
		CustomBehavior();
	}
	public void Jump(float height)
	{
		
		//do the jump, if the height is higher than the maxjump then just set it to maxjumpheight
		if(jumpdelay == 0)
		{
			height = Math.Clamp(height + 2.5f, 0f, MaxJumpHeight);
			NPC.velocity.Y = -height;
			jumpdelay = 3;
			NumJumps++;
		}
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (!NPC.confused)
			RunningMode = 0;
	}
	/*public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
	{
		isHit = true;
	}
	public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
	{
		isHit = true;
	}*/
}
