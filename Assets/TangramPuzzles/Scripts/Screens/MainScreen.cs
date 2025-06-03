using Assets._scripts.UI;
using MirraGames.SDK;
using MirraGames.SDK.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
			MirraSDK.Achievements.GetLeaderboard("score",OnScoreTableResolve);
			//MirraSDK.Socials.GetScoreTable("score", 3, true, 3, OnScoreTableResolve, OnScoreTableError);
		}

		#endregion

		public void OnScoreTableResolve(Leaderboard table)
		{
			var player=table.players.Where(player=>player.displayName == MirraSDK.Player.DisplayName).ToList();
			Debug.Log("Совпадений " + player.Count);
			List<PlayerScore> players= new List<PlayerScore>();
			if (player.Count > 0)
			{
				if (player[0].position > 7)
				{
					players = table.players.Where(player => player.position < 7).ToList();
					players.Add(player[0]);
                    Debug.Log("players1 " + players.Count);
                }
				else
				{
					players = table.players.Where(player => player.position <= 7).ToList();
					Debug.Log("players2 " + players.Count);
				}
			}

            /*player = new List<PlayerScore>();
			players = new List<PlayerScore>();
			var playerScore = new PlayerScore();
			playerScore.position = 1;
			playerScore.displayName = "alex k";
			player.Add(playerScore);*/
            /*var playerScore2 = new PlayerScore();
            playerScore2.position = 1;
            players.Add(playerScore2);
            var playerScore3 = new PlayerScore();
            playerScore3.position = 2;
            players.Add(playerScore3);
            var playerScore4 = new PlayerScore();
            playerScore4.position = 3;
            players.Add(playerScore4);
            var playerScore5 = new PlayerScore();
            playerScore5.position = 4;
            players.Add(playerScore5);
            var playerScore6 = new PlayerScore();
            playerScore6.position = 5;
            players.Add(playerScore6);
            var playerScore7 = new PlayerScore();
            playerScore7.position = 6;
            players.Add(playerScore7);
			players.Add(playerScore);*/

            if (players.Count == 0)
			{
				_leaderboardName.gameObject.SetActive(false);
			}
			for (int i = 0; i < players.Count; i++)
			{
				Player playerGO=Instantiate(playerPrefab, _scrollView);
				if (player[0].position == i)
				{
                    playerGO.SetStatistic(players[i].position, players[i].displayName, players[i].score, players[i].profilePictureUrl,true);
                }
				else
				{
					if (player[0].position > 7 && i == 6)
					{
						playerGO.SetStatistic(players[i].position, players[i].displayName, players[i].score, players[i].profilePictureUrl, true);
					}
					else
					{
						playerGO.SetStatistic(players[i].position, players[i].displayName, players[i].score, players[i].profilePictureUrl, false);
					}
                }
               
			}
		}

		public void OnScoreTableError()
		{
            _leaderboardName.gameObject.SetActive(false);
        }
	}
}
