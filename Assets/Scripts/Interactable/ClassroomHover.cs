using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ClassroomHover : MonoBehaviour, IClickable
{
    public string sceneName;
    
    private SpriteRenderer _sprite;
    private int _defaultOrder;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();

        _defaultOrder = _sprite.sortingOrder;
    }

    private void OnMouseEnter()
    {
        if (_sprite)
        {
            _sprite.color = new Color(.9f, .9f, .9f, 1);
            _sprite.sortingOrder = 5;
        }
    }

    private void OnMouseExit()
    {
        if (_sprite)
        {
            _sprite.color = Color.white;
            _sprite.sortingOrder = _defaultOrder;
        }
    }

    public void Clicked()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UnClicked()
    {
        
    }
}
