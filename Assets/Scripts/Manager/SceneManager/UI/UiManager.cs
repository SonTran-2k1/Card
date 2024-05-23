using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using Controller;
using DG.Tweening;
using Ring;
using UnityEngine.EventSystems;

public class UiManager : RingSingleton<UiManager>
{
    public UiController _uiController;
    public RectTransform _destination;
    public RectTransform _levelPosition;

    private void Start()
    {
        GameManager.Instance._gameController.destination = _destination;
    }

    public void PlayGame()
    {
        _uiController._listTransformOffGame.ForEach(a => a.gameObject.SetActive(false));
        _uiController._listTransformOnGame.ForEach(a => a.gameObject.SetActive(true));
    }

    public void HomeGame()
    {
        _uiController._listTransformOffGame.ForEach(a => a.gameObject.SetActive(true));
        _uiController._listTransformOnGame.ForEach(a => a.gameObject.SetActive(false));
    }

    public void InitDataUI()
    {
        _uiController._txtTurn.text = "Turn : " + 0;
        _uiController._txtMatch.text = "Match : " + 0;
        _uiController._txtMoney.text = "$" + GameManager.Instance._gameController
            ._listDataLevel[GameManager.Instance.LoadLevel() - 1].money;
    }

    public void CheckWinGame()
    {
        if (GameManager.Instance._gameController._card2 != null)
        {
            //Ring: Check win game
            if (GameManager.Instance._gameController._card1._cardManager._typeCard.ToString()
                .Equals(GameManager.Instance._gameController._card2._cardManager._typeCard.ToString()))
            {
                //move 2 card về 1 position
                GameManager.Instance._gameController._card1.transform.DOMove(
                    GameManager.Instance._gameController.destination.position, 1).OnStart((() =>
                {
                    GameManager.Instance._gameController._stateGame = StateGame.Move;
                })).OnComplete((() =>
                {
                    GameManager.Instance._gameController._stateGame = StateGame.Empty;
                    GameManager.Instance._gameController._card1.ClickCard(Settings.Direction_CLose);
                    GameManager.Instance._gameController._card1._cardManager._btnCard.interactable = false;
                }));
                GameManager.Instance._gameController._card2.transform.DOMove(
                    GameManager.Instance._gameController.destination.position, 1).OnStart((() =>
                {
                    GameManager.Instance._gameController._stateGame = StateGame.Move;
                })).OnComplete((() =>
                {
                    GameManager.Instance._gameController._stateGame = StateGame.Empty;
                    GameManager.Instance._gameController._card2.ClickCard(Settings.Direction_CLose);
                    GameManager.Instance._gameController._card2._cardManager._btnCard.interactable = false;
                }));
                GameManager.Instance._gameController._listCard.Remove(GameManager.Instance._gameController._card1);
                GameManager.Instance._gameController._listCard.Remove(GameManager.Instance._gameController._card2);
                UpdateMatch();
                if (GameManager.Instance._gameController._listCard.Count <= 0)
                {
                    WinGame();
                }
            }
            else
            {
                UpdateTurn();
                //trả lại trạng thái
                GameManager.Instance._gameController._card1.ClickCard(Settings.Direction_CLose);
                GameManager.Instance._gameController._card2.ClickCard(Settings.Direction_CLose);
            }
        }
    }

    private void WinGame()
    {
        _uiController._winTransform.gameObject.SetActive(true);
        _uiController._timer.timerState = TimerState.End;
        StartCoroutine(WaitWinner());
    }

    public void LoseGame()
    {
        _uiController._loseTransform.gameObject.SetActive(true);
        _uiController._timer.timerState = TimerState.End;
        StartCoroutine(WaitLoser());
    }

    private IEnumerator WaitWinner()
    {
        // Wait until the tween is complete
        yield return new WaitForSeconds(4);
        Destroy(GameManager.Instance._gameController._level.gameObject);
        _uiController._winTransform.gameObject.SetActive(false);
        int level = GameManager.Instance.LoadLevel();
        level++;
        GameManager.Instance.SaveLevel(level);
        HomeGame();
        // Do something after the tween is complete
    }

    private IEnumerator WaitLoser()
    {
        // Wait until the tween is complete
        yield return new WaitForSeconds(4);
        GameManager.Instance._gameController._listCard.ForEach(a => a._cardManager._btnCard.interactable = false);
        _uiController._loseTransform.gameObject.SetActive(false);
        HomeGame();
        // Do something after the tween is complete
    }

    public void UpdateTurn()
    {
        _uiController._turn++;
        _uiController._txtTurn.text = "Turn :" + _uiController._turn;
    }

    public void UpdateMatch()
    {
        _uiController._match++;
        _uiController._txtMatch.text = "Turn :" + _uiController._match;
    }

    public void UpdateMoney()
    {
        string money = _uiController._txtMoney.text.Split('$')[1];
        int countMoney = Convert.ToInt32(money);
        if (countMoney > 0)
        {
            countMoney -= 10;
            _uiController._txtMoney.text = "$" + countMoney;
            if (countMoney == 0)
            {
                LoseGame();
            }
        }
        else
        {
            LoseGame();
        }
    }

    #region Check UI Element

    public bool CheckUIReturn()
    {
        #region Kiểm tra xem có nhấn va UI nào không , nếu không thì return

#if UNITY_EDITOR || UNITY_STANDALONE
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject selectedObj = EventSystem.current.currentSelectedGameObject;
            if (selectedObj != null)
            {
                return true;
            }
        }
#else
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                GameObject selectedObj = EventSystem.current.currentSelectedGameObject;
                if (selectedObj != null)
                {
                    return true;
                }
            }
        }
#endif

        #endregion Kiểm tra xem có nhấn va UI nào không , nếu không thì return

        return false;
    }

    #endregion Method Game
}
