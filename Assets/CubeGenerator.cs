using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//このスクリプトでは、span変数で指定した時間間隔ごとにキューブを生成する処理を作成している
public class CubeGenerator : MonoBehaviour
{
    // キューブのPrefab
    public GameObject cubePrefab;

    // 時間計測用の変数
    private float delta = 0;

    // キューブの生成間隔
    private float span = 1.0f;

    // キューブの生成位置：X座標
    private float genPosX = 12;

    // キューブの生成位置オフセット
    private float offsetY = 0.3f;
    // キューブの縦方向の間隔
    private float spaceY = 6.9f;

    // キューブの生成位置オフセット
    private float offsetX = 0.5f;
    // キューブの横方向の間隔
    private float spaceX = 0.4f;

    // キューブの生成個数の上限
    private int maxBlockNum = 4;

    void Start()
    {
        
    }

    void Update()
    {
        //時間経過を測るため、delta変数にTime.deltaTimeを足している
        //毎フレーム間の時間を加算することで、delta変数には経過時間が代入される
        this.delta += Time.deltaTime;

        //if(this.delta > this.span)の条件によって、一定時間ごとに動作を繰り返す処理を作成しています。
        // span秒(今回は1秒)以上の時間が経過したかを調べる
        if (this.delta > this.span)
        {
            this.delta = 0;
            // 生成するキューブ数をランダムに決め、高さがランダムにする様にしている
            int n = Random.Range(1, maxBlockNum + 1);

            // 指定した数だけキューブを生成する
            for (int i = 0; i < n; i++)
            {
                // キューブの生成
                GameObject go = Instantiate(cubePrefab) as GameObject;
                //複数のキューブが一度ではなく段階的にトン・トン・トンと落ちてくるように、
                //キューブを生成する位置は縦方向にspaceY変数のぶんだけスペースを空けて生成している
                go.transform.position = new Vector2(this.genPosX, this.offsetY + i * this.spaceY);
            }
            // 次のキューブまでの生成時間を決める
            //また、生成するキューブの数が少ない場合は次のキューブ生成までの間隔を短くし、
            //キューブの数が多い場合は次のキューブ生成までの間隔を長くしている
            this.span = this.offsetX + this.spaceX * n;
        }
    }
}
