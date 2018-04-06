using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Play.Enemy
{
    public class EnemyStatus : MonoBehaviour
    {

        //現在のHP
        [SerializeField]
        private int m_HP;
        //HPの最大数
        private int m_maxHP;
        //エレメント
        private GameObject nearestElement;
        //最小範囲
        float minDis = 100.0f;
        //範囲
        [SerializeField]
        private float m_range = 1.0f;
        //
        private float RANGE = 100.0f;

        //スピード
        private Vector3 m_speed;
        //エレメントの座標
        private Vector3 m_ElementPos;

        // Use this for initialization
        void Start()
        {

            //エレメントを探し挿入するリスト
            GameObject[] Elements = GameObject.FindGameObjectsWithTag("Element");
            //
            foreach (GameObject element in Elements)
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
                    m_ElementPos = element.transform.position;

                    m_speed.x = (element.transform.position.x - transform.position.x) * 0.01f;
                    m_speed.y = (element.transform.position.y - transform.position.y) * 0.01f;
                }
            }
            //HPを最大に入れておく
            m_maxHP = m_HP;
        }

        // Update is called once per frame
        void Update()
        {

            //エレメントに近づいてく処理
            if (InRange() == true)
            {
                Move();
            }
            //ヒット
            //OnCollisionStay2D();

            //HPが0になったとき消滅
            if (m_HP < 0)
            {
                Destroy(this);
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

        //移動処理
        private void Move()
        {
            this.transform.position += m_speed;
            Debug.Log(m_range);
        }

        //範囲内か範囲内じゃないか
        private bool InRange()
        {
            //距離を求める
            var disx = m_ElementPos.x - transform.position.x;
            var disy = m_ElementPos.y - transform.position.y;
            float distance = disx * disx + disy * disy;
            //範囲内ならfalseを返す
            if (distance <= m_range || distance <= -m_range)
            {
                return false;
            }

            return true;
        }

    }
}
