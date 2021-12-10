

using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Inventory
{
    public class EquipmentConfig
    {
        public KindEquipmentId KindEquipmentId;
        public int TypeId;

        public string Name;
        public int Rating;

        public int Price;

        public string ImageName;

        public int QtyPurchasedEquipment;
        public bool IsSelectetitem = false;

        public EquipmentConfig(Equipment item)
        {
            KindEquipmentId = item.GetKindEquipment();
            TypeId = item.GetTypeId();
            Name = item.GetName();
            Rating = item.GetRating();
            Price = item.GetPrice();
            ImageName = item.GetImageName();
        }
    }
}
