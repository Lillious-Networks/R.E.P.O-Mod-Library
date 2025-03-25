using MelonLoader;
using UnityEngine;
using Repo_Library;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;
using Photon.Pun;
using System.Linq;
using System.Reflection;
using System.Threading;

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
        public static GameObject[] Enemies { get; set; }
        public static GameObject[] Items { get; set; }
    }

    public class SharedPlayerData
    {
        public static ulong SteamId { get; set; }
    }

    public class Library : MelonMod
    {
        private CancellationTokenSource _chatCancellationTokenSource;
        // Set scene data for the game
        public async void SetSceneData()
        {
            LevelGenerator levelGenerator = LevelGenerator.Instance;
            while (LevelGenerator.Instance == null)
            {
                await Task.Delay(100);
                levelGenerator = LevelGenerator.Instance;
            }

            // Wait for the level to be generated before setting up the game data
            while (!LevelGenerator.Instance.Generated)
            {
                await Task.Delay(100);
            }

            GameObject enemies = GameObject.Find("Level Generator").transform.Find("Enemies")?.gameObject;
            if (enemies == null)
            {
                MelonLogger.Msg("Unable to locate Enemies");
            } else
            {
                List<GameObject> enemyList = new List<GameObject>();
                foreach (Transform child in enemies.transform)
                {
                    GameObject childGameObject = child.gameObject;
                    enemyList.Add(childGameObject);
                }
                SetEnemies(enemyList.ToArray());
            }

            // Check if the item has ValueableObject component
            GameObject[] items = GameObject.FindGameObjectsWithTag("Phys Grab Object");
            if (items == null || items.Length == 0)
            {
                MelonLogger.Msg("Unable to locate items");
            } else
            {
                // Filter out items that don't have ValueableObject component
                foreach (GameObject item in items)
                {
                    // Filter out items that don't have ValueableObject component
                    ValuableObject valuableObject = item.GetComponent<ValuableObject>();
                    if (valuableObject == null)
                    {
                        items = items.Where(val => val != item).ToArray();
                    }
                }

                SetItems(items);
            }

            // Everything has been initialized
            SetInGame(true);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            // Set the run manager for the game that determines the current level
            RunManager runManager = RunManager.instance;

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
                SetSceneData();
            }
            else
            {
                SetInGame(false);
            }
        }

        // SET METHODS
        public void SetSteamId(ulong steamId)
        {
            SharedPlayerData.SteamId = steamId;
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

        public void SetEnemies(GameObject[] enemies)
        {
            SharedSceneData.Enemies = enemies;
        }
        public void SetItems(GameObject[] items)
        {
            SharedSceneData.Items = items;
        }

        public async void SetDisableItemDurability()
        {
            await Task.Delay(5000);
            DisableItemsDurability(true);
        }

        public async void SetEnableItemDurability()
        {
            await Task.Delay(5000);
            DisableItemsDurability(false);
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
            return PlayerController.instance;
        }
        public GameObject GetPlayerControllerObject()
        {
            return GameObject.Find("Player").transform.Find("Controller").gameObject;
        }

        public PlayerCollision GetPlayerCollision()
        {
            return PlayerCollision.instance;
        }

        public StatsManager GetStatsManager()
        {
            return StatsManager.instance;
        }

        public RunManager GetRunManager()
        {
            return RunManager.instance;
        }

        public GraphicsManager GetGraphicsManager()
        {
            return GraphicsManager.instance;
        }

        public GameplayManager GetGameplayManager()
        {
            return GameplayManager.instance;
        }

        public AssetManager GetAssetManager()
        {
            return AssetManager.instance;
        }

        public AudioManager GetAudioManager()
        {
            return AudioManager.instance;
        }

        public NetworkManager GetNetworkManager()
        {
            return NetworkManager.instance;
        }

        public GameObject GetMapObject()
        {
            return SharedSceneData.Map;
        }

        public Map GetMap ()
        {
            return Map.Instance;

        }
        public LevelGenerator GetLevelGenerator()
        {
            return LevelGenerator.Instance;
        }
        
        public GameDirector GetGameDirector()
        {
            return GameDirector.instance;
        }

        public PostProcessing GetPostProcessing()
        {
            return PostProcessing.Instance;
        }

        public PlayerAvatar GetPlayerAvatar()
        {
            return PlayerAvatar.instance;
        }

        public int GetEnemyCount()
        {
            GameObject[] enemies = GetEnemies().ToArray();
            return enemies.Length;
        }

        public GameObject[] GetEnemies()
        {
            return SharedSceneData.Enemies;
        }

        public RecordingDirector GetRecordingDirector()
        {
            return RecordingDirector.instance;
        }
        public ReverbDirector GetReverbDirector()
        {
            return ReverbDirector.instance;
        }
        public PunManager GetPunManager()
        {
            return PunManager.instance;
        }

        public PlayerVoice GetPlayerVoice()
        {
            return PlayerVoice.Instance;
        }
        public PlayerVoiceChat GetPlayerVoiceChat()
        {
            return PlayerVoiceChat.instance;
        }
        public RoundDirector GetRoundDirector()
        {
            return RoundDirector.instance;
        }
        public ShopManager GetShopManager()
        {
            return ShopManager.instance;
        }
        public SpectateCamera GetSpectateCamera()
        {
            return SpectateCamera.instance;
        }
        public ValuableDirector GetValuableDirector()
        {
            return ValuableDirector.instance;
        }
        public WindowManager GetWindowManager()
        {
            return WindowManager.instance;
        }
        public MenuController GetMenuController()
        {
            return MenuController.instance;
        }
        public LightManager GetLightManager()
        {
            return LightManager.instance;
        }

        // Freeze enemies in the game
        public void FreezeEnemies(bool freeze)
        {
            GameObject[] enemies = GetEnemies();
            foreach (GameObject enemy in enemies)
            {
                GameObject controller = enemy.transform.Find("Enable")?.gameObject.transform.Find("Controller")?.gameObject;
                controller.SetActive(freeze);
            }
        }

        // Upgrade player energy
        public void UpgradePlayerEnergy()
        {
            PunManager.instance.UpgradePlayerEnergy(GetSteamId().ToString());
        }

        // Upgrade player jump
        public void UpgradePlayerJump()
        {
            PunManager.instance.UpgradePlayerExtraJump(GetSteamId().ToString());
        }

        // Upgrade player grab range
        public void UpgradePlayerGrabRange()
        {
            PunManager.instance.UpgradePlayerGrabRange(GetSteamId().ToString());
        }

        // Non-Host Equivalent to Upgrade Player Grab Strength
        public void UpgradePlayerGrabStrengthNonHost()
        {
            StatsManager.instance.playerUpgradeStrength[SemiFunc.PlayerGetSteamID(PlayerAvatar.instance)]++;
        }

        // Upgrade player grab strength (Host Only)
        public void UpgradePlayerGrabStrength()
        {
            PunManager.instance.UpgradePlayerGrabStrength(GetSteamId().ToString());
        }

        // Upgrade player health
        public void UpgradePlayerHealth()
        {
            PunManager.instance.UpgradePlayerHealth(GetSteamId().ToString());
        }

        // Respawn player at a specific position
        public void RespawnPlayer(PlayerController playerController)
        {
            Vector3 respawn = new Vector3(0f, 0f, -21f);
            if (playerController == null) return;
            playerController.gameObject.transform.position = respawn;
        }

        // Upgrade player sprint speed
        public void UpgradePlayerSprintSpeed()
        {
            PunManager.instance.UpgradePlayerSprintSpeed(GetSteamId().ToString());
        }

        // Upgrade player throw strength
        public void UpgradePlayerThrowStrength()
        {
            PunManager.instance.UpgradePlayerThrowStrength(GetSteamId().ToString());
        }

        // Upgrade player tumble launch
        public void UpgradePlayerTumbleLaunch()
        {
            PunManager.instance.UpgradePlayerTumbleLaunch(GetSteamId().ToString());
        }

        // Upgrade battery
        public void UpgradeItemBattery(GameObject item)
        {
            PunManager.instance.UpgradeItemBattery(item.name);
        }

        // Upgrade map player count
        public void UpgradeMapPlayerCount()
        {
            PunManager.instance.UpgradeMapPlayerCount(GetSteamId().ToString());
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

        // Get player movement speed
        public float GetMovementSpeed(PlayerController playerController)
        {
            if (playerController == null) return 0f;
            return playerController.MoveSpeed;
        }

        // Set movement speed to a specific value
        public void SetMovementSpeed(PlayerController playerController, float speed)
        {
            if (playerController == null) return;
            playerController.MoveSpeed = speed;
        }

        // Get player sprint speed
        public float GetSprintSpeed(PlayerController playerController)
        {
            if (playerController == null) return 0f;
            return playerController.SprintSpeed;
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

        // Get custom gravity value
        public float GetCustomGravity(PlayerController playerController)
        {
            if (playerController == null) return 0f;
            return playerController.CustomGravity;
        }

        // Set custom gravity to a specific value
        public void SetCustomGravity(PlayerController playerController, float gravity)
        {
            if (playerController == null) return;
            playerController.CustomGravity = gravity;
        }

        // Spawn an enemy in the game
        public void SpawnEnemy(Enemy enemy)
        {
            SemiFunc.EnemySpawn(enemy);
        }

        // Kill a player
        public void KillPlayer(PlayerAvatar playerAvatar)
        {
            playerAvatar.PlayerDeath(0);
        }

        // Kill all players
        public void KillAllPlayers()
        {
            PlayerAvatar[] players = GetAllPlayers().ToArray();
            foreach (PlayerAvatar player in players)
            {
                player.PlayerDeath(0);
            }
        }

        // Revive a player
        public void RevivePlayer(PlayerAvatar playerAvatar)
        {
            playerAvatar.Revive(false);
        }

        // Revive all players
        public void ReviveAllPlayers()
        {
            PlayerAvatar[] players = GetAllPlayers().ToArray();
            foreach (PlayerAvatar player in players)
            {
                player.Revive(false);
            }
        }

        // Send a message as a player
        public void SendMessage(PlayerAvatar playerAvatar, string message)
        {
            playerAvatar.ChatMessageSend(message, false);
        }

        // Check if the game is multiplayer
        public bool IsMultiplayer()
        {
            return SemiFunc.IsMultiplayer();
        }

        // Check if the player is the master client
        public bool IsMasterClient()
        {
            return PhotonNetwork.IsMasterClient;
        }

        // Player Tumble
        public void PlayerTumble(PlayerAvatar playerAvatar)
        {
            playerAvatar.tumble.TumbleSet(true, false);
        }

        // Player Tumble All
        public void PlayerTumbleAll()
        {
            PlayerAvatar[] players = GetAllPlayers().ToArray();
            foreach (PlayerAvatar player in players)
            {
                player.tumble.TumbleSet(true, false);
            }
        }

        // Get a list of all players
        public List<PlayerAvatar> GetAllPlayers()
        {
            return SemiFunc.PlayerGetAll();
        }

        // Get player count
        public int GetPlayerCount()
        {
            PlayerAvatar[] players = GetAllPlayers().ToArray();
            return players.Length;
        }

        // Get player by steam id
        public PlayerAvatar GetPlayerBySteamId(string steamId)
        {
            return SemiFunc.PlayerGetFromSteamID(steamId);
        }

        // Get player by name
        public PlayerAvatar GetPlayerByName(string name)
        {
            return SemiFunc.PlayerGetFromName(name);
        }

        // Get player steam id
        public string GetPlayerSteamId(PlayerAvatar player)
        {
            return SemiFunc.PlayerGetSteamID(player);
        }

        // Check if all players are in the truck
        public bool AreAllPlayersInTruck()
        {
            return SemiFunc.PlayersAllInTruck();
        }

        // Disable enemies in the game
        // Some enemies can be reactivated by the game
        public void DisableEnemies(bool disable)
        {
            GameObject[] enemies = GetEnemies().ToArray();
            foreach (GameObject enemy in enemies)
            {
                GameObject enable = enemy.transform.Find("Enable")?.gameObject;
                enable.SetActive(!disable);
            }
        }

        // Get all items in the map
        public GameObject[] GetItems ()
        {
            return SharedSceneData.Items;
        }

        // Disable items durability in the game
        public void DisableItemsDurability (bool disable)
        {
            GameObject[] items = GetItems().ToArray();
            foreach (GameObject item in items)
            {
                PhysGrabObjectImpactDetector detector = item?.GetComponent<PhysGrabObjectImpactDetector>();
                if (detector != null)
                {
                    detector.enabled = !disable;
                }
            }
        }

        // Get player avatar object
        public GameObject GetPlayerAvatarObject()
        {
            return GameObject.Find("PlayerAvatar(Clone)").transform.Find("Player Avatar Controller").gameObject;
        }

        // Set god mode for the player
        public void SetGodMode(bool on)
        {
            typeof(PlayerHealth).GetField("godMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(GetPlayerAvatar().playerHealth, on);
        }

        // Set currency
        public void SetCurrency(int currency)
        {
            StatsManager.instance.runStats["currency"] = currency;
        }

        // Get max health of the player
        public int GetPlayerMaxHealth()
        {
            return 100 + StatsManager.instance.GetPlayerMaxHealth(SemiFunc.PlayerGetSteamID(PlayerAvatar.instance));
        }

        // Get current health of the player
        public int GetPlayerHealth()
        {
            return StatsManager.instance.GetPlayerHealth(SemiFunc.PlayerGetSteamID(PlayerAvatar.instance));
        }

        // Heal player
        public void HealPlayer(GameObject playerAvatar, int health)
        {
            playerAvatar.GetComponent<PlayerHealth>().Heal(health, true);
        }

        // Heal player to max health
        public void HealPlayerMax(GameObject playerAvatar)
        {
            playerAvatar.GetComponent<PlayerHealth>().Heal(GetPlayerMaxHealth(), true);
        }

        // Set player health to a specific value
        public void SetPlayerHealth(int health)
        {
            StatsManager.instance.SetPlayerHealth(SemiFunc.PlayerGetSteamID(PlayerAvatar.instance), health, false);
        }

        // Reset stats
        public void ResetStats()
        {
            StatsManager.instance.ResetAllStats();
        }

        // Buy all items
        public void BuyAllItems()
        {
            StatsManager.instance.BuyAllItems();
        }

        // Damage player
        public void DamagePlayer(GameObject playerAvatar, int damage)
        {   
            playerAvatar.GetComponent<PlayerHealth>().Hurt(damage, false);
        }

        // Save game
        public void SaveGame(string filename)
        {
            StatsManager.instance.SaveGame(filename);
        }

        // Load game
        public void LoadGame(string filename)
        {
            StatsManager.instance.LoadGame(filename);
        }

        // Update which player has the crown
        public void SetPlayerCrown(string steamId)
        {
            StatsManager.instance.UpdateCrown(steamId);
        }

        // Get player upgrades
        public Dictionary<string, int> GetPlayerUpgrades(string steamId)
        {
            return StatsManager.instance.FetchPlayerUpgrades(steamId);
        }

        // Add an item
        public void AddItem(string item)
        {
            StatsManager.instance.ItemAdd(item);
        }

        // Spawn an item in the game
        public void SpawnItem(GameObject item)
        {
            GameObject player = GetPlayerControllerObject();
            Vector3 position = player.transform.position + player.transform.forward * 2 + player.transform.up * 2;
            if (!SemiFunc.IsMultiplayer())
            {
                Object.Instantiate(item, position, Quaternion.identity);
            } else
            {
                PhotonNetwork.InstantiateRoomObject("Valuables/" + item.name, position, Quaternion.identity, 0);
            }
        }

        // Utilize the hurt collider on debug objects to damage enemies
        public void DestroyObject()
        {
            GameObject player = GetPlayerControllerObject();
            Transform hurtTransform = player.transform.Find("Hurt Collider");
            GameObject hurtCollider = hurtTransform ? hurtTransform.gameObject : null;
            if (hurtCollider == null)
            {
                GameObject debug = GameObject.Find("Debug");
                GameObject _debug = debug?.transform.Find("Debug Axel")?.gameObject;
                _debug.SetActive(true);

                GameObject debugScript = _debug.transform.Find("Debug Script")?.gameObject;
                GameObject _object = debugScript.transform.Find("Hurt Collider")?.gameObject;
                hurtCollider = GameObject.Instantiate(_object, player.transform);
                hurtCollider.name = "Hurt Collider";
                hurtCollider.SetActive(false);
                debug.SetActive(false);
            }

            // Activate and start coroutine to disable it
            hurtCollider.SetActive(true);
            DisableAfterTime(hurtCollider);
        }

        public async void DisableAfterTime(GameObject hurtCollider)
        {
            await Task.Delay(200);
            hurtCollider.SetActive(false);
        }

        // Draw a red line from player to enemy
        // Needs to be called in update loop
        public void DrawLineToEnemy()
        {
            GameObject[] enemies = GetEnemies().ToArray();
            GameObject player = GetPlayerControllerObject();

            if (enemies == null || enemies.Length == 0) return;

            foreach (GameObject enemy in enemies)
            {
                if (enemy == null)
                {
                    continue;
                }

                LineRenderer line = enemy.GetComponent<LineRenderer>();
                if (line == null)
                {
                    line = enemy.AddComponent<LineRenderer>();
                    ConfigureLineRenderer(line, Color.red);
                }

                Transform controllerTransform = enemy.transform.Find("Enable")?.Find("Controller");
                if (controllerTransform == null)
                {
                    continue;
                }

                // Update positions dynamically
                line.SetPosition(0, player.transform.position + player.transform.up * 1);
                line.SetPosition(1, controllerTransform.position + controllerTransform.up * 1);

                // Display distance as text above the enemy
                float distance = Vector3.Distance(player.transform.position, controllerTransform.position);
                UpdateDistanceText(enemy, distance, controllerTransform.position, player.transform, Color.red);
            }
        }

        // Draw a yellow line from player to item
        // Needs to be called in update loop
        public void DrawLineToItem()
        {
            GameObject player = GetPlayerControllerObject();
            GameObject[] items = GetItems().ToArray();
            if (items == null || items.Length == 0) return;

            foreach (GameObject item in items)
            {
                if (item == null)
                {
                    continue;
                }
                LineRenderer line = item.GetComponent<LineRenderer>();
                if (line == null)
                {
                    line = item.AddComponent<LineRenderer>();
                    ConfigureLineRenderer(line, Color.yellow);
                }
                // Update positions dynamically
                line.SetPosition(0, player.transform.position + player.transform.up * 1);
                line.SetPosition(1, item.transform.position + item.transform.up * 1);
                // Display distance as text above the item
                float distance = Vector3.Distance(player.transform.position, item.transform.position);
                UpdateDistanceText(item, distance, item.transform.position, player.transform, Color.yellow);
            }
        }

        public void ClearEnemyLines()
        {
            GameObject[] enemies = GetEnemies().ToArray();
            if (enemies == null || enemies.Length == 0) return;
            foreach (GameObject enemy in enemies)
            {
                if (enemy == null)
                {
                    continue;
                }
                LineRenderer line = enemy.GetComponent<LineRenderer>();
                if (line != null)
                {
                    Object.Destroy(line);
                }
                TextMesh textMesh = enemy.GetComponentInChildren<TextMesh>();
                if (textMesh != null)
                {
                    Object.Destroy(textMesh.gameObject);
                }
            }
        }

        public void ClearItemLines()
        {
            GameObject[] items = GetItems().ToArray();
            if (items == null || items.Length == 0) return;
            foreach (GameObject item in items)
            {
                if (item == null)
                {
                    continue;
                }
                LineRenderer line = item.GetComponent<LineRenderer>();
                if (line != null)
                {
                    Object.Destroy(line);
                }
                TextMesh textMesh = item.GetComponentInChildren<TextMesh>();
                if (textMesh != null)
                {
                    Object.Destroy(textMesh.gameObject);
                }
            }
        }

        public void ConfigureLineRenderer(LineRenderer line, Color color)
        {
            line.startWidth = 0.01f;
            line.endWidth = 0.01f;
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = color;
            line.endColor = color;
            line.positionCount = 2;
        }

        public void UpdateDistanceText(GameObject enemy, float distance, Vector3 position, Transform playerTransform, Color color)
        {
            TextMesh textMesh = enemy.GetComponentInChildren<TextMesh>();
            if (textMesh == null)
            {
                GameObject textObject = new GameObject("DistanceText");
                textObject.transform.SetParent(enemy.transform);
                textObject.transform.localPosition = Vector3.up * 2; // Adjust height

                textMesh = textObject.AddComponent<TextMesh>();
                textMesh.fontSize = 14;
                textMesh.characterSize = 0.1f;
                textMesh.color = color;
                textMesh.alignment = TextAlignment.Center;
                textMesh.anchor = TextAnchor.MiddleCenter;
            }

            // Scale font size with distance (min 14, max 24)
            textMesh.fontSize = Mathf.Clamp(Mathf.RoundToInt(14 + (distance * 0.2f)), 14, 24);

            textMesh.text = distance.ToString("F1") + "m";
            textMesh.transform.position = position + Vector3.up * 2; // Keep text above enemy
            textMesh.transform.LookAt(playerTransform);
            textMesh.transform.Rotate(0, 180, 0); // Ensure text is readable
        }
    }
}