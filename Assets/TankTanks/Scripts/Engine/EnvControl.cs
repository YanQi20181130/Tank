using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvControl : MonoBehaviour {
    protected List<Transform> heroSpawnPoslist=new List<Transform>();
    protected List<Transform> enemySpawnPoslist=new List<Transform>();

    private Transform heroSpawnPos;
    private Transform enemySpawnPos;

    public Transform HeroSpawnPos
    {
        get
        {
            return heroSpawnPos;
        }

        set
        {
            heroSpawnPos = value;
        }
    }

    public Transform EnemySpawnPos
    {
        get
        {
            return enemySpawnPos;
        }

        set
        {
            enemySpawnPos = value;
        }
    }

    // Use this for initialization
    public void InitSpawnPos () {

        foreach (Transform item in transform)
        {
            if(item.CompareTag("heroSpawner"))
            {
                heroSpawnPoslist.Add(item);
            }
            else if(item.CompareTag("enemySpawner"))
            {
                enemySpawnPoslist.Add(item);
            }
        }


        int _id_hero = Random.Range(0, heroSpawnPoslist.Count);
        int _id_enemy = Random.Range(0, enemySpawnPoslist.Count);
        HeroSpawnPos = heroSpawnPoslist[_id_hero];
        EnemySpawnPos = enemySpawnPoslist[_id_enemy];

        //InitTiles();
    }
	/*
    private void InitTiles()
    {
        Transform oripos = transform.Find("Plane/base/oripos");

        Transform parent = transform.Find("Plane");

        GameObject tile = Resources.Load("prefabs/Env/Tiles/"+ GameProperty.ENVID+"/"+ "Tile_"+ GameProperty.ENVID) as GameObject;

        TileGridManager.Instance.SetTiles(10, 10, tile, oripos.position, parent);
    }
	// Update is called once per frame
	void Update () {
		
	}
    */
}
