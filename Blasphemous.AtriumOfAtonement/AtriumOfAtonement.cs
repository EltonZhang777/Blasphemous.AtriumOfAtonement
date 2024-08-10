using Blasphemous.AtriumOfAtonement.Abjurations;
using Blasphemous.AtriumOfAtonement.BossRush.BossRushLoadout;
using Blasphemous.AtriumOfAtonement.BossRush.ModBossRushCourses;
using Blasphemous.AtriumOfAtonement.Commands;
using Blasphemous.CheatConsole;
using Blasphemous.ModdingAPI;
using Blasphemous.ModdingAPI.Localization;
using Framework.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Blasphemous.ModdingAPI.Helpers;

namespace Blasphemous.AtriumOfAtonement;

public class AtriumOfAtonement : BlasMod
{
    public AtriumOfAtonement() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    internal AbjurationHandler AbjurationHandler { get; private set; }
    internal BossRushLoadoutHandler BossRushLoadoutHandler { get; private set; }

    protected override void OnInitialize()
    {
        LocalizationHandler.RegisterDefaultLanguage("en");

        Config cfg = ConfigHandler.Load<Config>();
        ConfigHandler.Save<Config>(cfg);

        AbjurationHandler = new AbjurationHandler(cfg);
        BossRushLoadoutHandler = new BossRushLoadoutHandler(cfg);
    }

    protected override void OnRegisterServices(ModServiceProvider provider)
    {
        // register commands
        provider.RegisterCommand(new AbjurationsCommand());
        provider.RegisterCommand(new BossFightCommand());
        provider.RegisterCommand(new BossRushLoadoutCommand());
#if DEBUG
        provider.RegisterCommand(new BossRushDebugCommand());
#endif

        // register level items

        // register ModBossRushCourses
        // register every single BossFight course
        foreach (string bossId in BossFightCommand.bossIds)
        {
            var modBossRushCourse = ScriptableObject.CreateInstance<ModBossRushCourse>();
            provider.RegisterBossRushCourse(new ModBossRushCourse(
            Main.AtriumOfAtonement.LocalizationHandler.Localize("BossFight.name") + " - " +
                Main.AtriumOfAtonement.LocalizationHandler.Localize(bossId + ".name"),
            new List<string> { BossFightCommand.bossIdsToRoomIds[bossId] },
            new List<string>(),
            BossFightCommand.defaultNormalTimeIntervals,
            BossFightCommand.defaultHardTimeIntervals,
            BossFightCommand.bossIdsToBossRushCourseIds[bossId]));
        }
    }

    protected override void OnLevelLoaded(string oldLevel, string newLevel)
    {
        base.OnLevelLoaded(oldLevel, newLevel);

        if (SceneHelper.GameSceneLoaded)
        {
            // it is a game level, but not other scenes like main menu

            // re-activate all abjurations
            foreach (var abjuration
                in Main.AtriumOfAtonement.AbjurationHandler.Items
                .Where(abj => abj._isActive == true))
            {
                abjuration.DeactivateEffect();
                abjuration.ActivateEffect();
            }
        }
    }

    protected override void OnAllInitialized()
    {
        // re-initialize the BossRushManager after registering all the ModBossRushCourses
        Core.BossRushManager.Initialize();
    }

    protected override void OnDispose()
    {
        // deactivates all abjurations on exit
        foreach (var abjuration
            in Main.AtriumOfAtonement.AbjurationHandler.Items)
        {
            abjuration.DeactivateEffect();
        }
    }

}
