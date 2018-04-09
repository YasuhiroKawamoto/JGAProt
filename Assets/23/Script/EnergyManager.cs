using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// エナジーマネージャ
/// </summary>
/// 
namespace Play.Element
{
    public enum State//stateの種類
    {
        WAIT, SEND, RECIEVE, BREAK, FULL
    }
    public class EnergyManager : MonoBehaviour
    {
        [SerializeField]
        private float _chargeAmount;//チャージ量（秒間）

        [SerializeField]
        Element _senderElement;//エレメント（送る側）

        [SerializeField]
        Element _receiverElement;//エレメント（受ける側）

        [SerializeField]
        GameObject _trajectoryManager;//軌跡マネージャ

        [SerializeField]
        List<GameObject> _elementList;//エレメントリスト（エレメント管理用）

        [SerializeField]
        GameObject _checker;//当たり判定チェッカー

        private Vector3 _checkerPos;//チェッカーのポジション

        private Vector3 _mousePos;//マウスポジション

        private int _layerNo;//レイヤー番号（マウス判定用）

        private int _layerMask;//レイヤーマスク（マウス判定用）
        [SerializeField]
        private bool _isPush;//マウスプッシュ中か？

        // Use this for initialization
        void Start()
        {
            //シーン内のエレメントを取得しリストに入れる
            GetElementListOnScene();
            //チェッカーの取得
            _checker = transform.Find("ElementChecker").gameObject;
            //軌跡マネージャの取得
            _trajectoryManager = GameObject.Find("TrajectoryMan");
            //チャージ量設定
            _chargeAmount = 5;
            //レイヤーマスク作成
            //対応レイヤーの管理番号を取得
            _layerNo = LayerMask.NameToLayer("Default");
            // マスクへの変換（ビットシフト）
            _layerMask = 1 << _layerNo;

        }

        // Update is called once per frame
        void Update()
        {
            //軌跡マネージャがある場合動作
            if (_trajectoryManager)
            {
                //エレメント選択処理（現在はVer3）
                ConfigElement3();
            }
        }

        /// <summary>
        /// エレメント選択状態のリセット関数
        /// </summary>
        void ResetElement()
        {
            if (_senderElement)
            {
                //送り側のリセット
                _senderElement = null;//エレメント（送る側）
            }

            if (_receiverElement)
            {
                //受け側のリセット
                _receiverElement = null;//エレメント（受ける側）
            }
        }

        //連続選択用状態管理
        void ToNextElement()
        {
            //送り側のリセット
            _senderElement = null;//エレメント（送る側）
            //受け側のリセット
            _senderElement = _receiverElement;
            _receiverElement = null;//エレメント（受ける側）

        }

        //チャージ倍率の取得
        public float GetChargeAmount()
        {
            return _chargeAmount;

        }

        //エレメント設定１
        void ConfigElement()
        {
            //マウス操作で受け送り設定
            if (Input.GetMouseButtonDown(0))//押したとき
            {
                //判定用のレイを作成
                Ray ray = new Ray();
                RaycastHit2D hit = new RaycastHit2D();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 1, _layerMask);

                if (hit)
                {
                    _senderElement = hit.collider.GetComponent<Element>();//エレメント（送る側）
                    _trajectoryManager.GetComponent<Trajectory.Trajectory>().StartTrajectory(_senderElement);

                }
                else
                {
                    ResetElement();//選択状態をリセット
                }
            }
            else if (Input.GetMouseButtonUp(0))//離したとき
            {

                //判定用のレイを作成
                Ray ray = new Ray();
                RaycastHit2D hit = new RaycastHit2D();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 1, _layerMask);

