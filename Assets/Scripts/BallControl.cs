using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BallControl : MonoBehaviour
{
    public Rigidbody2D meuRb;
    public Vector2 meuVetor;
    public float velocidade = 5f;
    public float velocidadeMaxima = 12f;
    public float foralimite = -14f;
    public Transform bolatransform;
    public AudioClip Audio;
    public int player1Score = 0;
    public int player2Score = 0;
    public int scoreMaximo = 3;
    public bool pontoMarcado = false;
    private int vencedor = 0;
    private bool modoRandomMove = false;
    
    void Start()
    {
        DirecaoBola();
        StartCoroutine(AcelerarBola());
    }

    void Update()
    {
        if (bolatransform.position.x < foralimite && !pontoMarcado)
        {
            player2Score++;  
            Debug.Log("Player 2 Score: " + player2Score);
            pontoMarcado = true;  
            VerificarVitoria();  
            ReposicionarBola();  
        }
        else if (bolatransform.position.x > -foralimite && !pontoMarcado)
        {
            player1Score++;  
            Debug.Log("Player 1 Score: " + player1Score);
            pontoMarcado = true;  
            VerificarVitoria(); 
            ReposicionarBola();  
        }
    }

    IEnumerator AcelerarBola()
    {
        while (true)
        {
            float tempoEspera = Random.Range(9f, 10f);
            yield return new WaitForSeconds(tempoEspera);

            float chance = Random.value; 

            if (chance > 0.3f) 
            {
                if (velocidade < velocidadeMaxima)
                {
                    velocidade += 1f;
                    Debug.Log("Velocidade aumentada: " + velocidade);
                }
            }
            else 
            {
                if (velocidade > 5f) 
                {
                    velocidade -= 1f;
                    Debug.Log("Velocidade diminuída: " + velocidade);
                }
            }

            modoRandomMove = true;
            Debug.Log("Random Move Ativado!");
            yield return new WaitForSeconds(Random.Range(9f, 10f));
            modoRandomMove = false;
        }
    }
    
    void ReposicionarBola()
    {
        bolatransform.position = Vector2.zero;
        DirecaoBola();
        pontoMarcado = false;  
    }
    
    void DirecaoBola()
    {
        int direcao = Random.Range(0, 4);

        if (direcao == 0) 
        {
            meuVetor.x = velocidade;
            meuVetor.y = velocidade;
        }
        else if (direcao == 1) 
        {
            meuVetor.x = -velocidade;
            meuVetor.y = velocidade;
        }
        else if (direcao == 2) 
        {
            meuVetor.x = velocidade;
            meuVetor.y = -velocidade;
        }
        else  
        {
            meuVetor.x = -velocidade;
            meuVetor.y = -velocidade;
        }

        meuRb.linearVelocity = meuVetor;
    }
    
    void VerificarVitoria()
    {
        if (player1Score >= scoreMaximo)
        {
            Debug.Log("Player 1 Venceu!");
            vencedor = 1;
            Invoke("ReiniciarJogo", 1f);
        }
        else if (player2Score >= scoreMaximo)
        {
            Debug.Log("Player 2 Venceu!");
            vencedor = 2;
            Invoke("ReiniciarJogo", 1f);
        }
    }
    
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;  
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(10, 10, 200, 50), "Player 1: " + player1Score, style);
        GUI.Label(new Rect(Screen.width - 210, 10, 200, 50), "Player 2: " + player2Score, style);
        
        if (vencedor == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2, 300, 50), "Player 1 Venceu!", style);
            
        }
        else if (vencedor == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2, 300, 50), "Player 2 Venceu!", style);
        }
        
        if (modoRandomMove)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 50, 500, 500), "Random Move Ativado!", style);
        }
    }
    
    void ReiniciarJogo()
    {
        Debug.Log("Reiniciando jogo...");
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(Audio, bolatransform.position);
        if (collision.gameObject.CompareTag("Parede"))
        {
            AjustarAngulo();
        }
    }

    void AjustarAngulo()
    {
        float anguloMinimo = 30f; 

        Vector2 direcaoAtual = meuRb.linearVelocity.normalized; 
        float angulo = Mathf.Atan2(direcaoAtual.y, direcaoAtual.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(angulo) < anguloMinimo || Mathf.Abs(angulo) > 165f)
        {
            
            float novoAngulo = (angulo > 0) ? anguloMinimo : -anguloMinimo;
            float radianos = novoAngulo * Mathf.Deg2Rad; 

            Vector2 novaDirecao = new Vector2(Mathf.Cos(radianos), Mathf.Sin(radianos)).normalized;
            meuRb.linearVelocity = novaDirecao * velocidade; 
        }
    }

}
