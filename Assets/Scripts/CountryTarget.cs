using UnityEngine;

public class CountryTarget : MonoBehaviour
{
    private void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y <= -6f)
        {
            GameManager.instance.GameOver();
        }
    }
    private void OnMouseDown()
    {
        if (1 == 1)
        {
            GameManager.instance.ScoreUp();
        }
        else
            GameManager.instance.ScoreDown();
        Destroy(gameObject);
    }

}
