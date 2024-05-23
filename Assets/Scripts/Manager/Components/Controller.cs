using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Ring;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controller
{
    #region Component

    public enum StateGame
    {
        Win,
        Lose,
        Move,
        Empty
    }

    public enum TypeCard
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H
    }

    [Serializable]
    public class GameController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Game Controller")] [ChangeColorLabel(0.2f, 1, 1)]
        public StateGame _stateGame;

        [ReadOnly] [ChangeColorLabel(0.2f, 1, 1)]
        public CardManager _card1;

        [ReadOnly] [ChangeColorLabel(0.2f, 1, 1)]
        public CardManager _card2;

        [ReadOnly] [ChangeColorLabel(0.2f, 1, 1)]
        public List<DataGame> _listDataLevel;

        [ReadOnly] [ChangeColorLabel(0.2f, 1, 1)]
        public Transform _level;

        [ReadOnly] [ChangeColorLabel(0.2f, 1, 1)]
        public List<CardManager> _listCard;

        [ReadOnly] [ChangeColorLabel(0.2f, 1, 1)]
        public Transform destination;

        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Text Minus")] [ChangeColorLabel(0.2f, 1, 1)]
        public Transform txtMinus;
    }


    [Serializable]
    public class MusicController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Audio Clip")] [ChangeColorLabel(0.9f, .55f, .95f)]
        public AudioClip audioClip_;

        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Audio Source")] [ChangeColorLabel(0.2f, 1, 1)]
        public AudioSource audioSource_;
    }

    [Serializable]
    public class UiController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Ui Controller")] [ChangeColorLabel(0.2f, 1, 1)]
        public Transform _btnStartGame;

        [ChangeColorLabel(0.2f, 1, 1)] public TMP_Text _txtTurn;
        [ChangeColorLabel(0.2f, 1, 1)] public int _turn;
        [ChangeColorLabel(0.2f, 1, 1)] public TMP_Text _txtMatch;
        [ChangeColorLabel(0.2f, 1, 1)] public int _match;
        [ChangeColorLabel(0.2f, 1, 1)] public TMP_Text _txtMoney;
        [ChangeColorLabel(0.2f, 1, 1)] public TMP_Text _txtTime;
        [ChangeColorLabel(0.2f, 1, 1)] public List<Transform> _listTransformOffGame;
        [ChangeColorLabel(0.2f, 1, 1)] public List<Transform> _listTransformOnGame;
        [ChangeColorLabel(0.2f, 1, 1)] public Transform _winTransform;
        [ChangeColorLabel(0.2f, 1, 1)] public Transform _loseTransform;
        [ChangeColorLabel(0.2f, 1, 1)] public Timer _timer;
    }

    [Serializable]
    public class CheckScene
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Check Scene")] [ChangeColorLabel(.7f, 1f, 1f)]
        public bool _isGetCurrentNameScene;

        [ChangeColorLabel(.7f, 1f, 1f)] public string _nameSceneChange;
    }

    #endregion Component

    [Serializable]
    public class CardController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Check Scene")] [ChangeColorLabel(.7f, 1f, 1f)]
        public Tweener _tween;

        public DOTween A;
        [ChangeColorLabel(.7f, 1f, 1f)] public TypeCard _typeCard;
        [ChangeColorLabel(.7f, 1f, 1f)] public int _directionRotate;
        [ChangeColorLabel(.7f, 1f, 1f)] public RectTransform _countCard;
        [ChangeColorLabel(.7f, 1f, 1f)] public RectTransform _behindCard;
        [ChangeColorLabel(.7f, 1f, 1f)] public Button _btnCard;
    }
}


#region Base Method

public abstract class RingSingleton<T> : MonoBehaviour where T : RingSingleton<T>
{
    private static T _instance;

    public enum ChangeDestroy
    {
        DontDestroy,
        Destroy
    }

    public ChangeDestroy _changDestroy = ChangeDestroy.Destroy;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    UnityEngine.Debug.LogError("An instance of " + typeof(T) +
                                               " is needed in the scene, but there is none.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = (T)this;
        if (_changDestroy == ChangeDestroy.DontDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

#endregion Base Method
