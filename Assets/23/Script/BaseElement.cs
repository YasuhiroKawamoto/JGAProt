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
            _chageSpeed = 10;
        }

        // Update is called once per frame
        void Update()
        {

            //チャージ状態なら
            if (_isChage)
            {
                //時間経過とともに回復
                gameObject.GetComponent<Element>().ChageEnergy(_chageSpeed * Time.deltaTime);
            }

            if (gameObject.GetComponent<Element>().GetState() != 0)
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