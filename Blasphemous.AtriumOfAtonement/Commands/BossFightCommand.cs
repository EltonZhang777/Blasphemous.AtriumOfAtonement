using Blasphemous.CheatConsole;
using Framework.Managers;
using System;
using System.Collections.Generic;


namespace Blasphemous.AtriumOfAtonement.Commands;

internal class BossFightCommand : ModCommand
{
    public static readonly List<string> bossIds = new List<string>()
    {
        "BS01",
        "BS03",
        "BS04",
        "BS05",
        "BS06",
        "BS07",
        "BS12",
        "BS13",
        "BS14",
        "BS16",
        "BS16_2",
        "BS17",
        "BS101_1",
        "BS101_2",
        "BS101_3",
        "BS101_4",
        "BS101_5",
        "BS201",
        "BS202"
    };

    public static readonly Dictionary<string, string> bossIdsToRoomIds = new()
    {
        { "BS01", "D22Z01S02"},
        { "BS03", "D22Z01S03" },
        { "BS04", "D22Z01S04" },
        { "BS05", "D22Z01S06" },
        { "BS06", "D22Z01S07" },
        { "BS07", "D22Z01S10" },
        { "BS12", "D22Z01S05" },
        { "BS13", "D22Z01S01" },
        { "BS14", "D22Z01S08" },
        { "BS16", "D22Z01S09" },
        { "BS16_2", "D22Z01S19" },
        { "BS17", "D22Z01S11" },
        { "BS101_1", "D22Z01S14" },
        { "BS101_2", "D22Z01S12" },
        { "BS101_3", "D22Z01S13" },
        { "BS101_4", "D22Z01S15" },
        { "BS101_5", "D22Z01S16" },
        { "BS201", "D22Z01S18" },
        { "BS202", "D22Z01S17" }
    };

    public static readonly Dictionary<string, int> bossIdsToBossRushCourseIds = new()
    {
        { "BS01", (int)1e6},
        { "BS03", (int)1e6 + 1 },
        { "BS04", (int)1e6 + 2 },
        { "BS05", (int)1e6 + 3 },
        { "BS06", (int)1e6 + 4 },
        { "BS07", (int)1e6 + 5 },
        { "BS12", (int)1e6 + 6 },
        { "BS13", (int)1e6 + 7 },
        { "BS14", (int)1e6 + 8 },
        { "BS16", (int)1e6 + 9 },
        { "BS16_2", (int)1e6 + 10 },
        { "BS17", (int)1e6 + 11 },
        { "BS101_1", (int)1e6 + 12 },
        { "BS101_2", (int) 1e6 + 13 },
        { "BS101_3", (int) 1e6 + 14 },
        { "BS101_4", (int) 1e6 + 15 },
        { "BS101_5", (int) 1e6 + 16 },
        { "BS201", (int) 1e6 + 17 },
        { "BS202", (int) 1e6 + 18 }
    };

    public static readonly List<float> defaultNormalTimeIntervals = new()
    {
        2f, 2.5f, 3f, 3.5f, 4f, 4.5f, 5f, 5.5f, 6f, 6.5f, 7f, 7.5f
    };

    public static readonly List<float> defaultHardTimeIntervals = new()
    {
        3f, 3.5f, 4f, 4.5f, 5f, 5.5f, 6f, 6.5f, 7f, 7.5f, 8f, 8.5f
    };

    protected override string CommandName => "bossfight";

    protected override bool AllowUppercase => true;

    protected override Dictionary<string, Action<string[]>> AddSubCommands()
    {
        return new Dictionary<string, Action<string[]>>()
            {
                { "help", Help },
                { "list", ListBosses },
                { "start", StartBossFight },
                { "end", EndBossFight }
            };
    }

    private void Help(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0)) return;

        Write($"--- Work in progress ---");

        Write($"Available {CommandName} commands:");
        Write($"{CommandName} help: Display all available {CommandName} commands");
        Write($"{CommandName} list: List all the bosses' bossID");
        Write($"{CommandName} start [bossID] [difficulty]: " +
            $"Start fight with the selected boss with selected difficulty\n" +
            $"\tDifficulty: normal / hard");
        Write($"{CommandName} end: End the current boss fight");
    }

    private void ListBosses(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0)) return;

        Write($" --- All bosses: ");
        foreach (string ID in bossIds)
        {
            Write($"{ID}: {Main.AtriumOfAtonement.LocalizationHandler.Localize(ID + ".name")}");
        }
    }

    private void StartBossFight(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 2))
            return;

        string bossId = parameters[0];
        string difficulty = parameters[1];
        if (!bossIds.Contains(bossId))
        {
            Write("Invalid boss ID!");
            return;
        }
        if (!(new List<string>() { "normal", "hard" }.Contains(difficulty)))
        {
            Write("Invalid difficulty! Difficulty options: normal / hard");
            return;
        }

        BossRushManager.BossRushCourseMode courseMode;
        if (difficulty.Equals("hard"))
        {
            courseMode = BossRushManager.BossRushCourseMode.HARD;
        }
        else
        {
            courseMode = BossRushManager.BossRushCourseMode.NORMAL;
        }

        Core.BossRushManager.StartCourse((BossRushManager.BossRushCourseId)(bossIdsToBossRushCourseIds[bossId]), courseMode);
    }

    private void EndBossFight(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0))
            return;

        Core.BossRushManager.EndCourse(false);
    }
}
