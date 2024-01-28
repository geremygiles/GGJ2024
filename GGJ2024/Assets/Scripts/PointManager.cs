using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PointManager 
{
    // Vars for keeping track of when/if a player wins and for restarting rounds
    private const int BaseStartingPoints = 1000;
    private const int MaximumPossiblePoints = BaseStartingPoints * 2;

    // Vars for keeping track of how many points a player has total over all rounds
    private static float player1TotalPoints = BaseStartingPoints;
    private static float player2TotalPoints = BaseStartingPoints;

    // Vars for calculating how many points a player earns per round
    private static int currentPlayerNumber;
    
    // round vars
    private const float BasePointsPerRound = 100f;
    private static int roundNumber = 0;
    private const float BaseRoundMultiplier = 1f;
    private static float additionalRoundMultiplier = 0.5f;
    private static float thisRoundsBasePoints;

    // Vars for calculating how many points earned per turn
    private static float pointsGainedThisTurn;
    private const float DefaultPointsGainedDifficultyMultiplier = 1f;
    private const float MeduimDifficultyMultiplier = 1.2f;
    private const float HardDifficultyMultiplier = 1.4f;
    private static float pointsGainedDifficultyMultiplier;


    // tile vars
    private static float pointsPerTileHit;
    private static int totalTiles;
    private static int tilesHit;
    private static int minTiles;
    private static bool minTilesAcheived;
    private static float minTilesAcheivedReward;
    private const float MinTilePercentage = .5f;

    // rebuttle vars
    private static int DefaultRebuttleChoice = 0;
    // 0 = nuetral, 1 = cheer, 2 = heckel
    private static int currentRebuttleChoice;
    private const float RebuttelBonus = .2f;

    #region Getters

    public static float Player1TotalPoints {  get { return player1TotalPoints; } }

    public static float Player2TotalPoints { get { return player2TotalPoints; } }

    public static int RebuttleChoice
    {
        get { return currentRebuttleChoice; }
    }

    #endregion

    #region Setters

    public static void SetRebuttleChoice(int rebuttleChoice)
    {
        if (rebuttleChoice >= 0 && rebuttleChoice <= 2)
        {
            currentRebuttleChoice = rebuttleChoice;
        }
        else
        {
            currentRebuttleChoice = DefaultRebuttleChoice;
        }
    }

    #endregion


    #region Methods Running Match to Match

    public static void Restart()
    {
        roundNumber = 0;
        player1TotalPoints = BaseStartingPoints;
        player2TotalPoints = BaseStartingPoints;
    }

    #endregion 

    #region Methods Running Round to Round

    public static void RoundStart()
    {
        roundNumber++;
    }

    public static void RoundEnded()
    {
        if (minTilesAcheived)
        {
            CalculateFinalTotal();
        }
        else
        {
            AwardPointsToOpponent();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="totalNumberTiles"></param>
    /// <param name="currentPlayer">1 = player1, 2 = player2</param>
    /// <param name="jokeDifficulty">0 = easy, 1 = medium, 2 = hard</param>
    public static void StartTurn(int totalNumberTiles, int currentPlayer, int jokeDifficulty)
    {
        switch (jokeDifficulty)
        {
            case 0:
                pointsGainedDifficultyMultiplier = DefaultPointsGainedDifficultyMultiplier;
                break;

            case 1:
                pointsGainedDifficultyMultiplier = MeduimDifficultyMultiplier;
                break;

            case 2:
                pointsGainedDifficultyMultiplier = HardDifficultyMultiplier;
                break;

            default: break;
        }

        thisRoundsBasePoints = BasePointsPerRound * (BaseRoundMultiplier + additionalRoundMultiplier * (roundNumber - 1)) * pointsGainedDifficultyMultiplier;
        minTilesAcheived = false;
        totalTiles = totalNumberTiles;
        minTiles = (int)(totalNumberTiles * MinTilePercentage);
        minTilesAcheivedReward = thisRoundsBasePoints * 0.5f;
        pointsPerTileHit = thisRoundsBasePoints / (totalTiles - minTiles);
        pointsGainedThisTurn = 0;
        currentPlayerNumber = currentPlayer;

        if (player1TotalPoints >= MaximumPossiblePoints)
        {
            Singleton.Instance.GameManager.PlayerWins(1);
        }
        else if (player2TotalPoints >= MaximumPossiblePoints)
        {
            Singleton.Instance.GameManager.PlayerWins(2);
        }
    }

    public static void TileHit()
    {
        tilesHit++;
        
        if (tilesHit == minTiles)
        {
            RewardMinTilesReward();
            minTilesAcheived = true;
        }
        else if (tilesHit > minTiles)
        {
            pointsGainedThisTurn += pointsPerTileHit;
            AddPlayerPoints(currentPlayerNumber, pointsPerTileHit);
        }
    }

    private static void RewardMinTilesReward()
    {
        AddPlayerPoints(currentPlayerNumber, minTilesAcheivedReward);
        pointsGainedThisTurn += minTilesAcheivedReward;
    }

    private static void AddPlayerPoints(int playerGettingPoints, float pointsToAdd)
    {
        if (playerGettingPoints == 1)
        {
            player1TotalPoints += pointsToAdd;
            player2TotalPoints -= pointsToAdd;
        }
        else
        {
            player1TotalPoints -= pointsToAdd;
            player2TotalPoints += pointsToAdd;
        }
    }

    private static void CalculateFinalTotal()
    {
        switch (currentRebuttleChoice)
        {
            case 1:
                if (PlayerDidGood())
                {
                    AddPlayerPoints(GetOtherPlayer(),
                        pointsGainedThisTurn * RebuttelBonus);
                }
                else
                {
                    AddPlayerPoints(currentPlayerNumber,
                        pointsGainedThisTurn * RebuttelBonus);
                }
                break;

            case 2:
                if (PlayerDidGood())
                {
                    AddPlayerPoints(currentPlayerNumber,
                        pointsGainedThisTurn * RebuttelBonus);
                }
                else
                {
                    AddPlayerPoints(GetOtherPlayer(),
                        pointsGainedThisTurn * RebuttelBonus);
                }
                break;

            case 0:
            default:
                break;
        }
    }

    private static void AwardPointsToOpponent()
    {
        if (currentPlayerNumber == 1)
        {
            AddPlayerPoints(2, thisRoundsBasePoints);
        }
        else
        {
            AddPlayerPoints(1, thisRoundsBasePoints);
        }
    }

    private static bool PlayerDidGood()
    {
        if ((tilesHit - minTiles) / (totalTiles - minTiles) >= 0.7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static int GetOtherPlayer()
    {
        if (currentPlayerNumber == 1)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    #endregion
}
