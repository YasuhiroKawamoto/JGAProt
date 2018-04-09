using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//
//一般的な行動をする敵
//

namespace Play.Enemy
{
    public class SimpleEnemy : EnemyBase
    {

        [SerializeField]
        private int _hp=10;

        //攻撃エフェクト
        [SerializeField]
        private GameObject _attackEffect;
        //撃破された時のエフェクト
        [SerializeField]
        private GameObject _dieEffect;
        //移動するまでの時間
        [SerializeField]
        private float _moveTimeMax = 10.0f;
        [SerializeField]
        private float _moveTimeMin = 5.0f;

        // 移動tween
        Tweener _moveTween = null;

        // Use this for initialization
        void Start()
        {

            //エレメントを探す
            base.SearchElement();
            _HP = _hp;
            Move();
        }

        // Update is called once per frame
        void Update()
        {

           
            //範囲内なら攻撃
            if (InRange() == false)
            {
                if (_moveTween!=null)
                {
                    _moveTween.Kill();
                    _moveTween = null;
                }
                Attack();
            }

            
        }

        //攻撃 
        private void Attack()
        {
            _timeCnt += Time.deltaTime;

            if (_attackInterval < _timeCnt)
            {

                //Debug.Log("攻撃");
                //攻撃エフェクトの生成
                if (_attackEffect != null)
                {
                    base.SpawnEffect(_attackEffect, _ElementPos);
                }
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

        protected override void Move()
        {
            float moveTime = Random.Range(_moveTimeMin, _moveTimeMax);

            _moveTween = transform.DOMove(_ElementPos, moveTime);
        }

        //ダメージをセット
        public override void Damage(int damage)
        {
            base.Damage(damage);
            //HPが0になった場合破壊する
            if (_HP <= 0)
            {
                //死ぬエフェクト
                if (_dieEffect != null)
                {
                    base.SpawnEffect(_dieEffect, transform.position);
                }
                Destroy(this.gameObject);
            }
        }

    }
}