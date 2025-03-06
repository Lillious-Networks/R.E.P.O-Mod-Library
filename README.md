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
- **Revival System**: Multiple options for player respawning and revival at designated points

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

### Player Data Management

| Function | Description |
|----------|-------------|
| `SetSteamId(ulong steamId)` | Sets the player's Steam ID |
| `GetSteamId()` | Returns the player's Steam ID |
| `SetPlayerController(PlayerController playerController)` | Associates a PlayerController with the player data |
| `GetPlayerController()` | Returns the player's PlayerController instance |
| `SetPlayerCollision(PlayerCollision playerCollision)` | Sets the player's collision component |
| `GetPlayerCollision()` | Returns the player's collision component |

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

### Level Management

| Function | Description |
|----------|-------------|
| `SetLevels(List<Level> levels)` | Sets the available game levels |
| `GetLevels()` | Returns the list of available game levels |
| `SetMenuLevels(List<Level> levels)` | Sets the available menu levels |
| `GetMenuLevels()` | Returns the list of available menu levels |
| `SetMap(GameObject map)` | Sets the current map GameObject |
| `GetMap()` | Returns the current map GameObject |
| `GetMapContoller()` | Returns the Map component controller from the current map |
| `SetLevelGenerator(GameObject levelGenerator)` | Sets the level generator GameObject |
| `GetLevelGenerator()` | Returns the level generator GameObject |
| `GetLevelGeneratorController()` | Returns the LevelGenerator component from the level generator |
| `GetGameDirector(GameObject gameDirector)` | Returns the GameDirector component from the specified GameObject |
| `SetGameDirector(GameObject gameDirector)` | Sets the GameDirector component from the specified GameObject |
| `GetGameDirectorController()` | Returns the GameDirector component from the current map |
| `SetPostProcessing(PostProcessing postProcessing)` | Sets the PostProcessing component for the current map |
| `GetPostProcessing()` | Returns the PostProcessing component for the current map |

### System Managers

| Function | Description |
|----------|-------------|
| `SetStatsManager(StatsManager statsManager)` | Sets the stats manager instance |
| `GetStatsManager()` | Returns the stats manager instance |
| `SetRunManager(RunManager runManager)` | Sets the run manager instance |
| `GetRunManager()` | Returns the run manager instance |
| `SetGraphicsManager(GraphicsManager graphicsManager)` | Sets the graphics manager instance |
| `GetGraphicsManager()` | Returns the graphics manager instance |
| `SetGameplayManager(GameplayManager gameplayManager)` | Sets the gameplay manager instance |
| `GetGameplayManager()` | Returns the gameplay manager instance |
| `SetAssetManager(AssetManager assetManager)` | Sets the asset manager instance |
| `GetAssetManager()` | Returns the asset manager instance |
| `SetAudioManager(AudioManager audioManager)` | Sets the audio manager instance |
| `GetAudioManager()` | Returns the audio manager instance |
| `SetNetworkManager(NetworkManager networkManager)` | Sets the network manager instance |
| `GetNetworkManager()` | Returns the network manager instance |

### Enemy Management

| Function | Description |
|----------|-------------|
| `GetEnemyCount()` | Returns the current number of enemies in the level |
| `FreezeEnemies(bool freeze)` | Freezes or unfreezes all enemies in the current level by toggling their controller components |
| `DisableEnemies(bool disable)` | Disables or enables all enemies in the current level (note: some enemies might be reactivated by the game) |

### Item Management

| Function | Description |
|----------|-------------|
| `GetItemsInMap()` | Returns a list of items in the map |
| `DisableItemsDurability(bool disable)` | Disables or enables the durability of all items in the map |

### Player Control

| Function | Description |
|----------|-------------|
| `RevivePlayer(PlayerController playerController)` | Revives the player at a default spawn point (0, 0, -21) |
| `RespawnPlayer(PlayerController playerController)` | Instantly moves the player to a default spawn point (0, 0, -21) |
| `TeleportPlayer(PlayerController playerController, Vector3 position)` | Teleports the player to a specified position |
| `AntiGravity(PlayerController playerController, float time)` | Applies anti-gravity effect to the player for the specified duration |
| `HealPlayer(GameObject playerAvatar, int health)` | Heals the player by the specified amount |

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


## Credits
Created by [Lillious](https://github.com/Lillious) & [.Zer0](https://github.com/Elyriand21)