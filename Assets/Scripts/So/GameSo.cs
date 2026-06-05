using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class GameSo : ScriptableObject
{
    public MapSo[] mapArry;
    public int selectedMapID = -1;
    public int selectedLevelID = -1;
    public void UpdateLevel(int starNumber)
    {
        if (starNumber <= 0) return;
        //判断新的星星数量是否大于之前的存储
        int mapIndex=selectedMapID-1;
        int LevelIndex=selectedLevelID-1;
        if (mapIndex < 0 || mapIndex >= mapArry.Length)
        {
            return;
        }
        MapSo currentMap=mapArry[mapIndex];
        if (LevelIndex < 0 || LevelIndex >= currentMap.starNumberOfLevel.Length)
        {
            return;
        }
        //当星星的数量小于关卡星星数量的时候不更新
        if (starNumber <= currentMap.starNumberOfLevel[LevelIndex])
        {
            return;
        }
        //当星星的数量大于关卡星星数量更新
        currentMap.starNumberOfLevel[LevelIndex]=starNumber;
        int sum=0;
        foreach(int num in currentMap.starNumberOfLevel)
        {
            if (num > 0)
            {
                sum+=num;
            }
        }
        currentMap.starNumberOfMap=sum;
        int nextLevelIndex=LevelIndex+1;
        if (nextLevelIndex < currentMap.starNumberOfLevel.Length)
        {
            if (currentMap.starNumberOfLevel[nextLevelIndex] == -1)
            {
                currentMap.starNumberOfLevel[nextLevelIndex] = 0;
            }
        }
        else
        {
            int nextMapIndex=mapIndex+1;
            if (nextMapIndex < mapArry.Length)
            {
                if (mapArry[nextMapIndex].starNumberOfMap == -1)
                {
                    mapArry[nextMapIndex].starNumberOfMap = 0;
                    mapArry[nextMapIndex].starNumberOfLevel[0]=0;
                }
            }
        }
    }
}
