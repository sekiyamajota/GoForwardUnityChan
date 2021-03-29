using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //シーン管理

public class UIController : MonoBehaviour
{

    // ゲームオーバーテキスト
    private GameObject gameOverText;

    // 走行距離テキスト
    private GameObject runLengthText;

    // 走った距離
    private float len = 0;

    // 走る速度
    private float speed = 5f;

    // ゲームオーバーの判定
    private bool isGameOver = false;

    /*
    作成した2つのUI(GameOverとRunLength)に表示する文字列を更新するため、
    Start関数の中でFind関数を使ってシーン中からこれらのオブジェクトを探し、
    gameOverText変数とrunLengthText変数にそれぞれ代入している
    */
    void Start()
    {
        // シーンビューからオブジェクトの実体を検索する
        this.gameOverText = GameObject.Find("GameOver");
        this.runLengthText = GameObject.Find("RunLength");
    }

    //Update関数では、len変数にspeed変数を加算して走行距離を算出している
    void Update()
    {
        if (this.isGameOver == false)
        {
            // 走った距離を更新する
            //ここでもフレームレートによって差を出さないためにTime.deltaTimeを掛けている
            this.len += this.speed * Time.deltaTime;

            /*
            ・走った距離を表示する
            runLengthText変数のtextに代入する時はlen.ToString ("F2")としてlen変数をToString関数を使って文字列に変換している
            ・ToString()は数値を文字列に変換し、引数には文字列に変換する際の書式を指定できる
            ここでは、浮動小数点の値を文字列に変換し、引数を"F2"とすることで、小数部を2桁まで表示するように書式指定している
            */
            this.runLengthText.GetComponent<Text>().text = "Distance:  " + len.ToString("F2") + "m";
        }

        /*
            ここでは"SampleScene"を渡して同じシーンを再読み込みすることで、
            ゲームオーバーとなった時にワンタッチで再びゲームを開始している
            */
        // ゲームオーバーになった場合（追加）
        if (this.isGameOver == true)
        {
            // クリックされたらシーンをロードする
            if (Input.GetMouseButtonDown(0))
            {
                // SceneManagerクラスのLoadScene関数を使うとシーンを読み込むことができる
                // 引数には読み込むシーン名を渡す
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void GameOver()
    {
        // ゲームオーバーになったときに、画面上にゲームオーバを表示する
        this.gameOverText.GetComponent<Text>().text = "Game Over";
        this.isGameOver = true;
    }
}