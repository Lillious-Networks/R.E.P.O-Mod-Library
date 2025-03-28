# R.E.P.O Mod Library

## Overview

The R.E.P.O Mod Library is a mod library for the game [R.E.P.O](https://store.steampowered.com/app/3241660/REPO/), designed to provide a comprehensive set of utility functions and shared data management for modding the game. Created by Lillious & .Zer0, this library offers extensive control over player mechanics, game state tracking, and system interactions.

## Compatibility
- Supports the latest version of [R.E.P.O](https://store.steampowered.com/app/3241660/REPO/)

- Compatible with [MelonLoader v0.7.0](https://github.com/LavaGang/MelonLoader/releases/tag/v0.7.0) modding framework

- Compatibility may vary with [BepInEx](https://github.com/BepInEx/BepInEx) modding framework using the [BepInEx.MelonLoader.Loader](https://github.com/BepInEx/BepInEx.MelonLoader.Loader) plugin

- Currently still in early development, so expect missing features and bugs

## Key Features

The R.E.P.O Mod Library provides a comprehensive set of utilities that enable:

### Scene Management
- **Multi-scene Architecture**: Support for different game environments including main menu, lobby, shop, arena, and truck lobby with state tracking
- **Level Management**: Tools for managing and accessing both gameplay and menu levels
- **Map Control**: Direct access to map objects and their controllers for environmental manipulation

### Player Systems
- **Movement Mechanics**: Configurable movement speeds for walking, sprinting, and crouching
- **Energy System**: Customizable energy mechanics with adjustable maximum capacity and sprint drain rates
- **Gravity Manipulation**: Special movement abilities including anti-gravity effects and custom gravity settings

### Enemy Control
- **Enemy Management**: Functions to count, freeze, or disable enemies across the level
- **Controller Access**: Ability to manipulate enemy controllers for gameplay adjustments

### System Architecture
- **Manager Framework**: Centralized access to critical game systems including:
  - Stats tracking
  - Run management
  - Graphics settings
  - Gameplay rules
  - Asset management
  - Audio control
  - Network functionality
- **Steam Integration**: Support for Steam player identification

### Game State Utilities
- **State Tracking**: Comprehensive tracking of game state across different scenes and modes
- **Level Generation**: Access to level generation systems for dynamic content creation

### Player Data
- **Player Information Persistence**: Mechanisms to maintain player data across different game states
- **Player Stats Customization**: Tools for modifying player capabilities and performance

## Installation
1. Install this nuget package to your project
2. import the library into your mod

## Usage Example

```csharp
using MelonLoader;
using Repo_ExampleMod;
using Repo_Library;

// MelonInfo attribute to define the mod information    
[assembly: MelonInfo(typeof(Example), "R.E.P.O Example Mod", "1.0.0", "Example")]

// MelonGame attribute to specify the game the mod is for
[assembly: MelonGame("semiwork", "REPO")]

// Namespace for the mod
namespace Repo_ExampleMod
{
    public class Example : MelonMod
    {
        private readonly Library Repo_Lib = new Library();
        // Override the unity update method
        public override void OnUpdate()
        {
            // Check if player is in game
            if (!Repo_Lib.IsInGame()) return;
            // Get the player controller
            PlayerController playerController = Repo_Lib.GetPlayerController();
            // Check if player is sprinting
            if (Repo_Lib.IsSprinting(playerController))
            {
                // Set the sprint energy drain to 0
                Repo_Lib.SetSprintEnergyDrain(playerController, 0f);
                // Set the player's energy to the max energy
                Repo_Lib.SetPlayerCurrentEnergy(playerController,
                Repo_Lib.GetPlayerMaxEnergy(playerController));
            }
        }
    }
}
```

## Documentation

The following provides information about the utility functions available in the game system. These functions are categorized based on their purpose and functionality.

### Action Events
| Function | Description |
|----------|-------------|
| `OnEnemyDeath` | Event triggered when an enemy dies |

```csharp
public override void OnInitializeMelon()
{
    Library.OnEnemyDeath += (enemy) =>
    {
        MelonLogger.Msg("Enemy Died: " + enemy.name);
    };
}
```

| Function | Description |
|----------|-------------|
| `OnItemDestroyed` | Event triggered when an item is destroyed |

```csharp
public override void OnInitializeMelon()
{
    Library.OnItemDestroyed += (item) =>
    {
        MelonLogger.Msg("Item Destroyed: " + item.name);
    };
}
```

| Function | Description |
|----------|-------------|
| `OnItemDamaged` | Event triggered when an item is damaged |

```csharp
public override void OnInitializeMelon()
{
    Library.OnItemDamaged += (item) =>
    {
        MelonLogger.Msg("Item Damaged: " + item.name);
    };
}
```

### Player Data Management

| Function | Description |
|----------|-------------|
| `SetSteamId(ulong steamId)` | Sets the player's Steam ID |
| `GetSteamId()` | Returns the player's Steam ID |
| `GetPlayerController()` | Returns the player's PlayerController instance |
| `GetPlayerCollision()` | Returns the player's collision component |
| `SetPlayerCrown(string steamId)` | Updates the player's crown based on the Steam ID |
| `GetPlayerBySteamId(string steamId)` | Returns the player GameObject based on the Steam ID |
| `GetPlayerByName(string name)` | Returns the player GameObject based on the player name |
| `GetPlayerSteamId(PlayerAvatar playerAvatar)` | Returns the Steam ID of the player based from the player avatar |
| `AreAllPlayersInTruck()` | Checks if all players are in the truck lobby |

### Scene State Management

| Function | Description |
|----------|-------------|
| `SetInititialized(bool value)` | Sets the initialization status of the scene |
| `IsInitialized()` | Checks if the game has been initialized |
| `SetInMainMenu(bool value)` | Sets whether the player is in the main menu |
| `IsInMainMenu()` | Checks if the player is in the main menu |
| `SetInLobby(bool value)` | Sets whether the player is in the lobby |
| `IsInLobby()` | Checks if the player is in the lobby |
| `SetInShop(bool value)` | Sets whether the player is in the shop |
| `IsInShop()` | Checks if the player is in the shop |
| `SetInArena(bool value)` | Sets whether the player is in the arena |
| `IsInArena()` | Checks if the player is in the arena |
| `SetInTruckLobby(bool value)` | Sets whether the player is in the truck lobby |
| `IsInTruckLobby()` | Checks if the player is in the truck lobby |
| `SetInGame(bool value)` | Sets whether the player is in an active game |
| `IsInGame()` | Checks if the player is in an active game |
| `GetWindowManager()` | Returns the window manager instance |
| `GetMenuController()` | Returns the menu controller instance |

### Level Management

| Function | Description |
|----------|-------------|
| `SetLevels(List<Level> levels)` | Sets the available game levels |
| `GetLevels()` | Returns the list of available game levels |
| `SetMenuLevels(List<Level> levels)` | Sets the available menu levels |
| `GetMenuLevels()` | Returns the list of available menu levels |
| `GetMapObject()` | Returns the current map GameObject |
| `GetMap()` | Returns the Map component controller from the current map |

### System Management

| Function | Description |
|----------|-------------|
| `GetStatsManager()` | Returns the stats manager instance |
| `GetRunManager()` | Returns the run manager instance |
| `GetGraphicsManager()` | Returns the graphics manager instance |
| `GetGameplayManager()` | Returns the gameplay manager instance |
| `GetAssetManager()` | Returns the asset manager instance |
| `GetAudioManager()` | Returns the audio manager instance |
| `GetNetworkManager()` | Returns the network manager instance |
| `SaveGame(string filename)` | Saves the current game state to a file |
| `LoadGame(string filename)` | Loads a saved game state from a file |
| `GetRecordingDirector()` | Returns the recording director instance |
| `GetReverbDirector()` | Returns the reverb director instance |
| `GetPunManager()` | Returns the PUN manager instance |
| `GetRoundDirector()` | Returns the round director instance |
| `GetShopManager()` | Returns the shop manager instance |
| `GetSpectateCamera()` | Returns the spectate camera instance |
| `GetGameDirector()` | Returns the GameDirector component from the current map |
| `GetPostProcessing()` | Returns the PostProcessing component for the current map |
| `GetLevelGenerator()` | Returns the level generator GameObject |
| `GetLightManager()` | Returns the light manager instance |
| `IsMultiplayer()` | Checks if the game is in multiplayer mode |
| `IsMasterClient()` | Checks if the player is the master client in multiplayer mode |
| `GetAllPlayers()` | Returns a list of all players in the game |
| `GetPlayerCount()` | Returns the current number of players in the game |

### Enemy Management

| Function | Description |
|----------|-------------|
| `GetEnemyCount()` | Returns the current number of enemies in the level |
| `GetEnemies()` | Returns a list of all enemies in the current level |
| `FreezeEnemies(bool freeze)` | Freezes or unfreezes all enemies in the current level by toggling their controller components |
| `DisableEnemies(bool disable)` | Disables or enables all enemies in the current level (note: some enemies might be reactivated by the game) |
| `DrawLineToEnemy()` | Draws a line from the player to all enemies in the level and displays distance |
| `ClearEnemyLines()` | Clears all enemy lines drawn by DrawLineToEnemy() |
| `SetEnemies(GameObject[] enemies)` | Sets the list of enemies in the current level |
| `GetEnemies()` | Returns the list of enemies in the current level |
| `SpawnEnemy(Enemy enemy)` | Spawns an enemy in the level |
| `SetDeadEnemies(GameObject[] enemies)` | Sets the list of dead enemies in the current level |
| `GetDeadEnemies()` | Returns the list of dead enemies in the current level |
| `MonitorEnemies()` | Monitors enemies death and respawn in the level MelonCoroutines.Start(MonitorEnemies(enemyList)) |

### Item Management

| Function | Description |
|----------|-------------|
| `GetItemsInMap()` | Returns a list of items in the map |
| `SetDisableItemDurability()` | Disables durability of all items in the map |
| `SetEnableItemDurability()` | Enables durability of all items in the map |
| `SpawnValuable(GameObject item, Vector3 position)` | Spawns an item at the specified position (AssetManager.instance.surplusValuableSmall) |
| `SetItems(GameObject[] items)` | Sets the list of items in the map |
| `GetItems()` | Returns the list of items in the map |
| `DrawLineToItem()` | Draws a line from the player to all items in the map and displays distance |
| `ClearItemLines()` | Clears all item lines drawn by DrawLineToItem() |
| `BuyAllItems()` | Buys all items in the shop |
| `AddItem(string item)` | Adds an item to the player's inventory |
| `GetValuableDirector()` | Returns the valuable director instance |
| `UpgradeItemBattery(GameObject item)` | Upgrades the battery of the specified item |
| `SpawnItem(string name, Vector3 position)` | Spawns an item at the specified position |

### Player Control

| Function | Description |
|----------|-------------|
| `RespawnPlayer(PlayerController playerController)` | Instantly moves the player to a default spawn point (0, 0, -21) |
| `TeleportPlayer(PlayerController playerController, Vector3 position)` | Teleports the player to a specified position |
| `DestroyObject()` | Destroys breakable objects close to the player |
| `SetPlayerAvatar(GameObject playerAvatar)` | Sets the player's avatar GameObject |
| `GetPlayerAvatar()` | Returns the player's avatar GameObject |
| `GetPlayerAvatarComponent()` | Returns the player's avatar component |
| `GetPlayerVoice()` | Returns the player's voice component |
| `GetPlayerVoiceChat()` | Returns the player's voice chat component |
| `KillPlayer(PlayerAvatar playerAvatar)` | Kills a player |
| `KillAllPlayers()` | Kills all players |
| `RevivePlayer(PlayerAvatar playerAvatar)` | Revives a player |
| `ReviveAllPlayers()` | Revives all players |
| `SendMessage(PlayerAvatar playerAvatar, string message)` | Sends a message as the player |
| `PlayerTumble(PlayerAvatar playerAvatar)` | Tumbles the player |
| `PlayerTumbleAll()` | Tumbles all players |

### Player Stats Management

| Function | Description |
|----------|-------------|
| `SetPlayerCurrentEnergy(PlayerController playerController, float energy)` | Sets the player's current energy level |
| `SetPlayerMaxEnergy(PlayerController playerController, float energy)` | Sets the player's maximum energy capacity |
| `GetPlayerMaxEnergy(PlayerController playerController)` | Gets the player's maximum energy capacity |
| `SetSprintEnergyDrain(PlayerController playerController, float energy)` | Sets the energy drain rate when sprinting |
| `SetCrouchSpeed(PlayerController playerController, float speed)` | Sets the player's movement speed while crouching |
| `GetMovementSpeed(PlayerController playerController)` | Gets the player's normal movement speed |
| `SetMovementSpeed(PlayerController playerController, float speed)` | Sets the player's normal movement speed |
| `SetSprintSpeed(PlayerController playerController, float speed)` | Sets the player's sprint movement speed |
| `GetSprintSpeed(PlayerController playerController)` | Gets the player's sprint movement speed |
| `IsSprinting(PlayerController playerController)` | Checks if the player is currently sprinting |
| `SetSprinting(PlayerController playerController, bool value)` | Forces the player's sprint state on or off |
| `SetSpeedUpgradeAmounts(PlayerController playerController, int amount)` | Sets the number of speed upgrades the player has |
| `GetCustomGravity(PlayerController playerController)` | Gets the custom gravity value for the player |
| `SetCustomGravity(PlayerController playerController, float gravity)` | Sets a custom gravity value for the player |
| `AntiGravity(PlayerController playerController, float time)` | Applies anti-gravity effect to the player for the specified duration |
| `HealPlayer(GameObject playerAvatar, int health)` | Heals the player by the specified amount |
| `HealPlayerMax(GameObject playerAvatar)` | Heals the player to full health |
| `DamagePlayer(GameObject playerAvatar, int damage)` | Damages the player by the specified amount |
| `GetPlayerMaxHealth()` | Gets the player's maximum health |
| `GetPlayerHealth()` | Gets the player's current health |
| `SetPlayerHealth(int health)` | Sets the player's current health |
| `ResetStats()` | Resets the player's stats to default values |
| `SetCurrency(int currency)` | Sets the player's currency amount |
| `SetGodMode(bool on)` | Sets the player's god mode on or off |
| `GetPlayerUpgrades(string steamId)` | Returns the player's upgrade data based on the Steam ID |
| `UpgradePlayerJump(string steamId)` | Upgrades the player's jump ability |
| `UpgradePlayerEnergy(string steamId)` | Upgrades the player's energy capacity |
| `UpgradePlayerGrabRange(string steamId)` | Upgrades the player's grab range |
| `UpgradePlayerGrabStrength(string steamId)` | Upgrades the player's grab strength |
| `UpgradePlayerGrabStrengthNonHost()` | Upgrades the player's grab strength for non-host players |
| `UpgradePlayerHealth(string steamId)` | Upgrades the player's health capacity |
| `UpgradePlayerSprintSpeed(string steamId)` | Upgrades the player's sprint speed |
| `UpgradePlayerThrowStrength(string steamId)` | Upgrades the player's throw strength |
| `UpgradePlayerTumbleLaunch(string steamId)` | Upgrades the player's tumble launch ability |
| `UpgradeMapPlayerCount(string steamId)` | Upgrades the player count for the map |


## Credits
Created by [Lillious](https://github.com/Lillious) & [.Zer0](https://github.com/Elyriand21)