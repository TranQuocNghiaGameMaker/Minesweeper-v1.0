using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/InputSystem", order = 1)]
public class InputSystemSO : ScriptableObject
{
    public bool Click => Input.GetMouseButtonDown(0);
    public bool RightClick => Input.GetMouseButtonDown(1);
    public bool RestartButton => Input.GetKeyDown(KeyCode.R);
    public bool doubleClick => DoubleClick();

    float lastClickTime;
    private bool DoubleClick()
    {
        const float DOUBLE_CLICK_TIME = 0.2f;
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            if(timeSinceLastClick <= DOUBLE_CLICK_TIME)
            {
                return true;
            }
            lastClickTime = Time.time;
        }
        return false;
    }
}
