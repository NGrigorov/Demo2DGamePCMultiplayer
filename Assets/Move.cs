using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Move : NetworkBehaviour
{
    private float speed = 45f, horMove = 0f;
    bool jump = false, crouch = false;
    public CharacterController2D myChar;
    public Animator myAnimator;
    public Joystick joystick;
    public GameObject tmpHealth;
    public GameObject healthUI;
    public GameObject myCamera;
    private int healthPoints = 100;
    public GameObject flashLiht, ambientLight;
    // Update is called once per frame
    [SyncVar(hook = "OnSetScale")] private Vector3 scale;
    private void Start()
    {
        if (isLocalPlayer == true)
        {
            healthPoints = 100;
            healthUI = GameObject.FindGameObjectWithTag("heath");
            tmpHealth = GameObject.FindGameObjectWithTag("Coin");
            myCamera = GameObject.FindGameObjectWithTag("myCamera");
            myCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = this.transform;
        }
    }

    void Update()
    {

        if (isLocalPlayer == true)
        {

            horMove = Input.GetAxis("Horizontal") * speed;
            tmpHealth.GetComponent<TextMeshProUGUI>().text = healthPoints.ToString();
            healthUI.GetComponent<Slider>().value = healthPoints;

            //if (joystick.Horizontal >= .2f)
            //{
            //    horMove = speed;
            //}else if (joystick.Horizontal <= -.2f)
            //{
            //    horMove = -speed;
            //}
            //else
            //{
            //    horMove = 0f;
            //}



            //float verticalMove = joystick.Vertical;

            //if(verticalMove >= .5f)
            //{
            //    jump = true;
            //    myAnimator.SetBool("isFlying", true);
            //}

            //if(verticalMove <= -.5f)
            //{
            //    crouch = true;
            //}
            //else
            //{
            //    crouch = false;
            //}
            CmdSetScale(this.transform.localScale);
            OnSetScale(this.transform.localScale);
            myAnimator.SetFloat("Speed", Mathf.Abs(horMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                myAnimator.SetBool("isFlying", true);
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                myAnimator.SetTrigger("isAttacking");
                //myAnimator.ResetTrigger("isAttacking");
            }

            if (Input.GetButtonDown("flashLight"))
            {
                flashLiht.SetActive(!flashLiht.activeSelf);
                ambientLight.SetActive(!ambientLight.activeSelf);
                //ambientLight.enabled = !ambientLight.enabled;
            }
        }
    }

    public void OnLanding()
    {
        myAnimator.SetBool("isFlying", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        myAnimator.SetBool("isCrouching", isCrouching);
    }

    private void FixedUpdate()
    {
        myChar.Move(horMove * Time.fixedDeltaTime,crouch, jump);
        jump = false;
        
    }

    public bool getHit()
    {
        healthPoints -= 12;

        if(healthPoints < 0)
        {
            return true;
        }
        return false;
    }

    [Command]
    public void CmdSetScale(Vector3 vec)
    {
        this.scale = vec; // This is just to trigger the call to the OnSetScale while encapsulating.
    }

    private void OnSetScale(Vector3 vec)
    {
        this.scale = vec;
        this.transform.localScale = vec;
    }
}
