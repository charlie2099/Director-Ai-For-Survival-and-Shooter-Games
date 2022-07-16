# A Director Ai For Shooter and Survival Games

![Screenshot (75)](https://user-images.githubusercontent.com/55750961/179343007-e98cdc86-8e62-4180-8781-88b5c9bd2539.png)

## What is a Director Ai?
A Director Ai is a system designed to handle the pacing of a game, orchestrating how the drama unfolds to craft the best experience for the player so that no playthrough is the same as the last. The Director ensures that the player(s) abide to the game rules set out by the designer(s) to create a fair experience and to enforce peaks and dips of intensity to create a fresh and rewarding experience. 

## How does it work?
The performance of the player(s) is measured by the Director using a stress or intensity metric, which is monitored to determine when a player is having a tough time or when they are finding the game too easy and are in need of a challenge. The Director decides if and when a player should be rewarded with health and ammo drops, or when they should be punished with boss spawns and an increased population of enemies.

## Implementation

### Active Area Set
The **Active Area Set (AAS)** defines the active area in which enemies and items are populated into the game world. The **AAS** is attached to the player so that enemies are only spawned in the areas around the player (but outside of their camera view), and any enemies that are too far from the player are despawned.

### Rule-Based System
The Director Ai behaviour is crafted through the use of two simple rules engines. Any number of rules can be made by the game designer to control how the intensity metric is measured (**Intensity Rules Engine**), as well as how and when the Director may spawn enemies, items, etc (**Game Event Rules Engine**). 

## How To Use
0) Import the Director package (releases)
1) Place the Director prefab into the scene
2) Pass in the required data needed by the Director (inspector)
3) Locate the Rule directories at **/AiDirector/RuleSystem/Rules**
4) Create a script inside the directory you want to create a new rule for, either **/GameEventRules** OR **/IntensityRules**. 
5) Ensure the class inherits from the appropriate interface, being **IDirectorGameEventRule** or **IDirectorIntensityRule**. 
6) Create your rule
7) To make sure the rule is used by the Director, add the rule to the list in the constructor of the appropriate rule calculator script. 
8) Play around with the options in the Director's inspector to customise the Director to your liking!

## Dependencies
- The Director's AAS system makes use of the AstarPathfinding project for generating a pathfinding nav mesh. You can download it here **https://arongranberg.com/astar/**
