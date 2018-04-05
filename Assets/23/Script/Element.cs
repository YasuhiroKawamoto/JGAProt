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
        private double _energy;//保有エネルギー量

        [SerializeField]
        private double _capacity;//エネルギー上限


        private GameObject _energyManager;//エナジーマネージャー

        [SerializeField]
        private List<GameObject> _sendTargetList = new List<GameObject>();//送り先リスト

        private int _sendTargetCount;//送り先の数

        private GameObject _exclusionObj;//リスト排除対象オブジェクト

        [SerializeField]
        private int _state;//状態（0:非選択時  1:送り　　2:受け）

       




        // Use this for initialization
        void Start()
        {

            _capacity = 100;
            //初期状態を「非設定」に指定
            _state = 0;
            //送り先リストのクリア
            _sendTargetList.Clear();

           
            //エナジーマネージャーのセット
            _energyManager = GameObject.Find("EnergyManager");




        }

        // Update is called once per frame
        void Update()
        {



            //送る対象がある場合
            if (_sendTargetList.Count != 0 && _state != 0)
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
                        if (obj.GetComponent<Element>().GetState() != 0)
                        {
                            obj.GetComponent<Element>().ChageEnergy(chage * Time.deltaTime);
                        }
                        else
                        {
                            _exclusionObj = obj;
                        }
                    }

                    if (_exclusionObj)
                    {
                        //対象をリストから除く
                        _sendTargetList.Remove(_exclusionObj);
                        _exclusionObj = null;
                        //送り先数の更新
                        _sendTargetCount = _sendTargetList.Count;
                        Debug.Log(_sendTargetList.Count.ToString());
                    }


                }
                else
                {
                    _energy = 0.0f;
                    // リスト内処理
                    foreach (GameObject obj in _sendTargetList)
                    {
                        if (obj.GetComponent<Element>().IsTarget())
                        {
                            //対象の状態を「送り」に変更
                            obj.GetComponent<Element>().ChangeState(1);
                        }
                        else
                        {
                            //対象の状態を「非選択」に変更
                            obj.GetComponent<Element>().ChangeState(0);
                            _exclusionObj = obj;
                        }
                    }

                    if (_exclusionObj)
                    {
                        //対象をリストから除く
                        _sendTargetList.Remove(_exclusionObj);
                        _exclusionObj = null;
                    }

                    ChangeState(0);

                }

                if (_sendTargetCount == 0)
                {
                    ChangeState(0);
                }

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
        /// エネルギー加算、減算
        /// </summary>
        /// <param name="chage"></param>
        public void ChageEnergy(double chage)
        {
            _energy += chage;
            if (_energy < 0)
            {
                _energy = 0;
            }

            if (_energy > _capacity)
            {
                _energy = _capacity;

                if (_state != 1)
                {
                    ChangeState(0);
                }

            }

        }

        //エレメントの状態取得
        public int GetState()
        {
            return _state;
        }

        public double GetCapacity()
        {
            return _capacity;
        }
        public void ChangeState(int f)
        {
            _state = f;

            //状態に応じて色変更
            switch (_state)
            {
                case 0://非指定
                    //送り先リストの削除
                    _sendTargetList.Clear();
                    //エネルギーの端数処理（暫定）
                    _energy = System.Math.Round(_energy, System.MidpointRounding.AwayFromZero);
                    //this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);


                    break;
                case 1://送り
                    //this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);

                    break;

                case 2://受け
                    //this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);

                    break;
            }
        }


        /// <summary>
        /// 送り先セット関数
        /// </summary>
        /// <param name="obj"></param>
        public void SetTarget(GameObject obj)
        {

            //送り先リストに追加
            _sendTargetList.Add(obj);
            //Debug.Log(SendTargetList.Count);

        }

        public bool IsTarget()
        {

            if (_sendTargetList.Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}