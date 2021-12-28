using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class HudManager : MonoBehaviour
    {
        [SerializeField] private GameObject [] Buttons;


        //[SerializeField] private GameObject SettingButton;
       // [SerializeField] private GameObject ShopButton;

        private IPersistentProgress _progressService;

        public void Construct(IPersistentProgress progressService)
        {
            _progressService =  progressService;
        }

        public void Initialize()
        {
            _progressService.Progress.SettingWindow.StartGameLoop += DisableButton;
            _progressService.Progress.SettingWindow.EndGameLoop += EnableButton;
        }


        private void OnDestroy()
        {
            _progressService.Progress.SettingWindow.StartGameLoop -= DisableButton;
            _progressService.Progress.SettingWindow.EndGameLoop -= EnableButton;
        }

        private void EnableButton()
        {
            //   SettingButton.SetActive(true);
            //   ShopButton.SetActive(true);
            foreach (GameObject button in Buttons)
            {
                button.SetActive(true);
            }
        }

        private void DisableButton()
        {
            //  SettingButton.SetActive(false);
            // ShopButton.SetActive(false);
            foreach (GameObject button in Buttons)
            {
                button.SetActive(false);
            }
        }
    }
}
