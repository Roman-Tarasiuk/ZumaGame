namespace Zuma.GameEngine
{
    public interface IPath
    {
        PointF[] Points { get; }
    }


    public enum MovingDirection
    {
        Forward,
        Backward
    }
}