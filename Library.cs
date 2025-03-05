using MelonLoader;
using UnityEngine;
using Repo_Library;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;

[assembly: MelonInfo(typeof(Library), "R.E.P.O Mod Library", "1.0.0", "Lillious & .Zer0")]
[assembly: MelonGame("semiwork", "REPO")]

namespace Repo_Library
{
    public class SharedData
    {
        public static bool IsInMainMenu { get; set; }
        public static bool IsInitialized { get; set; }
        public static bool IsInLobby { get; set; }
        public static bool IsInGame { get; set; }
        public static bool IsInShop { get; set; }
        public static bool IsInArena { get; set; }
        public static List<Level> Levels = new List<Level>();
        public static List<Level> Menus = new List<Level>();
    }

    public class SharedSystemData
    {
        public static StatsManager StatsManager { get; set; }
        public static RunManager RunManager { get; set; }
    }

    public class SharedPlayerData
    {
        public static PlayerController PlayerController { get; set; }
        public static PlayerCollision PlayerCollision { get; set; }
        public static ulong SteamId { get; set; }
    }

    public class Library : MelonMod
    {
        public async void SetPlayerData()
        {
            await Task.Delay(1000);
            // Set controllers for the player
            GameObject player = GameObject.Find("Player").transform.Find("Controller").gameObject;
            GameObject collision = player.transform.Find("Collision").gameObject;
            if (player == null)
            {
                MelonLogger.Msg("Player not found");
                return;
            }
            else
            {
                MelonLogger.Msg("Player found");
            }

            PlayerController playerController = player.GetComponent<PlayerController>();
            PlayerCollision playerCollision = collision.GetComponent<PlayerCollision>();

            if (playerController == null)
            {
                MelonLogger.Msg("Player controller not found");
                return;
            }

            if (playerCollision == null)
            {
                MelonLogger.Msg("Player collision not found");
                return;
            }

            SetPlayerController(playerController);
            SetPlayerCollision(playerCollision);

            StatsManager statsManager = GameObject.Find("Stats Manager").GetComponent<StatsManager>();
            SetStatsManager(statsManager);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            RunManager runManager = GameObject.Find("Run Manager").GetComponent<RunManager>();
            SetRunManager(runManager);

            // Set and store levels for the game
            if (!IsInitialized())
            {
                SetSteamId(SteamClient.SteamId);
                SetInititialized(true);
                SetLevels(runManager.levels);
                SetMenuLevels(new List<Level> { runManager.levelMainMenu, runManager.levelLobbyMenu, runManager.levelLobby });
            }

            // Check if the player is in the main menu
            if (runManager.levelCurrent == runManager.levelMainMenu)
            {
                SetInMainMenu(true);
            }
            else
            {
                SetInMainMenu(false);
            }

            // Check if the player is in the lobby
            if (runManager.levelCurrent == runManager.levelLobbyMenu || runManager.levelCurrent == runManager.levelLobby)
            {
                SetInLobby(true);
            }
            else
            {
                SetInLobby(false);
            }

            // Check if the player is in the shop
            if (runManager.levelCurrent == runManager.levelShop)
            {
                SetInShop(true);
            }
            else
            {
                SetInShop(false);
            }

            // Check if the player is in the arena
            if (runManager.levelCurrent == runManager.levelArena)
            {
                SetInArena(true);
            }
            else
            {
                SetInArena(false);
            }

            // Checks if the player is in game
            if (!SharedData.Menus.Contains(runManager.levelCurrent))
            {
                // Check if we are already in a game
                if (IsInGame()) return;
                SetInGame(true);
                SetPlayerData();
            }
            else
            {
                SetInGame(false);
                SetPlayerController(null);
                SetPlayerCollision(null);
            }
        }
        
        // SET METHODS
        public void SetSteamId(ulong steamId)
        {
            SharedPlayerData.SteamId = steamId;
        }

        public void SetPlayerController(PlayerController playerController)
        {
            SharedPlayerData.PlayerController = playerController ?? null;
        }

        public void SetPlayerCollision(PlayerCollision playerCollision)
        {
            SharedPlayerData.PlayerCollision = playerCollision ?? null;
        }

        public void SetInititialized(bool value)
        {
            SharedData.IsInitialized = value;
        }

        public void SetInMainMenu(bool value)
        {
            SharedData.IsInMainMenu = value;
        }

        public void SetInLobby(bool value)
        {
            SharedData.IsInLobby = value;
        }

        public void SetInShop(bool value)
        {
            SharedData.IsInShop = value;
        }

        public void SetInArena(bool value)
        {
            SharedData.IsInArena = value;
        }

        public void SetLevels(List<Level> levels)
        {
            SharedData.Levels = levels;
        }

