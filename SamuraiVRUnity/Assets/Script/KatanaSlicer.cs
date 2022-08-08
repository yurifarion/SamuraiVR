using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class KatanaSlicer : MonoBehaviour
{
    //check if the katana is in hand
    public bool isGrabbed = false;
   

    //Tip and Base will feed the Slice class to create two new GameObjects from the slice. the positive up and negative down
    public GameObject _tip;
    public GameObject _base;
    private Vector3 _triggerEnterTipPosition;
    private Vector3 _triggerEnterBasePosition;
    private Vector3 _triggerExitTipPosition;
    private GameManager gm;


    //Katana Sound FX
    public AudioSource soundFXSource;
    public AudioClip unsheathedCutAudioClip;
    //after the cut this is the force that will be applyied to the object
    private float _forceAppliedToCut = 3f;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void SetGrab(bool grabStatus)
    {
        soundFXSource.clip = unsheathedCutAudioClip;
        soundFXSource.Play();
        isGrabbed = grabStatus;
    }
    private void OnTriggerEnter(Collider other)
    {
        _triggerEnterTipPosition = _tip.transform.position;
        _triggerEnterBasePosition = _base.transform.position;
    }

    // in order to know if the blade is or not cutting something is determined by trigger;
    private void OnTriggerExit(Collider other)
    {

        

        //If it cuts a bomb then it will gameOver
        if (other.gameObject.tag == "Bomb")
        {
            SceneManager.LoadScene("GameOverExplosion");
        }
        _triggerExitTipPosition = _tip.transform.position;

        //Create a triangle between the tip and base so that we can get the normal
        Vector3 side1 = _triggerExitTipPosition - _triggerEnterTipPosition;
        Vector3 side2 = _triggerExitTipPosition - _triggerEnterBasePosition;

        //Get the point perpendicular to the triangle above which is the normal
        //https://docs.unity3d.com/Manual/ComputingNormalPerpendicularVector.html
        Vector3 normal = Vector3.Cross(side1, side2).normalized;

        //Transform the normal so that it is aligned with the object we are slicing's transform.
        Vector3 transformedNormal = ((Vector3)(other.gameObject.transform.localToWorldMatrix.transpose * normal)).normalized;

        //Get the enter position relative to the object we're cutting's local transform
        Vector3 transformedStartingPoint = other.gameObject.transform.InverseTransformPoint(_triggerEnterTipPosition);

        Plane plane = new Plane();

        plane.SetNormalAndPosition(
                transformedNormal,
                transformedStartingPoint);

        var direction = Vector3.Dot(Vector3.up, transformedNormal);

        //Flip the plane so that we always know which side the positive mesh is on
        if (direction < 0)
        {
            plane = plane.flipped;
        }

        GameObject[] slices = Slicer.Slice(plane, other.gameObject);

        //Make sure that every gameObject after sliced will be destroied after 10 secs
        foreach (GameObject o in slices)
        {
            Destroy(o,10);
        }
        Destroy(other.gameObject);

        //Add score after cut
        gm.AddScore();

        Rigidbody rigidbody = slices[1].GetComponent<Rigidbody>();
        Vector3 newNormal = transformedNormal + Vector3.up * _forceAppliedToCut;
        rigidbody.AddForce(newNormal, ForceMode.Impulse);
    }
}
