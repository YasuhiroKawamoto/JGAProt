using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    public class InGameManager : MonoBehaviour {

        // ゲームシーンの状態
        enum State
        {
            None,
            Play,
            Pause,
            Over,
            Clear
        }

        // タイマー
        [SerializeField]
        private Timer.PlayTimer _timer;

        // 現在の状態
        [SerializeField,ReadOnly]
        private State _gameState = State.None;

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update()
        {
            // ゲームオーバーの確認
            CheckGameOver();
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
            }
        }
    }
}