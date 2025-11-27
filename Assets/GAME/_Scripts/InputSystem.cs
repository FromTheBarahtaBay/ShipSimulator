using UnityEngine;

public class InputSystem
{
    private string _horizontalKey = "Horizontal";

    public bool IsFKeyDown()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    public float GetHorizontalInputAD()
    {
        return Input.GetAxisRaw(_horizontalKey);
    }

    public float GetHorizontalInputQE()
    {
        float leftInput = (Input.GetKey(KeyCode.Q)) ? -1 : 0;
        float rightInput = (Input.GetKey(KeyCode.E)) ? 1 : 0;

        return leftInput + rightInput;
    }
}