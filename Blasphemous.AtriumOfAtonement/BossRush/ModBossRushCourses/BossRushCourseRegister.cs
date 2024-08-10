using Blasphemous.ModdingAPI;
using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.AtriumOfAtonement.BossRush.ModBossRushCourses;

/// <summary>
/// Register handler for new BossRush courses
/// </summary>
public static class BossRushCourseRegister
{

    private static readonly List<ModBossRushCourse> _courses = new();
    internal static IEnumerable<ModBossRushCourse> Courses => _courses;
    internal static ModBossRushCourse AtIndex(int index) => _courses[index];
    internal static int Total => _courses.Count;

    /// <summary>
    /// Registers a new ModBossRushCourse 
    /// </summary>
    public static void RegisterBossRushCourse(this ModServiceProvider provider, ModBossRushCourse course)
    {
        if (provider == null)
            return;

        // prevents repeated registering the same course
        if (_courses.Any(c => c.courseId == course.courseId))
            return;

        _courses.Add(course);
        ModLog.Info($"Registered custom BossRush course: {course.courseId}");
    }
}
