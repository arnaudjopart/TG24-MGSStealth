using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TestAssasination : MonoBehaviour
{

    public Transform m_player;
    [SerializeField]private bool _launchSequence;
    public PlayableDirector _controlPlayable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_launchSequence)
        {
            _launchSequence = false;
            var timeLineContainer = new GameObject("Sequence");
            timeLineContainer.transform.position = transform.position;
            timeLineContainer.transform.rotation = transform.rotation;

            m_player.transform.SetParent(timeLineContainer.transform);
            transform.SetParent(timeLineContainer.transform);
            _controlPlayable.enabled = true;
        }
    }
}
