using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour {
	
	public Image damageImage; //Damageimage will be called
	public float flashSpeed = 5f; //Controls the speed of the flash
	public Color32 flashColour = new Color32(255, 0, 0, 255); //Flash colour in RGB is red. 
	
	//Animator
	Animator anim;
	playerController playerMovement;
	
	bool damage;
	
	private void Awake(){
		anim = GetComponent<Animator>();
		playerMovement = GetComponent<playerController>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (Mathf.Abs(collision.relativeVelocity.y) > 18f) //If the sprite is travelling at a velocity of 18f
		{
			TakeDamage(); //The TakeDamage function is called. 
		}
	}
	
	public void TakeDamage()
	{
		damage = true; //Sets damage bool to true
		Death(); //Calls death function
	}
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		if(damage) //If damage is called
		{
			damageImage.color = flashColour; //flashColour is called. 
		}
		else
		{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime); // colour will be cleared. flashSpeed will be multiplied by deltaTime. 
        }
		damage = false;
	}
	
	void Death(){
		anim.SetTrigger("die"); // Triggers die animation
		Debug.Log("Death called"); //Inserts "Death called" into the debugger log
		
		playerMovement.enabled = false; //The player will be unable to move. 
		GameOver();
	}
	
	void GameOver(){ //The game will end. 
		print("Game Over"); //Game over will print
		StartCoroutine(Respawn()); //Respawn coroutine will start at this point. 
	}
	
	
	IEnumerator Respawn(){
		yield return new WaitForSeconds(5); // Five seconds will pass before the Respawn IEnumerator continues
		SceneManager.LoadScene("Scene", LoadSceneMode.Single); //The scene manager will load the scene from the game file
		SceneManager.UnloadSceneAsync("Scene"); // The previous scene will be killed
	}
}
