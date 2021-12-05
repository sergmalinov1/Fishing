using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EquipmentsList
{
    public class SelectedICardtem : MonoBehaviour
    {
        public TextMeshProUGUI CategoryName;

        public Button CategoryButton;

        public Image Rating;
        public Image EquipmentPicture;

        private PlayerProgress _progress;
        private IAssetProvider _assetProvider;



        public void Construct(
            PlayerProgress progress,
            IAssetProvider assetProvider)
        {
            _progress = progress;
            _assetProvider = assetProvider;
        }

        public async void Initialize(string name, int rating)
        {
            CategoryButton.onClick.AddListener(OnItemClick);


            CategoryName.text = name;
            DefineRating(rating);
        }

        private void OnItemClick()
        {

        }

        private async void DefineRating(int rating)
        {
            Rating.sprite = await _assetProvider.Load<Sprite>($"grade_" + rating);
        }
    }
}