        public void SetMenuLevels(List<Level> levels)
        {
            SharedData.Menus = levels;
        }

        public void SetInGame(bool value)
        {
            SharedData.IsInGame = value;
        }

        public void SetStatsManager(StatsManager statsManager)
        {
            SharedSystemData.StatsManager = statsManager;
        }

        public void SetRunManager(RunManager runManager)
        {
            SharedSystemData.RunManager = runManager;
        }

        // GET METHODS
        public ulong GetSteamId()
        {
            return SharedPlayerData.SteamId;
        }

        public bool IsInitialized()
        {
            return SharedData.IsInitialized;
        }

        public bool IsInMainMenu()
        {
            return SharedData.IsInMainMenu;
        }

        public bool IsInLobby()
        {
            return SharedData.IsInLobby;
        }

        public List<Level> GetLevels()
        {
            return SharedData.Levels;
        }

        public bool IsInGame() 
        { 
            return SharedData.IsInGame; 
        }

        public bool IsInShop()
        {
            return SharedData.IsInShop;
        }
        public bool IsInArena()
        {
            return SharedData.IsInArena;
        }

        public List<Level> GetMenuLevels()
        {
            return SharedData.Menus;
        }

        public PlayerController GetPlayerController()
        {
            return SharedPlayerData.PlayerController;
        }

        public PlayerCollision GetPlayerCollision()
        {
            return SharedPlayerData.PlayerCollision;
        }

        public StatsManager GetStatsManager()
        {
            return SharedSystemData.StatsManager;
        }

        public RunManager GetRunManager()
        {
            return SharedSystemData.RunManager;
        }

        // Revive player at a spawn point
        public void RevivePlayer (PlayerController playerController)
        {
            Vector3 respawn = new Vector3(0f, 0f, -21f);
            if (playerController != null) return;
            playerController?.Revive(respawn);
        }

        // Respawn player at a specific position
        public void RespawnPlayer(PlayerController playerController)
        {
            Vector3 respawn = new Vector3(0f, 0f, -21f);
            if (playerController == null) return;
            playerController.gameObject.transform.position = respawn;
        }

        // Teleport player to a vector3 position
        public void TeleportPlayer(PlayerController playerController, Vector3 position)
        {
            if (playerController == null) return;
            playerController.gameObject.transform.position = position;
        }

        // Set player current energy to a specific value
        public void SetPlayerCurrentEnergy(PlayerController playerController, float energy)
        {
            if (playerController == null) return;
            playerController.EnergyCurrent = energy;
        }

        // Set player max energy to a specific value
        public void SetPlayerMaxEnergy(PlayerController playerController, float energy)
        {
            if (playerController == null) return;
            playerController.EnergyStart = energy;
        }

        // Set sprint energy drain to a specific value
        public void SetSprintEnergyDrain(PlayerController playerController, float energy)
        {
            if (playerController == null) return;
            playerController.EnergySprintDrain = energy;
        }

        // Get player max energy
        public float GetPlayerMaxEnergy(PlayerController playerController)
        {
            if (playerController == null) return 0f;
            return playerController.EnergyStart;
        }

        // Add anti-gravity to the player
        public void AntiGravity(PlayerController playerController, float time)
        {
            if (playerController == null) return;
            playerController.AntiGravity(time);
        }

        // Set crounch speed to a specific value
        public void SetCrouchSpeed(PlayerController playerController, float speed)
        {
            if (playerController == null) return;
            playerController.CrouchSpeed = speed;
        }

        // Set movement speed to a specific value
        public void SetMovementSpeed(PlayerController playerController, float speed)
        {
            if (playerController == null) return;
            playerController.MoveSpeed = speed;
        }

        // Set sprint speed to a specific value
        public void SetSprintSpeed(PlayerController playerController, float speed)
        {
            if (playerController == null) return;
            playerController.SprintSpeed = speed;
        }

        // Check if the player is sprinting
        public bool IsSprinting(PlayerController playerController)
        {
            if (playerController == null) return false;
            return playerController.sprinting;
        }

        // Set sprinting to a specific value
        public void SetSprinting(PlayerController playerController, bool value)
        {
            if (playerController == null) return;
            playerController.sprinting = value;
        }

        // Set speed upgrade amounts to a specific value
        public void SetUpgradeAmounts(PlayerController playerController, int amount)
        {
            if (playerController == null) return;
            playerController.SprintSpeedUpgrades = amount;
        }

        // Set custom gravity to a specific value
        public void SetCustomGravity(PlayerController playerController, float gravity)
        {
            if (playerController == null) return;
            playerController.CustomGravity = gravity;
        }
    }
}