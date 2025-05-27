using MirraGames.SDK;
using MirraGames.SDK.Common;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets._scripts
{
    public class LocalizationManager:MonoBehaviour
    {
        private Dictionary<LanguageType, Dictionary<string, string>> _localizations;
        private LanguageType _currentLanguage;
        public Action onLanguageChange;
        private static LocalizationManager _instance;
        public static LocalizationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LocalizationManager>();
                }
                return _instance;
            }
        }
        private void Awake()
        {
            InitializeLocalizations();
          
        }
        private void Start()
        {
            if (MirraSDK.Data.HasKey("language"))
            {
                MirraSDK.Language.Current = MirraSDK.Data.GetObject<LanguageType>("language");
                _currentLanguage = MirraSDK.Language.Current;
            }
            else
            {
                _currentLanguage = MirraSDK.Language.Current;
            }
          
            switch (_currentLanguage)
            {
                case LanguageType.Russian:
                case LanguageType.English:
                case LanguageType.Turkish:
                case LanguageType.German:
                    break;
                default: _currentLanguage = LanguageType.English; break;
            }
            SetLanguage(_currentLanguage);
        }
        private void InitializeLocalizations()
        {
            _localizations = new Dictionary<LanguageType, Dictionary<string, string>>
            {
                [LanguageType.English] = new Dictionary<string, string>
                {
                    ["level"] = "LEVEL {0}",
                    ["continue"] = "Continue?",
                    ["restart"] = "Restart",
                    ["complete"] = "Complete",
                    ["failed"] = "Failed!",
                    ["ads"] = "COFFEE BREAK IN ",
                    ["bundles"] = "BUNDLES",
                    ["main"]="MAIN",
                    ["additional"] = "ADDITIONAL",
                    ["BEGINNER"]= "BEGINNER",
                    ["EASY"]="EASY",
                    ["MEDIUM"] = "MEDIUM",                   
                    ["HARD"]= "HARD",
                    ["EXPERT"]= "EXPERT",
                    ["GURU 1"] = "GURU 1",
                    ["GURU 2"] = "GURU 2",
                    ["GURU 3"] = "GURU 3",
                    ["GURU 4"] = "GURU 4",
                    ["GURU 5"] = "GURU 5",
                    ["FIGURE"] = " FIGURES",
                    ["LEVEL"] = "LVL ",
                    ["play"] = "PLAY",
                    ["level_complete"] = "LEVEL COMPLETE",
                    ["claim"] = "CLAIM",
                    ["unlock_pack"] = "UNLOCK PACK?",
                    ["unlock_pack_description"] = "Unlock pack {0} - {1} for:",
                    ["settings"] = "settings",
                    ["music"] = "music",
                    ["sounds"] = "sounds",
                    ["out_of_coins"] = "OUT OF COINS",
                    ["out_of_coins_description"] = "You have run out of coins.\r\nGet more coins:",
                    ["free_coins"] = "FREE COINS",
                    ["free_coins_description"] = "Watch the ad to get the coins",
                    ["not_enough_cups"] = "NOT ENOUGH CUPS",
                    ["not_enough_cups_description"] = "You don't have enough cups to play this pack. Complete levels in other packs to get more cups!",
                    ["free_coins_award"] = "You have been awarded free coins!",
                    ["leaderboard"] = "LEADERBOARD",
                },
                [LanguageType.Russian] = new Dictionary<string, string>
                {
                    ["level"] = "УРОВЕНЬ {0}",
                    ["continue"] = "Продолжить?",
                    ["restart"] = "Заново",
                    ["complete"] = "Пройден", 
                    ["failed"] = "Провал!",
                    ["main"] = "ОСНОВНОЙ",
                    ["ads"] = "КОФЕ БРЕЙК ЧЕРЕЗ ",
                    ["bundles"] = "НАБОРЫ",
                    ["additional"] = "ДОПОЛНИТЕЛЬНЫЙ",
                    ["BEGINNER"] = "НОВИЧОК",
                    ["EASY"] = "ЛЕГКИЙ",
                    ["MEDIUM"] = "СРЕДНИЙ",
                    ["HARD"] = "СЛОЖНЫЙ",
                    ["EXPERT"] = "ЭКСПЕРТ",
                    ["GURU 1"] = "ГУРУ 1",
                    ["GURU 2"] = "ГУРУ 2",
                    ["GURU 3"] = "ГУРУ 3",
                    ["GURU 4"] = "ГУРУ 4",
                    ["GURU 5"] = "ГУРУ 5",
                    ["FIGURE"] = " ФИГУР",
                    ["LEVEL"] = "УРОВЕНЬ ",
                    ["play"] = "ИГРАТЬ",
                    ["level_complete"] = "УРОВЕНЬ ПРОЙДЕН",
                    ["claim"] = "ПОЛУЧИТЬ",
                    ["unlock_pack"] = "РАЗБЛОКИРОВАТЬ НАБОР?",
                    ["unlock_pack_description"] = "Разблокировать набор {0} - {1} за:",
                    ["settings"] = "настройки",
                    ["music"] = "музыка",
                    ["sounds"] = "звуки",
                    ["out_of_coins"] = "НЕДОСТАТОЧНО МОНЕТ",
                    ["out_of_coins_description"] = "У вас недостаточно монет.\r\nПолучите больше монет:",
                    ["free_coins"] = "БЕСПЛАТНЫЕ МОНЕТЫ",
                    ["free_coins_description"] = "Посмотрите рекламу, чтобы получить монеты",
                    ["not_enough_cups"] = "НЕ ХВАТАЕТ КУБКОВ",
                    ["not_enough_cups_description"] = "У вас недостаточно кубков, чтобы сыграть в этот набор. Пройдите уровни в других наборах, чтобы получить больше кубков!",
                    ["free_coins_award"] = "Вам были выданы бесплатные монеты!",
                    ["leaderboard"] = "РЕЙТИНГ",
                },
                [LanguageType.Turkish] = new Dictionary<string, string>
                {
                    ["level"] = "SEVİYE {0}",
                    ["continue"] = "Devam et?",
                    ["restart"] = "Yeniden başlat",
                    ["complete"] = "Tamamlandı",
                    ["failed"] = "Başarısız!",
                    ["ads"] = "KAHVE MOLASI ",
                    ["bundles"] = "PAKETLER",
                    ["main"] = "ANA",
                    ["additional"] = "EKSTRA",
                    ["BEGINNER"] = "YENİ BAŞLAYAN",
                    ["EASY"] = "KOLAY",
                    ["MEDIUM"] = "ORTA",
                    ["HARD"] = "ZOR",
                    ["EXPERT"] = "UZMAN",
                    ["GURU 1"] = "GURU 1",
                    ["GURU 2"] = "GURU 2",
                    ["GURU 3"] = "GURU 3",
                    ["GURU 4"] = "GURU 4",
                    ["GURU 5"] = "GURU 5",
                    ["FIGURE"] = " ŞEKİLLER",
                    ["LEVEL"] = "SEVİYE ",
                    ["play"] = "OYNAT",
                    ["level_complete"] = "LEVEL ABGESCHLOSSEN",
                    ["claim"] = "TALEP ET",
                    ["unlock_pack"] = "PAKETİ AÇ?",
                    ["unlock_pack_description"] = "KARE - KOLAY paketini açmak için:",
                    ["settings"] = "ayarlar",
                    ["music"] = "müzik",
                    ["sounds"] = "sesler",
                    ["out_of_coins"] = "JETON YETERSİZ",
                    ["out_of_coins_description"] = "Yeterli jetonunuz yok.\r\nDaha fazla jeton alın:",
                    ["free_coins"] = "ÜCRETSİZ JETONLAR",
                    ["free_coins_description"] = "Jeton almak için reklam izleyin",
                    ["not_enough_cups"] = "YETERLİ KUPA YOK",
                    ["not_enough_cups_description"] = "Bu paketi oynamak için yeterli kupanız yok. Daha fazla kupa kazanmak için diğer paketlerde seviyeleri tamamlayın!",
                    ["free_coins_award"] = "Ücretsiz jetonlarınız var!",
                    ["leaderboard"] = "LİDER TABLOSU",
                },
                [LanguageType.German] = new Dictionary<string, string>
                {
                    ["level"] = "LEVEL {0}",
                    ["continue"] = "Fortfahren?",
                    ["restart"] = "Neu starten",
                    ["complete"] = "Vollständig",
                    ["failed"] = "Fehlgeschlagen!",
                    ["ads"] = "KAFFEEPAUSE IN ",
                    ["bundles"] = "PAKETE",
                    ["main"] = "HAUPT",
                    ["additional"] = "ZUSÄTZLICH",
                    ["BEGINNER"] = "ANFÄNGER",
                    ["EASY"] = "EINFACH",
                    ["MEDIUM"] = "MITTEL",
                    ["HARD"] = "SCHWER",
                    ["EXPERT"] = "EXPERTE",
                    ["GURU 1"] = "GURU 1",
                    ["GURU 2"] = "GURU 2",
                    ["GURU 3"] = "GURU 3",
                    ["GURU 4"] = "GURU 4",
                    ["GURU 5"] = "GURU 5",
                    ["FIGURE"] = " FIGUREN",
                    ["LEVEL"] = "LEVEL ",
                    ["play"] = "SPIELEN",
                    ["level_complete"] = "SEVİYE TAMAMLANDI",
                    ["claim"] = "ANSPRUCH",
                    ["unlock_pack"] = "PACKUNG ENTSPERREN?",
                    ["unlock_pack_description"] = "Entsperren Sie Pack SQAURE - EINFACH für:",
                    ["settings"] = "Einstellung",
                    ["music"] = "Musik",
                    ["sounds"] = "MSN",
                    ["out_of_coins"] = "AUS MÜNZEN",
                    ["out_of_coins_description"] = "\r\nSie haben keine Münzen mehr.Holen Sie sich mehr Münzen:",
                    ["free_coins"] = "KOSTENLOSE MÜNZEN",
                    ["free_coins_description"] = "Sehen Sie sich die Anzeige an, um die Münzen zu erhalten",
                    ["not_enough_cups"] = "NICHT GENUG TASSEN",
                    ["not_enough_cups_description"] = "Du hast nicht genug Tassen, um dieses Paket zu spielen. Schließe Level in anderen Packs ab, um mehr Tassen zu erhalten!",
                    ["free_coins_award"] = "Sie haben kostenlose Münzen erhalten!",
                    ["leaderboard"] = "RANGLISTE",
                }
            };
        }


        public string GetText(string key)
        {
            if (_localizations.ContainsKey(_currentLanguage) &&
                _localizations[_currentLanguage].ContainsKey(key))
            {
                return _localizations[_currentLanguage][key];
            }
            return $"Missing_{key}";
        }

        public string GetText(string key, params object[] args)
        {
            string text = GetText(key);
            return string.Format(text, args);
        }

        public void SetLanguage(LanguageType language)
        {
            
            if (_localizations.ContainsKey(language))
            {
                _currentLanguage = language;
            }
            else
            {
                _currentLanguage = LanguageType.Russian;
            }
            MirraSDK.Language.Current = _currentLanguage;
            MirraSDK.Data.SetObject("language", MirraSDK.Language.Current);
            MirraSDK.Data.Save();
            onLanguageChange?.Invoke();
        }

        public LanguageType GetCurrentLanguage()
        {
            return _currentLanguage;
        }
    }
}
