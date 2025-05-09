using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    float distance;

    GameObject[] ground;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;

    [Range(0.1f,0.05f)]
    public float parallaxSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        ground = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            ground[i] = transform.GetChild(i).gameObject;
            mat[i] = ground[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculated(backCount);


    }

    // Update is called once per frame
    void BackSpeedCalculated(int backCount)
    {
        for (int i =0;  i < backCount; i++)
        {
            if ((ground[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = ground[i].transform.position.z - cam.position.z;
            }
        }


        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1- (ground[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
         distance = cam.position.x - camStartPos.x;

        for (int i = 0; i <ground.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_Maintex", new Vector2(distance, 0) *speed);
        }

    }



}
