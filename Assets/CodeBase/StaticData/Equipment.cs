using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
    public abstract class Equipment : ScriptableObject, IEquipment
    {
        public EquipmentTypeId EquipmentTypeId;

        public KindEquipmentId _kindEquipmentId;

        public string Name;

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Price = 1;

        public string MainImage;

        public AssetReferenceGameObject PrefabReference;


        public EquipmentTypeId GetEquipmentTypeId() => EquipmentTypeId;

        public KindEquipmentId GetKindEquipment() => _kindEquipmentId;

        public string GetName() => Name;

        public int GetRating() => Rating;

        public int GetPrice() => Price;

        public string GetImageName() => MainImage;

        public abstract int GetTypeId();
        
    }
}
