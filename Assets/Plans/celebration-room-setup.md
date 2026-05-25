# Project Overview
- Game Title: VR Puzzle Rooms
- High-Level Concept: A VR puzzle game where players complete rooms to progress.
- Players: Single player VR.
- Target Platform: PC VR (Standalone Windows 64).
- Render Pipeline: Performance URP Config.

# Game Mechanics
## Core Gameplay Loop
The player enters the Celebration Room after completing the final puzzle. This room serves as a reward environment with visual effects and interactive celebratory items.

## Controls and Input Methods
- Standard VR movement (Teleport/Continuous) and grabbing via XR Interaction Toolkit.

# UI
- A simple "Congratulations!" panel will be placed in the room to acknowledge the player's victory.

# Key Assets & Context
- **Scene**: `Assets/Scenes/PuzzleRooms/CelebrationRoom/CelebrationRoom.unity`
- **Room Prefab**: `Assets/_Prefabs/Rooms/Room_Traditional.prefab`
- **XR Origin**: `Assets/Samples/XR Interaction Toolkit/3.4.1/Starter Assets/Prefabs/XR Origin (XR Rig).prefab`
- **Celebration Prefab**: `Assets/Samples/XR Interaction Toolkit/3.4.1/Starter Assets/DemoAssets/Prefabs/Interactables/Confetti.prefab`
- **Lighting**: `Assets/_Prefabs/Lighting/Chandelier_Traditional.prefab`, `Assets/_Prefabs/Lighting/Sconce_Candle.prefab`
- **Decorations**:
    - `Assets/_Prefabs/Tables/Table_Side_Traditional.prefab`
    - `Assets/_Prefabs/Art/Art_Painting_Swan.prefab`
    - `Assets/_Prefabs/Plants/Plant_A.prefab`
    - `Assets/_Prefabs/Objects/Speakers/Speaker_RecordPlayer_Brown.prefab`

# Implementation Steps
1. **Scene Setup**:
    - Open `CelebrationRoom.unity`.
    - Instantiate `Room_Traditional` at (0,0,0).
    - Instantiate `XR Origin (XR Rig)` at (0,0,0).
2. **Environment Decoration**:
    - Place `Chandelier_Traditional` on the ceiling.
    - Place `Sconce_Candle` on the walls.
    - Place `Table_Side_Traditional` in a corner.
    - Place `Art_Painting_Swan` on a wall.
    - Place `Plant_A` on the table.
    - Place `Speaker_RecordPlayer_Brown` on the table.
3. **Celebration Effects**:
    - Instantiate multiple `Confetti` prefabs around the room.
    - Set them to play on awake or trigger them via a simple script.
4. **UI Setup**:
    - Create a Canvas (World Space) with a "Congratulations!" TextMeshPro text.
    - Place it in a prominent position facing the player.

# Verification & Testing
- Enter Play Mode in the Unity Editor.
- Verify the player spawns correctly within the room.
- Verify all decorative assets are visible and correctly placed.
- Verify the confetti particles are firing.
- Confirm the UI is readable and correctly positioned.