                if (hit)
                {
                    //送り手と違うオブジェクト上で離した場合
                    if (hit.collider.gameObject != _senderElement && !hit.collider.gameObject.GetComponent<Element>().IsBase())
                    {

                        _receiverElement = hit.collider.GetComponent<Element>();//エレメント（受ける側）

                        if (_receiverElement.GetComponent<Element>().GetState() == State.WAIT || _receiverElement.GetComponent<Element>().GetState() == State.RECIEVE)
                        {
                            if (_senderElement)
                            {

                                if ((_senderElement.GetComponent<Element>().GetEnergy() / (double)_trajectoryManager.GetComponent<Trajectory.Trajectory>().Count) > 1.00)
                                {
                                    _senderElement.gameObject.GetComponent<Element>().ChangeState(State.SEND);//エレメントの状態を「送り」に指定
                                    _senderElement.GetComponent<Element>().SetTarget(_receiverElement.gameObject);//送り手に受け手を設定
                                    _trajectoryManager.GetComponent<Trajectory.Trajectory>().EndTrajectory();
                                    hit.collider.gameObject.GetComponent<Element>().ChangeState(State.RECIEVE);//エレメントの状態を「受け」に指定

                                }
                                else
                                {
                                    _trajectoryManager.GetComponent<Trajectory.Trajectory>().DestroyTraject();

                                }
                            }
                        }
                    }

                }
                _trajectoryManager.GetComponent<Trajectory.Trajectory>().DestroyTraject();

