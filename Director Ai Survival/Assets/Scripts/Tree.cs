using Items;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Tree : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Text uiPanelText;

    [Space]
    [SerializeField] private GameObject[] itemDrops;

    private Player _player;
    private float _health;
    private float _maxHealth;
    private bool _inRange;
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        _health = 100;
        _maxHealth = _health;
    }

    private void Update()
    {
        if (_health <= 0)
        {
            Destroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            uiPanel.SetActive(true);
            uiPanelText.text = gameObject.name;
            uiPanel.transform.position = transform.position + new Vector3(0, -1.1f);
            _inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            uiPanel.SetActive(false);
            _inRange = false;
        }
    }

    private void OnMouseOver()
    {
        uiPanel.SetActive(true);
        uiPanelText.text = gameObject.name;
        uiPanel.transform.position = transform.position + new Vector3(0, -1.1f);

        if (_inRange && Input.GetKeyDown(KeyCode.Mouse0) && _player.GetItemTypeInHand() == ItemType.Type.AXE)
        {
            print("Damage");
            ApplyDamage(10);
            _player.UseEnergy(5);
            
        }
    }

    private void OnMouseExit()
    {
        uiPanel.SetActive(false);
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;
    }

    private void Destroyed()
    {
        int woodDropLootAmount = Random.Range(2, 5);
        for (int i = 0; i < woodDropLootAmount; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(transform.position.x - 1.0f, transform.position.x + 1.0f), 
                                            Random.Range(transform.position.y - 1.0f, transform.position.y + 1.0f));
            
            Instantiate(itemDrops[0], randomPos, Quaternion.identity);
        }
        
        int appleDropLootAmount = Random.Range(0, 2);
        for (int i = 0; i < appleDropLootAmount; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(transform.position.x - 1.0f, transform.position.x + 1.0f), 
                                            Random.Range(transform.position.y - 1.0f, transform.position.y + 1.0f));
            
            Instantiate(itemDrops[1], randomPos, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    
    public float GetHealth()
    {
        return _health;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }
}
