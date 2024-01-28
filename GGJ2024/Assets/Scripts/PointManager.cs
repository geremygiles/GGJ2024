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
    private static int playerNumber;
    private const float BasePointsPerRound = 100f;
    private static int roundNumber = 1;
    private const float BaseRoundMultiplier = 1f;
    private static float additionalRoundMultiplier = 0.5f;
    private static float thisRoundsBasePoints;
    private static float pointsPerTileHit;
    private static int totalTiles;
    private static int tilesHit;
    private static int minTiles;
    private static bool minTilesAcheived;
    private static float minTilesAcheivedReward;
    private static bool minTilesRewardGiven;

    #region Getters

    public static float Player1TotalPoints {  get { return player1TotalPoints; } }

    public static float Player2TotalPoints { get { return player2TotalPoints; } }


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

    public static void RoundStart(int totalTilesInRound)
    {
        thisRoundsBasePoints = BasePointsPerRound * (BaseRoundMultiplier + additionalRoundMultiplier * (roundNumber - 1));
        minTilesAcheivedReward = thisRoundsBasePoints * 0.5f;
        totalTiles = totalTilesInRound;
        minTiles = (int)(totalTiles * 0.15);
    }


    public static void RoundEnded()
    {

    }

    public static void TileHit(int numTilesHit)
    {
        tilesHit += numTilesHit;
        
        if (tilesHit >= minTiles)
        {
            AddPlayerPoints(playerNumber, pointsPerTileHit);
        }
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

    }

    #endregion
}
