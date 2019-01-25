using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class TileGridManager : DDOLSingleton<TileGridManager> {
    const int TILE_LENGTH=12;

    private void Start()
    {
        //SetTiles(countX, countZ, tile, pos.position);
    }
    public void SetTiles(int countX, int countZ, GameObject _tile, Vector3 oriPos,Transform parent)
    {

        countX = EvenNumbers(countX);
        countZ = EvenNumbers(countZ);

        List<GameObject> allTile = new List<GameObject>();

        int tileNum = countX * countZ;

        for (int i = 0; i < tileNum; i++)
        {
            allTile.Add(Instantiate(_tile));
            allTile[i].name = "tile_"+i;
            allTile[i].transform.SetParent(parent, false);
            TileControl.Instance.ShowChild(allTile[i].transform);
        }

        SetPos(countX, countZ, allTile, oriPos);
    }

    /// <summary>
    /// 从左下角
    /// </summary>
    /// <param name="countX"></param>
    /// <param name="countZ"></param>
    /// <param name="allTile"></param>
    /// <param name="oriPos"></param>
    private void SetPos(int countX, int countZ, List<GameObject> allTile, Vector3 oriPos)
    {


        float halfTile = TILE_LENGTH / 2;
        for (int z = 0; z < countZ; z++)
        {
            float perDisZ = z * TILE_LENGTH;

            for (int i = 0; i < countX; i++)
            {
                float perDisX = i * TILE_LENGTH;
                allTile[0].transform.position = new Vector3((oriPos.x + halfTile + perDisX), Random.Range(-1.1f,0f), (oriPos.z + halfTile + perDisZ));
                allTile.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// 取偶数
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private int EvenNumbers(int i)
    {
        if(i%2==0)
        {
            return i;
        }
        else
        {
            return i + 1;
        }
    }





}
