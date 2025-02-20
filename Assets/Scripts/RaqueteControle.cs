using UnityEngine;


public class MeuScript : MonoBehaviour
{
    public Vector3 meuVector;
    public float meuY;
    public float minhaVelocidade = 5f;
    public float meuLimite = 3f;
    public bool player1;
    public bool automatico;
    public Transform bolatransform;
    public static MeuScript Instance;
    

    void Start()
    {
        meuVector = transform.position;
    }

    void Update()
    {
        meuVector.y = meuY;

        transform.position = meuVector;

        if (player1 == true)
        {
            if (Input.GetKey(KeyCode.UpArrow) && meuY < meuLimite)
            {
                Debug.Log("subindo");
                meuY += minhaVelocidade * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.DownArrow) && meuY > -meuLimite)
            {
                Debug.Log("descendo");
                meuY -= minhaVelocidade * Time.deltaTime;
            }
        }
        else if (player1 == false)
        {
            if (automatico == true)
            {
                if (bolatransform.position.y >= -meuLimite && bolatransform.position.y <= meuLimite)
                {
                    meuY = bolatransform.position.y;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W) && meuY < meuLimite)
                {
                    Debug.Log("subindo");
                    meuY += minhaVelocidade * Time.deltaTime;
                }

                if (Input.GetKey(KeyCode.S) && meuY > -meuLimite)
                {
                    Debug.Log("descendo");
                    meuY -= minhaVelocidade * Time.deltaTime;
                }
            }
        }
    }
}

