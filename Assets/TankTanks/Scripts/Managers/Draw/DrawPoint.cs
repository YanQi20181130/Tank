using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPoint : Singleton<DrawPoint> {

	// Use this for initialization

    public void DrawColor(Renderer rend, Vector2 pixelUV)
    {

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null)
            return;

        Texture2D tex = rend.material.mainTexture as Texture2D;

        pixelUV.x *= tex.width;

        pixelUV.y *= tex.height;
        Circle(tex, (int)pixelUV.x, (int)pixelUV.y,20,new Color32(170,160,40,255));

        //tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);

        //tex.Apply();
        //rend.material.mainTexture = tex;
        //Debug.Log(pixelUV);
    }

    public void Circle(Texture2D tex, int cx, int cy, int r, Color col)
    {
        int x, y, px, nx, py, ny, d;

        for (x = 0; x <= r; x++)
        {
            //Mathf.Sqrt:平方根； Mathf.Ceil：向上取整数
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
            for (y = 0; y <= d; y++)
            {
                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                tex.SetPixel(px, py, col);
                tex.SetPixel(nx, py, col);

                tex.SetPixel(px, ny, col);
                tex.SetPixel(nx, ny, col);

            }
        }
        tex.Apply();
    }
}
