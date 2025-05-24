using Assets._scripts;
using MirraGames.SDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace BizzyBeeGames.TangramPuzzles
{
    public class TopBar : MonoBehaviour
    {
        #region Inspector Variables

        [SerializeField] private CanvasGroup backButton = null;
        [SerializeField] private Text headerText = null;

        #endregion


        #region Member Variables

        private BundleInfo selectedBundleInfo;
        private string currentScreenId;
        #endregion

        #region Unity Methods

        private void Start()
        {
            backButton.alpha = 0f;

            ScreenManager.Instance.OnSwitchingScreens += OnSwitchingScreens;

            GameEventManager.Instance.RegisterEventHandler(GameEventManager.BundleSelectedEventId, OnBundleSelected);
            GameEventManager.Instance.RegisterEventHandler(GameEventManager.LevelStartedEventId, OnLevelStarted);
        }

        #endregion

        #region Private Methods

        private void OnBundleSelected(string eventId, object[] data)
        {
            selectedBundleInfo = data[0] as BundleInfo;
        }

        private void OnSwitchingScreens(string fromScreenId, string toScreenId)
        {
            if (fromScreenId == ScreenManager.Instance.HomeScreenId)
            {
                UIAnimation anim = UIAnimation.Alpha(backButton, 1f, 0.35f);

                anim.style = UIAnimation.Style.EaseOut;

                anim.Play();
            }
            else if (toScreenId == ScreenManager.Instance.HomeScreenId)
            {
                UIAnimation anim = UIAnimation.Alpha(backButton, 0f, 0.35f);

                anim.style = UIAnimation.Style.EaseOut;

                anim.Play();
            }

            UpdateHeaderText(toScreenId);
        }

        private void OnLevelStarted(string eventId, object[] data)
        {
            string text;

            text = string.Format(LocalizationManager.Instance.GetText("level"), GameManager.Instance.ActiveLevelData.LevelIndex + 1);

            if (ScreenManager.Instance.CurrentScreenId != "game")
            {
                UIAnimation.SwapText(headerText, text, 0.5f);
            }
            else
            {
                headerText.text = text;
            }
        }

        public void UpdateHeaderName()
        {
            UpdateHeaderText(currentScreenId);
        }

        private void UpdateHeaderText(string toScreenId)
        {
            currentScreenId = toScreenId;
            switch (toScreenId)
            {
                case "main":
                    UIAnimation.SwapText(headerText, "", 0.5f);
                    break;
                case "bundles":
                    UIAnimation.SwapText(headerText, LocalizationManager.Instance.GetText("bundles"), 0.5f);
                    break;
                case "pack_levels":
                    if (selectedBundleInfo.bundleName == "BASIC")
                        UIAnimation.SwapText(headerText, LocalizationManager.Instance.GetText("main"), 0.5f);
                    else
                        UIAnimation.SwapText(headerText, LocalizationManager.Instance.GetText("additional"), 0.5f);
                    break;
                case "game":
                    string text = string.Format(LocalizationManager.Instance.GetText("level"), GameManager.Instance.ActiveLevelData.LevelIndex + 1);
                    headerText.text = text;
                    break;
            }
        }

        #endregion
    }
}
