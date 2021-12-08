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

        WindowConfig ForWindow(WindowId windowId);
        FishStaticData ForFish(FishTypeId typeId);

        List<Equipment> GetListByKindNew(KindEquipmentId kindEquipmentId);
        Equipment GetEquipment(KindEquipmentId kindEquipmentId, int typeId);
    }
}