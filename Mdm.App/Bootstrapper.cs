using Mdm.App.Infrastructure;
using Mdm.App.ViewModels;
using SimpleInjector;

namespace Mdm.App
{
    public static class Bootstrapper
    {
        public static Container RegisterApp(this Container container)
        {
            container.RegisterSingleton<IStateManager, StateManager>();
            container.RegisterSingleton<IQueueSheetViewModel, QueueSheetViewModel>();
            container.RegisterSingleton<ITrackListViewModel, TrackListViewModel>();
            container.RegisterSingleton<IUserSettingsViewModel, UserSettingsViewModel>();
            container.RegisterSingleton<IUnlockQueueSheetViewModel, UnlockQueueSheetViewModel>();
            container.RegisterSingleton<ILoginViewModel, LoginViewModel>();

            return container;
        }
    }
}
