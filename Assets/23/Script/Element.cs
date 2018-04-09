using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エレメント
/// </summary>
/// 
namespace Play.Element
{
    public class Element : MonoBehaviour
    {

        [SerializeField]
        private bool _isBase;//ベースか否か

        [SerializeField]
        private bool _isRecovery;//回復中か

        [SerializeField]
        private double _recoveryTime;//回復時間

        [SerializeField]
        private double _energy;//保有エネルギー量

        [SerializeField]
        private double _capacity;//エネルギー上限

        [SerializeField]
        private int _durability;//耐久度


        private GameObject _energyManager;//エナジーマネージャー

        [SerializeField]
        private List<GameObject> _sendTargetList = new List<GameObject>();//送り先リスト

        private int _sendTargetCount;//送り先の数

        private GameObject _exclusionObj;//リスト排除対象オブジェクト

        [SerializeField]
        private State _state;//状態（0:非選択時  1:送り　　2:受け）


        [SerializeField]
        private Sprite _normalSprite;//通常時の画像

        [SerializeField]
        private Sprite _breakSprite1;//ダメージ深度1の画像

        [SerializeField]
        private Sprite _breakSprite2;//ダメージ深度2の画像

        [SerializeField]
        private Sprite _breakSprite3;//ダメージ深度3の画像

        [SerializeField]
        private Sprite _breakSprite4;//ダメージ深度3の画像

        [SerializeField]
        private Sprite _fullBreakSprite;//破壊状態の画像

        private bool _isPause;//ポーズ状態か

        [SerializeField]
        private GameObject[] _cilldrenList;//送り先リスト




        // Use this for initialization
        void Start()
        {
            _isPause = false;
            //イラストをnormalに指定（ベースは除外）
            if (!_isBase)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = _normalSprite;
            }
            //耐久度初期値設定
            _durability = 100;
            //最大容量を設定
            _capacity = 100;
            //回復所要時間を設定
            _recoveryTime = 10.00;
            //初期状態を「非設定」に指定
            _state = 0;
            //送り先リストのクリア
            _sendTargetList.Clear();
            //エナジーマネージャーのセット
            _energyManager = GameObject.Find("EnergyManager");
            //子オブジェクトのリスト
            _cilldrenList = gameObject.transform.GetAllChild();

        }

        // Update is called once per frame
        void Update()
        {
            if (!_isPause)
            {
                //エレメントの稼働
                OperationElement();
            }

        }

        /// <summary>
        /// エネルギー取得
        /// </summary>
        /// <returns></returns>
        public double GetEnergy()
        {
            return _energy;
        }

        /// <summary>
        /// 被ダメージ
        /// </summary>
        /// <param name="damage"></param>
        public void ReceiveDamage(int damage)
        {
            //エネルギーがある場合はエネルギーで受ける
            if (_energy > 0)
            {
                _energy -= damage;

                if (_energy < 0)
                {
                    _energy = 0.00;
                }

            }
            //エネルギーがなければ耐久度で受ける
            else
            {
                _durability -= damage;
                //残り耐久度に応じて破損状態の変更
                if (_durability < 0)
                {
                    //破壊状態にする
                    ChangeState(State.BREAK);
                    _capacity = 0.00;
                    //ヒビイメージの更新
                    if (!_isBase)
                    {
                        SoundManager.Instance.Play("GlassBreak");
                        gameObject.GetComponent<SpriteRenderer>().sprite = _fullBreakSprite;
                        _cilldrenList[0].GetComponent<SpriteRenderer>().sprite = _breakSprite4;
                    }
                }
                else if (_durability < 30)
                {
                    //最大容量の変更
                    _capacity = 30.00;
                    //ヒビイメージの更新
                    if (!_isBase)
                    {
                        SoundManager.Instance.Play("GlassBreak");
                        _cilldrenList[0].GetComponent<SpriteRenderer>().sprite = _breakSprite3;
                    }
                }
                else if (_durability < 50)
                {
                    //最大容量の変更
                    _capacity = 50.00;
                    //ヒビイメージの更新
                    if (!_isBase)
                    {
                        SoundManager.Instance.Play("GlassBreak");
                        _cilldrenList[0].GetComponent<SpriteRenderer>().sprite = _breakSprite2;
                    }
                }
                else if (_durability < 70)
                {
                    //最大容量の変更
                    _capacity = 70.00;
                    //ヒビイメージの更新
                    if (!_isBase)
                    {
                        SoundManager.Instance.Play("GlassBreak");
                        _cilldrenList[0].GetComponent<SpriteRenderer>().sprite = _breakSprite1;
                    }
                }
            }
        }


        /// <summary>
        /// エネルギー加算、減算
        /// </summary>
        /// <param name="chage"></param>
        public void ChageEnergy(double chage)
        {
            //エネルギーに加算
            _energy += chage;
            //エネルギーがマイナスになった際0にする
            if (_energy < 0)
            {
                _energy = 0;
            }
            //エネルギーが容量以上になった際
            if (_energy >= _capacity)
            {
                //エネルギーを最大値で固定
                _energy = _capacity;
                //「受け」状態を「満タン」に変更
                if (_state == State.RECIEVE)
                {
                    ChangeState(State.FULL);
                }
            }
        }

        //エレメントの状態取得
        public State GetState()
        {
            return _state;
        }

        //エレメントの容量取得関数
        public double GetCapacity()
        {
            return _capacity;
        }

