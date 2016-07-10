using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;

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

    private Vector2 dir_up = new Vector2(0f, 1f);
    private Vector2 dir_down = new Vector2(0f, -1f);
    private Vector2 dir_left = new Vector2(-1f, 0f);
    private Vector2 dir_right = new Vector2(1f, 0f);

    private Dictionary<Vector2, bool> direction_stack;// = new Dictionary<Vector2, bool>();
    public Rigidbody2D self;

    public Camera theCamera;
    [Range(0.3f, 3f)]
    public float PlayerSpeed;
    [Range(3f, 5f)]
    public float PlayerAcceleration;

    void Start ()
    {
        _saved_position = this.transform.position;
        direction_stack = new Dictionary<Vector2, bool>();
    }
	//----------------------------------------------------------------------------------------
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

        mapWASD();
	    _saved_position = this.transform.position;
	}
    //----------------------------------------------------------------------------------------


    void mapWASD()
    {

        direction_stack[dir_left] = false;
        direction_stack[dir_right] = false;
        direction_stack[dir_up] = false;
        direction_stack[dir_down] = false;

        if (Input.GetKey("a"))
            direction_stack[dir_left] = true;
        else
            direction_stack[dir_left] = false;
        if (Input.GetKey("d"))
            direction_stack[dir_right] = true;
        else
            direction_stack[dir_right] = false;

        MovePlayerBody();

    }

    void MovePlayerBody()
    {
        var _sum_dir = new Vector2(0f, 0f);
        foreach (Vector2 _key in direction_stack.Keys)
            if (direction_stack[_key])
                _sum_dir += _key;

        var GoalVector = _sum_dir.normalized * PlayerSpeed;
        var acceleration_direction = GoalVector - self.velocity;
        acceleration_direction.y = 0f;

        Debug.Log(acceleration_direction);

        acceleration_direction.Normalize();

        if (GAnalytics.EquifyDZ(GoalVector, self.velocity + (acceleration_direction * PlayerAcceleration), PlayerAcceleration) != self.velocity)
        {
            self.AddForce(acceleration_direction * PlayerAcceleration);
        }
        else
            self.velocity = GoalVector;

    }
}
