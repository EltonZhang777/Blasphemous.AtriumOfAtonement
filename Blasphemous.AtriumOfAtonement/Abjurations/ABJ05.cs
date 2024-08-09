using Framework.Managers;

namespace Blasphemous.AtriumOfAtonement.Abjurations;

public class ABJ05 : ModAbjuration
{

    private ABJ05Config _config;
    private int lifeUpgrades;
    private int fervourUpgrades;
    private int flaskCountUpgrades;
    private int flaskHealthUpgrades;
    private int beadSlotUpgrades;
    private int strengthUpgrades;

    public ABJ05(ABJ05Config config)
    {
        Id = "ABJ05";
        _isActive = false;
        _config = config;
    }

    public override void ActivateEffect()
    {
        if (_isActive) return;
        _isActive = true;

        var penitentStats = Core.Logic.Penitent.Stats;

        // record current upgrades, then reset all upgrades
        lifeUpgrades = penitentStats.Life.GetUpgrades();
        fervourUpgrades = penitentStats.Fervour.GetUpgrades();
        flaskCountUpgrades = penitentStats.Flask.GetUpgrades();
        flaskHealthUpgrades = penitentStats.FlaskHealth.GetUpgrades();
        beadSlotUpgrades = penitentStats.BeadSlots.GetUpgrades();
        strengthUpgrades = penitentStats.Strength.GetUpgrades();

        penitentStats.Life.ResetUpgrades();
        penitentStats.Fervour.ResetUpgrades();
        penitentStats.Flask.ResetUpgrades();
        penitentStats.FlaskHealth.ResetUpgrades();
        penitentStats.BeadSlots.ResetUpgrades();
        penitentStats.Strength.ResetUpgrades();

    }

    public override void DeactivateEffect()
    {
        if (!_isActive) return;
        _isActive = false;

        var penitentStats = Core.Logic.Penitent.Stats;

        // re-upgrade all stats based on recorded upgrades
        for (int i = 0; i < lifeUpgrades; i++)
        {
            penitentStats.Life.Upgrade();
        }
        for (int i = 0; i < fervourUpgrades; i++)
        {
            penitentStats.Fervour.Upgrade();
        }
        for (int i = 0; i < flaskCountUpgrades; i++)
        {
            penitentStats.Flask.Upgrade();
        }
        for (int i = 0; i < flaskHealthUpgrades; i++)
        {
            penitentStats.FlaskHealth.Upgrade();
        }
        for (int i = 0; i < beadSlotUpgrades; i++)
        {
            penitentStats.BeadSlots.Upgrade();
        }
        for (int i = 0; i < strengthUpgrades; i++)
        {
            penitentStats.Strength.Upgrade();
        }

    }
}

public class ABJ05Config
{
}