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

    //最大数
    [SerializeField]
    int maxNum;
    //最小数
    [SerializeField]
    int minNum;

    // Use this for initialization
    void Start()
    {

        timeOut = 0;

    }

    // Update is called once per frame
    void Update()
    {

        timeOut += Time.deltaTime;

        Debug.Log(timeOut);

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
            float randomValueX = Random.Range(minNum, maxNum);
            float randomValueY = Random.Range(minNum, maxNum);
            //敵の生成
            GameObject enemy = Instantiate(enemies) as GameObject;
            //位置をセット
            enemy.transform.position = new Vector3(randomValueX, randomValueY, 0);
        }
    }
}
