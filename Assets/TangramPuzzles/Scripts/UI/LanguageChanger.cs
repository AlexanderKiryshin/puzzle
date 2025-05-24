using Assets._scripts;
using MirraGames.SDK.Common;
using MirraGames.SDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames.TangramPuzzles;
using BizzyBeeGames;

public class LanguageChanger : MonoBehaviour
{
    [SerializeField] GameObject _russianFlag;
    [SerializeField] GameObject _englishFlag;
    [SerializeField] GameObject _turkeyFlag;
    [SerializeField] GameObject _germanFlag;
    [SerializeField] BundleScreen _bundleScreen;
    [SerializeField] TopBar _topBar;
    [SerializeField] LevelListScreen _levelListScreen;
    private void Awake()
    {
        while (!MirraSDK.IsInitialized)
        {             
            Debug.Log("Waiting for MirraSDK to initialize...");
            System.Threading.Thread.Sleep(100);
         }
        switch (MirraSDK.Language.Current)
        {
            case LanguageType.English:
                _russianFlag.SetActive(false);
                _englishFlag.SetActive(true);
                _turkeyFlag.SetActive(false);
                _germanFlag.SetActive(false);
                break;
            case LanguageType.Russian:
                _russianFlag.SetActive(true);
                _englishFlag.SetActive(false);
                _turkeyFlag.SetActive(false);
                _germanFlag.SetActive(false);
                break;
            case LanguageType.Turkish:
                _russianFlag.SetActive(false);
                _englishFlag.SetActive(false);
                _turkeyFlag.SetActive(true);
                _germanFlag.SetActive(false);
                break;
            case LanguageType.German:
                _russianFlag.SetActive(false);
                _englishFlag.SetActive(false);
                _turkeyFlag.SetActive(false);
                _germanFlag.SetActive(true);
                break;
        }
        switch (ScreenManager.Instance.CurrentScreenId)
        {
            case "bundles":
                _bundleScreen.UpdateUI(false);
                break;
            case "pack_levels":
                _levelListScreen.UpdateList();
                break;
        }
        _topBar.UpdateHeaderName();
    }
    public void LanguageChangeClick()
    {
        switch (MirraSDK.Language.Current)
        {
            case LanguageType.English:
                LocalizationManager.Instance.SetLanguage(LanguageType.Russian);
                _russianFlag.SetActive(true);
                _englishFlag.SetActive(false);
                _turkeyFlag.SetActive(false);
                _germanFlag.SetActive(false);
                break;
            case LanguageType.Russian:
                LocalizationManager.Instance.SetLanguage(LanguageType.Turkish);
                _russianFlag.SetActive(false);
                _englishFlag.SetActive(false);
                _turkeyFlag.SetActive(true);
                _germanFlag.SetActive(false);
                break;
            case LanguageType.Turkish:
                LocalizationManager.Instance.SetLanguage(LanguageType.German);
                _russianFlag.SetActive(false);
                _englishFlag.SetActive(false);
                _turkeyFlag.SetActive(false);
                _germanFlag.SetActive(true);
                break;
            case LanguageType.German:
                LocalizationManager.Instance.SetLanguage(LanguageType.English);
                _russianFlag.SetActive(false);
                _englishFlag.SetActive(true);
                _turkeyFlag.SetActive(false);
                _germanFlag.SetActive(false);
                break;
        }
        switch (ScreenManager.Instance.CurrentScreenId)
        {             case "bundles":
                       _bundleScreen.UpdateUI(false);
                       break;
                   case "pack_levels":
                       _levelListScreen.UpdateList();
                       break;
               }
        _topBar.UpdateHeaderName();
    }
}
