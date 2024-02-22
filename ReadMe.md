# Improved NPC Attribute Distribution Mod for Mount & Blade II: Bannerlord

## Overview
**Mod Name:** Improved NPC Attribute Distribution  
**Game:** Mount & Blade II: Bannerlord

## Why This Mod?
In the vanilla game of Mount & Blade II: Bannerlord, NPC attribute distribution can be frustrating for players. The allocation of attributes by NPCs may not seem sensible based on their skill levels, as the existing taxicab metric often leads to decisions that don't align with what players would consider logical attribute priorities.

For instance, when comparing the choice between three skills at 50 and one skill at 120, any reasonable person would invest in the skill at 120. However, due to the taxicab metric's reliance, the game may incorrectly decide to allocate attributes to the three skills at 50, which often favors more combat-related attributes.

This mod introduces an improved metric, enabled by default, to offer a more sensible attribute distribution for NPCs. Additionally, players have the flexibility to swap it for other distribution models, allowing customization of their experience with NPCs.

## Features

### Bug Fix
NPCs now correctly gain Attributes during their generation on levels divisible by 4, addressing an issue where it occurred on levels divisible by 4 plus 1.

### Attribute Point Spending Models
Choose from different attribute point spending models:

* **Vanilla:** Retains the original game behavior, utilizing a taxicab metric for attribute distribution.
* **Vanilla Enhanced (Default):** Slightly tweaks the Vanilla model by using a Euclidean metric (squared) for enhanced balance.
* **Optimized:** Removes balance considerations and directly invests in attributes needed, optimizing attribute allocation.

### NPC Base Attribute Points
Set predefined values for the amount of attributes any NPC has available at level 1:

* **Minimum:** 6
* **Weak:** 12
* **Vanilla (Default):** 15
* **Player:** 18
* **Advanced:** 21
* **Strong:** 24
* **Super Human:** 27

### Wanderers Only Spend Necessary Focus Points
Activate this option to make Wanderers spend only necessary focus points, providing more customization options.

## Installation
1. [Download the mod from Nexus Mods](https://www.nexusmods.com/mountandblade2bannerlord/mods/6540/) or [Subscribe to the mod on Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=3162455282).
2. Extract the files to your Mount & Blade II: Bannerlord Modules folder, if you downloaded it from Nexus Mods.
3. Enable the mod in the game's launcher.

## Compatibility
This mod should be compatible with any mod, since it modifies only 3 relatively recently added functions.

## Feedback
If you encounter any issues or have suggestions, there are multiple ways to provide feedback:

* [Submit an issue on the GitHub repository](https://github.com/MatyeusM/Bannerlord-Improved-NPC-Attribute-Distribution) for technical issues or detailed bug reports.
* Leave a comment on the [Steam Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=3162455282) with your thoughts, suggestions, or feedback related to your gameplay experience.
* Visit the [Nexus Mods page](https://www.nexusmods.com/mountandblade2bannerlord/mods/6540/) and leave your comments, suggestions, or feedback for the mod community.

Your feedback is valuable and helps improve the mod for everyone. Thank you for your contributions!

## Credits
Special thanks to the Mount & Blade II: Bannerlord modding community for their support and contributions.
