public interface IInitialBoard
{
    int Width { get; }
    int Height { get; }
    Cell[,] State { get; }

    void NewGame();
}