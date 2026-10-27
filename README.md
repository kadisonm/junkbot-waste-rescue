# Junkbot: Waste Rescue

A small interactive 3D demo of a robot that cleans up after you. Drop rubbish into the scene and Junkbot walks over, picks it up and sorts it into the right bin. Its decisions are driven by a **behaviour tree system I wrote from scratch** in C#.

<img width="953" height="584" alt="Gameplay" src="https://github.com/user-attachments/assets/73236f6a-e3a3-4b79-815b-0504a3affe35" />

Built in Unity 2022.3 (made in November 2024).

## The behaviour tree

`Assets/Scripts/BehaviourTree.cs` is a small, reusable behaviour tree framework with no external packages:

- `Node`: the base class, which returns `RUNNING`, `SUCCESS` or `FAILURE`
- `Sequence`: runs its children in order and remembers which child is running between ticks
- `Selector`: tries its children in priority order until one succeeds
- `Leaf`: wraps a delegate, so actions can be written inline
- `BehaviorTree`: holds the root node and is ticked every frame

`Assets/Scripts/RobotTree.cs` uses it to build Junkbot's brain as a priority selector:

1. **Being petted.** Hold left-click on the robot and it reacts happily when you let go.
2. **Being picked up.** Hold right-click to lift and carry it around.
3. **Sorting rubbish.** A sequence that finds an item, walks to it, carries it to the matching bin (red, yellow or green) and drops it in.
4. **Idle.** Turns to face the camera.

Higher-priority behaviours interrupt lower ones, so you can grab the robot mid-task and it picks up where it left off.

## Controls

| Action | Input |
| --- | --- |
| Spawn rubbish | UI buttons (hamburger, trash bag, pizza box, container) |
| Place rubbish | Move the mouse, then left-click to drop |
| Pet the robot | Hold left-click on it |
| Pick up the robot | Hold right-click and move the mouse |

## Running the project
Available [here](https://kadisonm.itch.io/junkbot-waste-rescue) on Itch.io!

or

1. Clone the repo.
2. Open it in Unity Hub with **Unity 2022.3.38f1**.
3. Open `Assets/Scenes/Main.unity` and press Play.

## License

MIT
