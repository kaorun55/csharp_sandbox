using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceCommand
{
    public class PowerPointPlugin : VoiceCommandPlugin
    {
        public PowerPointPlugin()
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
}
