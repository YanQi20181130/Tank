using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestScript : BulletBase {
    public Transform tarTrans;
    public Transform m_Trans;
    public Transform firepos;
    public float distance;
    public int shellID=0;
    private bool isfire=false;
	// Use this for initialization
	void Start () {
        
	}
	void findTar()
    {
        
        tarTrans.position = m_Trans.TransformPoint(Vector3.forward * distance);
    }

    void FindTarget()
    {
       // LunchBullet(firepos, shellID, 3, Ease.InOutCirc,m_Trans,distance);
    }
	// Update is called once per frame
	void Update () {

        //Debug.Log(m_Trans.TransformPoint(tarTrans.position));
        //Debug.Log(m_Trans.InverseTransformPoint(tarTrans.position));

        if(Input.GetKeyDown(KeyCode.A))
        {
            isfire = true;

            if (isfire)
            FindTarget();

            isfire = false;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            findTar();
        }
        //       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //       RaycastHit hit;
        //       if(Physics.Raycast(ray,out hit))
        //       {
        //           Debug.Log(hit.collider.name);
        //       }
        //       else
        //       {
        //           Debug.Log("");
        //       }
    }
}
