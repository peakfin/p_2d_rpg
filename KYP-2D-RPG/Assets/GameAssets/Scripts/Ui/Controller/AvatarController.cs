using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {
    
    Vector3 vel = Vector3.zero;

    public bool B_Right = false;
    public bool B_Left = false;
    public bool B_Attack = false;
    public bool B_Jump = false;

    public GameObject player;

    public float JumpMaxTime;
    float JumpTime = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR_WIN
        B_Left = Input.GetKey(KeyCode.A);
        B_Right = Input.GetKey(KeyCode.D);
        B_Jump = Input.GetKey(KeyCode.Space);
        B_Attack = Input.GetKey(KeyCode.J);
#endif
        vel = Vector3.zero;
        if (B_Jump)
        {
            JumpTime += Time.deltaTime;
        }
        else
        {
            JumpTime = 0;
        }
        player.GetComponent<Avatar>().OnAttack = B_Attack ? true : false;

        vel.x = B_Right ? 10 : 0;
        if (vel.x == 0)
        {
            vel.x = B_Left ? -10 : 0;
        }

        vel.y = B_Jump ? 10 : 0;
        if (JumpTime >= JumpMaxTime || player.GetComponent<Avatar>().Vel.y < 0)
        {
            vel.y = -1f;
            JumpTime = 0;
        }
        else if (player.GetComponent<Avatar>().Vel.y > 0 && vel.y == 0)
        {
            vel.y = -1f;
            JumpTime = 0;
        }

        player.GetComponent<Avatar>().Vel = vel;

        B_Right = false;
        B_Left = false;
        B_Attack = false;
        B_Jump = false;
    }

   

    public void OnRight()
    {
        B_Right = true;
        B_Left = false;
    }

    public void OnLeft()
    {
        B_Left = true;
        B_Right = false;
    }

    public void OnAttack()
    {
        B_Attack = true;
    }

    public void OnJump()
    {
        B_Jump = true;
    }

    public void OffJump()
    {
        B_Jump = false;
    }
}
