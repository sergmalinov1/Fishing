using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class MoneyCounter : MonoBehaviour
    {
        public TextMeshProUGUI Money;
        private PlayerProgress _playerProgress;

        public void Construct(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
            _playerProgress.MoneyData.Changed += UpdateCounter;

            UpdateCounter();
        }

        private void UpdateCounter()
        {
            Money.text = $"{_playerProgress.MoneyData.Money}";
        }

        private void OnDestroy()
        {
            //Нужно доделать логику отписки от события
         //   Debug.Log("MoneyCounter OnDestroy");
         //   _playerProgress.MoneyData.Changed -= UpdateCounter;
        }
    }
}