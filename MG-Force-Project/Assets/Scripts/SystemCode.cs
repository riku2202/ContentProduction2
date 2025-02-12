namespace Game
{
    public enum ErrorCode
    {
        // デバック用
        E0000 = 0, // テスト用コード

        // ユーザー関連

        // アプリケーション関連
        E2001 = 2001, // 正常に.exeを起動出来なかった場合
        E2002 = 2002, // 起動時に何らかの不具合が生じた場合

        // データ関連
        E3001 = 3001, // 正常にデータを取得出来なかった場合
        E3002 = 3002, // データに不正な値が見つかった場合

        // 進行関連
        E4001 = 4001, // 正常に進行不能になった場合
        E4002 = 4002, // シーン移動が正常に行えなかった場合

        // オブジェクト関連
        E5001 = 5001, // 正常にステージを生成出来なかった場合
        E5002 = 5002, // 正常にプレイヤーを生成出来なかった場合
        E5003 = 5003, // 正常にその他のオブジェクトを生成出来なかった場合
    }
}