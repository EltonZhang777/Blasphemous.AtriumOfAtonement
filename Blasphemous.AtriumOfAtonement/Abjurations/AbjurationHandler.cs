namespace Blasphemous.AtriumOfAtonement.Abjurations;

internal class AbjurationHandler(Config cfg) : ItemList<ModAbjuration>
{
    public ABJ05 ABJ05 { get; set; } = new ABJ05(cfg.ABJ05);
    public ABJ01 ABJ01 { get; set; } = new ABJ01(cfg.ABJ01);
    public ABJ02 ABJ02 { get; set; } = new ABJ02(cfg.ABJ02);
    public ABJ03 ABJ03 { get; set; } = new ABJ03(cfg.ABJ03);
    public ABJ04 ABJ04 { get; set; } = new ABJ04(cfg.ABJ04);

}
