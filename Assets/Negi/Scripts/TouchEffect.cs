using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour {

    // タップエフェクト
    [SerializeField]
    ParticleSystem _tapEffect;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var camera = Camera.main;
            // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
            var pos = camera.ScreenToWorldPoint(Input.mousePosition + camera.transform.forward * 10);
            GameObject obj = GameObject.Instantiate(_tapEffect.gameObject, pos, Quaternion.identity);
            var particle = obj.GetComponent<ParticleSystem>();
            particle.Emit(1);
            Destroy(obj, particle.main.duration);
        }
    }
}
