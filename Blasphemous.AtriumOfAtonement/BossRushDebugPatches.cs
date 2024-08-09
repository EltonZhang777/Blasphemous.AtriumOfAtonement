/*
using Blasphemous.ModdingAPI;
using Framework.BossRush;
using Framework.Managers;
using HarmonyLib;
using System.Collections.Generic;

namespace Blasphemous.AtriumOfAtonement;


/// <summary>
/// Display all BossRush courses' scene information on-start
/// </summary>
[HarmonyPatch(typeof(BossRushManager), "StartCourse")]
public class BossRushManager_InfoView_Patch
{
    public static void Postfix(Dictionary<BossRushManager.BossRushCourseId, BossRushCourse> ___courseScenesByCourseId)
    {
        foreach (KeyValuePair<BossRushManager.BossRushCourseId, BossRushCourse> kvp
            in ___courseScenesByCourseId)
        {
            ModLog.Info($"{kvp.Key}: ");

            int i = 0;
            foreach (string scene in kvp.Value.GetScenes())
            {
                ModLog.Info($"#{i} course scene: {scene}");
                i++;
            }
            i = 0;
            foreach (string scene in kvp.Value.GetFontRechargingScenes())
            {
                ModLog.Info($"#{i} font-recharging scene: {scene}");
                i++;
            }
        }
    }
}
*/