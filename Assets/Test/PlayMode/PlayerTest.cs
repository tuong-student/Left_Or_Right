using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTes5t
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerTes5tWithEnumeratorPasses()
    {
        // Prepare
        GameObject obj = new GameObject();
        var player = obj.AddComponent<PlayerScripts>();
        
        // Execute

        yield return null;
    }
}
