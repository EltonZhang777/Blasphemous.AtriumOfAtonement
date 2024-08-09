namespace Blasphemous.AtriumOfAtonement.Abjurations;

public class ModAbjuration
{
    public string Id { get; set; }

    public bool _isActive { get; set; }

    public virtual void ActivateEffect() { }

    public virtual void DeactivateEffect() { }
}
