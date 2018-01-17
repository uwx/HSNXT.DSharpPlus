using System;
using System.Collections.Generic;
using NUnit.Framework;
using HSNXT.ComLib.Lang.Types;
using HSNXT.ComLib.Lang.Tests.Common;


namespace HSNXT.ComLib.Lang.Tests.Integration.System
{
    [TestFixture]
    public class Types_Strings : ScriptTestsBase
    {
        [Test]
        public void Can_Escape_Chars()
        {
            var statements = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string), "abc" + "'", "var result = 'abc\\'';"),
                TestCase("result", typeof(string), "abc" + "\"", "var result = 'abc\"';"),
                TestCase("result", typeof(string), "abc" + "\"", "var result = \"abc\\\"\";"),
                TestCase("result", typeof(string), "abc" + "\\", "var result = 'abc\\\\';"),
                TestCase("result", typeof(string), "abc" + "\t", "var result = 'abc\t';"),
                TestCase("result", typeof(string), "abc" + Environment.NewLine, "var result = 'abc\r\n';")
            };
            Parse(statements);
        }


        [Test]
        public void Can_Read_Interpolated_Strings()
        {
            var statements = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("fullname", typeof(string), "3 plus reddy",       "var first = 'kishore'; var last = 'reddy'; var fullname = \"#{1 + 2} plus #{last}\";"),
                TestCase("fullname", typeof(string), "kishore plus reddy", "var first = 'kishore'; var last = 'reddy'; var fullname = \"#{first} plus #{last}\";"),
                TestCase("fullname", typeof(string), "before kishore plus reddy after", "var first = 'kishore'; var last = 'reddy'; var fullname = \"before #{first} plus #{last} after\";"),
                TestCase("fullname", typeof(string), "kishore plus reddy", "var first = 'kishore'; var last = 'reddy'; var fullname = \"#{first} plus #{last}\";"),
                TestCase("fullname", typeof(string), "kishore mid:kdog reddy", "var user = { name: 'kishore', middle: 'kdog' }; var last = 'reddy'; var fullname = \"#{user.name} mid:#{user.middle} #{last}\";"),                
                TestCase("result", typeof(string), "exp 7, func 3", "function add( a, b ) return a + b; var num = 2; var result = \"exp #{5 + num}, func #{add( num, 1)}\""),                
            };            
            Parse(statements);
        }


        [Test]
        public void Can_Read_Interpolated_Strings_With_Custom_Interpolated_StartChar()
        {
            var statements = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("fullname", typeof(string), "3 plus reddy",       "var first = 'kishore'; var last = 'reddy'; var fullname = \"${1 + 2} plus ${last}\";"),
                TestCase("fullname", typeof(string), "kishore plus reddy", "var first = 'kishore'; var last = 'reddy'; var fullname = \"${first} plus ${last}\";"),
                TestCase("fullname", typeof(string), "before kishore plus reddy after", "var first = 'kishore'; var last = 'reddy'; var fullname = \"before ${first} plus ${last} after\";"),
                TestCase("fullname", typeof(string), "kishore plus reddy", "var first = 'kishore'; var last = 'reddy'; var fullname = \"${first} plus ${last}\";"),
                TestCase("fullname", typeof(string), "kishore mid:kdog reddy", "var user = { name: 'kishore', middle: 'kdog' }; var last = 'reddy'; var fullname = \"${user.name} mid:${user.middle} ${last}\";"),
            };
            Parse(statements, true, i => i.Context.Settings.InterpolatedStartChar = '$');
        }


        [Test]
        public void Can_Do_String_Method_Calls()
        {
            var statements = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string),   "common@lib",  "var name = 'COmmOn@Lib'; var result = name.toLowerCase();"),
                TestCase("result", typeof(string),   "COMMON@LIB",  "var name = 'COmmOn@Lib'; var result = name.toUpperCase();"),
                TestCase("result", typeof(int),      6,             "var name = 'COmmOn@Lib'; var result = name.indexOf('@');"),
                TestCase("result", typeof(int),      4,             "var name = 'COmmOn@Lib'; var result = name.lastIndexOf('O');"),
                TestCase("result", typeof(string),   "mmOn",        "var name = 'COmmOn@Lib'; var result = name.substr(2, 4);"),
                TestCase("result", typeof(string),   "mmO",         "var name = 'COmmOn@Lib'; var result = name.substring(2, 4);"),
                TestCase("result", typeof(string),   "COnnOn@Lib",  "var name = 'COmmOn@Lib'; var result = name.replace('mm', 'nn');")
            };
            Parse(statements);
        }
    }


    [TestFixture]
    public class Types_Dictionary : ScriptTestsBase
    {    
        [Test]
        public void Can_Declare()
        {
            var date = new DateTime(2012, 8, 1);
            var statements = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string),  "kishore",  "var data = { name: 'kishore', id: 123, isactive: true, dt: new Date(), salary: 80.5 }; var result = data.name;" ),
                TestCase("result", typeof(double),  123,        "var data = { name: 'kishore', id: 123, isactive: true, dt: new Date(), salary: 80.5 }; var result = data.id;" ),
                TestCase("result", typeof(bool),    true,       "var data = { name: 'kishore', id: 123, isactive: true, dt: new Date(), salary: 80.5 }; var result = data.isactive;" ),
                TestCase("result", typeof(bool),    date,       "var data = { name: 'kishore', id: 123, isactive: true, dt: new Date(2012, 8, 1), salary: 80.5 }; var result = data.dt;" ),
            };
            Parse(statements);
        }


        [Test]
        public void Can_Get_Values()
        {
            var map = "var book = {  name: 'fs', author: 'ch', pages: 100 };";

            var statements = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string), "fs",  map + " var result = book['name'];" ),
                TestCase("result", typeof(string), "ch",  map + " var result = book['author'];" ),
                TestCase("result", typeof(double), 100,   map + " var result = book['pages'];" ),                
                TestCase("result", typeof(string), "fs",  map + " var result = book.name;" ),
                TestCase("result", typeof(string), "ch",  map + " var result = book.author;" ),
                TestCase("result", typeof(double), 100,   map + " var result = book.pages;" )
            };
            Parse(statements);
        }


        [Test]
        public void Can_Set_Values()
        {
            var map = "var book = {  name: 'fs', author: 'ch', pages: 100 };";

            var statements = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string), "fs2",  map + "book['name']   = 'fs2'; var result = book['name'];  " ),
                TestCase("result", typeof(string), "ch2",  map + "book['author'] = 'ch2'; var result = book['author'];" ),
                TestCase("result", typeof(double), 101,    map + "book['pages']  = 101;   var result = book['pages']; " ),                
                TestCase("result", typeof(string), "fs3",  map + "book.name      = 'fs3'; var result = book.name;     " ),
                TestCase("result", typeof(string), "ch3",  map + "book.author    = 'ch3'; var result = book.author;   " ),
                TestCase("result", typeof(double), 102,    map + "book.pages     = 102;   var result = book.pages;    " )
            };
            Parse(statements);
        }





        [Test]
        public void Can_Get_Nested_Member_Property()
        {
            var maptxt = @"{ FirstName: 'john', LastName: 'doe', Email: 'johndoe@email.com', IsMale: true, Salary: 10.5, BirthDate: new Date(), Address: { City: 'Queens', State: 'NY' } }";
            var testcases = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string),   "john",              "var p = " + maptxt + "; var result = p.FirstName;" ),
                TestCase("result", typeof(string),   "johndoe@email.com", "var p = " + maptxt + "; var result = p.Email;" ),
                TestCase("result", typeof(bool),     true,                "var p = " + maptxt + "; var result = p.IsMale;" ),
                TestCase("result", typeof(DateTime), DateTime.Today,      "var p = " + maptxt + "; var result = p.BirthDate;" ),                
                TestCase("result", typeof(string),   "Queens",            "var p = " + maptxt + "; var result = p.Address.City;" ),
                TestCase("result", typeof(string),   "NY",                "var p = " + maptxt + "; var result = p.Address.State;" )
            };
            RunTestCases(testcases);
        }


        [Test]
        public void Can_Set_Nested_Member_Property()
        {
            var maptxt = @"{ FirstName: 'john', LastName: 'doe', Email: 'johndoe@email.com', IsMale: true, Salary: 10.5, BirthDate: new Date(), Address: { City: 'Queens', State: 'NY' } }";
            var testcases = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string),   "jane",              "var p = " + maptxt + ";  p.FirstName = 'jane';     var result = p.FirstName;" ),
                TestCase("result", typeof(string),   "janedoe@email.com", "var p = " + maptxt + ";  p.Email     = 'janedoe@email.com'; var result = p.Email;" ),
                TestCase("result", typeof(bool),     false,               "var p = " + maptxt + ";  p.IsMale    =  false;       var result = p.IsMale;" ),
                TestCase("result", typeof(DateTime), DateTime.Today,      "var p = " + maptxt + ";  p.BirthDate = new Date();   var result = p.BirthDate;" ),    
                TestCase("result", typeof(string),   "Bronx",             "var p = " + maptxt + ";  p.Address.City = 'Bronx';   var result = p.Address.City;" ),
                TestCase("result", typeof(string),   "NJ",                "var p = " + maptxt + ";  p.Address.State = 'NJ';     var result = p.Address.State;" )
            };
            RunTestCases(testcases);
        }
    }


    [TestFixture]
    public class Types_Dates : ScriptTestsBase
    {

        [Test]
        public void Can_Subtract_Dates()
        {
            var statements = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(double), 0,  "var result = 0; var date1 = new Date(2012, 1, 8, 10, 30, 30); var date2 = new Date(2012, 1, 9, 8, 15, 40); var diff = date2 - date1; result = diff.Days;"), 
                TestCase("result", typeof(double), 21, "var result = 0; var date1 = new Date(2012, 1, 8, 10, 30, 30); var date2 = new Date(2012, 1, 9, 8, 15, 40); var diff = date2 - date1; result = diff.Hours;"), 
                TestCase("result", typeof(double), 45, "var result = 0; var date1 = new Date(2012, 1, 8, 10, 30, 30); var date2 = new Date(2012, 1, 9, 8, 15, 40); var diff = date2 - date1; result = diff.Minutes;"), 
                TestCase("result", typeof(double), 10, "var result = 0; var date1 = new Date(2012, 1, 8, 10, 30, 30); var date2 = new Date(2012, 1, 9, 8, 15, 40); var diff = date2 - date1; result = diff.Seconds;")
            };
            Parse(statements);
        }


        [Test]
        public void Can_Get_Properties()
        {
            var date = DateTime.Now;
            var testcases = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(int), date.Year,                            "var result = date.Year;"),
                TestCase("result", typeof(int), date.Month,                           "var result = date.Month;"),
                TestCase("result", typeof(int), date.Day,                             "var result = date.Day;"),
                TestCase("result", typeof(int), (int)date.DayOfWeek,                  "var result = date.DayOfWeek;"),
                TestCase("result", typeof(int), date.Hour,                            "var result = date.Hours;"),
                TestCase("result", typeof(int), date.Minute,                          "var result = date.Minutes;"),
                TestCase("result", typeof(int), date.Second,                          "var result = date.Seconds;"),
            };
            Parse(testcases, true, (i) => i.Memory.SetValue("date", new LDate(date)));
        }


        [Test]
        public void Can_Set_Properties()
        {
            var date = DateTime.Now;
            var testcases = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(int), 10,       "date.setDate(10);       var result = date.getDate();"),
                TestCase("result", typeof(int), 3,        "date.setMonth(3);       var result = date.getMonth();"),
                TestCase("result", typeof(int), 2010,     "date.setFullYear(2010); var result = date.getFullYear();"),
                TestCase("result", typeof(int), 14,       "date.setHours(14);      var result = date.getHours();"),
                TestCase("result", typeof(int), 30,       "date.setMinutes(30);    var result = date.getMinutes();"),
                TestCase("result", typeof(int), 45,       "date.setSeconds(45);    var result = date.getSeconds();")
            };
            Parse(testcases, true, (i) => i.Memory.SetValue("date", new LDate(date)));
        }


        [Test]
        public void Can_Do_Date_Method_Calls()
        {
            var date = DateTime.Now;
            var testcases = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(int), date.Day,                             "var result = date.getDate();"),
                TestCase("result", typeof(int), (int)date.DayOfWeek,                  "var result = date.getDay();"),
                TestCase("result", typeof(int), date.Month,                           "var result = date.getMonth();"),
                TestCase("result", typeof(int), date.Year,                            "var result = date.getFullYear();"),
                TestCase("result", typeof(int), date.Hour,                            "var result = date.getHours();"),
                TestCase("result", typeof(int), date.Minute,                          "var result = date.getMinutes();"),
                TestCase("result", typeof(int), date.Second,                          "var result = date.getSeconds();"),
                TestCase("result", typeof(int), date.ToUniversalTime().Day,           "var result = date.getUTCDate();"),
                TestCase("result", typeof(int), (int)date.ToUniversalTime().DayOfWeek,"var result = date.getUTCDay();"),
                TestCase("result", typeof(int), date.ToUniversalTime().Month,         "var result = date.getUTCMonth();"),
                TestCase("result", typeof(int), date.ToUniversalTime().Year,          "var result = date.getUTCFullYear();"),
                TestCase("result", typeof(int), date.ToUniversalTime().Hour,          "var result = date.getUTCHours();"),
                TestCase("result", typeof(int), date.ToUniversalTime().Minute,        "var result = date.getUTCMinutes();"),
                TestCase("result", typeof(int), date.ToUniversalTime().Second,        "var result = date.getUTCSeconds();"),

                TestCase("result", typeof(int), 10,       "date.setDate(10);       var result = date.getDate();"),
                TestCase("result", typeof(int), 3,        "date.setMonth(3);       var result = date.getMonth();"),
                TestCase("result", typeof(int), 2010,     "date.setFullYear(2010); var result = date.getFullYear();"),
                TestCase("result", typeof(int), 14,       "date.setHours(14);      var result = date.getHours();"),
                TestCase("result", typeof(int), 30,       "date.setMinutes(30);    var result = date.getMinutes();"),
                TestCase("result", typeof(int), 45,       "date.setSeconds(45);    var result = date.getSeconds();")
            };
            Parse(testcases, true, (i) => i.Memory.SetValue("date", new LDate(date)));
        }
    }



    [TestFixture]
    public class Types_Time : ScriptTestsBase
    {
        [Test]
        public void Can_Declare_Time_With_New()
        {
            var d1 = DateTime.Now;
            var d2 = DateTime.Now.AddDays(-1);
            var d3 = d2 - d1;

            var statements = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(double), 1,  "var result = 0; var time = new Time(1, 2, 30, 45 ); result = time.Days;"), 
                TestCase("result", typeof(double), 2,  "var result = 0; var time = new Time(1, 2, 30, 45 ); result = time.Hours;"), 
                TestCase("result", typeof(double), 30, "var result = 0; var time = new Time(1, 2, 30, 45 ); result = time.Minutes;"), 
                TestCase("result", typeof(double), 45, "var result = 0; var time = new Time(1, 2, 30, 45 ); result = time.Seconds;")
            };
            Parse(statements);
        }


        [Test]
        public void Can_Add_Times()
        {
            var statements = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(double), 1,  "var result = 0; var time1 = new Time(0, 2, 30, 0 ); var time2 = new Time(1, 1, 10, 20); var time3 = time1 + time2; result = time3.Days;"), 
                TestCase("result", typeof(double), 3,  "var result = 0; var time1 = new Time(0, 2, 30, 0 ); var time2 = new Time(0, 1, 10, 20); var time3 = time1 + time2; result = time3.Hours;"), 
                TestCase("result", typeof(double), 40, "var result = 0; var time1 = new Time(0, 2, 30, 0 ); var time2 = new Time(0, 1, 10, 20); var time3 = time1 + time2; result = time3.Minutes;"), 
                TestCase("result", typeof(double), 25, "var result = 0; var time1 = new Time(0, 2, 30, 5 ); var time2 = new Time(0, 1, 10, 20); var time3 = time1 + time2; result = time3.Seconds;")
            };
            Parse(statements);
        }
    }



    [TestFixture]
    public class Types_Array : ScriptTestsBase
    {
        [Test]
        public void Can_Do_Array_Declarations()
        {
            var statements = new List<Tuple<string, Type, Type, int, string>>()
            {
                new Tuple<string, Type, Type, int, string>("result", typeof(LArray), typeof(string),   0,  "var result = []; "),
                new Tuple<string, Type, Type, int, string>("result", typeof(LArray), typeof(string),   1,  "var result = ['a']; "  ),
                new Tuple<string, Type, Type, int, string>("result", typeof(LArray), typeof(double),   2,  "var result = [1,    2]; "   ),
                new Tuple<string, Type, Type, int, string>("result", typeof(LArray), typeof(bool),     3,  "var result = [true, false, true];" ),
                new Tuple<string, Type, Type, int, string>("result", typeof(LArray),  typeof(double),  4,  "var result = [1.1,  2.2,   3.33, 4.44];" ),
                new Tuple<string, Type, Type, int, string>("result", typeof(LArray), typeof(DateTime), 5,  "var result = [new Date(),  new Date(), new Date(), new Date(), new Date()];" )
            };
            foreach (var stmt in statements)
            {
                var i = new Interpreter();
                i.Execute(stmt.Item5);
                var array = i.Memory.GetAs<LArray>("result");

                Assert.AreEqual(array.Value.Count, stmt.Item4);

            }
        }


        [Test]
        public void Can_Do_Array_Nested()
        {
            var statements = new List<Tuple<string, Type, Type, int, string>>()
            {
                new Tuple<string, Type, Type, int, string>("result", typeof(LArray), typeof(string),   1,  "var result = [ [0, 1] ]; ")
            };
            foreach (var stmt in statements)
            {
                var i = new Interpreter();
                i.Execute(stmt.Item5);
                var array = i.Memory.Get<LArray>("result");

                Assert.AreEqual(array.Value.Count, stmt.Item4);
            }
        }


        [Test]
        public void Can_Get_Array_Basic_Type_Values_ByIndex()
        {
            var maptxt = @"[ 'john', 'johndoe@email.com', true, 10.5 ]";
            var testcases = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string),   "john",              "var p = " + maptxt + "; var result = p[0];"),
                TestCase("result", typeof(string),   "johndoe@email.com", "var p = " + maptxt + "; var result = p[1];"),
                TestCase("result", typeof(bool),     true,                "var p = " + maptxt + "; var result = p[2];"),
            };
            Parse(testcases);
        }


        [Test]
        public void Can_Get_Array_ByIndex()
        {
            var maptxt = @"[ 'john', 'johndoe@email.com', true, 10.5, new Date(), { City: 'Queens', State: 'NY' }, [0, 1, 2] ]";
            var testcases = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string),   "Queens",            "var p = " + maptxt + "; var result = p[5].City;"),
                TestCase("result", typeof(string),   "NY",                "var p = " + maptxt + "; var result = p[5].State;"),
                TestCase("result", typeof(string),   "john",              "var p = " + maptxt + "; var result = p[0];"),
                TestCase("result", typeof(string),   "johndoe@email.com", "var p = " + maptxt + "; var result = p[1];"),
                TestCase("result", typeof(bool),     true,                "var p = " + maptxt + "; var result = p[2];"),    
                
            };
            Parse(testcases);
        }


        [Test]
        public void Can_Get_Array_Item_By_Index_Right_After_Declaration()
        {
            var arr = @"[ 'john', 'johndoe@email.com', true, 10.5, new Date(), { City: 'Queens', State: 'NY' }, [0, 1, 2] ]";
            var testcases = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string),   "john",              "var result = " + arr + "[0];"),
                TestCase("result", typeof(string),   "johndoe@email.com", "var result = " + arr + "[1];"),
                TestCase("result", typeof(bool),     true,                "var result = " + arr + "[2];"),
                TestCase("result", typeof(string),   "Queens",            "var result = " + arr + "[5].City;"),
                TestCase("result", typeof(string),   "NY",                "var result = " + arr + "[5].State;"),
                
            };
            Parse(testcases);
        }


        [Test]
        public void Can_Get_Array_Item_By_Nested_Indexes()
        {
            var maptxt = @"var users = ['john1', 'john2', 'john3']; var indexes = [0, 1, 2]; ";
            var testcases = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string),   "john2",  maptxt + " var result = users[indexes[1]];"),
                TestCase("result", typeof(string),   "john3",  maptxt + " var result = users[indexes[1+1]];"),
                TestCase("result", typeof(string),   "john3",  maptxt + " var result = users[indexes[1]+1];"),
            };
            Parse(testcases);
        }


        [Test]
        public void Can_Set_Array_ByIndex()
        {
            var maptxt = @"[ 'john', 'johndoe@email.com', true, 10.5, new Date(), { City: 'Queens', State: 'NY' }, [0, 1, 2] ]";
            var testcases = new List<Tuple<string, Type, object, string>>()
            {
                TestCase("result", typeof(string),   "jane",              "var p = " + maptxt + "; p[0] = 'jane';  var result = p[0];"),
                TestCase("result", typeof(string),   "janedoe@email.com", "var p = " + maptxt + "; p[1] = 'janedoe@email.com'; var result = p[1];"),
                TestCase("result", typeof(bool),     false,               "var p = " + maptxt + "; p[2] = false;   var result = p[2];"),
                TestCase("result", typeof(string),   "Bronx",             "var p = " + maptxt + "; p[5].City = 'Bronx';  var result = p[5].City;"),
                TestCase("result", typeof(string),   "NJ",                "var p = " + maptxt + "; p[5].State = 'NJ';    var result = p[5].State;")
            };
            RunTestCases(testcases);
        }


        [Test]
        public void Can_Do_Array_Method_Calls()
        {
            var arrytext = "var arr = ['a', 'b', 'c', 'd'];";
            var statements = new List<Tuple<string, Type, object, int, string>>()
            {
                new Tuple<string, Type, object, int, string>("result", typeof(int),    4,           4,  arrytext + " var result = arr.length;"),
                new Tuple<string, Type, object, int, string>("result", typeof(string), "e",         5,  arrytext + " arr.push('e'); var result = arr[4];"),
                new Tuple<string, Type, object, int, string>("result", typeof(string), "d",         3,  arrytext + " var result = arr.pop();"),
                new Tuple<string, Type, object, int, string>("result", typeof(string), "a;b;c;d",   4,  arrytext + " var result = arr.join(';');"),
                new Tuple<string, Type, object, int, string>("result", typeof(string), "d;c;b;a",   4,  arrytext + " var result = arr.reverse().join(';');")
            };
            foreach (var stmt in statements)
            {
                var i = new Interpreter();
                Console.WriteLine(stmt.Item5);
                i.Execute(stmt.Item5);
                var result = i.Memory.Get<object>("result");

                LangTestsHelper.Compare(result, stmt.Item3);

                // Check length
                var arr = i.Memory.Get<LArray>("arr");
                Assert.AreEqual(stmt.Item4, arr.Value.Count);
            }
        }
    }
}
