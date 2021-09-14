using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class papis : MonoBehaviour

{
	public float velocidade;
	public float tempopulo;
	private float tempopulado;
	private Vector3 posicaoinicial;
	private Animator anim;
	private SpriteRenderer sprite;
	public GameObject textoQtdcristais;
	private int qnt_cristais;
    // Start is called before the first frame update
    void Start()
    {
        posicaoinicial = this.transform.position;
		 anim = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
		qnt_cristais = 0;
		AtualizarHUD();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
		{
			sprite.flipX = false;
			Vector3 pos = this.transform.position;
			//pos.x = pos.x = velocidade;
			pos.x += velocidade;
			this.transform.position = pos;
		}		
		if (Input.GetKey(KeyCode.A))
			{
			sprite.flipX = true;
			Vector3 pos = this.transform.position;
			//pos.x = pos.x = velocidade;
			pos.x -= velocidade;
			this.transform.position = pos;
		}		
		if (Input.GetKey(KeyCode.W) && tempopulado <= 0)
		{
			Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
			Vector2 forca = new Vector2(0.0f, 25f);
			rb.AddForce(forca, ForceMode2D.Impulse);
			tempopulado = tempopulo;
			anim.SetBool("estaPulando", true);
		}
		tempopulado -= Time.deltaTime;
		if(this.transform.position.y <-14f) 
	    {
			this.transform.position = posicaoinicial;
		}
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
	   
	{
		anim.SetBool("estaAndando", true);
	}
     else
	{
       anim.SetBool("estaAndando", false);
	}

	}
	void OnCollisionEnter2D(Collision2D col)
	{
        if(col.gameObject.tag == "man")
        {
            this.transform.position = posicaoinicial;
        }
        if(col.gameObject.tag == "chao")
        {
            anim.SetBool("estaPulando", false);
        }
		if(col.gameObject.tag == "cristal")
		{
			qnt_cristais++;
			AtualizarHUD();
			Destroy(col.gameObject);
		}
    }

	void AtualizarHUD()
    {
		textoQtdcristais.GetComponent<Text>().text = qnt_cristais.ToString();
	}
}