using System.Collections.Generic;

namespace Blasphemous.AtriumOfAtonement.BossRush.BossRushLoadout;

/// <summary>
/// Contains a single loadout information in BossRush
/// </summary>
public class BossRushLoadoutData
{
    public float health = -1;
    public float fervour = -1;
    public int flaskCount = -1;
    public List<string> beads;
    public string swordHeart;
    public string prayer;

    public bool isAutoLoad = false;

    public bool isEmpty
    {
        get
        {
            return health < 0 || fervour < 0 || flaskCount < 0;
        }
    }

    public override string ToString()
    {
        string result;

        result = Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.health") + health.ToString() + "  "
            + Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.fervour") + fervour.ToString() + "  "
            + Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.flask_count") + flaskCount.ToString() + "\n"
            + Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.beads");
        foreach (string bead in beads)
        {
            result += " " + bead;
        }
        result += "\n" + Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.sword_heart") + swordHeart + "  "
            + Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.prayer") + prayer;

        return result;
    }
}