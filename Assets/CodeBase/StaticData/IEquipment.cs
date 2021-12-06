
namespace CodeBase.StaticData
{
    public interface IEquipment
    {
        public int GetTypeId();

        public string GetName();
        public int GetRating();

        public int GetPrice();

        public KindEquipmentId GetKindEquipment();
        string GetImageName();
    }
}
