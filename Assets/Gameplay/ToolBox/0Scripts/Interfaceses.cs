namespace ToolBoxSystem
{
    public interface IAwake
    {
        void OnAwake();
    }
    public interface ITickLate
    {
        void TickLate();
    }
    public interface ITickFixed
    {
        void TickFixed();
    }
    public interface ITick
    {
        void Tick();
    }
    public interface IRPS
    {
        string Identificator { get; set; }
        void RPS();
    }
    public interface IUpdate
    {
        void Update();
    }
}