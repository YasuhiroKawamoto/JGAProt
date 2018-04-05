using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Play.Element
{
    public class EnergyText : MonoBehaviour
    {
        private TextMesh _meshText;//テキストメッシュの内容
                                  // Use this for initialization
        void Start()
        {
            //メッシュテキストのセット
            _meshText = gameObject.GetComponent<TextMesh>();

        }

        // Update is called once per frame
        void Update()
        {

            //表示エネルギー量の更新
            _meshText.text = GetComponentInParent<Element>().GetEnergy().ToString("f2");

        }
    }
}