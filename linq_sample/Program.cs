// thanks by http://ufcpp.net/study/csharp/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linq_sample
{
    class Program
    {
        static void Main( string[] args )
        {
            var 学生名簿 = new[] {
                new { 学生番号 = 14, 性 = "しー", 名 = "しゃーぷ" },
                new { 学生番号 = 10, 性 = "しー", 名 = "ぷらぷら" },
                new { 学生番号 = 20, 性 = "ぶい", 名 = "びー" },
                new { 学生番号 = 35, 性 = "えふ", 名 = "しゃーぷ" },
            };

            var 出席番号前半の人 =
                from p in 学生名簿
                where p.学生番号 <= 15
                orderby p.学生番号
                select p.名;

            foreach ( var 名 in 出席番号前半の人 ) {
                Console.WriteLine( "{0}", 名 );
            }
            Console.WriteLine( "" );

            var 性がしーの人 =
                from p in 学生名簿
                where p.性 == "しー"
                select new
                {
                    p.性,
                    p.名
                };

            foreach ( var 名前 in 性がしーの人 ) {
                Console.WriteLine( "{0} {1}", 名前.性, 名前.名 );
            }
            Console.WriteLine( "" );

            var 名がしゃーぷの人 =
                from p in 学生名簿
                where p.名 == "しゃーぷ"
                select new
                {
                    p.性,
                    p.名
                };

            foreach ( var 名前 in 名がしゃーぷの人 ) {
                Console.WriteLine( "{0} {1}", 名前.性, 名前.名 );
            }
            Console.WriteLine( "" );
        }
    }
}