        //エレメントの状態変更関数
        public void ChangeState(State state)
        {
            //ステータス変更
            _state = state;
                       
            switch (_state)
            {
                case State.WAIT://非指定
                    //送り先リストの削除
                    _sendTargetList.Clear();
                    //エネルギーの端数処理（暫定）
                    _energy = System.Math.Round(_energy, System.MidpointRounding.AwayFromZero);
                    break;
                case State.SEND://送り
                    break;

                case State.RECIEVE://受け
                    break;

                case State.BREAK://壊れ
                    //回復状態にする
                    _isRecovery = true;
                    break;

                case State.FULL://満タン
                    //送り先リストの削除
                    _sendTargetList.Clear();
                    break;
            }
        }


        /// <summary>
        /// 送り先セット関数
        /// </summary>
        /// <param name="obj"></param>
        public void SetTarget(GameObject obj)
        {
            _sendTargetList.Add(obj);
        }

        /// <summary>
        /// 重複チェック(まだ未完成)
        /// </summary>
        /// <returns></returns>     
        public bool CheckDuplication()
        {
            HashSet<GameObject> _hashSet = new HashSet<GameObject>(_sendTargetList);

            //重複がある場合は要素数が減る
            if (_sendTargetList.Count > _hashSet.Count)
            {
                return false;
            }
            return true;
        }


        //送り先がいるかどうかを判定
        public bool CheackHaveTarget()
        {
            //送り先がいればtrue
            if (_sendTargetList.Count != 0)
            {
                return true;
            }

            return false;
        }

        //生成エレメントかどうかを判別
        public bool IsBase()
        {
            return _isBase;
        }

        void OperationElement()
        {
            //回復中は動作不可能
            if (!_isRecovery)
            {
                //送る対象がある場合
                if (_sendTargetList.Count != 0 && _state != State.WAIT)
                {
                    //チャージ倍率の設定
                    double chage = _energyManager.GetComponent<EnergyManager>().GetChargeAmount();

                    //エナジーがある限り送りつつける
                    if (_energy > 0.0f)
                    {

                        //送り側のエネルギー減算（送り先の分だけ減少率増加）
                        _energy -= chage * _sendTargetCount * Time.deltaTime;
                      
                        foreach (GameObject obj in _sendTargetList)
                        {
                            //送り先数の更新
                            _sendTargetCount = _sendTargetList.Count;
                            if (obj.GetComponent<Element>().GetState() == State.WAIT||obj.GetComponent<Element>().GetState() == State.RECIEVE||obj.GetComponent<Element>().GetState() == State.SEND)
                            {
                                obj.GetComponent<Element>().ChageEnergy(chage * Time.deltaTime);
                            }
                            else
                            {
                                //対象をリスト排除対象に指定
                                _exclusionObj = obj;
                            }
                        }

                        if (_exclusionObj)
                        {
                            //対象をリストから除く
                            _sendTargetList.Remove(_exclusionObj);
                            //リスト排除対象オブジェクトを破棄
                            _exclusionObj = null;
                            //送り先数の更新
                            _sendTargetCount = _sendTargetList.Count;
                        }
                    }
                    else
                    {
                        _energy = 0.0f;
                        // リスト内処理
                        foreach (GameObject obj in _sendTargetList)
                        {
                            //対象に送り先が残っている場合
                            if (obj.GetComponent<Element>().CheackHaveTarget())
                            {
                                //対象の状態を「送り」に変更
                                obj.GetComponent<Element>().ChangeState(State.SEND);
                            }
                            //対象に送り先がない場合
                            else
                            {
                                //対象の状態を「待機」に変更
                                obj.GetComponent<Element>().ChangeState(State.WAIT);
                                //対象をリスト排除対象に指定
                                _exclusionObj = obj;
                            }
                        }

                        if (_exclusionObj)
                        {
                            //対象をリストから除く
                            _sendTargetList.Remove(_exclusionObj);
                            //リスト排除対象オブジェクトを破棄
                            _exclusionObj = null;
                        }
                        //状態を「待機」にする。
                        ChangeState(State.WAIT);
                    }

                    //送り先がなくなれば「待機」状態にする
                    if (_sendTargetCount == 0)
                    {
                        ChangeState(State.WAIT);
                    }
                }
            }
            else
            {
                //タイマー減算
                _recoveryTime -= 1.00 * Time.deltaTime;
                //回復に必要な時間経過後
                if (_recoveryTime < 0.00)
                {
                    //耐久度の回復
                    _durability = 100;
                    //容量の回復
                    _capacity = 100;
                    //画像をもとに戻す
                    if (!_isBase)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = _normalSprite;
                        //ヒビ画像を消す
                        _cilldrenList[0].GetComponent<SpriteRenderer>().sprite = null;
                    }
                    //状態を「待機」に変更
                    ChangeState(State.WAIT);
                    //回復フラグを折る
                    _isRecovery = false;
                    //回復時間の再設定
                    _recoveryTime = 10;


                }

            }

        }






        //エレメント情報のリセット関数
        public void ResetElement()
        {
            //修復フラグのリセット
            _isRecovery = false;
            //回復時間のリセット
            _recoveryTime = 10;
            //エネルギーのリセット
            _energy = 0;
            //最大容量のリセット
            _capacity = 100;
            //耐久度のリセット
            _durability = 100;
            //送り先リストのクリア
            _sendTargetList.Clear();
            //待機状態に変更
            ChangeState(State.WAIT);

        }


        //ポーズ切替関数
        public void SetIsPause()
        {
            //ポーズ切り替え
            _isPause = !_isPause;


        }

        //ポーズ状態確認関数
        public bool GetIsPause()
        {
            //ポーズ状態を返す
           return _isPause;


        }

    }



}