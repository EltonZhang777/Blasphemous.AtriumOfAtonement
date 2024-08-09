using Blasphemous.AtriumOfAtonement.BossRush.ModBossRushCourses;
using Blasphemous.ModdingAPI;
using Framework.BossRush;
using Framework.Managers;
using Gameplay.UI;
using Gameplay.UI.Others.MenuLogic;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.AtriumOfAtonement.BossRush;

/// <summary>
/// Load each custom bossrush course into the game when opening Sacred Sorrow page
/// </summary>
[HarmonyPatch(typeof(BossRushManager), "LoadCourseScenes")]
public class BossRushManager_LoadCourseScenes_RegisterNewCourses_Patch
{
    public static void Postfix(Dictionary<BossRushManager.BossRushCourseId, BossRushCourse> ___courseScenesByCourseId)
    {
        ModLog.Info($"starting LoadCourseScenes\n" +
            $"loading {BossRushCourseRegister.Courses.Count()} custom courses");
        foreach (var course in BossRushCourseRegister.Courses)
        {
            ModLog.Info($"registering ModBossRushCourse {course.courseName}");
            ___courseScenesByCourseId[course.courseId] = course;
        }
    }
}

#if DEBUG
/// <summary>
/// debug use only
/// </summary>
[HarmonyPatch(typeof(BossRushManager), "LoadCourseScenes")]
public class BossRushManager_LoadCourseScenes_Debug_Patch
{
    public static void Postfix(Dictionary<BossRushManager.BossRushCourseId, BossRushCourse> ___courseScenesByCourseId)
    {
        ModLog.Warn($"Displaying each BossRushCourse info:\n\n");
        foreach (var kvp in ___courseScenesByCourseId)
        {
            var id = kvp.Key;
            var course = kvp.Value;
            ModLog.Debug($"BossRushCourse Name: {id}:\n");
            ModLog.Debug($"Course maxFailedScore: {course.MaxScoreForFailedRuns}\n");
            ModLog.Debug($"Course normalScore:\n");
            foreach (var score in course.ScoresByMinutesInNormal)
            {
                ModLog.Debug($"{score.score}: {score.timeRangeInMinutes.x} -> {score.timeRangeInMinutes.y}");
            }
            ModLog.Debug($"\n");
            ModLog.Debug($"Course hardScore:\n");
            foreach (var score in course.ScoresByMinutesInHard)
            {
                ModLog.Debug($"{score.score}: {score.timeRangeInMinutes.x} -> {score.timeRangeInMinutes.y}");
            }
            ModLog.Debug($"\n");
        }
    }
}
#endif

/// <summary>
/// When opening Sword Heart tab of Inventory in BossRush hub, 
/// always allow (un)equipping sword hearts,
/// and gives the Apodictic sword heart.
/// </summary>
[HarmonyPatch(typeof(NewInventoryWidget), "SelectTab")]
public class NewInventoryWidget_SelectTab_HE201InBossRush_Patch
{
    public static void Prefix(NewInventoryWidget.TabType tabType)
    {
        if (Core.GameModeManager.IsCurrentMode(GameModeManager.GAME_MODES.BOSS_RUSH)
            && Core.LevelManager.currentLevel.LevelName.Equals("D22Z01S00")
            && tabType == NewInventoryWidget.TabType.Sword)
        {
            Core.InventoryManager.AddSword("HE201");
            Core.Logic.Penitent.AllowEquipSwords = true;
            UIController.instance.CanEquipSwordHearts = true;
        }
    }
}

/// <summary>
/// Disable any penitence at the start of BossRush course, 
/// make ChoosePenitenceWidget appear. 
/// Also, set the flag of first hub to true, and activate the auto loadout.
/// </summary>
[HarmonyPatch(typeof(BossRushManager), "StartCourse")]
public class BossRushManager_StartCourse_PenitenceSelection_Patch
{
    public static void Prefix()
    {
        Core.Events.SetFlag("IS_FIRST_BOSSRUSH_HUB", true);
    }

    public static void Postfix()
    {
        UIController.instance.ShowChoosePenitenceWidget(
            onChoosingPenitence,
            onContinueWithoutChoosingPenitence);
        Core.Input.SetBlocker("UIBLOCKING_PENITENCE", true);
    }
    private static void onChoosingPenitence()
    {
        ActivateAutoLoadout();
    }

    private static void onContinueWithoutChoosingPenitence()
    {
        ActivateAutoLoadout();
    }

    internal static void ActivateAutoLoadout()
    {
        var autoLoadout = Main.AtriumOfAtonement.BossRushLoadoutHandler.loadoutDatas
            .Find(x => x.isAutoLoad);
        if (autoLoadout == null) return;
        Main.AtriumOfAtonement.BossRushLoadoutHandler.LoadLoadoutToCurrentState(autoLoadout);
    }
}

/// <summary>
/// When loading the first fight of bossrush, set the flag of first hub to false
/// </summary>
[HarmonyPatch(typeof(BossRushManager), "LoadCourseNextScene")]
public class BossRushManager_LoadCourseNextScene_ShowChoosePenitenceWidget_Patch
{
    public static void Postfix()
    {
        Core.Events.SetFlag("IS_FIRST_BOSSRUSH_HUB", false);
    }

}

