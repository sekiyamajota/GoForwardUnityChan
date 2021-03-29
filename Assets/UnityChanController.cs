using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    Animator animator;
    //Unityちゃんを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;
    // 地面の位置を-3.0と仮定する
    private float groundLevel = -3.0f;
    // ジャンプの速度の減衰
    private float dump = 0.8f;
    // ジャンプの速度
    float jumpVelocity = 20;
    // ゲームオーバーになる位置（追加）
    private float deadLine = -9;

    void Start()
    {
        // アニメータのコンポーネントを取得する
        this.animator = GetComponent<Animator>();
        // Rigidbody2Dのコンポーネントを取得する
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 走るアニメーションを再生するために、Animatorのパラメータを調節する
        this.animator.SetFloat("Horizontal", 1);

        // 着地しているかどうかを調べる(普段はgroundLevelの方が高い位置にあると仮定している)
        //三項演算子の使用例
        //isGroundに代入する値を条件によって変える(trueならfalse、falseならtrue)
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;

        //isGroundを利用してアニメーションを常に制御している
        this.animator.SetBool("isGround", isGround);

        //AudioSourceコンポーネントを取得すると同時にvolume変数へ音量の値を代入している
        // AudioSourceクラスの「volume」変数は、音量を表している
        // ジャンプ状態のときにはボリュームを0にする（追加）
        GetComponent<AudioSource>().volume = (isGround) ? 1 : 0;

        // 着地状態(isGround=trueとなっている(transform.position.y < this.groundLevel))でクリックされた場合
        if (Input.GetMouseButtonDown(0) && isGround)
        {
            // 上方向の力をかける
            this.rigid2D.velocity = new Vector2(0, this.jumpVelocity);
        }

        // クリックをやめたら
        if (Input.GetMouseButton(0) == false)
        {
            // ただし、その時rigid2D.velocity.yが0より大きい(ジャンプしている)時
            //rigid2D.velocity.yは普段は0
            if (this.rigid2D.velocity.y > 0)
            {
                //上方向への速度を減速する
                this.rigid2D.velocity *= this.dump;
            }
        }

        /*
        Update関数の中でユニティちゃんの位置を毎フレーム調べ、ユニティちゃんが画面左端
        （ユニティちゃんのx座標がdeadLine変数より小さい値）に移動した場合はゲームオーバーと判定する

        UIControllerはCanvasオブジェクトにアタッチされているため、Find関数を使ってUIControllerがアタッチされているCanvasオブジェクトを検索し、
        GetComponent関数を使ってCanvasにアタッチされているUIContorllerスクリプトを取得している

        ！Findでオブジェクトを探してGetComponentでそのオブジェクトのスクリプトを取得する方法はよく使う！
        */

        // デッドラインを超えた場合ゲームオーバーにする（追加）
        if (transform.position.x < this.deadLine)
        {
            // UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            // ユニティちゃんを破棄する
            Destroy(gameObject);
        }
    }
}