                ResetElement();//選択状態をリセット
            }
        }

        //エレメント設定２
        void ConfigElement2()
        {

            if (Input.GetMouseButton(0))
            {
                //押している判定
                _isPush = true;
            }
            else
            {
                //押していない判定
                _isPush = false;
            }

            if (_isPush)
            {
                // Vector3でマウス位置座標を取得する
                _mousePos = Input.mousePosition;
                // Z軸修正
                _mousePos.z = 10f;
                // マウス位置座標をスクリーン座標からワールド座標に変換する
                _checkerPos = Camera.main.ScreenToWorldPoint(_mousePos);
                // ワールド座標に変換されたマウス座標を代入
                _checker.transform.position = _checkerPos;
                //マウス操作で受け送り設定

                if (!_senderElement)
                {
                    if (_checker.GetComponent<ElementChecker>().GetFirstElement() != null)
                    {
                        _senderElement = _checker.GetComponent<ElementChecker>().GetFirstElement().GetComponent<Element>();//エレメント（送る側）

                        _trajectoryManager.GetComponent<Trajectory.Trajectory>().StartTrajectory(_senderElement);

                    }
                }
                //送り手と違うオブジェクトに触れた
                if (_checker.GetComponent<ElementChecker>().GetSecondElement() && _checker.GetComponent<ElementChecker>().GetSecondElement() != _senderElement)
                {
                    //送り先がベースで無ければ
                    if (!_checker.GetComponent<ElementChecker>().GetSecondElement().GetComponent<Element>().IsBase())
                    {
                        _receiverElement = _checker.GetComponent<ElementChecker>().GetSecondElement().GetComponent<Element>();//エレメント（受ける側）

                        if (_receiverElement.GetComponent<Element>().GetState() == (int)State.WAIT || _receiverElement.GetComponent<Element>().GetState() == State.RECIEVE)
                        {
                            if (_senderElement)
                            {
                                _senderElement.gameObject.GetComponent<Element>().ChangeState(State.SEND);//エレメントの状態を「送り」に指定
                                _senderElement.GetComponent<Element>().SetTarget(_receiverElement.gameObject);//送り手に受け手を設定
                                _receiverElement.GetComponent<Element>().ChangeState(State.RECIEVE);//エレメントの状態を「受け」に指定                          
                                _trajectoryManager.GetComponent<Trajectory.Trajectory>().EndTrajectory();
                                _checker.GetComponent<ElementChecker>().ToNext();
                                _trajectoryManager.GetComponent<Trajectory.Trajectory>().StartTrajectory(_senderElement);
                                ToNextElement();//次のエレメント選択に移

                            }
                        }

                    }

                }
            }
            else
            {
                _checker.GetComponent<ElementChecker>().Reset();
                ResetElement();//選択状態をリセット
                _trajectoryManager.GetComponent<Trajectory.Trajectory>().DestroyTraject();
            }
        }


        //エレメント設定３（現行）
        void ConfigElement3()
        {
            // Vector3でマウス位置座標を取得する
            _mousePos = Input.mousePosition;
            // Z軸修正
            _mousePos.z = 10f;
            // マウス位置座標をスクリーン座標からワールド座標に変換する
            _checkerPos = Camera.main.ScreenToWorldPoint(_mousePos);
            // ワールド座標に変換されたマウス座標を代入
            _checker.transform.position = _checkerPos;


            if (Input.GetMouseButtonDown(0))
            {
                //押している判定
                _isPush = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //押していない判定
                _isPush = false;
            }

            if (_isPush)
            {
               

                //マウス操作で受け送り設定
                if (!_senderElement)
                {
                    if (_checker.GetComponent<ElementChecker>().GetFirstElement() != null)
                    {
                        //エレメント（送る側）の取得
                        _senderElement = _checker.GetComponent<ElementChecker>().GetFirstElement().GetComponent<Element>();
                        //軌跡の開始を告げる
                        _trajectoryManager.GetComponent<Trajectory.Trajectory>().StartTrajectory(_senderElement);

                    }
                }
                //送り手と違うオブジェクトに触れた
                if (_checker.GetComponent<ElementChecker>().GetSecondElement() && _checker.GetComponent<ElementChecker>().GetSecondElement() != _senderElement)
                {
                    //送り先がベースで無ければ
                    if (!_checker.GetComponent<ElementChecker>().GetSecondElement().GetComponent<Element>().IsBase())
                    {
                        //エレメント（受ける側）をチェッカーが取得したものに設定
                        _receiverElement = _checker.GetComponent<ElementChecker>().GetSecondElement().GetComponent<Element>();
                        //受け側のエレメントが「待機」or「受け」の場合なら
                        if (_receiverElement.GetComponent<Element>().GetState() == State.WAIT || _receiverElement.GetComponent<Element>().GetState() == State.RECIEVE)
                        {
                            //送る側のエレメントがある場合
                            if (_senderElement)
                            {
                                //送りエレメントから受けエレメント間の軌跡の距離（obj数）と送り側のエネルギーで計算
                                if ((_senderElement.GetComponent<Element>().GetEnergy() / (double)_trajectoryManager.GetComponent<Trajectory.Trajectory>().Count) > 1.00)
                                {

                                    //エレメントの状態を「送り」に指定
                                    _senderElement.gameObject.GetComponent<Element>().ChangeState(State.SEND);
                                    //送り手に受け手を設定
                                    _senderElement.GetComponent<Element>().SetTarget(_receiverElement.gameObject);
                                    //エレメントの状態を「受け」に指定   
                                    _receiverElement.GetComponent<Element>().ChangeState(State.RECIEVE);
                                    //軌跡に終わりを告げる                     
                                    _trajectoryManager.GetComponent<Trajectory.Trajectory>().EndTrajectory();
                                    //チェッカーのリセット
                                    _checker.GetComponent<ElementChecker>().Reset();
                                    //エレメント選択のリセット
                                    ResetElement();
                                    //押していない判定
                                    _isPush = false;
                                }
                                else
                                {
                                    //不可軌跡のカラー変更および削除
                                    _trajectoryManager.GetComponent<Trajectory.Trajectory>().SetTrajectoryInvalid();
                                    //次のエレメント選択
                                    ResetElement();
                                    //押していない判定
                                    _isPush = false;

                                }
                            }
                        }
                        else
                        {
                            //不可軌跡のカラー変更および削除
                            _trajectoryManager.GetComponent<Trajectory.Trajectory>().SetTrajectoryInvalid();
                            //エレメント選択状態のリセット
                            ResetElement();
                            //押していない判定
                            _isPush = false;

                        }
                    }
                }
            }
            else
            {
                //チェッカーの選択状態をリセット
                _checker.GetComponent<ElementChecker>().Reset();
                //選択状態をリセット
                ResetElement();
                //軌跡の削除
                _trajectoryManager.GetComponent<Trajectory.Trajectory>().DestroyTraject();
            }
        }

        //軌跡マネージャセット関数
        public void SetTrajectoryManager(GameObject trajectoryMan)
        {
            _trajectoryManager = trajectoryMan;
        }


        //エレメント一括ポーズ、ポーズ解除
        public void PauseElements()
        {
            foreach (GameObject obj in _elementList)
            {
                obj.GetComponent<Element>().SetIsPause();
            }
        }

        //エレメント一括リセット
        public void ResetElements()
        {
            //_elementList内のobjectのリセット
            foreach (GameObject obj in _elementList)
            {
                obj.GetComponent<Element>().ResetElement();
            }
        }


        //エレメントリスト取得関数
        public void GetElementListOnScene()
        {
            //シーン上のすべての「Element」タグ付きオブジェクトを取得
            _elementList = new List<GameObject>();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Element"))
            {
                _elementList.Add(obj);
            }
        }
    }
}