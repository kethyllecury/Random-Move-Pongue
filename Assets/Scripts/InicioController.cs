using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomMovementWithRigidbody : MonoBehaviour
{
  
    public Rigidbody2D meuRb;
    public Vector2 meuVetor;
    public float velocidade = 5f;
    public Transform bolatransform;
    public AudioClip Audio;
    public Button buttonJogar;
    public Button buttonBot;
    public GameObject raquetePrefab;
  

    
 
    void Start()
    {
        DirecaoBola();
        buttonJogar.onClick.AddListener(Jogar);
        buttonBot.onClick.AddListener(VS_BOT);
        
        
    }
      
        
    

    void Update()
    {
        
    }

    void Jogar()
    {
        SceneManager.LoadScene("SampleScene");
        MeuScript raquetecontrole = raquetePrefab.GetComponent<MeuScript>();
        raquetecontrole.automatico = false;
    }
    void VS_BOT()
    {
        SceneManager.LoadScene("SampleScene");
        MeuScript raquetecontrole = raquetePrefab.GetComponent<MeuScript>();
        raquetecontrole.automatico = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(Audio, bolatransform.position);
    }
    

}