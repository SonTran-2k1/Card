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
    [ReadOnly] public bool isCheckShowCard; //cờ thay đổi sorting layer card

    private void Start()
    {
    }

    private void OnMouseDown()
    {
        //ClickObject();
    }

    private void Update()
    {
        //Bug.LogError(transform.eulerAngles.y);
    }

    public void ClickObject()
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
                    if (GameManager.Instance._gameController._listCard[i] != this &&
                        this != GameManager.Instance._gameController._card1)
                    {
                        GameManager.Instance._gameController._card2 = this;
                    }
                }

                if (GameManager.Instance._gameController._card1 == this)
                {
                    return;
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
                }).OnUpdate((() =>
            {
                if (transform.eulerAngles.y >= 90 && !isCheckShowCard)
                {
                    isCheckShowCard = true;
                    //thay đổi vị trí các hình ảnh của thẻ
                    int indexCountCard = _cardManager._countCard.GetSiblingIndex();
                    int indexBehindCard = _cardManager._behindCard.GetSiblingIndex();
                    _cardManager._countCard.SetSiblingIndex(indexBehindCard);
                    _cardManager._behindCard.SetSiblingIndex(indexCountCard);
                }
            })).OnComplete((() =>
            {
                isCheckShowCard = false;
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
