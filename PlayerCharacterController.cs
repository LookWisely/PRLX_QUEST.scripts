using UnityEngine;
using System.Collections;

public class PlayerCharacterController : MonoBehaviour
{


    public GameObject ForegroundLayer2;
    public GameObject ForegroundLayer1;
    public GameObject BackgroundLayer1;
    public GameObject BackgroundLayer2;
    public GameObject BackgroundLayer3;
    public GameObject BackgroundLayer4;
    public GameObject BackgroundLayerSkybox;

    [Range(0.001f, 1f)]
    public float FL2;
    [Range(0.001f, 1f)]
    public float FL1;

    [Range(0.001f, 1f)]
    public float BL1;
    [Range(0.001f, 1f)]
    public float BL2;
    [Range(0.001f, 1f)]
    public float BL3;
    [Range(0.001f, 1f)]
    public float BL4;
    [Range(0.001f, 1f)]
    public float BLsb;

    private Vector2 _saved_position;
    private Vector2 _delta_buffer;




    // Use this for initialization
    void Start ()
    {
        _saved_position = this.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    _delta_buffer = (Vector2)this.transform.position - _saved_position;
	    if (_delta_buffer.magnitude > 0f)
	    {
	        ForegroundLayer2.transform.Translate((-1) * _delta_buffer * FL2);
            ForegroundLayer1.transform.Translate((-1) * _delta_buffer * FL1);

            BackgroundLayer1.transform.Translate(_delta_buffer * BL1);
            BackgroundLayer2.transform.Translate(_delta_buffer * BL2);
            BackgroundLayer3.transform.Translate(_delta_buffer * BL3);
            BackgroundLayer4.transform.Translate(_delta_buffer * BL4);
            BackgroundLayerSkybox.transform.Translate(_delta_buffer * BLsb);


        }
	}
}
