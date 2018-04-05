using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : EnemyBase {

	// Use this for initialization
	void Start () {
        //
        base.SearchElement();
	}
	
	// Update is called once per frame
	void Update () {
		
        //範囲外なら移動
        if(InRange() == true)
        {
            base.Move();
        }
        //範囲内なら攻撃
        else
        {
            
        }

        if(_HP <= 0)
        {
            Destroy(this);
        }

	}
    
    //攻撃 
    private void Attack()
    {
        //エレメントの攻撃関数を受け取る
    }

    //範囲内か範囲内じゃないか
    private bool InRange()
    {
        //距離を求める
        var disx = _ElementPos.x - transform.position.x;
        var disy = _ElementPos.y - transform.position.y;
        float distance = disx * disx + disy * disy;
        //範囲内ならfalseを返す
        if (distance <= m_range || distance <= -m_range)
        {
            return false;
        }

        return true;
    }

    //ダメージをセット
    protected override void SetDamage(int damage)
    {
        base.SetDamage(damage);
    }

}
