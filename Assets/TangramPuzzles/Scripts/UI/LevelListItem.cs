﻿using Assets._scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace BizzyBeeGames.TangramPuzzles
{
	public class LevelListItem : RecyclableListItem<LevelData>
	{
		#region Inspector Variables
		[SerializeField] private Text	levelNumberText		= null;
		[SerializeField] private Image	playIcon			= null;
		[SerializeField] private Image	completeIcon		= null;
		[SerializeField] private Image	lockedIcon			= null;

		#endregion

		#region Public Methods

		public override void Initialize(LevelData levelData)
		{
		}

		public override void Setup(LevelData levelData)
		{
            levelNumberText.text =LocalizationManager.Instance.GetText("LEVEL") + (levelData.LevelIndex + 1).ToString();			

			if (GameManager.Instance.IsLevelCompleted(levelData))
			{
				SetCompleted();
			}
			else if (GameManager.Instance.IsLevelLocked(levelData))
			{
				SetLocked();
			}
			else
			{
				SetPlayable();
			}
		}

		public override void Removed()
		{
		}

		#endregion

		#region Private Methods

		private void SetCompleted()
		{
			playIcon.enabled		= false;
			completeIcon.enabled	= true;
			lockedIcon.enabled		= false;
		}

		private void SetLocked()
		{
			playIcon.enabled		= false;
			completeIcon.enabled	= false;
			lockedIcon.enabled		= true;
		}

		private void SetPlayable()
		{
			playIcon.enabled		= true;
			completeIcon.enabled	= false;
			lockedIcon.enabled		= false;
		}

		#endregion
	}
}
