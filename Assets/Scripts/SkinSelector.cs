using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public GameObject scrollbar;
    private float scrollPos = 0;
    private float[] pos;

    private void Start()
    {
        // Make all the skin images larger in the skin selector
        foreach (Transform child in transform)
        {
            child.GetChild(0).localScale = new Vector3(1.8f, 1.8f, 1f);
        }
    }

    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value =
                        Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale =
                    Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale,
                            new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }
}
