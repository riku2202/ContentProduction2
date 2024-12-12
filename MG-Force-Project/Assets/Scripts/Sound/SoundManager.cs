using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game 
{
    public class SoundManager : MonoBehaviour
    {
        /*
            ここでBGMとSEの両方を管理してください
            以下は実装してほしい内容とヒントです
         
            ・このクラスを'シングルトン'で宣言する
              (詳しい内容は授業で聞くか調べてください)

            ・このスクリプトをアタッチしたオブジェクトをゲームを開始して一番最初に読み込まれるシーンに配置してください

            ・BGMとSEを[SerializeField]がついたprivateのAudioClip変数を配列で宣言して、Unity側で音源をアタッチする

            ・配列でどの場所にどの音源が入っているか、enumで管理する

            ・BGMとSEそれぞれ流すための関数を作成する
              引数は配列番号(音源の場所)
              戻り値はintで正常に音源が流れたら0を
                           引数が配列の要素数より多かったり、意図した数値ではない場合-1を返す

              以下はできるなら実装してほしい内容です

            ・BGMとSEの音量をそれぞれ管理する変数とその値を動的に変更できる関数を作成する
        */
    }
}