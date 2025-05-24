using Assets._scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace BizzyBeeGames.TangramPuzzles
{
	public class LevelListScreen : Screen
	{
		#region Inspector Variables

		[Space]

		[SerializeField] private LevelListItem	levelListItemPrefab	= null;
		[SerializeField] private RectTransform	levelListContainer	= null;
		[SerializeField] private ScrollRect		levelListScrollRect	= null;
		[SerializeField] private Text			nameText		= null;

		#endregion

		#region Member Variables

		private PackInfo							currentPackInfo;
		private RecyclableListHandler<LevelData>	levelListHandler;

		#endregion

		#region Public Methods

		public override void Initialize()
		{
			base.Initialize();

			GameEventManager.Instance.RegisterEventHandler(GameEventManager.PackSelectedEventId, OnPackSelected);
		}

		public override void Show(bool back, bool immediate)
		{
			base.Show(back, immediate);

			levelListHandler.Refresh();
		}

		#endregion

		#region Private Methods

		private void OnPackSelected(string eventId, object[] data)
		{
			PackInfo selectedPackInfo = data[0] as PackInfo;

			if (currentPackInfo != selectedPackInfo)
			{
				UpdateList(selectedPackInfo);
			}
		}

		public void UpdateList()
		{
			UpdateList(currentPackInfo);
		}
		private void UpdateList(PackInfo packInfo)
		{
			currentPackInfo = packInfo;
            switch (packInfo.packName)
            {
                case "BEGINNER":
                    nameText.text = LocalizationManager.Instance.GetText("BEGINNER");
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

			if (levelListHandler == null)
			{
				levelListHandler					= new RecyclableListHandler<LevelData>(packInfo.LevelDatas, levelListItemPrefab, levelListContainer, levelListScrollRect);
				levelListHandler.OnListItemClicked	= OnLevelListItemClicked;
				levelListHandler.Setup();
			}
			else
			{
				levelListHandler.UpdateDataObjects(packInfo.LevelDatas);
			}
		}

		private void OnLevelListItemClicked(LevelData levelData)
		{
			if (!GameManager.Instance.IsLevelLocked(levelData))
			{
				GameManager.Instance.StartLevel(currentPackInfo, levelData);
			}
		}

		#endregion
	}
}
