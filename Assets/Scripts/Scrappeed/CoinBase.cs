using UnityEngine;

public abstract class CoinBase : MonoBehaviour
{
    int multiplier;
    //public abstract void applyPowerUpAndCollectCoint(PlayerMovement player);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if(player!= null)
            {
                //applyPowerUpAndCollectCoint(player);
                Destroy(gameObject);
            }
        }
    }
}
