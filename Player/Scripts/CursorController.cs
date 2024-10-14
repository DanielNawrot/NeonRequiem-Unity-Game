using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D reticleTexture;
    public Vector2 hotspot;
    public CursorMode cursorMode = CursorMode.Auto;
    // Start is called before the first frame update
    void Start()
    {
        hotspot = new Vector2(reticleTexture.width / 2, reticleTexture.height / 2);
        Cursor.SetCursor(reticleTexture, hotspot, cursorMode);
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
