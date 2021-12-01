using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData.Fish;
using CodeBase.StaticData.Hook;
using CodeBase.StaticData.Lure;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.WindowsService;

namespace CodeBase.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
        Dictionary<FishTypeId, FishStaticData> Fishes();
        Dictionary<LureTypeId, LureStaticData> Lures();

        Dictionary<HookTypeId, HookStaticData> Hooks();

        WindowConfig ForWindow(WindowId windowId);
        FishStaticData ForFish(FishTypeId typeId);
        LureStaticData ForLure(LureTypeId lureTypeId);
        HookStaticData ForHook(HookTypeId hookTypeId);
    }
}