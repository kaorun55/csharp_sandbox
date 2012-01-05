using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace VoiceCommand
{
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
            try {
                MicroThread mt = new MicroThread();

                foreach ( var c in VoiceCommandPluginHost.GetInstance( "../../../plugin/PowerPointPlugin.dll" ) ) {
                    mt.AddApp( c );
                }

                mt.DoWork( "powerpoint" );
                mt.DoWork( "next" );

                mt.DoWork( "powerpoint" );
                mt.DoWork( "close" );

                mt.DoWork( "powerpoint" );
                mt.DoWork( "prev" );

                mt.DoWork( "powerpoint" );

                mt.DoWork( "exproler" );
                mt.DoWork( "close" );


                mt.DoWork( "powerpoint" );
                mt.DoWork( "prev" );
            }
            catch ( Exception ex ) {
                Console.WriteLine( ex.Message );
            }
        }
    }
}
