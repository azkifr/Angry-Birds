using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    public  List<Birds> Birds;
    public List<Enemy> Enemies;
    private bool _isGameEnded = false;
    private Birds _shotBird;
    public BoxCollider2D TapCollider;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for(int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InstantiateBird(Birds[0]);
        _shotBird = Birds[0];
    }
    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if(Birds.Count > 0)
        {
            SlingShooter.InstantiateBird(Birds[0]);
            _shotBird = Birds[0];
        }           
    }
    void OnMouseUp()
    {
        if(_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if(Enemies.Count == 0)
        {
            _isGameEnded = true;
        }
    }
    public void AssignTrail(Birds bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
    }
    
}
