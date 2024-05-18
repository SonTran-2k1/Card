using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using DG.Tweening;
using Ring;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardController _cardManager;

    private void Start()
    {
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance._gameController._stateGame != StateGame.Empty)
        {
            return;
        }

        if (_cardManager._tween != null)
        {
            return;
        }

        ClickCard(Settings.Direction_Open);
    }


    #region Minus

    Vector2 GetMouseUISpacePosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            UiManager.Instance.GetComponent<RectTransform>(),
            mousePosition,
            UiManager.Instance.GetComponent<Canvas>().worldCamera,
            out Vector2 localPoint
        );
        return localPoint;
    }

    void SpawnAndMoveObject(Vector2 spawnPosition)
    {
        GameObject newObject = Instantiate(GameManager.Instance._gameController.txtMinus.gameObject,
            UiManager.Instance.transform);
        RectTransform rectTransform = newObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = spawnPosition;

        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + 550, 2)
            .SetEase(Ease.Linear).OnComplete(() => { Destroy(newObject); }); // Di chuyển lên trên
    }

    #endregion

    public void ClickCard(int Direction)
    {
        #region Get Card

        if (GameManager.Instance._gameController._card1 == null)
        {
            GameManager.Instance._gameController._card1 = this; //lấy thẻ 1 trước
        }
        else
        {
            if (GameManager.Instance._gameController._card2 == null)
            {
                for (int i = 0; i < GameManager.Instance._gameController._listCard.Count; i++)
                {
                    if (GameManager.Instance._gameController._listCard[i] != this)
                    {
                        GameManager.Instance._gameController._card2 = this;
                    }
                }
            }
        }

        #endregion

        _cardManager._tween.Kill();
        _cardManager._tween =
            transform.DORotate(
                Vector2.up * Direction, .45f).OnStart(
                () =>
                {
                    GameManager.Instance._gameController._stateGame = StateGame.Move;

                    #region Display Minus

                    if (Direction == Settings.Direction_Open)
                    {
                        UiManager.Instance.UpdateMoney();
                        Vector2 spawnPosition = GetMouseUISpacePosition();
                        SpawnAndMoveObject(spawnPosition);
                    }

                    #endregion

                    _cardManager._directionRotate = Direction;
                }).OnComplete((() =>
            {
                _cardManager._tween = null;
                GameManager.Instance._gameController._stateGame = StateGame.Empty;
                if (Direction == Settings.Direction_Open)
                {
                    if (GameManager.Instance._gameController._card2 != null)
                    {
                        UiManager.Instance.CheckWinGame();
                    }
                }
                else
                {
                    ReturnData();
                }
            }));
    }

    private static void ReturnData()
    {
        GameManager.Instance._gameController._card1._cardManager._directionRotate = Settings.Direction_Open;
        GameManager.Instance._gameController._card2._cardManager._directionRotate = Settings.Direction_Open;
        GameManager.Instance._gameController._card1 = null;
        GameManager.Instance._gameController._card2 = null;
    }
}
