using System.Collections;
using System.Collections.Generic;
using Ring;
using UnityEngine;

public class StartGame : BaseClickButton
{
    // Start is called before the first frame update
    protected override void OnButtonClick()
    {
        GameManager.Instance.CreateLevels();
    }
}
