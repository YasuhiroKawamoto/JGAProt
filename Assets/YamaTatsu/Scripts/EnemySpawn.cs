using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

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

    // Use this for initialization
    void Start()
    {

        timeOut = 0;

    }

    // Update is called once per frame
    void Update()
    {

        timeOut += Time.deltaTime;

        if (setTime <= timeOut)
        {
            SpawnEnemy();
            //カウントを0にする
            timeOut = 0.0f;
        }
    }

    //敵の生成関数
    void SpawnEnemy()
    {
        for (int i = 0; i < ENEMY_NUM; i++)
        {
            //　出現させる位置をランダムに選ぶ
            float randomValueX = Random.Range(minNumX, maxNumX);
            float randomValueY = Random.Range(minNumY, maxNumY);
            //敵の生成
            GameObject enemy = Instantiate(enemies) as GameObject;
            //位置をセット
            enemy.transform.position = new Vector3(randomValueX, randomValueY, 0);
        }
    }
}
