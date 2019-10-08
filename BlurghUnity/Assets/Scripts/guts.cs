using UnityEngine;

public class guts : MonoBehaviour
{
    public float ScrollX = 0.5f;
    public float ScrollY = 0.5f;


    // Update is called once per frame
    void Update()
    {
        float offsetX = Time.time * ScrollX;
        float offsetY = Time.time * ScrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);

    }
}
