﻿using Assets._scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BizzyBeeGames.TangramPuzzles
{
	public class PurchasePackPopup : Popup
	{
		#region Inspector Variables
		
		[Space]

		[SerializeField] private Text messageText	= null;
		[SerializeField] private Text amountText	= null;
		
		#endregion // Inspector Variables

		#region Member Variables
		
		private PackInfo packInfo;
		
		#endregion // Member Variables

		#region Public Methods
		
		public override void OnShowing(object[] inData)
		{
			base.OnShowing(inData);


			BundleInfo bundleInfo = inData[0] as BundleInfo;

			packInfo = inData[1] as PackInfo;

			messageText.text	= string.Format(LocalizationManager.Instance.GetText("unlock_pack_description"), bundleInfo.bundleName, packInfo.packName);
			amountText.text		= "x " + packInfo.unlockAmount;
		}

		public void Unlock()
		{
			Hide(false, new object[] { packInfo });
		}
		
		#endregion // Public Methods
	}
}
