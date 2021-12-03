using CodeBase.Infrastructure.AssetManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EquipmentsList
{
    public class EquipmentCardItem : MonoBehaviour
    {
      //  public Button BuyButton;
        public Button SelectButton;

        public TextMeshProUGUI EquipmentName;

        public Image Rating;
        public Image EquipmentPicture;

        public TextMeshProUGUI Price;

        private IAssetProvider _assetProvider;

        public void Constuct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async void Initialize(string name, int rating)
        {
            EquipmentName.text = name;
            DefineRating(rating);
        }


        private async void DefineRating(int rating)
        {
            Debug.Log($"grade_" + rating);
            Rating.sprite = await _assetProvider.Load<Sprite>($"grade_" + rating);
        }
    }
}
