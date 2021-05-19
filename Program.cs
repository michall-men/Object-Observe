using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
//using System.Type;

namespace hands_on_drushim
{


    class Program
    {
        
        class Name
        {
            public List<string> firstName { get; set; }
            public String lastName { get; set; }
        }
        class Person
        {
            public int age { get; set; }
            public Name name { get; set; }
        }

       
     /// <summary>
     /// help class, Like "Node" in Trees. 
     /// used to save the information in the stack
     /// </summary>
        class MyProparty
        {
            public string name { get; set; }
            public Object value { get; set; }
            public MyProparty(string n, Object v) { name = n; value = v; }
        }

        public static string Iterative<T>(T ob)
        {
            string return_value = "", tab = "";
            Stack<MyProparty> stack = new Stack<MyProparty>();

            return_value += Visit(ob, stack, ref tab);

            while (stack.Count != 0)
            {
                //pop remove, peek jast look
                var Top = stack.Pop().value;
                return_value += Visit(Top, stack, ref tab);

            }

            return return_value;
        }
        
        //version 2 of visit function- print collections
        static string Visit(object ob, Stack<MyProparty> stack, ref string tab)
        {
            string return_value = "";
            if (ob.GetType().IsClass)
            {
                return_value += tab + "objet of class: " + ob.GetType().Name + "\n";
                return_value += tab + "***********" + "\n";
                tab += "\t";
            }
            try
            {
                foreach (var prop in ob.GetType().GetProperties())
                {
                    var currentProp = prop.GetValue(ob);
                    
                    if (currentProp.GetType().IsPrimitive || (currentProp.GetType() == typeof(string)))
                    {
                        return_value += tab + prop.Name + "= " + currentProp + "\n";
                    }
                    // extending: print Collections
                    else if (typeof(IEnumerable).IsAssignableFrom(currentProp.GetType()))
                    {
                        return_value += tab + prop.Name + "= [";
                        foreach (var i in currentProp as IEnumerable) return_value += i+",";
                        return_value += "] \n";
                    }
                    else
                    {
                        if (currentProp.GetType().ToString().Contains("System.Object")) return return_value;
                        stack.Push(new MyProparty(prop.Name, currentProp));
                    }
                }
            }
            catch (Exception e) { throw e; }
            return return_value;
        }


        public static void Main(string[] args)
        {
            List<string> fn = new List<string> { "Bill", "William" };
            Name n = new Name { firstName =fn, lastName = "Gates" };
            Person p = new Person {age =55, name = n};

            /* here should be the objects properties comprehension */

            Console.WriteLine(Iterative(p));
            Console.ReadKey();

        }
    }
}

