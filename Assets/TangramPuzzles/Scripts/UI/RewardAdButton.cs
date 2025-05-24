using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace BizzyBeeGames.TangramPuzzles
{
	[RequireComponent(typeof(Button))]
	public class RewardAdButton : MonoBehaviour
	{
		#region Inspector Variables

		[SerializeField] private string		currencyId				= "";
		[SerializeField] private int		amountToReward			= 0;
		[SerializeField] private GameObject	uiContainer				= null;
		[SerializeField] private bool		testInEditor			= false;

		[Space]

		[SerializeField] private bool	showOnlyWhenCurrencyIsLow	= false;
		[SerializeField] private int	currencyShowTheshold		= 0;

		[Space]

		[SerializeField] private bool	showRewardGrantedPopup		= false;
		[SerializeField] private string	rewardGrantedPopupId		= "";

        #endregion

        #region Unity Methods

        void Rewarded(int id)
        {
			if (id == 0)
				OnRewardAdGranted("", 0);
        }

        private void Start()
		{
			uiContainer.SetActive(false);
			UpdateUI();
			CurrencyManager.Instance.OnCurrencyChanged	+= OnCurrencyChanged;

			gameObject.GetComponent<Button>().onClick.AddListener(OnClicked);
		}

		#endregion

		#region Private Methods

		private void OnCurrencyChanged(string changedCurrencyId)
		{
			if (currencyId == changedCurrencyId)
			{
				UpdateUI();
			}
		}

		private void UpdateUI()
		{
			uiContainer.SetActive(true);
		}

		private void OnAdsRemoved()
		{
			CurrencyManager.Instance.OnCurrencyChanged	-= OnCurrencyChanged;

			uiContainer.SetActive(false);
		}

		private void OnClicked()
		{
			/*#if UNITY_EDITOR
			if (testInEditor)
			{
				OnRewardAdGranted("", 0);

				return;
			}
			#endif*/
			AdManager.instance.StartRewarded("_", "reward_ad_button", OnRewarded, OnNonReady);
		}

		private void OnRewarded()
		{

		}

		private void OnNonReady()
		{

		}
		private void OnRewardAdGranted(string id, double amount)
		{
			// Increment the currency right now
			CurrencyManager.Instance.Give(currencyId, amountToReward);

			if (showRewardGrantedPopup)
			{
				object[] popupData =
				{
					amountToReward
				};

				// Show a reward ad granted popup
				PopupManager.Instance.Show(rewardGrantedPopupId, popupData);
			}
			else
			{
				// If no reward ad granted popup will appear then update the currency text right away
				CurrencyManager.Instance.UpdateCurrencyText(currencyId);
			}
            SaveManager.Instance.Save();
        }

		#endregion
	}
}
