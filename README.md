# R.E.P.O Mod Library

## Overview

The R.E.P.O Mod Library is a MelonLoader mod for the game REPO, designed to provide a comprehensive set of utility functions and shared data management for modding the game. Created by Lillious & .Zer0, this library offers extensive control over player mechanics, game state tracking, and system interactions.

## Features

### Shared Data Management
- Track game state across different scenes and menus
- Monitor player location and current game context
- Manage levels and menu states

### Player Controller Utilities
- Modify player attributes dynamically
- Control player movement and energy
- Manipulate player physics and movement speeds

### Key Capabilities

#### Game State Tracking
- Detect current game context (Main Menu, Lobby, Shop, Arena, In-Game)
- Track game initialization status
- Manage Steam integration

#### Player Mechanics
- Revive and respawn player
- Teleport player
- Modify energy levels
- Control movement speeds (sprint, crouch, normal)
- Adjust gravity
- Toggle sprinting

### System Managers
- Access to RunManager
- Access to StatsManager
- Steam ID retrieval

## Installation
1. Install this nuget package to your project
2. import the library into your mod

## Usage Examples

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

## Compatibility
- Supports latest R.E.P.O game
- Compatible with MelonLoader modding framework
- Currently still in development, so expect missing features and bugs

## License
Created by Lillious & .Zer0