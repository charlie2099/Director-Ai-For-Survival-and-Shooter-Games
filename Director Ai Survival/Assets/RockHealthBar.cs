using UnityEngine;
using UnityEngine.UI;

public class RockHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Rock _rock;

    private void Awake()
    {
        _rock = gameObject.GetComponent<Rock>();
    }

    private void Start()
    {
        //slider.gameObject.GetComponentInParent<GameObject>().SetActive(false);
    }

    private void Update()
    {
        UpdateHealthBar();

        if (_rock.GetHealth() <= _rock.GetMaxHealth())
        {
            //slider.gameObject.GetComponentInParent<GameObject>().SetActive(true);
        }
    }

    private void UpdateHealthBar()
    {
        slider.value = _rock.GetHealth();
        slider.maxValue = _rock.GetMaxHealth();
    }
}
