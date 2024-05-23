using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Controller;
using UnityEngine.SceneManagement;

public class GameManager : RingSingleton<GameManager>
{
    public GameController _gameController;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #region Save

    public void SaveLevel(int Level)
    {
        PlayerPrefs.SetInt(Settings.Level, Level);
    }

    public int LoadLevel()
    {
        return PlayerPrefs.GetInt(Settings.Level, 1);
    }

    #endregion

    public void CreateLevels()
    {
        // Kiểm tra nếu _gameController._level đã được xóa hoàn toàn
        StartCoroutine(CheckLevelDeletedAndCreate());
    }

    private IEnumerator CheckLevelDeletedAndCreate()
    {
        // Chờ một khoảng thời gian nhỏ trước khi kiểm tra lại
        if (_gameController._level != null) Destroy(_gameController._level.gameObject);
        yield return new WaitForSeconds(0.1f);

        if (_gameController._level == null)
        {
            // Nếu _gameController._level đã bị xóa hoàn toàn, tiến hành tạo cấp độ mới
            if (LoadLevel() >= 4)
            {
                SaveLevel(1);
            }

            var level = Resources.Load<GameObject>("Level " + LoadLevel());
            _gameController._level = Instantiate(level, Vector3.zero, Quaternion.identity).transform;
            _gameController._level.SetParent(UiManager.Instance._levelPosition);
            _gameController._level.localPosition = Vector3.zero;
            UiManager.Instance.InitDataUI();
            UiManager.Instance.PlayGame();
            if (level != null)
                Debug.Log("Đã tải cấp độ " + LoadLevel());
            else
                Debug.LogError("Không thể tải cấp độ " + LoadLevel());
        }
        else
        {
            // Nếu _gameController._level vẫn tồn tại, tiếp tục kiểm tra
            StartCoroutine(CheckLevelDeletedAndCreate());
        }
    }
}
