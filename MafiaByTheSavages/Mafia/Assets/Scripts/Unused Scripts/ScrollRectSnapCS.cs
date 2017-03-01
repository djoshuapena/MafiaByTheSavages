using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnapCS : MonoBehaviour {

    //public variables
    public RectTransform panel; //Holds the scroll panel
    public Button[] bttn;
    public RectTransform center; //Center to compare the distance for each button

    //private variables
    private float[] distance;
    private bool dragging = false; // will be true while we drag panel
    private int bttnDistance; // will hold the distance between the buttons
    private int minButtonNum; //To hold the number of the button, with smallest distance to center

    void Start()
    {
        int bttnLength = bttn.Length;
        distance = new float[bttnLength];

        // Get distance between buttons
        bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    void Update()
    {
        for (int i = 0; i < bttn.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - bttn[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance); // Get the min distance

        for (int a = 0; a < bttn.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minButtonNum = a;
            }
        }

        if (!dragging)
        {
            LeapToBttn(minButtonNum * -bttnDistance);
        }
    }

    void LeapToBttn(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 15f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}
