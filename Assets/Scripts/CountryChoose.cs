using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]   // ensures a 2D collider exists
public class CountryChoose : MonoBehaviour, IPointerDownHandler
{
    public int continent;

    private void Update()
    {
        // if a flag drops off the screen
        if (transform.position.y <= -6f )
            if (continent == GameManager.instance.GetConNum())
                {
                    GameManager.instance.GameOver(); //stop the game
                }
            else
            {
                Destroy(gameObject);
            }
    }

    public void OnPointerDown(PointerEventData e)
    {
        HandleTap();
    }

    void HandleTap()
    {
        int screenContinent = GameManager.instance.GetConNum();  // get the current target now
        Debug.Log($"Tapped {name} | mine:{continent} target:{screenContinent}");

        if (continent == screenContinent)
            GameManager.instance.ScoreUp();//when you touch the country it ups the score
        else
            GameManager.instance.ScoreDown();//when you touch the country it declines the score

        Destroy(gameObject); // and destroys the country
    }
}
