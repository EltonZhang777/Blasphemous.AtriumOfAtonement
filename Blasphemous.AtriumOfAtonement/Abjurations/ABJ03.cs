using Framework.FrameworkCore.Attributes.Logic;
using Framework.Managers;

namespace Blasphemous.AtriumOfAtonement.Abjurations;

public class ABJ03 : ModAbjuration
{

    private ABJ03Config _config;
    private RawBonus strengthReduction;


    public ABJ03(ABJ03Config config)
    {
        Id = "ABJ03";
        _isActive = false;
        _config = config;
    }

    public override void ActivateEffect()
    {
        if (_isActive) return;
        _isActive = true;

        strengthReduction = new((Core.Logic.Penitent.Stats.Strength.PermanetBonus + Core.Logic.Penitent.Stats.Strength.Base) * -1f * _config.STRENGTH_REDUCTION_PERCENTAGE);

        Core.Logic.Penitent.Stats.Strength.AddRawBonus(strengthReduction);
    }

    public override void DeactivateEffect()
    {
        if (!_isActive) return;
        _isActive = false;

        Core.Logic.Penitent.Stats.Strength.RemoveRawBonus(strengthReduction);
    }
}

public class ABJ03Config
{
    /// <summary>
    /// the percentage of attack power reduced
    /// </summary>
    public float STRENGTH_REDUCTION_PERCENTAGE = 0.5f;
}