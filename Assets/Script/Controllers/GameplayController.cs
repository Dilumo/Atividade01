using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    /// <summary>
    /// Simulating a database
    /// </summary>
    public List<FallThingData> fallThingsDataBase;

    private static GameplayController _instance;

    public static GameplayController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameplayController>();
            }

            return _instance;
        }
    }


    public List<SpawPoint> spawPoints;
    public List<FallThing> fallThings;

    public GameObject lostLine;


    public AudioSource sfxController;
    
    [Header("UI")]
    public Text txtPoints;
    public Text txtLife;

    private int points = 0;

    private int lifePoint = 0;


    public int countThingsAtGame;
    private bool allThingsAtGame = false;


    private void Update()
    {

        allThingsAtGame = countThingsAtGame == fallThings.Count;
        if (allThingsAtGame) return;
        SpawThing();
    }

    private void SpawThing()
    {
        SpawPoint spawPoint = GetSpawPoint();
        FallThing fallThing = GetFallThing();
        if (spawPoint == null || fallThing == null)
        {
            StartCoroutine("MyWaitSeconds", 1);
            return;
        }

        FallThingData fallThingData = fallThingsDataBase[Random.Range(0, fallThingsDataBase.Count)];

        fallThing.Spawn(fallThingData.imageThing, fallThingData.speedFall, fallThingData.points, fallThingData.sfxClicked , spawPoint.rect.position);
        spawPoint.data.spawned = true;


        countThingsAtGame++;
    }

    private FallThing GetFallThing()
    {
        return fallThings.Where(ingame => ingame.data.isAtGame == false).FirstOrDefault();
    }

    private SpawPoint GetSpawPoint()
    {
        List<SpawPoint> pawnActive =  spawPoints.Where(usable => usable.data.inCooldown == false).ToList();
        if (pawnActive.Count < 1)
        {
            return null;
        }
        else
        {
            return pawnActive[Random.Range(0, pawnActive.Count)];
        }
    }

    void Start()
    {
        foreach (var thing in fallThings)
        {
            thing.GetComponent<Button>().onClick.AddListener(() =>
            {
                ClickFallThing(thing);
            });
        }        
    }

    private void ClickFallThing(FallThing thing)
    {
        sfxController.PlayOneShot(thing.data.sfxClicked);
        thing.Colect();
        SetPoints(thing.data.points);
    }

    private void SetPoints(int value)
    {
        points += value;
        txtPoints.text = points.ToString();
        countThingsAtGame--;
    }

    IEnumerable MyWaitSeconds(int valeu)
    {
        yield return new WaitForSeconds(valeu);
        SpawThing();
    }
}
