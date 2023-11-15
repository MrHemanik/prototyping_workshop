using System.Collections;
using System.Linq;
using Class;
using UnityEngine;
using UnityEngine.EventSystems;

public class Planet : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public float size;
    public Resource[] Resources;
    private PlanetUIManager _planetUiManager;
    private PlayerManager _playerManager;
    private AudioManager _audioManager;


    public void SetPlanetData(float sizeIn, Resource[] resourcesIn, PlanetUIManager puim, PlayerManager pm, AudioManager am)
    {
        size = sizeIn;
        Resources = resourcesIn;
        _planetUiManager = puim;
        _playerManager = pm;
        _audioManager = am;
        transform.localScale = new Vector3(size, size, size);

        
        Resources = Resources.OrderBy(r => r.Amount).Reverse().ToArray();
        
        
        GetComponent<MeshRenderer>().material.color = Resource.GetResourceColor(resourcesIn[0].rt);
    }

    // ------------------ Unity MouseEvents --------------------

    public void OnPointerClick(PointerEventData eventData)
    {
        var position = transform.position + size * Vector3.up;
        _playerManager.onPlanetClick.Invoke(Resources, position);
        Resources = new Resource[]{};
        _audioManager.PlayTravelSound();
        StartCoroutine(FadeColor());
    }

    public IEnumerator FadeColor()
    {
        //slowly fades the material color from the original to 0.7f,0.7f,0.7f
        var material = GetComponent<MeshRenderer>().material;
        var originalColor = material.color;
        var targetColor = new Color(0.7f,0.7f,0.7f);
        var t = 0f;
        while (t < 1)
        {
            yield return new WaitForSeconds(0.05f);
            t += 0.05f;
            material.color = Color.Lerp(originalColor, targetColor, t);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var position = transform.position + size * Vector3.up;
        _planetUiManager.onPlanetHoverEnter.Invoke(Resources, eventData.position, position, _playerManager.fuelEfficiency);
        _playerManager.onPlanetHoverEnter.Invoke(size, position);
        _audioManager.PlayHoverSound(1-(size-1)/6);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _planetUiManager.onPlanetHoverExit.Invoke();
        //_playerUIManager.onPlanetHoverExit.Invoke();
    }
}