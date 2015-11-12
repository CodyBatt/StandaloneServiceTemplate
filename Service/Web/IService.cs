namespace Service.Web
{
    interface IService
    {
        void Start();

        void Stop();

        string DisplayName { get; }

        string ServiceName { get; }

        string Description { get; }
    }
}