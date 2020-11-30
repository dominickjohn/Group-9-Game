using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Game.GetGameManager().Player.canControl = false;
        yield return new WaitForSecondsRealtime(3f);
        Game.GetGameManager().Player.canControl = false;
        GameObject.Find("WinScreen").GetComponent<CanvasGroup>().DOFade(1f, 1f);
        GameObject.Find("WinScreen").GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
