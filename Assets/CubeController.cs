using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    // キューブの移動速度
    private float speed = -12;

    // 消滅位置
    private float deadLine = -10;

    void Start()
    {
        
    }

    void Update()
    {
    /*
    ・TransformクラスのTranslate関数は、引数に与えた値のぶんだけ現在の位置から移動します
     （指定した値の座標に移動するわけではないことに注意）
     第一引数から順にX軸方向、Y軸方向、Z軸方向の移動距離を指定できる

    ・Time.deltaTimeは前フレームからの経過時間を表します。フレームレートが高ければ小さく、
     フレームレートが低ければ大きくなります。このTime.deltaTimeがない場合、
     フレームレートによって速度に差が生じます。

     たとえばフレームレートが秒間30の場合は毎秒 speed * 30 進みますが、
     秒間60の場合は speed * 60 進むことになります。この差を埋めるためにTime.deltaTimeを掛け算しています。
    */
        // キューブを移動させる
        transform.Translate(this.speed * Time.deltaTime, 0, 0);

        //フレームごとにキューブのx座標の位置を調べ、キューブが画面外に出た場合
        //（キューブのx座標がdeadLine変数より小さい値になった時）
        //にはDestroy関数を使ってキューブを破棄している
        // 画面外に出たら破棄する
        if (transform.position.x < this.deadLine)
        {
            Destroy(gameObject);
        }
    }

    //追加BGM
    //トリガーではなくコライダー衝突
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground1" || collision.gameObject.tag == "block1")
        {
            //地面かブロックに当たった時のみ再生するので、
            //インスペクターウィンドウの「Play on Awake」欄のチェックを外す
            GetComponent<AudioSource>().Play();
        }
    }
}
