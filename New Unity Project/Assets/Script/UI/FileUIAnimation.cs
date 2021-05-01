using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileUIAnimation : MonoBehaviour
{
    public float timeDelay;
    public CanvasGroup BGFile;
    public Transform chooseFileText;
    public float chooseFileTextPos;

    public Transform File1;
    public Transform File2;
    public Transform File3;
    public Transform File4;
    public Transform File5;

    public float File1Pos;
    public float File2Pos;
    public float File3Pos;
    public float File4Pos;
    public float File5Pos;

    public void OnEnable()
    {
        StartCoroutine(RunAnimation());
    }

    public void Close()
    {
        StartCoroutine(CloseAnimation());
    }

    IEnumerator RunAnimation()
    {
        File1Pos = File1.localPosition.x;
        File2Pos = File2.localPosition.x;
        File3Pos = File3.localPosition.x;
        File4Pos = File4.localPosition.x;
        File5Pos = File5.localPosition.x;

        BGFile.alpha = 0;
        BGFile.LeanAlpha(1, 0.5f);

        chooseFileTextPos = chooseFileText.localPosition.y;
        chooseFileText.localPosition = new Vector2(chooseFileText.localPosition.x, Screen.height);
        chooseFileText.LeanMoveLocalY(chooseFileTextPos, 0.5f).setEaseOutQuint();

        File1.localPosition = new Vector2(Screen.width, File1.localPosition.y);
        File2.localPosition = new Vector2(Screen.width, File2.localPosition.y);
        File3.localPosition = new Vector2(Screen.width, File3.localPosition.y);
        File4.localPosition = new Vector2(Screen.width, File4.localPosition.y);
        File5.localPosition = new Vector2(Screen.width, File5.localPosition.y);

        File1.LeanMoveLocalX(File1Pos, 1f).setEaseOutQuint();
        yield return new WaitForSeconds(timeDelay);
        File2.LeanMoveLocalX(File2Pos, 1f).setEaseOutQuint();
        yield return new WaitForSeconds(timeDelay);
        File3.LeanMoveLocalX(File3Pos, 1f).setEaseOutQuint();
        yield return new WaitForSeconds(timeDelay);
        File4.LeanMoveLocalX(File4Pos, 1f).setEaseOutQuint();
        yield return new WaitForSeconds(timeDelay);
        File5.LeanMoveLocalX(File5Pos, 1f).setEaseOutQuint();
    }

    IEnumerator CloseAnimation()
    {
        chooseFileText.LeanMoveLocalY(Screen.height, 0.5f).setEaseInQuint();

        File5.LeanMoveLocalX(Screen.width, 0.5f).setEaseInQuint();
        yield return new WaitForSeconds(timeDelay);
        File4.LeanMoveLocalX(Screen.width, 0.5f).setEaseInQuint();
        yield return new WaitForSeconds(timeDelay);
        File3.LeanMoveLocalX(Screen.width, 0.5f).setEaseInQuint();
        yield return new WaitForSeconds(timeDelay);
        File2.LeanMoveLocalX(Screen.width, 0.5f).setEaseInQuint();
        yield return new WaitForSeconds(timeDelay);
        File1.LeanMoveLocalX(Screen.width, 0.5f).setEaseInQuint().setOnComplete(OnComplete);

        BGFile.LeanAlpha(0, 0.5f);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);

        chooseFileText.localPosition = new Vector2(chooseFileText.localPosition.x, chooseFileTextPos);

        File1.localPosition = new Vector2(File1Pos, File1.localPosition.y);
        File2.localPosition = new Vector2(File2Pos, File2.localPosition.y);
        File3.localPosition = new Vector2(File3Pos, File3.localPosition.y);
        File4.localPosition = new Vector2(File4Pos, File4.localPosition.y);
        File5.localPosition = new Vector2(File5Pos, File5.localPosition.y);
    }
}
