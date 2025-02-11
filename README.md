# CAP4053 - Shadow of the Sword

## Table of contents
* [General Info](#general-info)
* [My Role and Contributions](my-role-and-contributions)
* [Technologies](#technologies)
* [How to play the game](#how-to-play-the-game)
* [Bugs](#bugs)
* [Contributers](#contributers)
* [3rd-party items in project](3rd-party-items-in-project)

## General Info
The final project for CAP 4053-AI for Game Programming at UCF, a video game created during a single semester by four students.

Shadow of the Sword is video game created using Unity and C#. The game is focused on combat with two different types of enemies with probability based attacks and ability for players to parry attacks.

A Dark-Souls esque dungeon crawler mainly focusing on:
- Attacking
- Parrying telegraphed enemy attacks
- Enemy AI including: walking to player and attacking with one of three attacks

## My Role and Contributions
My primary responsibilities in the project included programming, asset acquisition, level Design, and support & troubleshooting for my teammates. Programmed sophisticated enemy AI, incorporating probability-based attacks and parry mechanics to enhance gameplay. Since I had more experience with game engines, I also took on a support role by assisting my teammates when they encountered issues. 

### Level Design
I designed and built two levels for the video game. Both of the level were inside of a manor. The first had a dungeon-style layout where the player encountered enemies throughout the interior. The second was the throne room, serving as the final boss arena.

The level design process actually started with hand-drawn layouts to plan out the structure and flow of each environment. After that, I found dungeon themed 3D assets and assembled the main layout in Unity, including walls, floors, and ceilings. Once the main layout was complete, I added environmental details such as furniture, torches, and other decorative elements to add to the atmosphere. Finally, I worked on the lighting setup for both maps to create spooky atmosphere and still allow the player to see.

### Enemy Design & AI Programming
I was responsible for designing and implementing the enemies in the game. I created three enemy types: Hollow Guard, Cowboy, and the Final Boss. This included obtaining the enemies' 3D models, most of the weapon assets, and setting up animations for each enemy, including: idle, walking, death, being parried, and multiple attack variations.

For enemy behavior, I developed a simple enemy AI that allowed enemies to detect and follow the player when in range. Once in close range, they engage in combat by selecting attacks randomly based on a probability system.

I also implemented the parry system, adding mechanics to make the enemy's attacks parryable. This included:
- Making the enemy’s weapon glow to indicate a parryable attack.
- Creating unique parry windows for different attack types.
- Implementing logic to stun the enemy momentarily when successfully parried.

While I didn’t work directly on the health system, I collaborated with a teammate to integrate enemy health and ensure proper interactions between the player and enemies.



## Technologies
- Unity
- C#


## How to play the game
- Left click mouse to hit an enemy
- Right click mouse to parry
- Click ‘E’ to read in-game books
- Left Shift is for dashing and stamina 
- Regular WASD controller movement 
- 'ESC' shows the mouse

## Bugs
Players can use blocks on the walls to jump. This was originally a bug but was kept because it made navigating the map more interesting.
Sprinting, moving backwards, and parrying/attacking would allow for faster movement through the level. In the case that someone wanted to find the fastest way through the game, this would be a potential method to follow.

## Contributers 
- Megan Murphy
- Hunter Davis
- Julien Nashi
- Jacob Roberts

## 3rd-party items in project
Animations
- https://www.mixamo.com/#/

Environment
- Ultimate Low Poly Dungeon | 3D Dungeons | Unity Asset Store
   - https://assetstore.unity.com/packages/3d/environments/dungeons/ultimate-low-poly-dungeon-143535 
- Poly Halloween | 3D Props | Unity Asset Store
   - https://assetstore.unity.com/packages/3d/props/poly-halloween-236625
  
Player
- Lowpoly Magician RIO | 3D Humanoids | Unity Asset Store
   - https://assetstore.unity.com/packages/3d/characters/humanoids/lowpoly-magician-rio-288942 
- Low Poly Swords - RPG Weapons
   - https://assetstore.unity.com/packages/3d/props/weapons/free-low-poly-swords-rpg-weapons-198166
  
Enemies
- Poly Halloween | 3D Props | Unity Asset Store
   - https://assetstore.unity.com/packages/3d/props/poly-halloween-236625 
- Lowpoly Cowboy RIO V1.1 | 3D Humanoids | Unity Asset Store
   - https://assetstore.unity.com/packages/3d/characters/humanoids/lowpoly-cowboy-rio-v1-1-288965 
- Lowpoly Medieval Weapon Pack | 3D Weapons | Unity Asset Store 
   - https://assetstore.unity.com/packages/3d/props/weapons/lowpoly-medieval-weapon-pack-291374

Sound effects
- Sword Slash-Sound Effect HD
   - https://youtu.be/DmDV445zi1Y?si=7pf-2LusOgu0mK4T
- Sword Whoosh Sound Effects
   - https://youtu.be/o7nAIbtMoxQ?si=pr-g8fhyZJ5xpqlG
- Walking 
   - https://youtu.be/yHDh_GDHKKI?si=gvjk0M3-Bps2EmqO
