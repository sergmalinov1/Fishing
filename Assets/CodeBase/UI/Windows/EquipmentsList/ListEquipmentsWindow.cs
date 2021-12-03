using CodeBase.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.EquipmentsList
{
    public class ListEquipmentsWindow : BaseWindow
    {
        public TextMeshProUGUI Title;

        public ListContainer ListContainer;

        public void Construct(PlayerProgress progress)
        {
            ListContainer.Construct(progress);
        }


        protected override void Initialize()
        {
            Title.text = "Equipment";
            ListContainer.Initialize();
        }

        protected override void SubscribeUpdate() { }

        protected override void Cleanup() { }

    }
}
