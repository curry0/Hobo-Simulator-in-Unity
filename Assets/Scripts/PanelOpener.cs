using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    // Start is called before the first frame update
 public void OpenPanel()
    {
        Panel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ClosePanel()
    {
        Panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
