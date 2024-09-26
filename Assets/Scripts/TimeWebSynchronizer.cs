using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeWebSynchronizer : MonoBehaviour
{
    private class YandexTimeJSON
    {    
        public long time;

        public DateTime ToDateTime()
        {
            return DateTime.UnixEpoch.AddMilliseconds(time);
        }
    }

    private const string ServerUri = "yandex.com/time/sync.json";

    public static DateTime SyncTime { get; set; } = DateTime.UtcNow;

    void Start()
    {
        Synchronize();
        StartCoroutine(SyncTimer());
    }

    public void Synchronize()
    {
        StartCoroutine(GetTime(ServerUri, 3));
    }

    IEnumerator GetTime(string uri, int attempts)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                SyncTime = JsonUtility.FromJson<YandexTimeJSON>(webRequest.downloadHandler.text).ToDateTime();
                Debug.Log("Time synchronizeded from '" + uri + "': " + SyncTime);
            }
            else
            {
                Debug.LogError(uri + " Error: " + webRequest.error);
                yield return new WaitForSeconds(5);
                if (attempts > 0)
                    StartCoroutine(GetTime(uri, attempts - 1));
            }
        }
    }

    private IEnumerator SyncTimer()
    {
        yield return new WaitForSeconds(3600);
        Synchronize();
        StartCoroutine(SyncTimer());
    }

    public void Update()
    {
        SyncTime = SyncTime.AddSeconds(Time.deltaTime);
    }
}