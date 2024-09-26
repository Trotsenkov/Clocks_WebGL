using UnityEngine;
using UnityEngine.UI;

public class AnalogueClock : MonoBehaviour
{
    [SerializeField] private RectTransform SecondsArrow;
    [SerializeField] private RectTransform MinutesArrow;
    [SerializeField] private RectTransform HoursArrow;
    private int ScreenWidth;

    private void Start()
    {
        ResetLayout();
    }

    private void ResetLayout()
    {
        ScreenWidth = Screen.width;
        int MinSize = (int)Mathf.Min(((RectTransform)transform).rect.size.x, ((RectTransform)transform).rect.size.y);
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(MinSize, MinSize);
        SecondsArrow.sizeDelta = new Vector2(5, MinSize / 2.5f);
        MinutesArrow.sizeDelta = new Vector2(5, MinSize / 2.5f);
        HoursArrow.sizeDelta = new Vector2(10, MinSize / 5f);
    }

    private void Update()
    {
        if (ScreenWidth != Screen.width)
            ResetLayout();

        SecondsArrow.rotation = Quaternion.Euler(0, 0, -TimeWebSynchronizer.SyncTime.Second * 6);
        MinutesArrow.rotation = Quaternion.Euler(0, 0, -TimeWebSynchronizer.SyncTime.Minute * 6);
        HoursArrow.rotation = Quaternion.Euler(0, 0, -TimeWebSynchronizer.SyncTime.Hour * 30);
    }
}