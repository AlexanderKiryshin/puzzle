using Assets._scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace BizzyBeeGames.TangramPuzzles
{
	public class PackListItem : ClickableListItem
	{
		#region Inspector Variables

		[SerializeField] private Text			nameText				= null;
		[SerializeField] private Text			descriptionText			= null;
		[SerializeField] private ProgressBar	progressBarContainer	= null;
		[SerializeField] private Text			progressText			= null;
		[Space]
		[SerializeField] private GameObject		lockedContainer			= null;
		[SerializeField] private GameObject		coinsLockedContainer	= null;
		[SerializeField] private GameObject		starsLockedContainer	= null;
		[SerializeField] private GameObject		iapLockedContainer		= null;
		[SerializeField] private Text			coinsUnlockAmountText	= null;
		[SerializeField] private Text			starsUnlockAmountText	= null;
		[SerializeField] private Text			iapPriceText			= null;

		#endregion

		#region Public Variables

		public void Setup(PackInfo packInfo)
		{
			switch (packInfo.packName)
			{
				case "BEGINNER": 
                    nameText.text =LocalizationManager.Instance.GetText("BEGINNER");
                    break;
					case "EASY":
					nameText.text = LocalizationManager.Instance.GetText("EASY");
                    break;
					case "MEDIUM":
					nameText.text = LocalizationManager.Instance.GetText("MEDIUM");
                    break;
					case "HARD":
					nameText.text = LocalizationManager.Instance.GetText("HARD");
                    break;
					case "EXPERT":
					nameText.text = LocalizationManager.Instance.GetText("EXPERT");
                    break;
					case "GURU 1":
					nameText.text = LocalizationManager.Instance.GetText("GURU 1");
                    break;
					case "GURU 2":
					nameText.text = LocalizationManager.Instance.GetText("GURU 2");
                    break;
					case "GURU 3":
					nameText.text = LocalizationManager.Instance.GetText("GURU 3");
                    break;
					case "GURU 4":
					nameText.text = LocalizationManager.Instance.GetText("GURU 4");
                    break;
					case "GURU 5":
					nameText.text = LocalizationManager.Instance.GetText("GURU 5");
                    break;
			}

			string[] parts = packInfo.packDescription.Split(' ');
            string numbers = string.Join(" ", parts[0], parts[1], parts[2]);
            descriptionText.text =  numbers + LocalizationManager.Instance.GetText("FIGURE");

			// Check if the pack is locked and update the ui
			bool isPackLocked = GameManager.Instance.IsPackLocked(packInfo);

			lockedContainer.SetActive(isPackLocked);
			progressBarContainer.gameObject.SetActive(!isPackLocked);
			coinsLockedContainer.SetActive(isPackLocked && packInfo.unlockType == PackUnlockType.Coins);
			starsLockedContainer.SetActive(isPackLocked && packInfo.unlockType == PackUnlockType.Stars);
			iapLockedContainer.SetActive(isPackLocked && packInfo.unlockType == PackUnlockType.IAP);

			if (isPackLocked)
			{
				switch (packInfo.unlockType)
				{
					case PackUnlockType.Coins:
						coinsUnlockAmountText.text = packInfo.unlockAmount.ToString();
						break;
					case PackUnlockType.Stars:
						starsUnlockAmountText.text = packInfo.unlockAmount.ToString();
						break;
					case PackUnlockType.IAP:
						SetIAPText(packInfo.unlockIAPProductId);
						break;
				}
			}
			else
			{
				int numLevelsInPack		= packInfo.levelFiles.Count;
				int numCompletedLevels	= GameManager.Instance.GetNumCompletedLevels(packInfo);

				progressBarContainer.SetProgress((float)numCompletedLevels / (float)numLevelsInPack);
				progressText.text = string.Format("{0} / {1}", numCompletedLevels, numLevelsInPack);
			}
		}

		#endregion

		#region Private Methods

		private void SetIAPText(string productId)
		{
			string text = "";

			#if BBG_IAP
			UnityEngine.Purchasing.Product product = IAPManager.Instance.GetProductInformation(productId);

			if (product == null)
			{
				text = "NULL";
			}
			else if (!product.availableToPurchase)
			{
				text = "N/A";
			}
			else
			{
				text = product.metadata.localizedPriceString;
			}
			#else
			text = "IAP not enabled";
			#endif

			iapPriceText.text = text;
		}

		#endregion
	}
}
