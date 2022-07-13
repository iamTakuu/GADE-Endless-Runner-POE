using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
   private int lbID = 4606;
   [SerializeField] private TextMeshProUGUI playerNamesText;
   [SerializeField] private TextMeshProUGUI playerScoreText;


   public IEnumerator SubmitScoreRoutine(int score)
   {
      bool complete = false;
      string playerID = PlayerPrefs.GetString("PlayerID");
      LootLockerSDKManager.SubmitScore(playerID, score, lbID, (response) =>
      {
         if (response.success)
         {
            Debug.Log("Score uploaded!");
            complete = true;
         }

         else
         {
            Debug.Log("Failed" + response.Error);
            complete = true;
         }
      });
      yield return new WaitWhile((() => complete == false));
   }

   public IEnumerator GrabHighScores()
   {
      bool complete = false;
      LootLockerSDKManager.GetScoreList(lbID, 8, 0, (response) =>
      {
         if (response.success)
         {
            string playerNames = "Names\n";
            string playerScores = "Scores\n";

            LootLockerLeaderboardMember[] players = response.items;

            for (int i = 0; i < players.Length; i++)
            {
               playerNames += players[i].rank;
               if (players[i].player.name != "")
               {
                  playerNames += players[i].player.name;
               }
               else
               {
                  playerNames += players[i].player.id;
               }

               playerScores += players[i].score + "\n";
               playerNames += "\n";

            }

            complete = true;
            playerNamesText.text = playerNames;
            playerScoreText.text = playerScores;

         }else
         {
            Debug.Log("Failed "+response.Error);
            complete = true;
         }
      });
      yield return new WaitWhile(() => complete == false);
   }

   public void UpdateLeaderBoard()
   {
      StartCoroutine(GrabHighScores());
   }
}
