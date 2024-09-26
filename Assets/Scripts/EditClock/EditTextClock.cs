using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class EditTextClock : MonoBehaviour
{
    private TMP_InputField text;

    private void Awake()
    {
        text = GetComponent<TMP_InputField>();
    }

    private void OnEnable()
    {
        text.text = TimeWebSynchronizer.SyncTime.ToString("H:mm");
    }

    public void OnSave()
    {
        DateTime dt;
        if (DateTime.TryParse(text.text, out dt))
            TimeWebSynchronizer.SyncTime = dt;
        else
            text.text = TimeWebSynchronizer.SyncTime.ToString("H:mm");
    }
}