package international.redpine.example;

import android.os.Handler;
import android.os.Looper;
import android.util.Log;

public class JvmExample {

    //borrowed from https://github.com/bugsnag/bugsnag-unity/blob/next/example/android-lib-proj/android-lib/src/main/java/com/example/lib/BugsnagCrash.java#L13
    public void throwJvmException() {
        Log.d("UnityMonitoringDatadog", "throwJvmException in Java");
        throw new RuntimeException("Uncaught JVM exception");
    }

    //borrowed from https://github.com/bugsnag/bugsnag-unity/blob/next/example/android-lib-proj/android-lib/src/main/java/com/example/lib/BugsnagCrash.java#L17
    public void throwBackgroundJvmException() {
        Log.d("UnityMonitoringDatadog", "throwBackgroundJvmException in Java");
        new Thread(new Runnable() {
            public void run() {
                throw new RuntimeException("Uncaught JVM exception from background thread");
            }
        }).start();
    }

    //borrowed from https://github.com/bugsnag/bugsnag-unity/blob/next/example/android-lib-proj/android-lib/src/main/java/com/example/lib/BugsnagCrash.java#L25
    public void triggerAnr() {
        Log.d("UnityMonitoringDatadog", "triggerAnr in Java");
        Handler handler = new Handler(Looper.getMainLooper());
        handler.post(new Runnable() {
            @Override
            public void run() {
                try {
                    Thread.sleep(10000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        });
    }
}