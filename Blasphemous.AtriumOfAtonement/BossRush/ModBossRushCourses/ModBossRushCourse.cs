using Framework.BossRush;
using Framework.Managers;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace Blasphemous.AtriumOfAtonement.BossRush.ModBossRushCourses;

/// <summary>
/// Parent class for all mod-registered BossRush courses
/// </summary>
public class ModBossRushCourse : BossRushCourse
{
    public BossRushManager.BossRushCourseId courseId;
    public string courseName;

    /// <summary>
    /// Construct a ModBossRushCourse with designated ID.
    /// </summary>
    public ModBossRushCourse(
        string name,
        List<string> Scenes,
        List<string> FontRechargingScenes,
        List<float> scoreNormal,
        List<float> scoreHard,
        int id)
    {
        // set basics
        courseName = name;
        courseId = (BossRushManager.BossRushCourseId)id;
        Traverse.Create(this).Field("Scenes").SetValue(Scenes);
        Traverse.Create(this).Field("FontRechargingScenes").SetValue(FontRechargingScenes);

        // set font recharging Vfx
        var scenesById = Traverse.Create(Core.BossRushManager).Field("courseScenesByCourseId")
            .GetValue() as Dictionary<BossRushManager.BossRushCourseId, BossRushCourse>;
        BossRushCourse course_A1 = scenesById[BossRushManager.BossRushCourseId.COURSE_A_1];
        healthFontRechargingVfx = course_A1.healthFontRechargingVfx;
        fervourFontRechargingVfx = course_A1.fervourFontRechargingVfx;

        // format input scores
        // the list of floats indicate time(in minutes) of completion, from fast to slow, 
        //   corresponding from S+ score to the worst score included.
        ScoresByMinutesInNormal = new List<ScoreInterval>();
        ScoresByMinutesInHard = new List<ScoreInterval>();
        float minTime, maxTime;
        BossRushManager.BossRushCourseScore currentScore;
        minTime = 0f;
        maxTime = 0f;
        currentScore = BossRushManager.BossRushCourseScore.S_PLUS;
        foreach (float timeElem in scoreNormal)
        {
            minTime = maxTime;
            maxTime = timeElem;
            ScoresByMinutesInNormal.Add(new ScoreInterval()
            {
                score = currentScore,
                timeRangeInMinutes = new Vector2(minTime, maxTime)
            });
            currentScore--;
        }
        minTime = 0f;
        maxTime = 0f;
        currentScore = BossRushManager.BossRushCourseScore.S_PLUS;
        foreach (float timeElem in scoreHard)
        {
            minTime = maxTime;
            maxTime = timeElem;
            ScoresByMinutesInHard.Add(new ScoreInterval()
            {
                score = currentScore,
                timeRangeInMinutes = new Vector2(minTime, maxTime)
            });
            currentScore--;
        }
        MaxScoreForFailedRuns = currentScore;
    }

    /// <summary>
    /// Construct a ModBossRushCourse, 
    /// and automatically assign its CourseId to a spare Id slot
    /// </summary>
    public ModBossRushCourse(
        string name,
        List<string> Scenes,
        List<string> FontRechargingScenes,
        List<float> scoreNormal,
        List<float> scoreHard) :
        this(name, Scenes, FontRechargingScenes, scoreNormal, scoreHard, -1)
    {
        // find a CourseId not yet occupied starting from 30 (Sixth Sorrow's id)
        List<BossRushManager.BossRushCourseId> allRegisteredCourseIds = [
            BossRushManager.BossRushCourseId.COURSE_A_1,
            BossRushManager.BossRushCourseId.COURSE_A_2,
            BossRushManager.BossRushCourseId.COURSE_A_3,
            BossRushManager.BossRushCourseId.COURSE_B_1,
            BossRushManager.BossRushCourseId.COURSE_C_1,
            BossRushManager.BossRushCourseId.COURSE_D_1
        ];
        foreach (var course in BossRushCourseRegister.Courses)
        {
            allRegisteredCourseIds.Add(course.courseId);
        }

        int i = 30;
        do
        {
            i++;
        } while (allRegisteredCourseIds.Contains((BossRushManager.BossRushCourseId)i));
        courseId = (BossRushManager.BossRushCourseId)i;
    }

}
