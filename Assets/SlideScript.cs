using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideScript : MonoBehaviour
{
    [Header("Controllers")]
    [Range(1f,5f)] [SerializeField] private float scaleOffset;
    [Range(0f, 50f)] [SerializeField] private float step;
    [SerializeField] [Range(1,50)] private float panelOffset;
    [Range(0f, 20f)] [SerializeField] private float scaleSpeed;
   
    [Header("Variables")]
    [SerializeField] private GameObject panelPrefab;
    private Vector2[] panelPos;
    private Vector2[] panelScale;
    private RectTransform contentRT;
    public int selectedPanelID;
    [SerializeField] private ScrollRect scrollRect;

    private Vector2 contentVector;

    private bool isScrolling;
   
    private float scale;
    [SerializeField] private MastersData _mastersData;
    public List<GameObject> pannelsArray;

    public int count;
    
    void Start()
    {
        count = _mastersData.MastersList.Count;

        panelScale = new Vector2[_mastersData.MastersList.Count];
        contentRT = GetComponent<RectTransform>();
        pannelsArray = new List<GameObject>();
        panelPos = new Vector2[_mastersData.MastersList.Count];
        for (int i = 0;  i < _mastersData.MastersList.Count; i++)
        {
            pannelsArray.Add(panelPrefab);
            pannelsArray[i] = (Instantiate(panelPrefab, transform,false));
            pannelsArray[i].GetComponent<Image>().sprite = _mastersData.MastersList[i].MainPhoto;
            if (i == 0) continue;
            pannelsArray[i].transform.localPosition = new Vector2(pannelsArray[i-1].transform.localPosition.x + 
                                                             panelPrefab.GetComponent<RectTransform>().sizeDelta.x + panelOffset, 
                                                             panelPrefab.transform.localPosition.y);
            panelPos[i] = -pannelsArray[i].transform.localPosition;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (contentRT.anchoredPosition.x >= panelPos[0].x && !isScrolling || contentRT.anchoredPosition.x <= panelPos[panelPos.Length-1].x && !isScrolling)
        {
            isScrolling = false;
            scrollRect.inertia = false;
        }
        float nearestPos = float.MaxValue;
        for (int i = 0; i < _mastersData.MastersList.Count; i++)
        {
            float distance = Mathf.Abs(contentRT.anchoredPosition.x - panelPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanelID = i;
            }
            scale = Mathf.Clamp(1 / (distance / panelOffset) * scaleOffset,0.7f,1f);
            panelScale[i].x = Mathf.SmoothStep(pannelsArray[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            panelScale[i].y = Mathf.SmoothStep(pannelsArray[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            pannelsArray[i].transform.localScale = panelScale[i];

        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRT.anchoredPosition.x, panelPos[selectedPanelID].x,step*Time.fixedDeltaTime);
        contentRT.anchoredPosition = contentVector;
        
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}
