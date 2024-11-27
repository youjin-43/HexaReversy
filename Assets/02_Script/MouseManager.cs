using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] GameObject SelectedTile;
    private void Start()
    {
        SelectedTile = null;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (SelectedTile == null)
            {
                SelectTile(hit.transform.gameObject);
            }
            else
            {
                if (hit.transform.gameObject != SelectedTile)
                {
                    UnSeletTile(); 
                    SelectTile(hit.transform.gameObject);
                }
            }
          
        }
        else
        {
            //레이캐스드가 아무랑도 충돌하지 않으면
            if(SelectedTile) UnSeletTile();
        }
    }

    void SelectTile(GameObject obj)
    {
        SelectedTile = obj.transform.gameObject;
        SelectedTile.GetComponent<Outline>().enabled = true;
        Debug.Log(SelectedTile.name + "is Selected");

    }

    void UnSeletTile()
    {
        Debug.Log(SelectedTile.name + "is Selected");
        SelectedTile.GetComponent<Outline>().enabled = false;
        SelectedTile = null;
       
        
    }
}
