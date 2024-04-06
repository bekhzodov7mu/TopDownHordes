using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownHordes.UI
{
    public class ReloadTimerView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Image _fillImage;

        [SerializeField] private TextMeshProUGUI _spellNameText;

        [SerializeField] private GameObject _activeBorder;
        
        public void Init(string spellName)
        {
            _spellNameText.text = spellName;
        }
        
        public void SetTimerValue(float value)
        {
            _fillImage.fillAmount = value;
        }

        public void SetAsActive(bool isActive)
        {
            _activeBorder.SetActive(isActive);
        }
    }
}