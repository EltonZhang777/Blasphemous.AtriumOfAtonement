using Blasphemous.ModdingAPI;

namespace Blasphemous.AtriumOfAtonement;

public class AtriumOfAtonement : BlasMod
{
    public AtriumOfAtonement() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    protected override void OnInitialize()
    {
        Main.AtriumOfAtonement.Log($"{ModInfo.MOD_NAME} has been initialized");
    }
}
