using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebase : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            FindObjectOfType<PanelOpener>().OpenPanel();
           
        }
    }
    public void ButtonClick()
    {
        //FindObjectOfType<Player>().SoldMessage();
        FindObjectOfType<PanelOpener>().ClosePanel();
        

    }
}
