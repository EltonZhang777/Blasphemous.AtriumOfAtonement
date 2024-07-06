using BepInEx;

namespace Blasphemous.AtriumOfAtonement;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("Blasphemous.ModdingAPI", "0.1.0")]
public class Main : BaseUnityPlugin
{
    public static AtriumOfAtonement AtriumOfAtonement { get; private set; }

    private void Start()
    {
        AtriumOfAtonement = new AtriumOfAtonement();
    }
}
