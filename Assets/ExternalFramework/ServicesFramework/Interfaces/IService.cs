namespace ServiceFramework
{
    public interface IService
    {
        bool ShouldStartOnRegister{get ;}
        string ServiceName{get;}
        void StartService();
        void StopService();
    }
}
