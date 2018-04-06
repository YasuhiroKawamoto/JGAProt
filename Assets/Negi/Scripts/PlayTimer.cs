using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Play.Timer
{
    // PlayTimer クラス
    public class PlayTimer : MonoBehaviour
    {
        // 初期計測時間
        [SerializeField]
        private float _countTime = 60.0f;

        [SerializeField,Range(0,5)]
        private int _floatPlaces = 2;

        // 計測時間
        public float TimeCount { get; private set; }

        // 計測中フラグ
        [SerializeField,ReadOnly]
        private bool _isCounting = false;
        public bool IsCounting
        {
            get { return _isCounting; }
            private set { _isCounting = value; }
        }

        // 値を表示するUI
        [SerializeField]
        private Text _timerText = null;

        public void Start()
        {
            StartTimer(_countTime);
        }

        /// <summary>
        /// 計測開始
        /// </summary>
        /// <param name="time"></param>
        public void StartTimer(float time)
        {
            TimeCount = time;
    
            // 計測開始
            StartCoroutine(StartTimer());
        }

        /// <summary>
        /// 時間の計測
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartTimer()
        {
            // 計測開始
            IsCounting = true;

            // 0いかになるまで計測
            while (0 < TimeCount)
            {
                // 1秒に1ずつ減らしていく
                TimeCount -= Time.deltaTime;

                // 表示更新
                TextUpdate();

                yield return null;
            }

            // 小数点はなし
            TimeCount = 0;

            // 表示更新
            TextUpdate();

            // 計測終了
            IsCounting = false;

        }

        /// <summary>
        /// 指定しているUIの更新
        /// </summary>
        private void TextUpdate()
        {
            if (_timerText == null)
            {
                // テキストがなければ更新しない
                return;
            }

            // 更新
            _timerText.text = TimeCount.ToString("f" + _floatPlaces.ToString());
        }

    }
}