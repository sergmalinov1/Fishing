using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class LureCounter : MonoBehaviour
    {
        public TextMeshProUGUI LureCount;
        private PlayerProgress _playerProgress;

        public void Construct(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
            _playerProgress.EquipmentStats.ChangeStats += UpdateCounter;

            UpdateCounter();
        }

        private void UpdateCounter()
        {
            int count = _playerProgress.EquipmentStats.StackToCatchFish.Count;
            LureCount.text = $"{count}";
        }
    }
}
