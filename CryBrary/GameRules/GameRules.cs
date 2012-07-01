using System;
using System.Runtime.CompilerServices;

using CryEngine.Initialization;
using CryEngine.Extensions;
using CryEngine.Native;

namespace CryEngine
{
	/// <summary>
	/// This is the base GameRules interface. All game rules must implement this.
	/// </summary>
	/// <remarks>For most use cases, deriving from CryGameCode's BaseGameRules is a more efficient solution.</remarks>
	public abstract class GameRules : CryScriptInstance
	{
        #region Statics
		public static GameRules Current { get; internal set; }
		#endregion

        #region Events
        public delegate void OnSetTeamDelegate(EntityId actorId, EntityId teamId);

        public delegate void OnSpawnDelegate();

        public delegate void OnClientConnectDelegate(int channelId, bool isReset = false, string playerName = "");
        public delegate void OnClientDisconnectDelegate(int channelId);

        public delegate void OnClientEnteredGameDelegate(int channelId, EntityId playerId, bool reset, bool loadingSaveGame);

        public delegate void OnItemDroppedDelegate(EntityId itemId, EntityId actorId);
        public delegate void OnItemPickedUpDelegate(EntityId itemId, EntityId actorId);

        public delegate void OnAddTaggedEntityDelegate(EntityId shooterId, EntityId targetId);

        public delegate void OnChangeSpectatorModeDelegate(EntityId actorId, byte mode, EntityId targetId, bool resetAll);
        public delegate void RequestSpectatorTargetDelegate(EntityId playerId, int change);

        public delegate void OnChangeTeamDelegate(EntityId actorId, int teamId);

        public delegate void OnSpawnGroupInvalidDelegate(EntityId playerId, EntityId spawnGroupId);

        // client only
        public delegate void OnConnectDelegate();
        public delegate void OnDisconnectDelegate(DisconnectionCause cause, string description);

        public delegate void OnReviveDelegate(EntityId actorId, Vec3 pos, Vec3 rot, int teamId);
        public delegate void OnReviveInVehicleDelegate(EntityId actorId, EntityId vehicleId, int seatId, int teamId);
        public delegate void OnKillDelegate(EntityId actorId, EntityId shooterId, string weaponClassName, int damage, int material, int hitType);

        public delegate void OnVehicleDestroyedDelegate(EntityId vehicleId);
        public delegate void OnVehicleSubmergedDelegate(EntityId vehicleId, float ratio);
        #endregion

        public static event OnSetTeamDelegate SetTeam;
        public static event OnSpawnDelegate Spawn;

        public static event OnClientConnectDelegate ClientConnect;
        public static event OnClientDisconnectDelegate ClientDisconnect;

        public static event OnClientEnteredGameDelegate ClientEnteredGame;

        public static event OnItemDroppedDelegate ItemDropped;
        public static event OnItemPickedUpDelegate ItemPickedUp;

        public static event OnVehicleDestroyedDelegate VehicleDestroyedServer;
        public static event OnVehicleSubmergedDelegate VehicleSubmergedServer;

        public static event OnAddTaggedEntityDelegate AddTaggedEntity;

        public static event OnChangeSpectatorModeDelegate ChangeSpectatorMode;
        public static event RequestSpectatorTargetDelegate RequestSpectatorTarget;

        public static event OnChangeTeamDelegate ChangeTeam;

        public static event OnSpawnGroupInvalidDelegate SpawnGroupInvalid;

        // client only
        public static event OnConnectDelegate Connect;
        public static event OnDisconnectDelegate Disconnect;

        public static event OnReviveDelegate Revive;
        public static event OnReviveInVehicleDelegate ReviveInVehicle;
        public static event OnKillDelegate Kill;

        public static event OnVehicleDestroyedDelegate VehicleDestroyed;
        public static event OnVehicleSubmergedDelegate VehicleSubmerged;
	}

    public class MyClass
    {
        public MyClass()
        {
            GameRules.SetTeam += OnPlayerSetTeam;
        }

        // public delegate void OnSetTeamDelegate(EntityId actorId, EntityId teamId);
        public void OnPlayerSetTeam(EntityId actorId, EntityId teamId)
        {
        }
    }

	[AttributeUsage(AttributeTargets.Class)]
	public sealed class GameRulesAttribute : Attribute
	{
		/// <summary>
		/// Sets the game mode's name. Uses the class name if not set.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// If set to true, the game mode will be set as default.
		/// </summary>
		public bool Default { get; set; }
	}

	public enum DisconnectionCause
	{
		/// <summary>
		/// This cause must be first! - timeout occurred.
		/// </summary>
		Timeout = 0,
		/// <summary>
		/// Incompatible protocols.
		/// </summary>
		ProtocolError,
		/// <summary>
		/// Failed to resolve an address.
		/// </summary>
		ResolveFailed,
		/// <summary>
		/// Versions mismatch.
		/// </summary>
		VersionMismatch,
		/// <summary>
		/// Server is full.
		/// </summary>
		ServerFull,
		/// <summary>
		/// User initiated kick.
		/// </summary>
		Kicked,
		/// <summary>
		/// Teamkill ban/ admin ban.
		/// </summary>
		Banned,
		/// <summary>
		/// Context database mismatch.
		/// </summary>
		ContextCorruption,
		/// <summary>
		/// Password mismatch, cdkey bad, etc.
		/// </summary>
		AuthenticationFailed,
		/// <summary>
		/// Misc. game error.
		/// </summary>
		GameError,
		/// <summary>
		/// DX11 not found.
		/// </summary>
		NotDX11Capable,
		/// <summary>
		/// The nub has been destroyed.
		/// </summary>
		NubDestroyed,
		/// <summary>
		/// Icmp reported error.
		/// </summary>
		ICMPError,
		/// <summary>
		/// NAT negotiation error.
		/// </summary>
		NatNegError,
		/// <summary>
		/// Punk buster detected something bad.
		/// </summary>
		PunkDetected,
		/// <summary>
		/// Demo playback finished.
		/// </summary>
		DemoPlaybackFinished,
		/// <summary>
		/// Demo playback file not found.
		/// </summary>
		DemoPlaybackFileNotFound,
		/// <summary>
		/// User decided to stop playing.
		/// </summary>
		UserRequested,
		/// <summary>
		/// User should have controller connected.
		/// </summary>
		NoController,
		/// <summary>
		/// Unable to connect to server.
		/// </summary>
		CantConnect,
		/// <summary>
		/// Arbitration failed in a live arbitrated session.
		/// </summary>
		ArbitrationFailed,
		/// <summary>
		/// Failed to successfully join migrated game
		/// </summary>
		FailedToMigrateToNewHost,
		/// <summary>
		/// The session has just been deleted
		/// </summary>
		SessionDeleted,
		/// <summary>
		/// Unknown cause
		/// </summary>
		Unknown
	}
}