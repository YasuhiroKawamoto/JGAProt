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
    public class EnergyManager : MonoBehaviour
    {




        [SerializeField]
        private float _chargeAmount ;//チャージ量（秒間）

        [SerializeField]
        GameObject _senderElement;//エレメント（送る側）

        [SerializeField]
        GameObject _receiverElement;//エレメント（受ける側）



        // Use this for initialization
        void Start()
        {
            //チャージ量設定
            _chargeAmount = 5;

        }

        // Update is called once per frame
        void Update()
        {
            //マウス操作で受け送り設定
            if (Input.GetMouseButtonDown(0))//押したとき
            {

                Ray ray = new Ray();
                RaycastHit2D hit = new RaycastHit2D();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //レイヤーマスク作成
                // レイヤーの管理番号を取得
                int layerNo = LayerMask.NameToLayer("Default");
                // マスクへの変換（ビットシフト）
                int layerMask = 1 << layerNo;

                hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 1, layerMask);

                if (hit)
                {
                    _senderElement = hit.collider.gameObject;//エレメント（送る側）

                }
                else
                {
                    ResetElement();//選択状態をリセット
                }
            }
            if (Input.GetMouseButtonUp(0))//離したとき
            {

                //判定用のレイを作成
                Ray ray = new Ray();
                RaycastHit2D hit = new RaycastHit2D();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //レイヤーマスク作成
                // レイヤーの管理番号を取得
                int layerNo = LayerMask.NameToLayer("Default");
                // マスクへの変換（ビットシフト）
                int layerMask = 1 << layerNo;

                hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 1, layerMask);

                if (hit)
                {
                    //IsCharge = false;

                    //送り手と違うオブジェクト上で離した場合
                    if (hit.collider.gameObject != _senderElement)
                    {
                        
                        _receiverElement = hit.collider.gameObject;//エレメント（受ける側）
                        Debug.Log(_receiverElement.ToString());
                        if (_receiverElement.GetComponent<Element>().GetState() == 0 || _receiverElement.GetComponent<Element>().GetState() == 2)
                        {
                            if (_senderElement)
                            {
                                _senderElement.gameObject.GetComponent<Element>().ChangeState(1);//エレメントの状態を「送り」に指定
                                _senderElement.GetComponent<Element>().SetTarget(_receiverElement);//送り手に受け手を設定

                                hit.collider.gameObject.GetComponent<Element>().ChangeState(2);//エレメントの状態を「受け」に指定
                            }
                        }
                    }

                }

                ResetElement();//選択状態をリセット
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


        public float GetChargeAmount()
        {
            return _chargeAmount;

        }
    }
}