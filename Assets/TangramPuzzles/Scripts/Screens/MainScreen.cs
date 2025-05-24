using Assets._scripts.UI;
using MirraGames.SDK;
using MirraGames.SDK.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace BizzyBeeGames.TangramPuzzles
{
	public class MainScreen : Screen
	{
		[SerializeField] private Player playerPrefab;
		[SerializeField] private Transform _scrollView;
		[SerializeField] private Text _leaderboardName;

		#region Inspector Variables

		#endregion

		#region Unity Methods

		protected override void Start()
		{
			base.Start();
			MirraSDK.Socials.InvokeLeaderboard("score");
			MirraSDK.Socials.GetScoreTable("score", 3, true, 3, OnScoreTableResolve, OnScoreTableError);
		}

		#endregion

		public void OnScoreTableResolve(IScoreTable table)
		{
			if (table.Count==0)
			{
				_leaderboardName.gameObject.SetActive(false);
			}
			for (int i = 0; i < table.Count; i++)
			{
				Player player=Instantiate(playerPrefab, _scrollView);
				player.SetStatistic(table[i].Position, table[i].DisplayName, table[i].Score, table[i].PictureURL);
			}
		}

		public void OnScoreTableError()
		{
            _leaderboardName.gameObject.SetActive(false);
        }
	}
}
