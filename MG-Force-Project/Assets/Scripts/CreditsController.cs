using Game;
using System.Collections;
using TMPro;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private TextMeshProUGUI _textL;
    [SerializeField] private TextMeshProUGUI _textR;
    [SerializeField] private GameObject _bg1;
    [SerializeField] private GameObject _bg2;
    [SerializeField] private GameObject _message;

    private void Start()
    {
        _bg1.SetActive(true);
        _bg2.SetActive(false);
        _message.SetActive(false);

        StartCoroutine(EndCredits());
    }

    private IEnumerator EndCredits()
    {
        yield return new WaitForSeconds(8.0f);

        _bg1.SetActive(false);
        _bg2.SetActive(true);
        _message.SetActive(true);

        _titleText.text = "企画";

        _mainText.text = "寺園 紳平　　原田 武尊";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "メインプログラマー";

        _mainText.text = "塚由 理功";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "プログラマー";

        _mainText.text = "東 大樹　　上山 大輔\n林 郁哉";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "キャラクターデザイナー";

        _mainText.text = "出口 大和";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "3Dモデラー";

        _mainText.text = "植𠮷 咲　　阪井 悠太";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "リギングアーティスト";

        _mainText.text = "植𠮷 咲";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "モーションデザイナー";

        _mainText.text = "池本 成那　　出口 大和";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "背景デザイナー";

        _mainText.text = "植𠮷 咲　　竹内 はな\n宮西 希美";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "UIデザイナー";

        _mainText.text = "寺園 紳平";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "使用音源";

        _mainText.text = "";

        _textL.text = "　　audiostock_121062　　　　STUDIO COM\n　　audiostock_111471\n　　audiostock_120986";

        yield return new WaitForSeconds(5.0f);

        _textL.text = "　　audiostock_936970　　　　8.864";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "";

        _textL.text = "";

        yield return new WaitForSeconds(5.0f);

        _titleText.text = "制作";

        yield return new WaitForSeconds(3.0f);

        _mainText.text = "2024年度ゲームカレッジ1年";

        yield return new WaitForSeconds(5.0f);

        _mainText.text = "";

        _titleText.text = "\n\nThank you for Playing";

        yield return new WaitForSeconds(5.0f);

        SceneLoader.Instance.LoadScene(GameConstants.Scene.Title.ToString());
    }
}