using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float velocidade;
    public float laneVelocidade;
    public float puloDistancia;
    public float puloAltura;
    public float abaixarDistancia;
    public float velocidadeMin = 10f;
    public float velocidadeMax = 30f;

    private Animator anim;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private int atualLane = 1;
    private Vector3 verticalAlvoPosicao;
    private bool estaPulando = false;
    private float puloInicio;
    private bool estaAbaixando = false;
    private float abaixarInicio;
    private Vector3 boxColiderTamanho;
    private bool estaTocando = false;
    private Vector2 iniciandoToque;
    private UIManager uIManager;
    private int moedas;
    private int moedasTotais;
    private float pontos;
    private float maiorPontuacao;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        boxColiderTamanho = boxCollider.size;
        anim.Play("runStart");
        velocidade = velocidadeMin;
        uIManager = FindObjectOfType<UIManager>();

        if (PlayerPrefs.GetInt("MoedasGanhas") <= 0)
            PlayerPrefs.SetInt("MoedasGanhas", 0);
        if (PlayerPrefs.GetFloat("Pontuacao") <= 0)
            PlayerPrefs.SetFloat("Pontuacao", 0);

        if (SceneManager.GetActiveScene().name == "Fase1")
            PlayerPrefs.SetInt("MoedasCorridaAtual", 0);
        else
            uIManager.AtualizarMoedas(PlayerPrefs.GetInt("MoedasCorridaAtual"));
    }

    void Update()
    {
        pontos += Time.deltaTime * velocidade;
        maiorPontuacao = pontos;
        uIManager.AtualizarPontos((int)pontos);

        //Inputs para computador
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MudarLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MudarLane(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Pular();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Abaixar();
        }

        //Inputs para celular
        if(Input.touchCount == 1)
        {
            if(estaTocando)
            {
                Vector2 diff = Input.GetTouch(0).position - iniciandoToque;
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);
                if(diff.magnitude > 0.01f)
                {
                    if(Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0)
                            Abaixar();
                        else
                            Pular();
                    }
                    else
                    {
                        if (diff.x < 0)
                            MudarLane(-1);
                        else
                            MudarLane(1);
                    }

                    estaTocando = false;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                iniciandoToque = Input.GetTouch(0).position;
                estaTocando = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                estaTocando = false;
            }
        }


        //Pular e abaixar
        if (estaPulando)
        {
            float razao = (transform.position.z - puloInicio) / puloDistancia;
            if(razao >= 1)
            {
                estaPulando = false;
                anim.SetBool("Jumping", false);
            }
            else
            {
                verticalAlvoPosicao.y = Mathf.Sin(razao * Mathf.PI) * puloAltura;
            }
        }
        else
        {
            verticalAlvoPosicao.y = Mathf.MoveTowards(verticalAlvoPosicao.y, 0, 5 * Time.deltaTime);
        }

        if(estaAbaixando)
        {
            float razao = (transform.position.z - abaixarInicio) / abaixarDistancia;
            if(razao >=1f)
            {
                estaAbaixando = false;
                anim.SetBool("Sliding", false);
                boxCollider.size = boxColiderTamanho;
            }
        }


        Vector3 alvoPosicao = new Vector3(verticalAlvoPosicao.x, verticalAlvoPosicao.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, alvoPosicao, laneVelocidade * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * velocidade;
    }

    void MudarLane(int direcao)
    {
        int alvoLane = atualLane + direcao;

        if (alvoLane < 0 || alvoLane > 2)
            return;

        atualLane = alvoLane;
        verticalAlvoPosicao = new Vector3((atualLane - 1), 0, 0);
    }

    void Pular()
    {
        if(!estaPulando)
        {
            puloInicio = transform.position.z;
            anim.SetFloat("JumpSpeed", velocidade / puloDistancia);
            anim.SetBool("Jumping", true);
            estaPulando = true;
        }
    }

    void Abaixar()
    {
        if(!estaPulando && !estaAbaixando)
        {
            abaixarInicio = transform.position.z;
            anim.SetFloat("JumpSpeed", velocidade / abaixarDistancia);
            anim.SetBool("Sliding", true);
            Vector3 novoTamanho = boxCollider.size;
            novoTamanho.y = novoTamanho.y / 2;
            boxCollider.size = novoTamanho;
            estaAbaixando = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Moeda"))
        {
            moedas = PlayerPrefs.GetInt("MoedasCorridaAtual") + 1;
            uIManager.AtualizarMoedas(moedas);
            other.transform.parent.gameObject.SetActive(false);
            PlayerPrefs.SetInt("MoedasCorridaAtual", moedas);
        }

        if(other.CompareTag("Obstaculos"))
        {
            velocidade = 0;
            anim.SetBool("Dead", true);
            uIManager.gameOver.SetActive(true);
            Invoke("ChamarMenu", 5f);

            moedasTotais = PlayerPrefs.GetInt("MoedasGanhas") + moedas;
            PlayerPrefs.SetInt("MoedasGanhas", moedasTotais);

            if (maiorPontuacao > PlayerPrefs.GetFloat("Pontuacao"))
                PlayerPrefs.SetFloat("Pontuacao", maiorPontuacao);
        }

        if(other.CompareTag("Finish"))
        {
            Debug.Log("tocou");

            if (SceneManager.GetActiveScene().name == "Fase1")
            {
                if (PlayerPrefs.GetInt("MoedasCorridaAtual") >= 50)
                    RepetirOuNao.instancia.RepetirNao();
                else
                    RepetirOuNao.instancia.RepetirSim();
            }
            else if (SceneManager.GetActiveScene().name == "Fase2")
            {
                if (PlayerPrefs.GetInt("MoedasCorridaAtual") >= 120)
                    RepetirOuNao.instancia.RepetirNao();
                else
                    RepetirOuNao.instancia.RepetirSim();
            }
        }
    }  

    void ChamarMenu()
    {
        GameManager.instancia.IrMenu();
    }

    public void AumentarVelocidade()
    {
        velocidade *= 1.2f;
        if (velocidade >= velocidadeMax)
            velocidade = velocidadeMax;
    }
}
