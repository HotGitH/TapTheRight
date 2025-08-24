using UnityEngine;

public class Target : MonoBehaviour
{
    //original script

    void Update()
    {
        if(transform.position.y <=-6f)
        {
            GameManager.instance.GameOver();
        }
    }
    private void OnMouseDown()
    {
        GameManager.instance.ScoreUp();
        Destroy(gameObject);
    }
}
