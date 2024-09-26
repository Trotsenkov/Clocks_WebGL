using System;
using UnityEngine;
using UnityEngine.UI;

public class AnalogueAlarm : MonoBehaviour
{
    [SerializeField] private RectTransform MinutesArrow;
    [SerializeField] private RectTransform HoursArrow;
    private int ScreenWidth;

    public RectTransform draggedArrow;

    private void Start()
    {
        ResetLayout();
    }

    private void ResetLayout()
    {
        ScreenWidth = Screen.width;
        int MinSize = (int)Mathf.Min(((RectTransform)transform).rect.size.x, ((RectTransform)transform).rect.size.y);
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(MinSize, MinSize);
        MinutesArrow.sizeDelta = new Vector2(5, MinSize / 2.5f);
        HoursArrow.sizeDelta = new Vector2(10, MinSize / 5f);
    }

    private void Update()
    {
        if (ScreenWidth != Screen.width)
            ResetLayout();

        if (draggedArrow != null)
        {
            Vector3 dir = Input.mousePosition - draggedArrow.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            draggedArrow.rotation = Quaternion.Euler(0, 0, angle);

            if (Input.GetMouseButtonUp(0))
            {
                draggedArrow = null;
                OnSave();
            }
        }
    }

    private void OnEnable()
    {
        MinutesArrow.rotation = Quaternion.Euler(0, 0, -TimeWebSynchronizer.SyncTime.Minute * 6);
        HoursArrow.rotation = Quaternion.Euler(0, 0, -TimeWebSynchronizer.SyncTime.Hour * 30);
    }

    public void OnSave()
    {
        TimeWebSynchronizer.SyncTime = new DateTime(1, 1, 1, Mathf.Min(24 - (int)Mathf.Round(HoursArrow.rotation.eulerAngles.z / 30), 23), Mathf.Min(60 - (int)Mathf.Round(MinutesArrow.rotation.eulerAngles.z / 6), 59), 0);
        MinutesArrow.rotation = Quaternion.Euler(0, 0, -TimeWebSynchronizer.SyncTime.Minute * 6);
        HoursArrow.rotation = Quaternion.Euler(0, 0, -TimeWebSynchronizer.SyncTime.Hour * 30);
    }

    public void OnStartDragging(RectTransform arrow)
    {
        draggedArrow = arrow;
    }
}