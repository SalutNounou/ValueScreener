namespace ValueScreener.Controllers.Screeners
{
    public interface IScreenerFactory
    {
        IScreener GetScreener(string criteria);
    }
}