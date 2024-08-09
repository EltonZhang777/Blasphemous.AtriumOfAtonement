using Blasphemous.CheatConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.AtriumOfAtonement.Commands;

internal class BossRushLoadoutCommand : ModCommand
{

    protected override string CommandName => "bossrushloadout";

    protected override bool AllowUppercase => true;

    protected override Dictionary<string, Action<string[]>> AddSubCommands()
    {
        return new Dictionary<string, Action<string[]>>()
        {
            { "help", Help },
            { "show", Show },
            { "save", Save },
            { "load", Load },
            { "deactivate", Deactivate }
        };
    }

    private void Help(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0)) return;

        Write($"Available {CommandName} commands:");
        Write($"{CommandName} help: Display all available {CommandName} commands");
        Write($"{CommandName} show [LoadoutSlotNumber]: Show the details of the specified loadout");
        Write($"{CommandName} show all: Show the details of all stored loadouts");
        Write($"{CommandName} show current: Show the details of currently auto-applied loadout");
        Write($"{CommandName} save [LoadoutSlotNumber]: Store the current loadout to the specified empty slot");
        Write($"{CommandName} save [LoadoutSlotNumber] override: Store the current loadout to the specified slot (data in that slot will be overwritten)");
        Write($"{CommandName} load [LoadoutSlotNumber]: Apply the specified loadout to TPO");
        Write($"{CommandName} load [LoadoutSlotNumber] auto: Automatically apply the specified loadout to TPO each time when starting a BossRush");
        Write($"{CommandName} deactivate: Disable the current automatically-applied loadout");

    }

    private void Show(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 1)) return;
        if (parameters[0].Equals("all"))
        {
            foreach (var loadout in Main.AtriumOfAtonement.BossRushLoadoutHandler.loadoutDatas
                .Where(x => !x.isEmpty))
            {
                Write($"{loadout}");
            }
            return;
        }
        if (parameters[0].Equals("current"))
        {
            foreach (var loadout in Main.AtriumOfAtonement.BossRushLoadoutHandler.loadoutDatas
                .Where(x => x.isAutoLoad))
            {
                Write($"{loadout}");
            }
            return;
        }

        int slotNum = int.Parse(parameters[0]);
        if (!Main.AtriumOfAtonement.BossRushLoadoutHandler.IsSlotIndexInbounds(slotNum)) return;
        Write($"{Main.AtriumOfAtonement.BossRushLoadoutHandler.loadoutDatas[slotNum]}");
    }

    private void Save(string[] parameters)
    {
        if (!(ValidateParameterList(parameters, 1)
            || ValidateParameterList(parameters, 2))) return;

        int slotNum = int.Parse(parameters[0]);
        if (!Main.AtriumOfAtonement.BossRushLoadoutHandler.IsSlotIndexInbounds(slotNum)) return;

        bool isOverride = false;
        try
        {
            isOverride = parameters[1].Equals("override");
        }
        catch
        {
            isOverride = false;
        }

        if (Main.AtriumOfAtonement.BossRushLoadoutHandler.loadoutDatas[slotNum].isEmpty
            || isOverride)
        {
            Main.AtriumOfAtonement.BossRushLoadoutHandler.SaveCurrentStateToSlot(slotNum);
            Write($"Successfully stored current state to slot {slotNum}!");
        }
        else
        {
            Write($"Blocked attempt to override a slot with data in it. \n" +
                $"If you want to override this slot with current state, " +
                $"use `{CommandName} save {slotNum} override` instead.");
        }
    }


    private void Load(string[] parameters)
    {
        if (!(ValidateParameterList(parameters, 1)
            || ValidateParameterList(parameters, 2))) return;

        int slotNum = int.Parse(parameters[0]);
        if (!Main.AtriumOfAtonement.BossRushLoadoutHandler.IsSlotIndexInbounds(slotNum)) return;
        Main.AtriumOfAtonement.BossRushLoadoutHandler.LoadSlotToCurrentState(slotNum);

        bool isAuto = false;
        try
        {
            isAuto = parameters[1].Equals("auto");
        }
        catch
        {
            isAuto = false;
        }
        if (isAuto)
        {
            foreach (var loadout in Main.AtriumOfAtonement.BossRushLoadoutHandler.loadoutDatas)
            {
                loadout.isAutoLoad = false;
            }
            Main.AtriumOfAtonement.BossRushLoadoutHandler.loadoutDatas[slotNum].isAutoLoad = true;
        }
    }

    private void Deactivate(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0)) return;

        foreach (var loadout in Main.AtriumOfAtonement.BossRushLoadoutHandler.loadoutDatas)
        {
            loadout.isAutoLoad = false;
        }
    }

}