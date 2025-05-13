# CustomLevelNames
*Swiftbroom Academy? No, that's Hogwarts Online!*

This mod adds the ability to customize the names of the levels. You can change the name of the shop, arena, and all extraction levels. To change the names of the levels use the config file.

## Changing Extraction Level Names
Levels like Headman Manor and Swiftbroom Academy can have their names changed with the levelNames config value. For example, we can change Swiftbroom Academy to Hogwarts Online.
> Wizard:Hogwarts Online

Wizard is the level ID. All level IDs will be logged by the mod when you load the game. You can also have the mod randomize between different custom names. For example, maybe we also want Swiftbroom Academy to have a chance of being called Magic School.
> Wizard:Hogwarts Online;Magic School

You can also change the name of multiple extraction levels. For example, maybe we also want Headman Manor to be called Woodland Mansion.
> Wizard:Hogwarts Online;Magic School,Manor:Woodland Mansion

## Changing Arena And Shop Level Names
The arena and shop levels can have their names changed with the arenaName and shopName config values. For example, we can change the arena from Disposal Arena to Garbage Chute.
> Garbage Chute

Like extraction level names, you can have the mod randomize between different custom names. For example, maybe we also want the arena to have a chance of being called Waste Dump.
> Garbage Chute;Waste Dump

**If anything is formatted incorrectly in the config file then custom level names may not appear or appear wrong.** Also, the config values are case insensitive, so there is no need to worry about correct capitalization.

If you have different possible names set for levels in the config note that they will change by default only when you game over. You have the option to set the names to randomize after each individual level instead in the config.

To install the mod manually (outside Thunderstore) put the "CustomLevelNames.dll" file inside a folder called CustomLevelNames (or whatever you want to name it) under the BepInEx/plugins directory.
