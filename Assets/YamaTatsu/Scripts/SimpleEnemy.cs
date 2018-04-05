﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//一般的な行動をする敵
//

namespace Play.Enemy
{
    public class SimpleEnemy : EnemyBase
    {

        [SerializeField]
        private int _hp=10;

        // Use this for initialization
        void Start()
        {

            //エレメントを探す
            base.SearchElement();
            _HP = _hp;
        }

        // Update is called once per frame
        void Update()
        {

            //範囲外なら移動
            if (InRange() == true)
            {
                base.Move();
            }
            //範囲内なら攻撃
            else
            {
                Attack();
            }

            //HPが0になった場合破壊する
            if (_HP <= 0)
            {
                Destroy(this);
            }
        }

        //攻撃 
        private void Attack()
        {
            _timeCnt += Time.deltaTime;

            if (_attackInterval < _timeCnt)
            {
                //
                Debug.Log("攻撃");
                //エレメントの攻撃関数を受け取る
                nearestElement.GetComponent<Element.Element>().ReceiveDamage(_damage);
                _timeCnt = 0.0f;
            }
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
        public override void Damage(int damage)
        {
            base.Damage(damage);
        }

    }
}