// thanks by http://ufcpp.net/study/csharp/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace coroutine
{
    abstract class VoiceCommandPlugin
    {
        public VoiceCommandPlugin( string name )
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }

        public abstract void Command( string command );
    }

    class PowerPoint : VoiceCommandPlugin
    {
        public PowerPoint()
            : base( "powerpoint" )
        {
        }

        public override void Command( string command )
        {
            if ( command == "next" ) {
                Console.WriteLine( "next" );
            }
            else if ( command == "prev" ) {
                Console.WriteLine( "prev" );
            }
            else {
                Console.WriteLine( "invalid command." );
            }
        }
    }

    class MicroThread
    {
        public int x = 0;
        public int y = 0;
        IEnumerator<Command> thread;

        List<VoiceCommandPlugin> apps = new List<VoiceCommandPlugin>();

        public MicroThread()
        {
            init();
        }

        public void AddApp( VoiceCommandPlugin app )
        {
            apps.Add( app );
        }

        void init()
        {
            thread = get();
            thread.MoveNext();
        }

        public void DoWork( string command )
        {
            if ( thread != null ) {
                thread.Current( command );
                if ( !thread.MoveNext() ) {
                    init();
                }
            }
        }

        delegate void Command( string command );
        IEnumerator<Command> get()
        {
            string name = "";
            yield return ( app ) => name = app;

            var c = apps.Where( ( a ) => a.Name == name );
            if ( c.Count() != 0 ) {
                yield return c.First().Command;
            }

            Console.WriteLine( "invalid application." );
        }
    }

    class Program
    {
        static void Main( string[] args )
        {
            MicroThread t = new MicroThread();
            t.AddApp( new PowerPoint() );

            t.DoWork( "powerpoint" );
            t.DoWork( "next" );

            t.DoWork( "powerpoint" );
            t.DoWork( "close" );

            t.DoWork( "powerpoint" );
            t.DoWork( "prev" );

            t.DoWork( "powerpoint" );

            t.DoWork( "exproler" );
            t.DoWork( "close" );

            t.DoWork( "powerpoint" );
            t.DoWork( "prev" );
        }
    }
}
