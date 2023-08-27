using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Core core;

    public static Sun Instance;

    public bool canDraw = true;

    private void Awake()
    {
        Instance = this;

    }

    private void OnMouseDown()
    {
        if (!canDraw) return;
        DrawWithMouse.Instance.StartLine(transform.position);
    }

    private void OnMouseDrag()
    {
        if (!canDraw) return;
        DrawWithMouse.Instance.Updateline();
    }

    private void OnMouseUp()
    {
        if (!canDraw) return;
        core.positions = new Vector3[DrawWithMouse.Instance.line.positionCount];
        DrawWithMouse.Instance.line.GetPositions(core.positions);
        core.canMove = true;
        DrawWithMouse.Instance.ResetLine();

    }
}
