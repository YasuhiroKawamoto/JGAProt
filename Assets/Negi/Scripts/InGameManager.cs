using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Play
{
    public class InGameManager : MonoBehaviour {

        // ゲームシーンの状態
        enum State
        {
            None,
            Play,
            Pause,
            Count,
            Over,
            Clear
        }

        // プレイ時間
        [SerializeField]
        private float _playTime = 10.0f;

        // タイマー
        [SerializeField]
        private Timer.PlayTimer _timer;

        // スタートボタン
        [SerializeField]
        private Button _startButton;

        // ゲームオーバーテキスト
        [SerializeField]
        private Text _overtext;

        // 現在の状態
        [SerializeField,ReadOnly]
        private State _gameState = State.None;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Start()
        {
            // 表示、非表示
            _overtext.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(true);

            // イベント設定
            _startButton.onClick.AddListener(() => {
                GameStart();
            });
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update()
        {
            // プレイ中
            if (_gameState == State.Play)
            {
                // ゲームクリア確認
                CheckGameClear();

                // ゲームオーバーの確認
                CheckGameOver();
            }
        }

        /// <summary>
        /// ゲームの開始
        /// </summary>
        private void GameStart()
        {
            _gameState = State.Play;
            _timer.StartTimer(_playTime);

            // 非表示
            _overtext.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(false);
        }

        /// <summary>
        /// ゲームクリアの確認
        /// </summary>
        private void CheckGameClear()
        {
            // TODO: ここにクリア確認
        }

        /// <summary>
        /// ゲームオーバーの確認
        /// </summary>
        private void CheckGameOver()
        {
            // 時間切れの場合ゲームオーバー
            if (_timer.IsCounting == false)
            {
                _gameState = State.Over;
                // 表示、非表示
                _overtext.gameObject.SetActive(true);
                _startButton.gameObject.SetActive(true);
            }
        }
    }
}