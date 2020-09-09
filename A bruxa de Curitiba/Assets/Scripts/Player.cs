using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controle;

    public float velocidade;
    public float alturaPulo;
    private float velocidadePulo;
    public float gravidade;
    public float velocidadeHorizontal;

    public float rayRaio;
    public LayerMask layer;
    public LayerMask moedaLayer;

    private bool estaMovendoDireita;
    private bool estaMovendoEsquerda;

    public Animator animacao;
    public bool estaMorto = false;

    private GameControle gc;

    void Start()
    {
        controle = GetComponent<CharacterController>();
        gc = FindObjectOfType<GameControle>();
    }

    void Update()
    {
        Vector3 direcao = Vector3.forward * velocidade;

        if(controle.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                velocidadePulo = alturaPulo;
            }

            if(Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 3f && !estaMovendoDireita)
            {
                estaMovendoDireita = true;
                StartCoroutine(DireitaMover());
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > 3f && !estaMovendoEsquerda)
            {
                estaMovendoEsquerda = true;
                StartCoroutine(EsquerdaMover());
            }
        }
        else
        {
            velocidadePulo -= gravidade;
        }

        direcao.y = velocidadePulo;

        controle.Move(direcao * Time.deltaTime);

        EmColisao();
    }

    IEnumerator EsquerdaMover()
    {
        for(float  i = 0; i < 10; i += 0.1f)
        {
            controle.Move(Vector3.left * Time.deltaTime * velocidadeHorizontal);
            yield return null;
        }

        estaMovendoEsquerda = false;
    }

    IEnumerator DireitaMover()
    {
        for (float i = 0; i < 10; i += 0.1f)
        {
            controle.Move(Vector3.right * Time.deltaTime * velocidadeHorizontal);
            yield return null;
        }

        estaMovendoDireita = false;
    }

    void EmColisao()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRaio, layer) && !estaMorto)
        {
            animacao.SetTrigger("morte");
            velocidade = 0;
            alturaPulo = 0;
            velocidadeHorizontal = 0;

            //O Invoke faz chamar um método após um determinado tempo, para chamar, é necessário que esteja no mesmo Script
            Invoke("FimDeJogo", 3f);

            estaMorto = true;
        }

        RaycastHit moedaHit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(0,1f,0)), out moedaHit, rayRaio, moedaLayer))
        {
            gc.AddMoedas();
            Destroy(moedaHit.transform.gameObject);
        }
    }

    void FimDeJogo()
    {
        gc.MostrarFimDeJogo();
    }
}
