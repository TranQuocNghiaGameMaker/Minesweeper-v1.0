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
    

    private bool DoubleClick()
    {
        float clicked = 0;
        float clicktime = 0;
        float clickdelay = 0.5f;
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }
}
