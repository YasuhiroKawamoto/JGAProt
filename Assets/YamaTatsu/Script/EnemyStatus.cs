using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {

    //現在のHP
    [SerializeField]
    private int m_HP;
    //HPの最大数
    private int m_maxHP;
    //エレメント
    GameObject nearestEnemy = null;
    //最小範囲
    float minDis = 100.0f;



	// Use this for initialization
	void Start () {

        //
        m_maxHP = m_HP;
		
	}
	
	// Update is called once per frame
	void Update () {

        //ヒット
        //OnCollisionStay2D();

        //HPが0になったとき消滅
        if (m_HP < 0)
        {
            Destroy(this);
            //エフェクトの生成

        }

	}

    //現在のHPを返す
    int getHP()
    {
        return m_HP;
    }

    //攻撃にあたり続けてるとき
    void OnCollisionStay2D()
    {
        //タグ、レイヤーが一致しているか調べる


        //ダメージを与える
        m_HP -= 1;
    }

}
