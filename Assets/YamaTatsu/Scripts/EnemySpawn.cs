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
        public float intervalTime;

        //xの最大数
        [SerializeField]
        int maxNumX = 10;
        //xの最小数
        [SerializeField]
        int minNumX = -10;
        //yの最大数
        [SerializeField]
        int maxNumY = 4;
        //yの最小数
        [SerializeField]
        int minNumY = -4;

        // エネミーの出現ルート
        private GameObject _root = null;

        private void Start()
        {
            _root = new GameObject("EnemyRoot");
            _root.transform.parent = PlayManager.Instance.ObjectRoot.gameObject.transform;

            // 作成コルーチン
            StartCoroutine(SpawnEnemy());
        }

        //敵の生成関数
        private IEnumerator SpawnEnemy()
        {
            while (this.gameObject)
            {
                // 5秒ごと
                yield return new WaitForSeconds(intervalTime);

                var gameManager = PlayManager.Instance.GameManager;
                if ( gameManager.GameState != InGameManager.State.Play)
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
                    enemy.transform.parent = _root.transform;
                    gameManager.SetResetObject(enemy);
                    //位置をセット
                    enemy.transform.position = new Vector3(randomValueX, randomValueY, 0);
                }
            }
        }
    }
}
