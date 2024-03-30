using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isUserActionsDisabled;

    public void DisableUserActions()
    {
        isUserActionsDisabled = true;
    }

    public void EnableUserActions()
    {
        isUserActionsDisabled = false;
    }

    public bool IsUserActionsDisabled()
    {
        return isUserActionsDisabled;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
