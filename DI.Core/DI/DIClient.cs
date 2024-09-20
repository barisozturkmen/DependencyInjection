namespace DI
{
    public abstract class DIClient
    {
        protected DIClient()
        {
            DIContext.Injector.InjectDependencies(this);
        }
    }
}
