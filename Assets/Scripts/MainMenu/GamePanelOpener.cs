using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelOpener : MonoBehaviour
{
    public GameObject GamePlayPanel;
    // Start is called before the first frame update
    public void OpenPanel()
    {
        if (GamePlayPanel != null)
        {
            bool isActive = GamePlayPanel.activeSelf;
            GamePlayPanel.SetActive(!isActive);
        }
    }

}
