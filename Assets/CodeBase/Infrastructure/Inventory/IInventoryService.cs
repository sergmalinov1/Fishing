﻿

using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Inventory
{
    public interface IInventoryService : IService
    {
        List<IEquipment> GetSelectedEquipments();
        List<EquipmentConfig> GetEquipmentsConfigByKind();
        void BuyEquipment(KindEquipmentId kindEquipmentId, int typeId, int price);
        void SelectEquipment(KindEquipmentId kindEquipmentId, int equipmentTypeId);
        bool IsCanBuy(int ProductPrice);
        
    }
}
