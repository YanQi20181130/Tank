using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestScript : BulletBase {
    public float force = 500f;
    public float forceOffset = 5f;
    void Start () {
        
	}

    private void OnCollisionEnter(Collision collision)
    {
     

        HandleInput(collision);

        foreach (ContactPoint cp in collision.contacts)
        {
            Debug.Log(cp.point);

            RaycastHit hit;
            float rayLength = 100f;
            Ray ray = new Ray(  transform.position, Vector3.down);

            Debug.Log(ray);
            Debug.Log(cp.otherCollider.name);

            if (Physics.Raycast(ray, out hit, rayLength))
            {


                Debug.Log(hit.textureCoord+" , dfsdfadf");

                DrawPoint.Instance.DrawColor(collision.collider.GetComponent<MeshRenderer>(), hit.textureCoord);

                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("hit notion");

            }
        }
        

       // gameObject.SetActive(false);
    }

    void HandleInput(Collision collision)
    {
 
            MeshDeformer deformer = collision.collider.GetComponent<MeshDeformer>();
            if (deformer)
            {
                Vector3 point = collision.contacts[0].point;
                point += collision.contacts[0].normal * forceOffset;
                deformer.AddDeformingForce(point, force);
            }
        
    }


  
}
