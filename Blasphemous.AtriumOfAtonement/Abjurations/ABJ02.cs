using Framework.FrameworkCore.Attributes.Logic;
using Framework.Managers;

namespace Blasphemous.AtriumOfAtonement.Abjurations;

public class ABJ02 : ModAbjuration
{

    private ABJ02Config _config;
    private RawBonus fervourReduction;


    public ABJ02(ABJ02Config config)
    {
        Id = "ABJ02";
        _isActive = false;
        _config = config;
    }

    public override void ActivateEffect()
    {
        if (_isActive) return;
        _isActive = true;

        fervourReduction = new((Core.Logic.Penitent.Stats.Fervour.PermanetBonus + Core.Logic.Penitent.Stats.Fervour.Base) * -1f * _config.FERVOUR_REDUCTION_PERCENTAGE);

        Core.Logic.Penitent.Stats.Fervour.AddRawBonus(fervourReduction);
    }

    public override void DeactivateEffect()
    {
        if (!_isActive) return;
        _isActive = false;

        Core.Logic.Penitent.Stats.Fervour.RemoveRawBonus(fervourReduction);
    }
}

public class ABJ02Config
{
    /// <summary>
    /// the percentage of base fervour reduced
    /// </summary>
    public float FERVOUR_REDUCTION_PERCENTAGE = 0.5f;
}