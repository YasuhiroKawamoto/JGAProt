using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/// <summary>
/// 敵の基盤クラス
/// </summary>
namespace Play.Enemy
{
    public class EnemyBase : MonoBehaviour
    {

        //移動速度
        protected Vector3 _speed = new Vector3(1.0f, 1.0f, 0);
        //攻撃頻度
        protected float _attackInterval = 3.0f;
        //攻撃先エレメント
        protected GameObject nearestElement;
        //最小範囲
        float minDis = 100.0f;
        //範囲
        [SerializeField]
        protected float m_range = 1.0f;
        //
        protected float RANGE = 100.0f;
        //エレメントの座標
        protected Vector3 _ElementPos;
        //タイムカウント
        protected float _count = 0;
        //HP
        protected int _HP = 10;
        //与えるダメージ
        [SerializeField]
        protected int _damage = 10;

        //買ううんと
        protected float _timeCnt = 0.0f;

        //移動処理
        virtual protected void Move()
        {
            transform.DOMove(_ElementPos, 1.0f);
        }

        //近いエレメントを探す
        virtual protected void SearchElement()
        {
           
            GameObject[] Elements = GameObject.FindGameObjectsWithTag("Element");
            //近いエレメントを探す処理
            foreach (GameObject element in Elements)
            {
                //エレメントを探し挿入するリスト
                if (!element.GetComponent<Element.Element>().IsBase())
                {
                    //距離を求める
                    float dis = Vector3.Distance(transform.position, element.transform.position);
                    //範囲内かつ最小距離よりも近い場合
                    if (dis <= RANGE && dis < minDis)
                    {
                        //最小距離を代入
                        minDis = dis;
                        //最小距離のエレメントを代入
                        nearestElement = element;
                        //座標の値を入れる
                        _ElementPos = element.transform.position;

                        

                        //_speed.x = (element.transform.position.x - transform.position.x) * 0.01f;
                        //_speed.y = (element.transform.position.y - transform.position.y) * 0.01f;
                    }
                }
            }
        }

        //ランダムなエレメントを探す
        virtual protected void SearchRandomElement()
        {

            List<GameObject> Elements = new List<GameObject>(GameObject.FindGameObjectsWithTag("Element"));

            Element.Element element = null;

            do{
                var random = Random.Range(0, Elements.Count);
                element = Elements[random].GetComponent<Element.Element>();
            } while (element.IsBase());

            //座標の値を入れる
            _ElementPos = element.transform.position;
        }

        //エフェクトの生成
        virtual protected void SpawnEffect(GameObject Effect,Vector3 pos)
        {
           
            //エフェクトの生成
            GameObject effect = Instantiate(Effect);
            effect.transform.position = new Vector3(pos.x,pos.y,pos.z);
        }
       

        //ダメージを受ける
        virtual public void Damage(int damage)
        {
            _HP -= damage;
        }

        //撃破された情報を入れる
        virtual protected void KillData()
        {
            EnemyManager.Instance.SetKills();
        }

    }
}
