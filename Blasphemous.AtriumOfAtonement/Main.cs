using BepInEx;
using System;

namespace Blasphemous.AtriumOfAtonement;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("Blasphemous.CheatConsole", "1.0.1")]
[BepInDependency("Blasphemous.Framework.Levels", "0.1.4")]
[BepInDependency("Blasphemous.Framework.Menus", "0.3.4")]
[BepInDependency("Blasphemous.Framework.UI", "0.1.2")]
[BepInDependency("Blasphemous.ModdingAPI", "2.4.1")]
internal class Main : BaseUnityPlugin
{
    public static AtriumOfAtonement AtriumOfAtonement { get; private set; }

    private void Start()
    {
        AtriumOfAtonement = new AtriumOfAtonement();
    }

    public static T Validate<T>(T obj, Func<T, bool> validate)
    {
        return validate(obj)
            ? obj
            : throw new System.Exception($"{obj} is an invalid argument");
    }
}
