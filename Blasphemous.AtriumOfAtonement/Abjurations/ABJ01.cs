using Framework.FrameworkCore.Attributes.Logic;
using Framework.Managers;
using UnityEngine;

namespace Blasphemous.AtriumOfAtonement.Abjurations;

public class ABJ01 : ModAbjuration
{

    private ABJ01Config _config;
    private RawBonus healthReduction;
    private RawBonus flaskReduction;


    public ABJ01(ABJ01Config config)
    {
        Id = "ABJ01";
        _isActive = false;
        _config = config;
    }

    public override void ActivateEffect()
    {
        if (_isActive) return;
        _isActive = true;

        healthReduction = new((Core.Logic.Penitent.Stats.Life.PermanetBonus + Core.Logic.Penitent.Stats.Life.Base) * -1f * _config.HEALTH_REDUCTION_PERCENTAGE);
        flaskReduction = new(Mathf.Ceil((Core.Logic.Penitent.Stats.Flask.Base + Core.Logic.Penitent.Stats.Flask.PermanetBonus) * -1f * _config.HEALTH_REDUCTION_PERCENTAGE));

        Core.Logic.Penitent.Stats.Life.AddRawBonus(healthReduction);
        Core.Logic.Penitent.Stats.Flask.AddRawBonus(flaskReduction);
    }

    public override void DeactivateEffect()
    {
        if (!_isActive) return;
        _isActive = false;

        Core.Logic.Penitent.Stats.Life.RemoveRawBonus(healthReduction);
        Core.Logic.Penitent.Stats.Flask.RemoveRawBonus(flaskReduction);
    }
}

public class ABJ01Config
{
    /// <summary>
    /// the percentage of base health reduced
    /// </summary>
    public float HEALTH_REDUCTION_PERCENTAGE = 0.5f;

    /// <summary>
    /// the percentage of flasks reduced (rounded up)
    /// </summary>
    public float FLASK_REDUCTION_PERCENTAGE = 0.5f;
}