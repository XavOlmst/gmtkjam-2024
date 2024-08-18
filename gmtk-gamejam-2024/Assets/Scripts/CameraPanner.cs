using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanner : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float xPadding;
    [SerializeField] private float yPadding;
    private Vector2 maxPixels;
    private Vector2 minPixels;

    // Start is called before the first frame update
    void Start()
    {
        minPixels = new(xPadding, yPadding);
        maxPixels = new(Camera.main.pixelWidth - xPadding, Camera.main.pixelHeight - yPadding);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetGameState() == GameState.RUNNING)
        {
            Vector3 moveDir = new();

            if (Input.mousePosition.x > maxPixels.x)
            {
                moveDir += Vector3.right;
            }
            else if (Input.mousePosition.x < minPixels.x)
            {
                moveDir -= Vector3.right;
            }

            if (Input.mousePosition.y > maxPixels.y)
            {
                moveDir += Vector3.up;
            }
            else if (Input.mousePosition.y < minPixels.y)
            {
                moveDir -= Vector3.up;
            }

            transform.position += moveDir.normalized * cameraSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z), Time.deltaTime * 10.0f);
        }
    }
}
