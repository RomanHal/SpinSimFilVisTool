namespace CoreLibrary.Interfaces
{
    public interface IEnergyPart
    {
        string Region { get; set; }
        double DirectionX { get; set; }
        double DirectionY { get; set; }
        double DirectionZ { get; set; }
        double PositionX { get; set; }
        double PositionY { get; set; }
        double PositionZ { get; set; }
        double Value { get; set; }
        IColor Color { get; set; }
        bool Visible { get; set; }
    }
}
