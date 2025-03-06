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
    public class SharedSceneData
    {
        public static bool IsInMainMenu { get; set; }
        public static bool IsInitialized { get; set; }
        public static bool IsInLobby { get; set; }
        public static bool IsInGame { get; set; }
        public static bool IsInShop { get; set; }
        public static bool IsInArena { get; set; }
        public static bool IsInTruckLobby { get; set; }
        public static List<Level> Levels = new List<Level>();
        public static List<Level> Menus = new List<Level>();
        public static GameObject Map { get; set; }
    }

    public class SharedSystemData
    {
        public static StatsManager StatsManager { get; set; }
        public static RunManager RunManager { get; set; }
        public static GraphicsManager GraphicsManager { get; set; }
        public static GameplayManager GameplayManager { get; set; }
        public static AssetManager AssetManager { get; set; }
        public static AudioManager AudioManager { get; set; }
        public static NetworkManager NetworkManager { get; set; }
        public static GameObject LevelGenerator { get; set; }
    }

    public class SharedPlayerData
    {
        public static PlayerController PlayerController { get; set; }
        public static PlayerCollision PlayerCollision { get; set; }
        public static ulong SteamId { get; set; }
    }

    public class Library : MelonMod
    { 
        // Set controllers for the player
        public async void SetPlayerData()
        {
            await Task.Delay(1000);
            GameObject player = GameObject.Find("Player").transform.Find("Controller").gameObject;
            GameObject collision = player.transform.Find("Collision").gameObject;

            PlayerController playerController = player.GetComponent<PlayerController>();
            PlayerCollision playerCollision = collision.GetComponent<PlayerCollision>();

            SetPlayerController(playerController);
            SetPlayerCollision(playerCollision);
        }

        // Set scene data for the game
        public async void SetSceneData()
        {
            await Task.Delay(1000);
            StatsManager statsManager = GameObject.Find("Stats Manager").GetComponent<StatsManager>();
            SetStatsManager(statsManager);

            GraphicsManager graphicsManager = GameObject.Find("Graphics Manager").GetComponent<GraphicsManager>();
            SetGraphicsManager(graphicsManager);

            GameplayManager gameplayManager = GameObject.Find("Gameplay Manager").GetComponent<GameplayManager>();
            SetGameplayManager(gameplayManager);

            AssetManager assetManager = GameObject.Find("Asset Manager").GetComponent<AssetManager>();
            SetAssetManager(assetManager);

            AudioManager audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
            SetAudioManager(audioManager);

            NetworkManager networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
            SetNetworkManager(networkManager);

            GameObject map = GameObject.Find("Map").gameObject;
            SetMap(map);

            GameObject levelGenerator = GameObject.Find("Level Generator").gameObject;
            SetLevelGenerator(levelGenerator);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            // Set the run manager for the game that determines the current level
            RunManager runManager = GameObject.Find("Run Manager").GetComponent<RunManager>();
            SetRunManager(runManager);

            // Set and store levels for the game
            if (!IsInitialized())
            {
                SetSteamId(SteamClient.SteamId);
                SetInititialized(true);
                SetLevels(runManager.levels);
                SetMenuLevels(new List<Level> { runManager.levelMainMenu, runManager.levelLobbyMenu });
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
            if (runManager.levelCurrent == runManager.levelLobbyMenu || runManager.levelCurrent)
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

            if (runManager.levelCurrent == runManager.levelLobby)
            {
                SetInTruckLobby(true);
            }
            else
            {
                SetInTruckLobby(false);
            }

            // Checks if the player is in game
            if (!SharedSceneData.Menus.Contains(runManager.levelCurrent))
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
            SharedSceneData.IsInitialized = value;
        }

        public void SetInMainMenu(bool value)
        {
            SharedSceneData.IsInMainMenu = value;
        }

        public void SetInLobby(bool value)
        {
            SharedSceneData.IsInLobby = value;
        }

        public void SetInShop(bool value)
        {
            SharedSceneData.IsInShop = value;
        }

        public void SetInArena(bool value)
        {
            SharedSceneData.IsInArena = value;
        }

        public void SetInTruckLobby(bool value)
        {
            SharedSceneData.IsInTruckLobby = value;
        }

        public void SetLevels(List<Level> levels)
        {
            SharedSceneData.Levels = levels;
        }

        public void SetMenuLevels(List<Level> levels)
        {
            SharedSceneData.Menus = levels;
        }

        public void SetInGame(bool value)
        {
            SharedSceneData.IsInGame = value;
        }

        public void SetStatsManager(StatsManager statsManager)
        {
            SharedSystemData.StatsManager = statsManager;
        }

        public void SetRunManager(RunManager runManager)
        {
            SharedSystemData.RunManager = runManager;
        }
        public void SetGraphicsManager(GraphicsManager graphicsManager)
        {
            SharedSystemData.GraphicsManager = graphicsManager;
        }
        public void SetGameplayManager(GameplayManager gameplayManager)
        {
            SharedSystemData.GameplayManager = gameplayManager;
        }

        public void SetAssetManager(AssetManager assetManager)
        {
            SharedSystemData.AssetManager = assetManager;
        }

        public void SetAudioManager(AudioManager audioManager)
        {
            SharedSystemData.AudioManager = audioManager;
        }

        public void SetNetworkManager(NetworkManager networkManager)
        {
            SharedSystemData.NetworkManager = networkManager;
        }

        public void SetMap(GameObject map)
        {
            SharedSceneData.Map = map;
        }
        public void SetLevelGenerator(GameObject levelGenerator)
        {
            SharedSystemData.LevelGenerator = levelGenerator;
        }

        // GET METHODS
        public ulong GetSteamId()
        {
            return SharedPlayerData.SteamId;
        }

        public bool IsInitialized()
        {
            return SharedSceneData.IsInitialized;
        }

        public bool IsInMainMenu()
        {
            return SharedSceneData.IsInMainMenu;
        }

        public bool IsInLobby()
        {
            return SharedSceneData.IsInLobby;
        }

        public List<Level> GetLevels()
        {
            return SharedSceneData.Levels;
        }

        public bool IsInGame() 
        { 
            return SharedSceneData.IsInGame; 
        }

        public bool IsInShop()
        {
            return SharedSceneData.IsInShop;
        }
        public bool IsInArena()
        {
            return SharedSceneData.IsInArena;
        }

        public bool IsInTruckLobby()
        {
            return SharedSceneData.IsInTruckLobby;
        }

        public List<Level> GetMenuLevels()
        {
            return SharedSceneData.Menus;
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

        public GraphicsManager GetGraphicsManager()
        {
            return SharedSystemData.GraphicsManager;
        }

        public GameplayManager GetGameplayManager()
        {
            return SharedSystemData.GameplayManager;
        }

        public AssetManager GetAssetManager()
        {
            return SharedSystemData.AssetManager;
        }

        public AudioManager GetAudioManager()
        {
            return SharedSystemData.AudioManager;
        }

        public NetworkManager GetNetworkManager()
        {
            return SharedSystemData.NetworkManager;
        }

        public GameObject GetMap()
        {
            return SharedSceneData.Map;
        }

        public Map GetMapContoller ()
        {
            return SharedSceneData.Map.transform.Find("Map Controller").GetComponent<Map>();
        }
        public GameObject GetLevelGenerator()
        {
            return SharedSystemData.LevelGenerator;
        }

        public LevelGenerator GetLevelGeneratorController()
        {
            return SharedSystemData.LevelGenerator.GetComponent<LevelGenerator>();
        }

        public int GetEnemyCount()
        {
            GameObject levelGenerator = GetLevelGenerator();
            GameObject enemies = levelGenerator.transform.Find("Enemies").gameObject;
            return enemies.transform.childCount;
        }

        // Freeze enemies in the game
        public void FreezeEnemies(bool freeze)
        {
            GameObject levelGenerator = GetLevelGenerator();
            GameObject enemies = levelGenerator.transform.Find("Enemies").gameObject;
            foreach (Transform enemy in enemies.transform)
            {
                GameObject _enemy = enemy.transform.gameObject;
                GameObject controller = _enemy.transform.Find("Enable")?.gameObject.transform.Find("Controller")?.gameObject;
                controller.SetActive(freeze);
            }
        }

        // Disable enemies in the game
        // Some enemies can be reactivated by the game
        public void DisableEnemies(bool disable)
        {
            GameObject levelGenerator = GetLevelGenerator();
            GameObject enemies = levelGenerator.transform.Find("Enemies").gameObject;
            foreach (Transform enemy in enemies.transform)
            {
                GameObject _enemy = enemy.transform.gameObject;
                GameObject enable = _enemy.transform.Find("Enable")?.gameObject;
                enable.SetActive(!disable);
            }
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
        public void SetSpeedUpgradeAmounts(PlayerController playerController, int amount)
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