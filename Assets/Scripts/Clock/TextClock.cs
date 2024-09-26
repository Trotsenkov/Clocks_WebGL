using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextClock : MonoBehaviour
{
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = TimeWebSynchronizer.SyncTime.ToString("H:m:s.ff");
    }
}