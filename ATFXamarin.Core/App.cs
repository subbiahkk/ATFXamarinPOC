using ATFXamarin.Core.Services;
using ATFXamarin.Core.ViewModels;
using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore.Platform;

namespace ATFXamarin.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

           // Mvx.RegisterSingleton<ISimpleRestService>(new SimpleRestService());           

            RegisterAppStart<EngagementViewModel>();
        }
    }
}