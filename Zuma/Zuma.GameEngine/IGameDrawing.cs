namespace Zuma.GameEngine
{
    public interface IGameDrawing
    {
        void DrawBoard(IField board);
        void DrawPath(IPath path);
        void DrawBall(Ball ball);
        void DrawFrog(Frog frog);
        void DrawBonus(Bonus bonus);
    }
}