﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    public CellScript parentCell;
    public GameObject tree;

    static Vector3 growthRate = new Vector3(.0005f,.0005f,.0005f);
    public ulong seed;

    public static List<Vector2> trees=new List<Vector2>();
    public float minimumDistance=10;

    public IEnumerator drop()
    {
        float grown=0;
        while(grown < 1f)
        {
            transform.Translate(0,-.1f,0);
            grown+=.1f;
            yield return new WaitForSeconds(.1f);
        }
    }

    public IEnumerator grow()
    {
        int leaves = parentCell.countLeaves();

        if(leaves>0) while(transform.localScale.x<1f)
        {
            leaves = parentCell.countLeaves();
            transform.localScale+=(growthRate * leaves);
            yield return new WaitForSeconds(.1f);
        }

        else
        {
            Destroy(gameObject);
        }

        StartCoroutine(fall());
    }

    public IEnumerator fall()
    {
        seed=parentCell.getPedigree()*(ulong)parentCell.countLeaves();
        yield return new WaitForSeconds(seed%20);
        Rigidbody r = gameObject.AddComponent<Rigidbody>();
        r.mass=transform.localScale.x;
        r.isKinematic=false;
        transform.parent = null;
    }

    public void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Ground")
        {
            Vector2 spawnPoint = new Vector2(transform.position.x,transform.position.z);
            if(safeArea(spawnPoint))
            {
                GameObject t = Instantiate(tree);
                t.transform.position=transform.position;
                TreeScript ts = t.GetComponent<TreeScript>();
                ts.seed=seed;

                trees.Add(new Vector2(t.transform.position.x,t.transform.position.z));
            }

            Destroy(gameObject);
        }
    }

    public bool safeArea(Vector2 p1)
    {
        float safeArea=float.MaxValue;

        foreach(Vector2 p2 in trees)
        {
            float tmp=Vector2.Distance(p1,p2);
            safeArea = tmp<safeArea ? tmp:safeArea;
        }

        return safeArea>minimumDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation=Quaternion.AngleAxis(0,Vector3.up);
        StartCoroutine(grow());
        StartCoroutine(drop());
    }
}