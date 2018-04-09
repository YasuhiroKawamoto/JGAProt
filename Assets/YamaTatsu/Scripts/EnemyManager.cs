using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    //撃破数
    private int _enemyCnt = 0;
	
    //撃破数の値を返す
    public int GetKills()
    {
        return _enemyCnt;
    }

    //撃破数を入れる
    public void SetKills()
    {
        _enemyCnt += 1;
    }

    public void ResetKills()
    {
        _enemyCnt = 0;
    }

}
