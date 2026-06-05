using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class MapSo : ScriptableObject
{
    //该地图星星的总数量，-1表示地图并未解锁
    public int starNumberOfMap=-1;
    //该地图下的所有关卡存放在里面，默认值为-1，表示关卡未解锁
    public int[] starNumberOfLevel={-1};
}
