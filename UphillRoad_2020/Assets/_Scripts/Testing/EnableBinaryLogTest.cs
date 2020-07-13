using UnityEngine;
using System.Collections;
using UnityEngine.Profiling;

public class EnableBInaryLogTest : MonoBehaviour
{
    void Start()
    {
        Profiler.logFile = "mylog"; //Also supports passing "myLog.raw"
        Profiler.enableBinaryLog = true;
        Profiler.enabled = true;

        // Optional, if more memory is needed for the buffer
        Profiler.maxUsedMemory = 256 * 1024 * 1024;

        // ...
    }
}
