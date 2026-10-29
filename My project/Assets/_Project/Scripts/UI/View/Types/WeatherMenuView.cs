using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.View.Types
{
    public class WeatherMenuView : BaseMenuView
    {
        #region Fields

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;
        [Tooltip("{0} - temperature, {1} - unit")]
        [SerializeField] private string _format = "Сегодня - {0}{1}";

        #endregion

        #region Public

        public void SetForecast(Sprite icon, int temperature, string unit)
        {
            _icon.sprite = icon;
            _icon.enabled = icon != null;
            _text.text = string.Format(_format, temperature, unit);
        }

        #endregion
    }
}
