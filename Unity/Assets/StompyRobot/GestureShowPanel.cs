using System.Collections.Generic;
using UnityEngine;

public class GestureShowPanel : MonoBehaviour
{
    [Header("画几圈展示Log")]
    public int numOfCircleToShow = 1;
    private readonly object _threadLock = new object();

#if !UNITY_EDITOR
    private void OnEnable()
    {
        Application.logMessageReceived -= LogBack;
        Application.logMessageReceived += LogBack;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= LogBack;
    }

    void LogBack(string condition, string stackTrace, LogType type)
    {
        lock (_threadLock)
        {
            if (type.Equals(LogType.Error) || type.Equals(LogType.Exception) || type.Equals(LogType.Assert))
            {    if (!SRDebug.Instance.IsDebugPanelVisible)
                SRDebug.Instance.ShowDebugPanel(SRDebugger.DefaultTabs.Console);
            }
        }
    }

    
    private void OnDestroy()
    {
        Application.logMessageReceived -= LogBack;
    }
#endif

    private void Update()
    {
        if (isGestureDone())
        {
            if (!SRDebug.Instance.IsDebugPanelVisible)
                SRDebug.Instance.ShowDebugPanel(SRDebugger.DefaultTabs.Console);
        }
    }

    List<Vector2> gestureDetector = new List<Vector2>();
    Vector2 gestureSum = Vector2.zero;
    float gestureLength = 0;
    int gestureCount = 0;
    bool isGestureDone()
    {
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touches.Length != 1)
            {
                gestureDetector.Clear();
                gestureCount = 0;
            }
            else
            {
                if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
                    gestureDetector.Clear();
                else if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    Vector2 p = Input.touches[0].position;
                    if (gestureDetector.Count == 0 || (p - gestureDetector[gestureDetector.Count - 1]).magnitude > 10)
                        gestureDetector.Add(p);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                gestureDetector.Clear();
                gestureCount = 0;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Vector2 p = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    if (gestureDetector.Count == 0 || (p - gestureDetector[gestureDetector.Count - 1]).magnitude > 10)
                        gestureDetector.Add(p);
                }
            }
        }

        if (gestureDetector.Count < 10)
            return false;

        gestureSum = Vector2.zero;
        gestureLength = 0;
        Vector2 prevDelta = Vector2.zero;
        for (int i = 0; i < gestureDetector.Count - 2; i++)
        {

            Vector2 delta = gestureDetector[i + 1] - gestureDetector[i];
            float deltaLength = delta.magnitude;
            gestureSum += delta;
            gestureLength += deltaLength;

            float dot = Vector2.Dot(delta, prevDelta);
            if (dot < 0f)
            {
                gestureDetector.Clear();
                gestureCount = 0;
                return false;
            }

            prevDelta = delta;
        }

        int gestureBase = (Screen.width + Screen.height) / 4;

        if (gestureLength > gestureBase && gestureSum.magnitude < gestureBase / 2)
        {
            gestureDetector.Clear();
            gestureCount++;
            if (gestureCount >= numOfCircleToShow)
                return true;
        }

        return false;
    }

}
