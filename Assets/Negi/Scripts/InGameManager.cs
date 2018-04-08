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

        // エネルギーの管理クラス
        [SerializeField]
        private Element.EnergyManager _energyManager = null;

        // エネルギーのプレハブ
        [SerializeField]
        private GameObject _energyPrefabs = null;

        // 軌跡のプレハブ
        [SerializeField]
        private Trajectory.Trajectory _trajectory = null;

        // リセットするとき削除するオブジェクト
        private List<GameObject> _resetList = null;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Start()
        {
            // リストの作成
            _resetList = new List<GameObject>();

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

            // TODO: のちほど分ける ==================================================
            // リセット
            if (0 < _resetList.Count)
            {
                foreach (var obj in _resetList)
                {
                    GameObject.Destroy(obj);
                }
            }
            // エネルギー周りの初期化
            var energyObj = GameObject.Instantiate(_energyPrefabs);
            // Root 下に追加
            energyObj.transform.parent = PlayManager.Instance.ObjectRoot.transform;
            // リセットするオブジェクトに追加
            _resetList.Add(energyObj);
            // 軌跡の作成
            var traject = GameObject.Instantiate(_trajectory);
            // マネージャーに軌跡を設定
            _energyManager.SetTrajectoryManager(traject.gameObject);
            // Root 下に追加
            traject.transform.parent = PlayManager.Instance.ObjectRoot.transform;
            // リセットするオブジェクトに追加
            _resetList.Add(traject.gameObject);
            // エレメントリスト更新
            _energyManager.GetElementListOnScene();
            // ========================================================================
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
                _energyManager.PauseElements();

                _gameState = State.Over;
                // 表示、非表示
                _overtext.gameObject.SetActive(true);
                _startButton.gameObject.SetActive(true);
            }
        }
    }
}