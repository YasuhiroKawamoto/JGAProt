using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Enemy
{
    public class EnemySpawn : MonoBehaviour
    {
        //敵の最大数
        [SerializeField]
        private int ENEMY_NUM;

        public GameObject enemies;

        //設定した時間
        public float setTime;
        //数えるカウント
        private float timeOut;

        //xの最大数
        [SerializeField]
        int maxNumX;
        //xの最小数
        [SerializeField]
        int minNumX;
        //yの最大数
        [SerializeField]
        int maxNumY;
        //yの最小数
        [SerializeField]
        int minNumY;

        private void Start()
        {
            // 作成コルーチン
            StartCoroutine(SpawnEnemy());
        }

        //敵の生成関数
        private IEnumerator SpawnEnemy()
        {
            while (this.gameObject)
            {
                // 5秒ごと
                yield return new WaitForSeconds(5.0f);

                var gameState = PlayManager.Instance.GameManager.GameState;
                if ( gameState != InGameManager.State.Play)
                {
                    continue;
                }

                for (int i = 0; i < ENEMY_NUM; i++)
                {
                    //　出現させる位置をランダムに選ぶ
                    float randomValueX = Random.Range(minNumX, maxNumX);
                    float randomValueY = Random.Range(minNumY, maxNumY);
                    //敵の生成
                    GameObject enemy = Instantiate(enemies) as GameObject;
                    enemy.transform.parent = Play.PlayManager.Instance.ObjectRoot.transform;
                    //位置をセット
                    enemy.transform.position = new Vector3(randomValueX, randomValueY, 0);
                }
            }
        }
    }
}
