using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour {

    // 寿命(外部コンポーネントから設定,取得可能) 
    // (寿命が0以下の場合は寿命無限とする)
    [SerializeField]
    private float _lifeTime;
    public float LifeTime
    {
        get { return _lifeTime; }
        set { _lifeTime = value; }
    }
    // 発生してからの経過時間
    private float _currntTime;

	// Use this for initialization
	void Start () {
        _currntTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        _currntTime += Time.deltaTime;

        // 経過時間が寿命を超過したらオブジェクトを破棄
        if(_lifeTime > 0 &&_currntTime >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
