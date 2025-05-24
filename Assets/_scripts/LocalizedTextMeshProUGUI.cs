using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets._scripts
{
    public class LocalizedTextMeshProUGUI : MonoBehaviour
    {
        [SerializeField] private string localizationKey;
        
        private TextMeshProUGUI _tmpText;

        private void Start()
        {
            _tmpText = GetComponent<TextMeshProUGUI>();          
            UpdateText();
            LocalizationManager.Instance.onLanguageChange += UpdateText;
        }

        private void UpdateText()
        {
            string localizedText = LocalizationManager.Instance.GetText(localizationKey);
            _tmpText.text = localizedText;
        }

        public void SetKey(string key)
        {
            localizationKey = key;
            UpdateText();
        }

        private void OnDestroy()
        {
            if (LocalizationManager.Instance != null)
            {
                LocalizationManager.Instance.onLanguageChange -= UpdateText;
            }
        }
    }
} 