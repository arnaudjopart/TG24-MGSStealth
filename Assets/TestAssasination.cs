using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TestAssasination : MonoBehaviour, IInteractable
{

    public Transform m_player;
    [SerializeField] private bool _launchSequence;
    public PlayableDirector _controlPlayable;
    private GameObject _timeLineContainer;
    [SerializeField] private GameObject _sign;

    public KeyCode ActionKey => KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {
        var timeline = _controlPlayable.playableAsset as TimelineAsset;

        var track = timeline.GetOutputTrack(2);
        Debug.Log(track.name);

        _controlPlayable.SetGenericBinding(track, m_player.GetComponent<Animator>());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        _timeLineContainer = new GameObject("Sequence");
        _timeLineContainer.transform.position = transform.position;
        _timeLineContainer.transform.rotation = transform.rotation;

        m_player.transform.SetParent(_timeLineContainer.transform);
        transform.SetParent(_timeLineContainer.transform);
        _controlPlayable.enabled = true;
        _controlPlayable.Play();
    }
    public void CloseInteraction()
    {
        var playerPosition = m_player.transform.position;
        var playerRotation = m_player.transform.rotation;

        var position = transform.position;
        var rotation = transform.rotation;

        m_player.transform.SetParent(null);
        m_player.transform.position = playerPosition;
        m_player.transform.rotation = playerRotation;
        /*
        transform.SetParent(null);
        transform.position = position;
        transform.rotation = rotation;*/

        //Destroy(_timeLineContainer);
    }

    public void InitInteraction()
    {
        _sign.SetActive(true);
    }

    public void CancelInteraction()
    {
        _sign.SetActive(false);
    }
}
