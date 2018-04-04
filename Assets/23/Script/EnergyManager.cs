using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// エナジーマネージャ
/// </summary>
public class EnergyManager : MonoBehaviour
{


   

    [SerializeField]
    float ChargeAmount;//チャージ量（秒間）

    

    [SerializeField]
    GameObject SenderElement;//エレメント（送る側）

    [SerializeField]
    GameObject ReceiverElement;//エレメント（受ける側）



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //マウス操作で受け送り設定
        if (Input.GetMouseButtonDown(0))//押したとき
        {

            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
              
                
                SenderElement = hit.collider.gameObject;//エレメント（送る側）

            }
            else
            {
                ResetElement();//選択状態をリセット

             

            }




        }
        if (Input.GetMouseButtonUp(0))//離したとき
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                //IsCharge = false;

                //送り手と違うオブジェクト上で離した場合
                if (hit.collider.gameObject != SenderElement)
                {
                    ReceiverElement = hit.collider.gameObject;//エレメント（受ける側）
                    if (ReceiverElement.GetComponent<Element>().GetState() == 0|| ReceiverElement.GetComponent<Element>().GetState() == 2)
                    {
                        if (SenderElement)
                        {
                            SenderElement.gameObject.GetComponent<Element>().ChangeState(1);//エレメントの状態を「送り」に指定
                            SenderElement.GetComponent<Element>().SetTarget(ReceiverElement);//送り手に受け手を設定

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
        if (SenderElement)
        {
            SenderElement = null;//エレメント（送る側）
        }

        if (ReceiverElement)
        {
            ReceiverElement = null;//エレメント（受ける側）
        }
    }


    public float GetChargeAmount()
    {
        return ChargeAmount;

    }
}
