using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerTestWithEnumeratorPasses()
    {
        var obj = new GameObject();
        var player = obj.AddComponent<PlayerScripts>();
        player.points.Add(new Vector3(0, 1, 0));
        player.points.Add(new Vector3(6, 1, 0));
        player.points.Add(new Vector3(6, 4.5f, 0));

        player.nextPoint = player.points[0];
        player.MoveToTheNextPoint();
        yield return new WaitForSeconds(2f);

        Assert.AreEqual(player.points[+1], player.nextPoint);
    }
}
