
namespace CodeBase.StaticData
{
    public interface IEquipment
    {
        public int GetTypeId();

        public string GetName();
        public int GetRating();

        public KindEquipmentId GetKindEquipment();
    }
}
