using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScript : MonoBehaviour
{
    [SerializeField] private SlideScript topPlane;
    [SerializeField] private BotScript botPlane;

    private Ray myRay;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Check()
    {
        if (topPlane.selectedPanelID == botPlane.selectedPanelID)
        {
            Debug.Log("Bingo!!!");
        }
    }
}
