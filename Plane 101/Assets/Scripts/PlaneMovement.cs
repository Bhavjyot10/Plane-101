using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class PlaneMovement : MonoBehaviour
{
    public float lerpDuration = 1f;
    private Quaternion targetLeft;
    private Quaternion targetRight;
    private Quaternion orignal;
    bool isTouch = false;
    float horizontalInput;
    public Transform planeObject;
    public float speed = 1f;
    public float steerSpeed = 1f;
    public float rotationSpeed = 1f;
    private vfxManager vfxManager;
    private ScreenShake screenShake;
    public AudioSource propellerSound;
    public AudioSource explosionSound;
    public AudioSource diamondSound;
    private int score;
    private int diamonds;
    private uiManager uiManager;
    private Vector3 lastDistPos;
    public float distanceToAddScore;
    public GameObject GameOverPanel;
    public GameObject PauseBtn;
    // Start is called before the first frame update
    void Start()
    {
        orignal = planeObject.rotation;
        targetRight = Quaternion.Euler(planeObject.eulerAngles.x + 45f, planeObject.eulerAngles.y + 15f, planeObject.eulerAngles.z);
        targetLeft = Quaternion.Euler(planeObject.eulerAngles.x -45f, planeObject.eulerAngles.y - 15f, planeObject.eulerAngles.z);
        vfxManager = GameObject.Find("VFX Manager").GetComponent<vfxManager>();
        screenShake = GameObject.Find("Shake Manager").GetComponent<ScreenShake>();
        uiManager = GameObject.Find("UI Manager").GetComponent<uiManager>();
        lastDistPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -75f, 105f), transform.position.y, transform.position.z);
        transform.Translate(new Vector3(horizontalInput * steerSpeed,0,1 * speed) * Time.deltaTime * 2f);

        if(Vector3.Distance(lastDistPos, transform.position) > distanceToAddScore)
        {
            score += 1;
            uiManager.UpdateScore(score);
            lastDistPos = transform.position;
        }

        if (Input.touchCount > 0)
        {
            if (!IsPointerOverUIObject())
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    isTouch = true;
                }

                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        planeObject.rotation = Quaternion.Lerp(planeObject.rotation, targetLeft, Time.deltaTime * rotationSpeed);
                        horizontalInput = -1f;
                    }

                    else
                    {
                        planeObject.rotation = Quaternion.Lerp(planeObject.rotation, targetRight, Time.deltaTime * rotationSpeed);
                        horizontalInput = 1f;
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    horizontalInput = 0f;
                    isTouch = false;
                }
            }
        }

        if(!isTouch)
        {
            planeObject.rotation = Quaternion.Lerp(planeObject.rotation, orignal, Time.deltaTime * rotationSpeed);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Building")
        {
            enabled = false;
            vfxManager.SpawnVFX(0, planeObject);
            screenShake.ShakeScreen();
            planeObject.GetComponent<MeshRenderer>().enabled = false;
            MeshRenderer[] meshes = planeObject.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer mesh in meshes) {
                if(mesh != null)
                {
                    mesh.enabled = false;
                }
            }
            propellerSound.Stop();
            explosionSound.Play();
            GameOverPanel.SetActive(true);
            PauseBtn.SetActive(false);
            uiManager.UpdateGameOverScore(score, diamonds);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            diamonds += 1;
            uiManager.UpdateDiamonds(diamonds);
            diamondSound.Play();
            vfxManager.SpawnVFX(1, other.transform.Find("VFX Spawn pt"));
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("End Point"))
        {
            
            Destroy(other.gameObject.transform.parent.gameObject, 2f);
        }
    }

    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
