using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public GameObject bullet;
	public float lineal_speed = 0.1f;
	public float rotation_speed = 100.0f;
	public float base_bullet_speed = 0.15f;
	public float rof = 0.1f;

	private Rigidbody2D rb;
	private float rotation;
	private float shot_cd;

	#if UNITY_ANDROID

	public JoystickScript LeftJoystick, RightJoystick;

	#endif

	// Use this for initialization
	void Start ()
	{
		rotation = 0.0f;
		transform.rotation = Quaternion.identity;
		shot_cd = 0.0f;
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
		#if UNITY_ANDROID

		if (shot_cd >= rof) {
			GameManager.instance.AudioManager.PlaySFX ("PlayerShot");  
			GameObject temp_bullet = Instantiate (bullet, transform.position - transform.up / 2.5f, Quaternion.Euler (0.0f, 0.0f, rotation));
			temp_bullet.GetComponent<Bullet> ().direction = -transform.up;
			temp_bullet.GetComponent<Bullet> ().speed = base_bullet_speed + lineal_speed;
			temp_bullet.GetComponent<Bullet> ().UpdateVelocity ();
			shot_cd = 0.0f;
		}

		if (Input.touchCount > 0) {
			Vector3 touchDirection = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position;
			rb.velocity = new Vector2 (touchDirection.x, touchDirection.y).normalized * lineal_speed * Time.deltaTime;

			float current_angle = Mathf.Atan2 (touchDirection.x, -touchDirection.y);

			current_angle = Mathf.Rad2Deg * current_angle;

			rotation = transform.rotation.eulerAngles.z;
			transform.rotation = Quaternion.RotateTowards (Quaternion.Euler (0.0f, 0.0f, rotation), Quaternion.Euler (0.0f, 0.0f, current_angle), rotation_speed * Time.deltaTime);
		
		}else{
			rb.velocity = Vector2.zero;
		}

		if (shot_cd < rof)
			shot_cd += Time.deltaTime;

		/*

		Vector2 LeftDirection = LeftJoystick.GetDirection();
		Vector2 RightDirection = RightJoystick.GetDirection();

		float leftHorizontal = LeftDirection.x;
		float leftVertical = LeftDirection.y;

		float rightHorizontal = RightDirection.x;
		float rightVertical = -RightDirection.y;

		float leftMagnitude = new Vector2 (leftHorizontal, leftVertical).magnitude;
		float rightMagnitude = new Vector2 (rightHorizontal, rightVertical).magnitude;

		if (transform.position.y > 4.4f) {
			transform.position = new Vector3 (transform.position.x, 4.4f, transform.position.z);
			rb.velocity = new Vector2 (0.0f, 0.0f);
		}

		if (leftMagnitude > 0.1f) {

			//transform.Translate(-transform.up * lineal_speed * magnitude * Time.deltaTime, Space.World);
			rb.velocity = new Vector2 (leftHorizontal, leftVertical).normalized * lineal_speed * leftMagnitude * Time.deltaTime;

		} else {
			rb.velocity = new Vector2 (0, 0);
		}

		if (rightMagnitude > 0.05f) {

			float current_angle = Mathf.Atan2 (rightHorizontal, rightVertical);

			current_angle = Mathf.Rad2Deg * current_angle;

			rotation = transform.rotation.eulerAngles.z;
			transform.rotation = Quaternion.RotateTowards (Quaternion.Euler (0.0f, 0.0f, rotation), Quaternion.Euler (0.0f, 0.0f, current_angle), rotation_speed * Time.deltaTime * rightMagnitude);

			if (shot_cd >= rof) {
				GameManager.instance.AudioManager.PlaySFX ("PlayerShot");  
				GameObject temp_bullet = Instantiate (bullet, transform.position - transform.up / 2.5f, Quaternion.Euler (0.0f, 0.0f, rotation));
				temp_bullet.GetComponent<Bullet> ().direction = -transform.up;
				temp_bullet.GetComponent<Bullet> ().speed = base_bullet_speed + lineal_speed * leftMagnitude;
				temp_bullet.GetComponent<Bullet> ().UpdateVelocity ();
				shot_cd = 0.0f;
			}

			if (shot_cd < rof)
				shot_cd += Time.deltaTime;

		} */

		#else

	    float leftHorizontal = Input.GetAxis("Horizontal");
	    float leftVertical = -Input.GetAxis("Vertical");
		  float rightHorizontal = Input.GetAxis("MouseX");
		  float rightVertical = -Input.GetAxis("MouseY");
	    float strafe = Input.GetAxis("Strafe");
	    bool shooting = Input.GetKey("joystick button 2");
	    if(shooting == false) shooting = Input.GetKey("space");

    if(transform.position.y > 4.4f) {
      transform.position = new Vector3(transform.position.x,4.4f,transform.position.z);
      rb.velocity = new Vector2(0.0f,0.0f);
    }

	    //float magnitude = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
		float leftMagnitude = new Vector2(leftHorizontal, leftVertical).magnitude;
		float rightMagnitude = new Vector2(rightHorizontal, rightVertical).magnitude;

		if (Input.GetMouseButton (0) && GameManager.instance.game_state != GameManager.GameState.PauseMenu){
			
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePos = new Vector3 (mousePos.x, mousePos.y, 0);
			transform.up = -mousePos + transform.position;

			rotation = transform.rotation.eulerAngles.z;

			if (shot_cd >= rof) {
        GameManager.instance.AudioManager.PlaySFX("PlayerShot");  
				GameObject temp_bullet = Instantiate(bullet, transform.position - transform.up/2.5f, Quaternion.Euler(0.0f, 0.0f, rotation));
				temp_bullet.GetComponent<Bullet>().direction = -transform.up;
				temp_bullet.GetComponent<Bullet>().speed = base_bullet_speed + lineal_speed * leftMagnitude;
				temp_bullet.GetComponent<Bullet>().UpdateVelocity();
				shot_cd = 0.0f;
			}

			if(shot_cd < rof) shot_cd += Time.deltaTime;
		}

		if (leftMagnitude > 0.1f) {

			//transform.Translate(-transform.up * lineal_speed * magnitude * Time.deltaTime, Space.World);
			rb.velocity =  new Vector2(leftHorizontal, -leftVertical).normalized * lineal_speed * leftMagnitude * Time.deltaTime;

		} else {
			rb.velocity = new Vector2 (0, 0);
		}

		if (rightMagnitude > 0.05f) {

			float current_angle = Mathf.Atan2(rightHorizontal,-rightVertical);
			current_angle = Mathf.Rad2Deg * current_angle;

			rotation = transform.rotation.eulerAngles.z;
			transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0.0f,0.0f,rotation),Quaternion.Euler(0.0f,0.0f,current_angle),rotation_speed * Time.deltaTime * rightMagnitude);
		
			if (shot_cd >= rof) {
        GameManager.instance.AudioManager.PlaySFX("PlayerShot");  
				GameObject temp_bullet = Instantiate(bullet, transform.position - transform.up/2.5f, Quaternion.Euler(0.0f, 0.0f, rotation));
				temp_bullet.GetComponent<Bullet>().direction = -transform.up;
				temp_bullet.GetComponent<Bullet>().speed = base_bullet_speed + lineal_speed * leftMagnitude;
				temp_bullet.GetComponent<Bullet>().UpdateVelocity();
				shot_cd = 0.0f;
			}

			if(shot_cd < rof) shot_cd += Time.deltaTime;
		
		}

		#endif
	}
}
	
