using Autofac;

namespace Logic
{
    public class LogicIocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestLogic>().As<ITestLogic>();
        }
    }
}
