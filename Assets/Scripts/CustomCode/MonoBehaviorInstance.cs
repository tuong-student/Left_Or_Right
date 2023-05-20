using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NOOD
{
    public class MonoBehaviorInstance <T> : AbstractMonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                T[] temps = (T[])FindObjectsOfType(typeof(T)); 

                if (temps == null)
                {
                    Debug.Log("Errorrrrr: " + typeof(T) + " not exit");
                }
                else
                {
                    if(temps.Length > 1)
                    {
                        if(instance == null)
                        {
                            instance = temps[0];
                            DontDestroyOnLoad(instance.gameObject);
                        }
                        else
                        {
                            for (int i = 1; i < temps.Length; i++)
                            {
                                Destroy(temps[i].gameObject);
                            }
                        }
                    }
                    else
                    {
                        instance = temps[0];
                    }
                }

                return instance;
            }
        }
    }
}

