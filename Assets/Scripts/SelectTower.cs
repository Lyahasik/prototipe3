using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectTower : MonoBehaviour
{
    public Camera Camera;
    public GameObject Canvas;
    public GameObject GameManager;

    public GameObject[] Towers;

    private GameObject _selectedTower;
    private gameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameManager.GetComponent<gameManager>();
    }

    void Update()
    {
        CheckClick();
        Move();
    }

    void Move()
    {
        if (_selectedTower != null)
        {
            _selectedTower.transform.position = Camera.ScreenToWorldPoint(Input.mousePosition);
            _selectedTower.transform.position = new Vector3(_selectedTower.transform.position.x, _selectedTower.transform.position.y, 0);
        }
    }
    
    void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(CastVector.CastToVector2(mousePosition), Vector3.forward, 0.01f);

            if (hit
                && hit.collider.gameObject.CompareTag("towerUI")
                && hit.collider.gameObject.transform.parent.GetComponent<LoadTowerData>().IsActive())
            {
                _selectedTower = Instantiate(hit.collider.gameObject, Canvas.transform);
            }
        }

        if (_selectedTower != null
            && Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            CheckHits(Physics2D.RaycastAll(CastVector.CastToVector2(mousePosition), Vector3.forward, 0.01f));
            
            Destroy(_selectedTower);
        }
    }

    void CheckHits(RaycastHit2D[] hits)
    {
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("empty"))
                {
                    if (_selectedTower.name == "Canon(Clone)")
                    {
                        _gameManager.playerEnergy -= Towers[0].GetComponent<towerScript>().energy;
                        Instantiate(Towers[0], hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
                    }
                    else if (_selectedTower.name == "Gatling(Clone)")
                    {
                        _gameManager.playerEnergy -= Towers[1].GetComponent<towerScript>().energy;
                        Instantiate(Towers[1], hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
                    }
                    else if (_selectedTower.name == "Rocket(Clone)")
                    {
                        _gameManager.playerEnergy -= Towers[2].GetComponent<towerScript>().energy;
                        Instantiate(Towers[2], hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
                    }

                    hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }
}
