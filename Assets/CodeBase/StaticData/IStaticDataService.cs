using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData.Fish;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.WindowsService;

namespace CodeBase.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
        Dictionary<FishTypeId, FishStaticData> Fishes();
        Dictionary<LureTypeId, LureStaticData> Lures();
        Dictionary<BobberTypeId, BobberStaticData> Bobber();
        Dictionary<HookTypeId, HookStaticData> Hooks();

        Dictionary<FishingLineId, FishingLineStaticData> FishingLine();
        Dictionary<FishingRodId, FishingRodStaticData> FishingRod();

        Dictionary<LakeTypeId, LakeStaticData> Lake();


        WindowConfig ForWindow(WindowId windowId);
        FishStaticData ForFish(FishTypeId typeId);
        LureStaticData ForLure(LureTypeId lureTypeId);
        HookStaticData ForHook(HookTypeId hookTypeId);
        BobberStaticData ForBobber(BobberTypeId bobberTypeId);
        
        FishingLineStaticData ForFishingLine(FishingLineId fishingLineTypeId);
        FishingRodStaticData ForFishingRod(FishingRodId fishingRodTypeId);
        List<IEquipment> GetListByKind(KindEquipmentId kindEquipmentId);
        LakeStaticData ForLake(LakeTypeId lakeTypeId);
       
    }
}