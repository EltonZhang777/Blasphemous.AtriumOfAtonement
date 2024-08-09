using Blasphemous.AtriumOfAtonement.Abjurations;
using Blasphemous.CheatConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.AtriumOfAtonement.Commands;

internal class AbjurationsCommand : ModCommand
{

    protected override string CommandName => "abjuration";

    protected override bool AllowUppercase => true;

    protected override Dictionary<string, Action<string[]>> AddSubCommands()
    {
        return new Dictionary<string, Action<string[]>>()
            {
                { "help", Help },
                { "list", ListAllAbjurations },
                { "current", ListCurrentAbjurations },
                { "activate", ActivateAbjuration },
                { "deactivate", DeactivateAbjuration }
            };
    }

    private void Help(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0)) return;

        Write($"Available {CommandName} commands:");
        Write($"{CommandName} help: Display all available {CommandName} commands");
        Write($"{CommandName} list: List all abjurations, their ID's, and their effects");
        Write($"{CommandName} current: Display all active abjurations");
        Write($"{CommandName} activate [abjurationID]: Activate the specified abjuration");
        Write($"{CommandName} activate all: Activate all abjurations");
        Write($"{CommandName} deactivate [abjurationID]: Deactivate the specified abjuration");
        Write($"{CommandName} deactivate all: Deactivate all abjurations");
    }

    private void ListAllAbjurations(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0)) return;

        Write($" --- All abjurations: ");
        foreach (ModAbjuration abjuration in Main.AtriumOfAtonement.AbjurationHandler.Items)
        {
            Write($"{abjuration.Id}: {Main.AtriumOfAtonement.LocalizationHandler.Localize(abjuration.Id + ".name")} \n" +
                $"{Main.AtriumOfAtonement.LocalizationHandler.Localize(abjuration.Id + ".description")}\n");
        }
    }

    private void ListCurrentAbjurations(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0)) return;

        bool hasActiveAbjuration = false;

        Write($" --- Currently active abjurations: ");
        foreach (ModAbjuration abjuration in Main.AtriumOfAtonement.AbjurationHandler.Items)
        {
            if (abjuration._isActive)
            {
                hasActiveAbjuration = true;
                Write($"{abjuration.Id}: {Main.AtriumOfAtonement.LocalizationHandler.Localize(abjuration.Id + ".name")} \n" +
               $"{Main.AtriumOfAtonement.LocalizationHandler.Localize(abjuration.Id + ".description")}\n");
            }
        }

        if (!hasActiveAbjuration)
        {
            Write($"No currently active abjurations!");
        }
    }

    private void ActivateAbjuration(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 1)) return;

        string abjID = parameters[0];
        bool isValidId = false;

        if (abjID.ToLower().Equals("all"))
        {
            isValidId = true;
            foreach (var abjuration
            in Main.AtriumOfAtonement.AbjurationHandler.Items)
            {
                abjuration.ActivateEffect();
            }
            Write($"Successfully activated all abjurations!");
            return;
        }
        else
        {
            foreach (var abjuration
            in Main.AtriumOfAtonement.AbjurationHandler.Items
            .Where(abj => abj.Id == abjID))
            {
                isValidId = true;
                if (abjuration._isActive)
                {
                    Write($"{abjuration.Id} already activated!");
                    return;
                }

                abjuration.ActivateEffect();
                Write($"Successfully activated {abjID}!");
            }
        }

        if (!isValidId)
        {
            Write($"{abjID} not found!");
        }
    }

    private void DeactivateAbjuration(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 1)) return;

        string abjID = parameters[0];
        bool isValidId = false;

        if (abjID.ToLower().Equals("all"))
        {
            isValidId = true;
            foreach (var abjuration
            in Main.AtriumOfAtonement.AbjurationHandler.Items)
            {
                abjuration.DeactivateEffect();
            }
            Write($"Successfully deactivated all abjurations!");
            return;
        }
        else
        {
            foreach (var abjuration
            in Main.AtriumOfAtonement.AbjurationHandler.Items
            .Where(abj => abj.Id == abjID))
            {
                if (!abjuration._isActive)
                {
                    Write($"{abjuration.Id} already deactivated!");
                    return;
                }

                isValidId = true;
                abjuration.DeactivateEffect();
                Write($"Successfully deactivated {abjID}!");
            }

        }

        if (!isValidId)
        {
            Write($"{abjID} not found!");
        }
    }

}
