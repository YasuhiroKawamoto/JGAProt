﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Trajectory
{

    public class Trajectory : MonoBehaviour
    {
        // 軌跡として配置するオブジェクトのPrefab
        [SerializeField]
        private GameObject _plot;

        // 軌跡の間隔
        [SerializeField]
        private float _interval;

        // 初期位置(開始エレメント)
        [SerializeField]
        GameObject _startElement;

        // マウス座標を格納
        private Vector2 _mousePos;

        // 軌跡となるオブジェクトのリスト
        private List<GameObject> _trajectry;

        // 一番最後に生成されたオブジェクト
        GameObject _lastObj;


        // Use this for initialization
        void Start()
        {
            _trajectry = new List<GameObject>();
            // 仮処理
            _lastObj = _startElement;
        }

        // Update is called once per frame
        void Update()
        {
            // マウス座標を常に更新
            _mousePos = Input.mousePosition;

            // マウスのワールド座標を算出
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(_mousePos);

            // 前オブジェクトとの距離を算出
            Vector2 lastPos;
            lastPos.x = _lastObj.transform.position.x;
            lastPos.y = _lastObj.transform.position.y;

            // 前オブジェクトとの距離が指定間隔より大きければ新たな軌跡を生成
            if ((lastPos - worldPos).magnitude >= _interval)
            {
                _plot.transform.position = worldPos;

                _lastObj = Instantiate(_plot);
                _trajectry.Add(_lastObj);
            }


            // エレメントの上で離されたか
            if(true)
            {
                // エネルギーが足りているか
                if(true)
                {
                    // 攻撃エフェクトを発生
                }
            }
        }
    }
}