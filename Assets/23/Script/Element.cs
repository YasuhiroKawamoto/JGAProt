using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エレメント
/// </summary>
public class Element : MonoBehaviour
{

    [SerializeField]
    double Energy;//保有エネルギー量


    GameObject EM;

    [SerializeField]
    private List<GameObject> SendTargetList = new List<GameObject>();//送り先リスト

    private int SendTargetCount;

    private GameObject ExclusionObj;

    [SerializeField]
    int state;//状態（0:非選択時  1:送り　　2:受け）

    TextMesh MeshText;//テキストメッシュの内容




    // Use this for initialization
    void Start()
    {
        state = 0;

        SendTargetList.Clear();


        MeshText = gameObject.GetComponentInChildren<TextMesh>();
        EM = GameObject.Find("EnergyManager");




    }

    // Update is called once per frame
    void Update()
    {



        //送る対象がある場合
        if (SendTargetList.Count != 0)
        {
            double Chage = EM.GetComponent<EnergyManager>().GetChargeAmount();

            //エナジーがある限り送りつつける
            if (Energy > 0.0f && state != 0)
            {



                Energy -= Chage * SendTargetCount * Time.deltaTime;

                foreach (GameObject obj in SendTargetList)
                {

                    SendTargetCount = SendTargetList.Count;

                    obj.GetComponent<Element>().ChageEnergy(Chage * Time.deltaTime);
                }



            }
            else
            {
                Energy = 0.0f;
                // リスト内処理
                foreach (GameObject obj in SendTargetList)
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

                        ExclusionObj = obj;
                    }
                }

                if (ExclusionObj)
                {
                    //対象をリストから除く
                    SendTargetList.Remove(ExclusionObj);
                    ExclusionObj = null;
                }

                ChangeState(0);


            }

        }
        //表示エネルギー量の更新
        MeshText.text = Energy.ToString("f2");

    }

    /// <summary>
    /// エネルギー取得
    /// </summary>
    /// <returns></returns>
    public double GetEnergy()
    {
        return Energy;
    }


    /// <summary>
    /// エネルギー加算、減算
    /// </summary>
    /// <param name="chage"></param>
    public void ChageEnergy(double chage)
    {
        Energy += chage;
        if (Energy < 0)
        {
            Energy = 0;

        }

    }


    public int GetState()
    {
        return state;
    }
    public void ChangeState(int f)
    {
        state = f;

        //状態に応じて色変更
        switch (state)
        {
            case 0://非指定
                   //送り先リストの削除
                SendTargetList.Clear();
                //エネルギーの端数処理（暫定）
                Energy = System.Math.Round(Energy, System.MidpointRounding.AwayFromZero);
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);


                break;
            case 1://送り
                this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);

                break;

            case 2://受け
                this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);

                break;
        }
    }


    /// <summary>
    /// 送り先セット関数
    /// </summary>
    /// <param name="obj"></param>
    public void SetTarget(GameObject obj)
    {
        Debug.Log(SendTargetList.Count);
        SendTargetList.Add(obj);//送り先リストに追加


    }

    public bool IsTarget()
    {

        if (SendTargetList.Count != 0)
        {
            return true;
        }

        return false;
    }
}