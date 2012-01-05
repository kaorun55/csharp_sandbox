// thanks by http://ufcpp.net/study/csharp/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace coroutine
{
    class MicroThread
    {
        public int x = 0;
        public int y = 0;
        IEnumerator thread;

        public MicroThread()
        {
            thread = get();
        }

        public void update()
        {
            if ( thread != null ) {
                if ( !thread.MoveNext() ) {
                    thread = null;
                }
            }
        }

        IEnumerator get()
        {
            for ( int i = 1; i <= 10; ++i ) {
                x = i;
                yield return null;
            }

            for ( int i = 1; i <= 10; ++i ) {
                y = i;
                yield return null;
            }
        }
    }

    class Program
    {
        static void Main( string[] args )
        {
            MicroThread t = new MicroThread();

            for ( int i = 0; i < 15; ++i ) {
                Console.WriteLine( "{0}:{1}, {2}", i, t.x, t.y );
                t.update();
            }
        }
    }
}
