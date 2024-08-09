#if DEBUG
using Blasphemous.CheatConsole;
using Framework.Managers;
using System;
using System.Collections.Generic;

namespace Blasphemous.AtriumOfAtonement.Commands;

/// <summary>
/// For debug use only
/// </summary>
internal class BossRushDebugCommand : ModCommand
{
    protected override string CommandName => "bossrushdebug";

    protected override bool AllowUppercase => true;

    protected override Dictionary<string, Action<string[]>> AddSubCommands()
    {
        return new Dictionary<string, Action<string[]>>()
        {
            { "help", Help },
            { "showcurrent", ShowCurrent }
        };
    }

    private void Help(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0))
            return;

        Write($"--- Work in progress ---");

        Write($"Available {CommandName} commands:");
        Write($"{CommandName} help: Display all available {CommandName} commands");
        Write($"{CommandName} showcurrent: display the current BossRushCourse info");
    }

    private void ShowCurrent(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0))
            return;

        //Write($"current BossRushCourse: {}");
        int i = 0;
        foreach (string str in BossRushManager.GetAllCoursesNames())
        {
            Write($"#{i} course scene: {str}");
            i++;
        }
        i = 0;
        foreach (string str in brCourse?.GetFontRechargingScenes())
        {
            Write($"#{i} font-recharging scene: {str}");
            i++;
        }
    }
}
#endif