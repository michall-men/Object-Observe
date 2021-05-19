using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;


namespace hands_on_drushim
{
    class RecursiveSolution
    {

        class Boo
        {
            public Stack<int> C { get; set; }
            public String D { get; set; }
            public override string ToString()
            {
                return "C=" + C.ToString() + "D=" + D;
            }
        }
        class Foo
        {
            public int A { get; set; }
            public Boo B { get; set; }
        }

        public static string RegularRecursive(object ob, string return_value = "", string tab = "")
        {

            if (ob.GetType().IsClass)
            {
                return_value += tab + "objet of class: " + ob.GetType().Name + "\n";
                return_value += tab + "-----" + "\n";
            }

            foreach (var prop in ob.GetType().GetProperties())
            {
                var currentProp = prop.GetValue(ob);

                if (currentProp.GetType().IsPrimitive || (currentProp.GetType() == typeof(string)))
                {
                    return_value += tab + prop.Name + "= " + currentProp + "\n";
                }
                else if (typeof(IEnumerable).IsAssignableFrom(currentProp.GetType()))
                {
                    return_value += tab + prop.Name + "= [";
                    foreach (var i in currentProp as IEnumerable) return_value += i + ",";
                    return_value += "] \n";
                }
                else
                {
                    return_value += RegularRecursive(currentProp, tab + prop.Name + "= " + "\n", tab + "\t");
                }
            }
            return return_value;
        }
       


        public static void RecMain(string[] args)
        {

            Foo foo = new Foo { A = 1, B = new Boo { C = new Stack<int>(), D = "kkk" } };
            Console.WriteLine(RegularRecursive(foo));                                              
           

            Console.ReadKey();

        }
    }
}
