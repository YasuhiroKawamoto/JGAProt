using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネルギー画像の動的サイズ変更
/// </summary>
namespace Play.Element
{
    public class EnergyImage : MonoBehaviour
    {

        SpriteRenderer _spriteRenderer;

        // Use this for initialization
        void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            //容量に対してのチャージ量で画像のサイズを算出
            double size =  gameObject.GetComponentInParent<Element>().GetEnergy()/gameObject.GetComponentInParent<Element>().GetCapacity();

            //サイズを画像に反映
            if (size > 0)
            {
                _spriteRenderer.GetComponent<SpriteRenderer>().transform.localScale = new Vector3((float)size, (float)size, 1);
            }
            else
            {
                _spriteRenderer.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(0.0f, 0.0f, 1);
            }
        }
    }
}