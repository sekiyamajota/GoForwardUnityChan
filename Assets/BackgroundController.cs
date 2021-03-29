using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
このスクリプトはUpdate関数の中でTranslate関数を使って背景画像を左方向に移動している
背景画像は遠景なので、キューブの移動速度に比べて移動速度を少し遅く設定している
*/
public class BackgroundController : MonoBehaviour
{

    // スクロール速度
    private float scrollSpeed = -1;
    // 背景終了位置
    private float deadLine = -16;
    // 背景開始位置
    private float startLine = 15.8f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //フレームレートによって速度が変わらないようにTime.deltaTimeを掛け算している
        // 背景を移動する
        transform.Translate(this.scrollSpeed * Time.deltaTime, 0, 0);

        //背景画像のx座標の位置をフレームごとにチェックして、背景画像が左端（背景のx座標がdeadLine変数より小さい値）
        //までスクロールしたら画面右端のstartLine変数の位置に戻している
        // 画面外に出たら、画面右端に移動する
        if (transform.position.x < this.deadLine)
        {
            transform.position = new Vector2(this.startLine, 0);
        }
    }
}