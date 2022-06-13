using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

public class TestButtonHandler : MonoBehaviour, IPlayerSubmitHandler, IPlayerCancelHandler
{
    public void OnPlayerCancel(IPlayerInput playerInput)
    {
        Debug.Log(playerInput);
    }

    public void OnPlayerSubmit(IPlayerInput playerInput)
    {
        Debug.Log(playerInput);
    }
}
