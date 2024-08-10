# Blasphemous LocalizationPatcher
---

## Features

- Applicable "Abjurations" can limit your stats and abilities, adding challenge to Blasphemous at your disposal through the `abjuration` command
- Challenge all the vanilla bosses of Blasphemous using the `bossfight` command
- Save and apply different loadouts for BossRush mode using `bossrushloadout` command.
	- loadouts include health/fervour status, relieving no-hitters from the pain of having to blood-sacrifice every time when entering a BossRush
- Allows penitence selection in BossRush mode
- Allows using "Apodictic Heart of Mea Culpa" in BossRush mode
- Allows other mods to create custom BossRush courses

## Bug report

If any bug occurs, locate your log file at `[your Blasphemous directory]\BepInEx\LogOutput.log`, and send it as a discussion thread [here](https://github.com/EltonZhang777/Blasphemous.AtriumOfAtonement/discussions) or DM NewbieElton (discord id: eltonzhang777) on Discord.

## Available commands

- Press the 'backslash' key to open the debug console
- Type the desired command followed by the parameters all separated by a single space

| command                      | parameters                                              | description                                                                                                                                      |
|------------------------------|---------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------|
| `abjuration list`            | _none_                                                  | List all abjurations, their ID's, and their effects                                                                                              |
| `abjuration current`         | _none_                                                  | Display all active abjurations                                                                                                                   |
| `abjuration activate`        | `[abjurationID]` or `all`                               | Activate the specified abjuration(s)                                                                                                             |
| `abjuration deactivate`      | `[abjurationID]` or `all`                               | Deactivate the specified abjuration(s)                                                                                                           |
| `bossfight list`             | _none_                                                  | List all the bosses' bossID                                                                                                                      |
| `bossfight start`            | `[bossID] normal` or `[bossID] hard`                    | Start fight with the selected boss with selected difficulty(normal / hard)                                                                       |
| `bossfight end`              | _none_                                                  | End the current boss fight                                                                                                                       |
| `bossrushloadout show`       | `[LoadoutSlotNumber]` or `all` or `current`             | Show the details of the specified loadout                                                                                                        |
| `bossrushloadout save`       | `[LoadoutSlotNumber]` or `[LoadoutSlotNumber] override` | Store the current loadout to the specified slot  (if passing in the `override` parameter, current data in that slot will be overwritten)         |
| `bossrushloadout load`       | `[LoadoutSlotNumber]` or `[LoadoutSlotNumber] auto`     | Apply the specified loadout to TPO (if passing in the `auto` parameter, the loadout is automatically applied each time when starting a BossRush) |
| `bossrushloadout deactivate` | _none_                                                  | Disable the current automatically-applied loadout                                                                                                |

## Planned Features

- Display the custom ModBossRushCourses in the BossRush selection GUI tab
- Move the functionality of loadout/penitence selection to custom interactable objects in the bossrush hub room.
- A custom room with interactables leading to all the BossRush courses and Miriam parkours

if you want to suggest any features, send it as a discussion thread [here](https://github.com/EltonZhang777/Blasphemous.AtriumOfAtonement/discussions) or DM NewbieElton (discord id: eltonzhang777) on Discord.

