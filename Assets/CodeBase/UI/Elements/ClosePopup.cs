using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class ClosePopup : MonoBehaviour
    {
        public Button ClosePopupButton;
        public GameObject Popup;

        public void Awake()
        {
            ClosePopupButton.onClick.AddListener(ClosePopupWindow);
        }

        private void ClosePopupWindow()
        {
            Popup.SetActive(false);
        }

    }
}
