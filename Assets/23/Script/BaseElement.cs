using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Element
{


    public class BaseElement : MonoBehaviour
    {

        [SerializeField]
        private int _chageSpeed;//エネルギーチャージ速度

        [SerializeField]
        private bool _isChage;//チャージ状態かどうか？

     



        // Use this for initialization
        void Start()
        {
            //エネルギーチャージ速度の設定
            _chageSpeed = 10;
        }

        // Update is called once per frame
        void Update()
        {
            if (!gameObject.GetComponent<Element>().GetIsPause())
            {
                //チャージ状態なら
                if (_isChage)
                {
                    //時間経過とともに回復
                    gameObject.GetComponent<Element>().ChageEnergy(_chageSpeed * Time.deltaTime);
                }

                //「待機」以外では回復しない
                if (gameObject.GetComponent<Element>().GetState() != State.WAIT)
                {
                    _isChage = false;
                }
                else
                {
                    _isChage = true;
                }
            }
        }
    }

}