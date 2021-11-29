using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private Transform levelPart_Finish;
    [SerializeField] private List<Transform>  levelPartList;
    [SerializeField] private Transform player;

    private Vector3 lastEndPosition;
    private bool finish = false;
    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("levelEnd").position;
        
        int startingLevelSpawn = 5;
        for (int i = 0; i < startingLevelSpawn; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        if(Vector3.Distance(player.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART && lastEndPosition.y < 50 && !finish)
        {
            //Spawn level part everytime the player is in range of the LAST level spawn
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform chooseLevel = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chooseLevel, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("levelEnd").position;

        if (lastEndPosition.y >= 50)
        {
            SpawnLevelPart(levelPart_Finish, lastEndPosition);
            finish = true;
        }
    }
    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
