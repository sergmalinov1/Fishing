
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.UI.Windows.EquipmentsList
{
    public class ListContainer : MonoBehaviour
    {
        public Transform SelectedItem;
        public Transform CardsList;

        private PlayerProgress _progress;

        public void Construct(PlayerProgress progress)
        {
            _progress = progress;
        }

        public void Initialize()
        {
        }
    }
}
