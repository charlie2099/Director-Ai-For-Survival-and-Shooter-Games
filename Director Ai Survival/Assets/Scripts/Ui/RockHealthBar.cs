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
        slider.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateHealthBar();

        if (_rock.GetHealth() < _rock.GetMaxHealth())
        {
            slider.transform.parent.gameObject.SetActive(true);
        }
    }

    private void UpdateHealthBar()
    {
        slider.value = _rock.GetHealth();
        slider.maxValue = _rock.GetMaxHealth();
    }
}
