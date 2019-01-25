using UnityEngine;

public class MeshDeformerInput : MonoBehaviour {
	
	public float force = 500f;
	public float forceOffset = 5f;
    public GameObject ball;
    public Transform ballpos;
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(inputRay, out hit))
            {
                Updateeee(hit);
            }
           
        }

        else if(Input.GetMouseButton(1))
        {
            HandleInput();
        }

        else if (Input.GetKeyDown(KeyCode.B))
        {
            InstanBall();
        }
	}

    void InstanBall()
    {
        Instantiate(ball, ballpos.position, ball.transform.rotation);
    }

	void HandleInput () {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(inputRay, out hit)) {
			MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
			if (deformer) {
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;
				deformer.AddDeformingForce(point, force);
			}
		}
	}

    void Updateeee(RaycastHit hit)
    {


        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
        tex.Apply();
    }
}