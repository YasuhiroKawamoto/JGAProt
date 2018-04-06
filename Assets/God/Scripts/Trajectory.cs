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

        // タップ開始エレメント
        [SerializeField]
        private Element.Element _element;

        // 攻撃
        [SerializeField]
        private Attack _attack;

        // 攻撃の発生時差
        [SerializeField]
        private float _attackDiff;

        // マウス座標を格納
        private Vector2 _mousePos;

        // 仮軌跡となるオブジェクトのリスト
        private List<GameObject> _trajectory;

        // 攻撃軌跡のオブジェクトリスト
        // private List<Attack> _effects;

        // 一番最後に生成されたオブジェクト
        GameObject _lastObj;


        // Use this for initialization
        void Start()
        {
            _trajectory = new List<GameObject>();
            //_effects = new List<Attack>();
        }

        // Update is called once per frame
        void Update()
        {
            // 仮処理====================================ここから
            if (Input.GetMouseButtonDown(0))
            {
                StartTrajectory(_element.gameObject);
            }

            if (Input.GetMouseButtonUp(0))
            {
                EndTrajectory();
            }

            // 仮処理====================================ここまで



            // 軌跡継続条件を満たしていれば軌跡を辿る
            if (IsContinueTranjectory())
            {
                Trajectry();
            }

        }


        /// <summary>
        /// 軌跡の作成を実行する関数
        /// </summary>
        void Trajectry()
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
                _trajectory.Add(_lastObj);
            }
        }

        /// <summary>
        /// 軌跡継続判定
        /// </summary>
        /// <returns></returns>
        bool IsContinueTranjectory()
        {
            // 最後の軌跡オブジェクトがnullでなければ継続
            if (_lastObj != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  時差で攻撃エフェクトを発生させる
        /// </summary>
        /// <param name="part"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private IEnumerator CreateAttack(GameObject part, float seconds, int index)
        {
            yield return new WaitForSeconds(seconds);

            _attack.transform.position = part.transform.position;
            Attack atkObj = Instantiate(_attack);
            atkObj.Parent = _element;
            atkObj.Index = index;
            //_effects.Add( atkObj);

            // 仮軌跡のオブジェクトを削除
            Destroy(part);
        }

        /// <summary>
        /// 発生元エレメントを指定する関数
        /// </summary>
        /// <param name="element"></param>
        public void StartTrajectory(GameObject element)
        {
            _lastObj = element;
        }

        public int EndTrajectory()
        {
            // 仮軌跡の発生数(距離)を格納
            int distance = _trajectory.Count;

            // エフェクトを発生
            float timeDiff = 0;
            int i = 1;
            foreach (GameObject part in _trajectory)
            { 
                StartCoroutine(CreateAttack(part, timeDiff, i));
                timeDiff += _attackDiff;
                i++;

            }

            // 仮軌跡を削除
            _trajectory.Clear();

            // 軌跡の中断
            _lastObj = null;

            // 距離を返す
            return distance;
        }
    }
}