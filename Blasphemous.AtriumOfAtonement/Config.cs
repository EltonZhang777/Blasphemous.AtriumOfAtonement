using Blasphemous.AtriumOfAtonement.Abjurations;
using Blasphemous.AtriumOfAtonement.BossRush.BossRushLoadout;

namespace Blasphemous.AtriumOfAtonement;

/// <summary>
/// Master config class containing all the configurations of the mod AtriumOfAtonement
/// </summary>
public class Config
{
    public ABJ01Config ABJ01 = new();
    public ABJ02Config ABJ02 = new();
    public ABJ03Config ABJ03 = new();
    public ABJ04Config ABJ04 = new();
    public ABJ05Config ABJ05 = new();
    public BossRushLoadoutHandler_Config BossRushLoadoutHandler = new();
}
