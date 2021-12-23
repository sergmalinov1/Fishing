using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace CodeBase.UI.Windows.InfoPopup
{
    public class InfoPopupWindow : BaseWindow
    {
        public TextMeshProUGUI Title;
        public TextMeshProUGUI TextBlock;

        public void Initialize(string description)
        {
            Title.text = "Проблемка";
            TextBlock.text = description;
        }
    }
}
