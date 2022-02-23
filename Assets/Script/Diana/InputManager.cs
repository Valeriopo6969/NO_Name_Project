using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public string horizontalMoveName = "HorizontalMove";
    public string verticalMoveName = "VerticalMove";
    public string jumpButtonName = "Jump";

    public static float HORIZONTALMOVE;
    public static float VERTICALMOVE;
    public static bool JUMPBUTTON;

    private Player playerControls;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        playerControls = ReInput.players.GetPlayer(0);
    }

    // Update is called once per frame
    void Update()
    {
        HORIZONTALMOVE = playerControls.GetAxisRaw(horizontalMoveName);
        HORIZONTALMOVE = playerControls.GetAxisRaw(horizontalMoveName);

        JUMPBUTTON = playerControls.GetButtonDown(jumpButtonName);
    }
}
