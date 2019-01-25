using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour {
    public bool isDeformer = false;
	private float springForce = 0f;
	private float damping = 10f;

    private float speed=1;

	Mesh deformingMesh;
	Vector3[] originalVertices, displacedVertices;
	Vector3[] vertexVelocities;

	float uniformScale = 1f;

	void Start () {
        if(GetComponent<MeshCollider>()==null)
        {
            gameObject.AddComponent<MeshCollider>();
        }
		deformingMesh = GetComponent<MeshFilter>().mesh;
		originalVertices = deformingMesh.vertices;
		displacedVertices = new Vector3[originalVertices.Length];
		for (int i = 0; i < originalVertices.Length; i++) {
			displacedVertices[i] = originalVertices[i];
		}
		vertexVelocities = new Vector3[originalVertices.Length];

        //克隆贴图
        fddfdf(GetComponent<MeshRenderer>());

    }

    /// <summary>
    /// 克隆贴图
    /// </summary>
    /// <param name="rend"></param>
    void fddfdf(Renderer rend)
    {
        // duplicate the original texture and assign to the material
        Texture2D texture = Instantiate(rend.material.mainTexture) as Texture2D;
        rend.material.mainTexture = texture;
    }
    public void AddDeformingForce (Vector3 point, float force) {
        point = transform.InverseTransformPoint(point);
		for (int i = 0; i < displacedVertices.Length; i++) {
			AddForceToVertex(i, point, force);
		}

        StartCoroutine(UpdateMesh());

    }

    void AddForceToVertex (int i, Vector3 point, float force) {
		Vector3 pointToVertex = displacedVertices[i] - point;
		pointToVertex *= uniformScale;
		float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
		//float velocity = attenuatedForce * Time.deltaTime;
		float velocity = attenuatedForce * speed;
        vertexVelocities[i] += pointToVertex.normalized * velocity;
	}
    private IEnumerator UpdateMesh()
    {
      
        int t = 0;

        while(t<20)
        {
            uniformScale = transform.localScale.x;
            for (int i = 0; i < displacedVertices.Length; i++)
            {
                UpdateVertex(i);
            }
            deformingMesh.vertices = displacedVertices;
            deformingMesh.RecalculateNormals();
            t += 1;

            if(t>19)
            {
                UpdateMeshCollider();
            }
        }
       

        yield return null;

    }
    void UpdateVertex(int i)
    {
        Vector3 velocity = vertexVelocities[i];
        Vector3 displacement = displacedVertices[i] - originalVertices[i];
        displacement *= uniformScale;
        velocity -= displacement * springForce * Time.deltaTime;
        velocity *= 1f - damping * Time.deltaTime;

        vertexVelocities[i] = velocity;
        displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);

    }

    void UpdateMeshCollider()
    {
        Destroy(gameObject.GetComponent<MeshCollider>());

        gameObject.AddComponent<MeshCollider>();

    }

    //void Update()
    //{
    //    uniformScale = transform.localScale.x;
    //    for (int i = 0; i < displacedVertices.Length; i++)
    //    {
    //        UpdateVertex(i);
    //    }
    //    deformingMesh.vertices = displacedVertices;
    //    deformingMesh.RecalculateNormals();

    //}
}