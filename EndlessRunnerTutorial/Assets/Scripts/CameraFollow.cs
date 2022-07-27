using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform player;

//     public float rotateSpeed = 1.2f;
    Vector3 offset;

	private void Start () {
        offset = transform.position - player.position;
	}

	private void Update () {
        Vector3 targetPos = player.position + offset;
        // targetPos.x = 0;
        transform.position = targetPos;
        // RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
	}
}