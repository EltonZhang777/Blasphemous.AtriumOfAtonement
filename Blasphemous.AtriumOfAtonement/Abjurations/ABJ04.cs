using Framework.FrameworkCore.Attributes.Logic;
using Framework.Managers;
using UnityEngine;

namespace Blasphemous.AtriumOfAtonement.Abjurations;

public class ABJ04 : ModAbjuration
{

    private ABJ04Config _config;
    private RawBonus beadSlotReduction;


    public ABJ04(ABJ04Config config)
    {
        Id = "ABJ04";
        _isActive = false;
        _config = config;
    }

    public override void ActivateEffect()
    {
        if (_isActive) return;
        _isActive = true;

        beadSlotReduction = new(Mathf.Ceil(Core.Logic.Penitent.Stats.BeadSlots.Final
            * -1f * _config.BEAD_SLOT_REDUCTION_PERCENTAGE));

        Core.Logic.Penitent.Stats.BeadSlots.AddRawBonus(beadSlotReduction);
    }

    public override void DeactivateEffect()
    {
        if (!_isActive) return;
        _isActive = false;

        Core.Logic.Penitent.Stats.BeadSlots.RemoveRawBonus(beadSlotReduction);
    }
}

public class ABJ04Config
{
    /// <summary>
    /// the percentage of bead slots reduced (rounded up)
    /// </summary>
    public float BEAD_SLOT_REDUCTION_PERCENTAGE = 0.5f;
}