/*
// WIP, still have bugs
/// <summary>
/// Apply the currently auto-activated BossRush loadout at the start of BossRush
/// </summary>
[HarmonyPatch(typeof(BossRushManager), "StartCourse")]
public class BossRushManager_StartCourse_AutoApplyLoadout_Patch
{
    public static void Postfix()
    {
        var autoLoadout = Main.AtriumOfAtonement.BossRushLoadoutHandler.loadoutDatas
            .Find(x => x.isAutoLoad);
        ModLog.Info($"autoloadout: {autoLoadout}\n penitent: {Core.Logic.Penitent}");  
        if (autoLoadout == null) return;
        Main.AtriumOfAtonement.BossRushLoadoutHandler.LoadLoadoutToCurrentState(autoLoadout);
    }
}
*/

/// <summary>
/// Load the ModBossRushCourses into the game
/// </summary>
[HarmonyPatch(typeof(BossRushManager), "LoadCourseScenes")]
public class BossRushManager_LoadCourseScenes_LoadModBossRushCourses_Patch
{
    public static void Postfix(BossRushManager __instance,
        Dictionary<BossRushManager.BossRushCourseId, BossRushCourse> ___courseScenesByCourseId)
    {
        foreach (var course in BossRushCourseRegister.Courses)
        {
            ___courseScenesByCourseId[course.courseId] = course;
        }
    }
}

// WIP
/*
/// <summary>
/// Remove the fade-in and fade-out of the score widget
/// </summary>
[HarmonyPatch(typeof(BossRushManager), "EndCourse")]
public class BossRushManager_EndCourse_SpeedUpScorePanel_Patch
{
    public static bool Prefix(BossRushManager __instance,
        bool completed)
    {
        // Copy-pasted the whole original method, and removes the coroutine part.

        bool unlockHard = false;
        if (completed)
        {
            __instance.currentScore.NumScenesCompleted++;
            if (__instance.currentCourseMode == BossRushManager.BossRushCourseMode.NORMAL)
            {
                unlockHard = __instance.courseData.UnlockCourse(__instance.currentCourseId, BossRushManager.BossRushCourseMode.HARD);
            }
        }
        __instance.UnsuscribeToHighScoreRelatedEvents();
        Core.GameModeManager.ChangeMode(GameModeManager.GAME_MODES.MENU);
        __instance.StopTimerAndHide();
        __instance.currentScore.CalculateScoreObtained(completed);
        BossRushManager.CourseData courseData = null;
        if (!__instance.courseData.Data.TryGetValue(__instance.currentCourseId, out courseData))
        {
            __instance.courseData.UnlockCourse(__instance.currentCourseId, __instance.currentCourseMode);
            courseData = __instance.courseData.Data[__instance.currentCourseId];
        }
        courseData.SetHighScoreIfBetter(__instance.currentCourseMode, __instance.currentScore);
        __instance.configFile.SaveData();
        __instance.LogHighScoreObtained();
        ModifiedShowScoreAndGoNext(true, completed, unlockHard, __instance);
        if (__instance.currentCourseId == BossRushManager.BossRushCourseId.COURSE_C_1 && __instance.courseData.Data[__instance.currentCourseId].GetHighScore(BossRushManager.BossRushCourseMode.HARD) != null && __instance.courseData.Data[__instance.currentCourseId].GetHighScore(BossRushManager.BossRushCourseMode.HARD).WasTheCourseCompleted)
        {
            Core.ColorPaletteManager.UnlockBossRushColorPalette();
        }
        __instance.CheckToGrantBossRushRankSPalette();

        return false;
    }

    private static void ModifiedShowScoreAndGoNext(bool pauseGame, 
        bool complete, 
        bool unlockHard, 
        BossRushManager __instance)
    {
        ModifiedShowBossRushRanksAndWait(__instance.currentScore, pauseGame, complete, unlockHard);
        if (UIController.instance.BossRushRetryPressed)
        {
            __instance.StartCourse(__instance.currentCourseId, __instance.currentCourseMode, __instance.SourceSlot);
        }
        else
        {
            UIController.instance.ShowLoad(true, new Color?(Color.black));
            Core.Logic.LoadMenuScene(false);
            UIController.instance.ShowPurgePoints();
        }
    }

    private static void ModifiedShowBossRushRanksAndWait(BossRushHighScore score, 
        bool pauseGame, 
        bool complete, 
        bool unlockHard)
    {

        if (complete)
        {
            UIController.instance.PlayBossRushRankAudio(true);
            //yield return UIController.instance.ShowFullMessageCourrutine(UIController.FullMensages.EndBossDefeated, 4f, 1f, -1f);
            Core.UI.Fade.Fade(true, 1f, 0f, null);
            UIController.instance.fullMessages.SetActive(false);
        }
        UIController.instance.bossRushRankWidget.ShowHighScore(score, pauseGame, complete, unlockHard);
        if (Core.Input.HasBlocker("FADE"))
        {
            Core.UI.Fade.Fade(false, 0.5f, 0f, null);
        }
        UIController.instance.BossRushRetryPressed = UIController.instance.bossRushRankWidget.RetryPressed;
    }
}
*/