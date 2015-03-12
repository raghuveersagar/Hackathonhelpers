/*Author : Raghuveer Sagar
 *
 *Date   : 2/16/2015 
 * 
 *Version: v1.0 
 * 
 */

using System;
using System.Collections.Generic;

namespace RIT_CS
{
    /// <summary>
    /// An exception used to indicate a problem with how
    /// a HashTable instance is being accessed
    /// </summary>
    public class NonExistentKey<Key> : Exception
    {
        /// <summary>
        /// The key that caused this exception to be raised
        /// </summary>
        public Key BadKey { get; private set; }

        /// <summary>
        /// Create a new instance and save the key that
        /// caused the problem.
        /// </summary>
        /// <param name="k">
        /// The key that was not found in the hash table
        /// </param>
        public NonExistentKey(Key k) :
            base("Non existent key in HashTable: " + k)
        {
            BadKey = k;
        }

    }

    /// <summary>
    /// An associative (key-value) data structure.
    /// A given key may not appear more than once in the table,
    /// but multiple keys may have the same value associated with them.
    /// Tables are assumed to be of limited size are expected to automatically
    /// expand if too many entries are put in them.
    /// </summary>
    /// <param name="Key">the types of the table's keys (uses Equals())</param>
    /// <param name="Value">the types of the table's values</param>
    interface Table<Key, Value> : IEnumerable<Key>
    {
        /// <summary>
        /// Add a new entry in the hash table. If an entry with the
        /// given key already exists, it is replaced without error.
        /// put() always succeeds.
        /// (Details left to implementing classes.)
        /// </summary>
        /// <param name="k">the key for the new or existing entry</param>
        /// <param name="v">the (new) value for the key</param>
        void Put(Key k, Value v);

        /// <summary>
        /// Does an entry with the given key exist?
        /// </summary>
        /// <param name="k">the key being sought</param>
        /// <returns>true iff the key exists in the table</returns>
        bool Contains(Key k);

        /// <summary>
        /// Fetch the value associated with the given key.
        /// </summary>
        /// <param name="k">The key to be looked up in the table</param>
        /// <returns>the value associated with the given key</returns>
        /// <exception cref="NonExistentKey">if Contains(key) is false</exception>
        Value Get(Key k);
    }

    class TableFactory
    {
        /// <summary>
        /// Create a Table.
        /// (The student is to put a line of code in this method corresponding to
        /// the name of the Table implementor s/he has designed.)
        /// </summary>
        /// <param name="K">the key type</param>
        /// <param name="V">the value type</param>
        /// <param name="capacity">The initial maximum size of the table</param>
        /// <param name="loadThreshold">
        /// The fraction of the table's capacity that when
        /// reached will cause a rebuild of the table to a 50% larger size
        /// </param>
        /// <returns>A new instance of Table</returns>
        public static Table<K, V> Make<K, V>(int capacity = 100, double loadThreshold = 0.75)
        {
            return new LinkedHashTable<K, V>(capacity, loadThreshold);
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Table<String, String> ht = TableFactory.Make<String, String>(4, 0.5);
            ht.Put("Joe", "Doe");
            ht.Put("Jane", "Brain");
            ht.Put("Chris", "Swiss");
            try
            {
                foreach (String first in ht)
                {
                    Console.WriteLine(first + " -> " + ht.Get(first));
                }
                Console.WriteLine("=========================");

                ht.Put("Wavy", "Gravy");
                ht.Put("Chris", "Bliss");
                foreach (String first in ht)
                {
                    Console.WriteLine(first + " -> " + ht.Get(first));
                }
                Console.WriteLine("=========================");

                Console.Write("Jane -> ");
                Console.WriteLine(ht.Get("Jane"));
                Console.Write("John -> ");
                Console.WriteLine(ht.Get("John"));
            }
            catch (NonExistentKey<String> nek)
            {
                Console.WriteLine(nek.Message);
                Console.WriteLine(nek.StackTrace);
            }

            TestTable.test();

            Console.ReadLine();
        }
    }


    ///<summary>
    ///This is helper class.
    ///It is used as a unit of data in the bucket.
    /// It contains one key-value pair.And reference to
    /// next Entry in the same bucket.
    ///</summary>
    public class Entries<Key, Value>
    {
        /// <summary>
        ///Holds the key property
        /// </summary>
        private Key key;

        /// <summary>
        /// K property </summary>
        /// <value>
        /// getter setter for the key property</value>
        public Key K
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// Holds the value property
        /// </summary>
        private Value value;


        /// <summary>
        /// V property </summary>
        /// <value>
        /// getter setter for the value property</value>
        public Value V
        {
            get { return value; }
            set { this.value = value; }
        }
        /// <summary>
        /// Holds the next property.
        /// This points to Entry or null
        /// </summary>
        private Entries<Key, Value> next;



        /// <summary>
        /// Next property </summary>
        /// <value>
        /// getter setter for the next property</value>
        public Entries<Key, Value> Next
        {

            get { return next; }
            set { next = value; }
        }
        /// <summary>
        ///Constructor
        /// </summary>
        /// <param name="k">key</param>
        /// <param name="v">value</param>
        public Entries(Key k, Value v)
        {
            key = k;
            value = v;
        }


    }



    /// <summary>
    /// class LinkedHashTable is an implementation of the Table Interface
    /// It is an associative data structure.
    /// It also provides an GetEnumerator() method so that it can be used in a foreach block
    ///</summary>
    public class LinkedHashTable<Key, Value> : Table<Key, Value>
    {
        /// <summary>
        /// Store for the capacity property.It stores default initial capacity.
        /// Always we will use capacity as a power of two.
        /// It gets easy to calculate modulo by using bit-wise 'and'(&).
        /// </summary>
        private int capacity = 16;
        /// <summary>
        /// Store for the load property.It stores default load capacity.
        /// </summary>
        private double load = 0.75;

        /// <summary>
        /// Store for the array which acts like buckets.
        /// </summary>
        public Entries<Key, Value>[] lh_table;
        /// <summary>
        /// The size of the lh_table as visible to the user.Not the actual size of buckets.
        /// </summary>
        private int size = 0;
        /// <summary>
        /// This is an additional list of keys which provides Enumerator for
        /// use in foreach loop
        /// </summary>
        public List<Key> keys;
        /// <summary>
        /// Size property </summary>
        /// <value>
        /// getter setter for the size property</value>
        public int Size { get { return size; } }


        /// <summary>
        /// Default constructor
        /// </summary>
        public LinkedHashTable()
        {
            lh_table = new Entries<Key, Value>[capacity];
            keys = new List<Key>(capacity);

        }

        /// <summary>
        /// Overridden Constructor
        /// </summary>
        /// <param name="_capacity">Custom capacity.Though closest power of 2 is taken</param>
        /// <param name="_load">Custom load</param>
        public LinkedHashTable(int _capacity, double _load)
        {
            load = _load;
            ///Here we take the table capacity as closest power of 2.
            int temp = 1;
            while (temp < _capacity)
                temp <<= 1;

            lh_table = new Entries<Key, Value>[temp];
            keys = new List<Key>(temp);

        }

        /// <summary>
        /// Insert into the HashTable.If key is already present older entry is replaced.
        /// Key should not be null
        /// </summary>
        /// <param name="k">key</param>
        /// <param name="v">value</param>
        public void Put(Key k, Value v)
        {
            if (k == null)
                return;
            int hashcode = k.GetHashCode();

            /// Logic behind this is,
            /// if n is a power of 2,
            /// then x mod n = x & (n-1)
            int index = hashcode & (lh_table.Length - 1);

            insert(lh_table, index, new Entries<Key, Value>(k, v));


            /// resize the table if size grows over certain limit defined load.
            if (size >= load * (lh_table.Length))
            {
                // Console.Write(" size " + size);
                // Console.Write(" capacity " + lh_table.Length);

                resize();

            }

        }

        /// <summary>
        /// This method resizes the lh_table array and rehashes all elements
        /// and creates a new bigger lh_table.
        /// </summary>
        private void resize()
        {

            // Console.WriteLine("Resizing");
            int capacity_new = 2 * lh_table.Length;
            int _hash;
            int _index = -99;
            keys = new List<Key>(capacity_new);
            int old_size = size;
            size = 0;
            Entries<Key, Value>[] new_lh_table = new Entries<Key, Value>[capacity_new];
            foreach (Entries<Key, Value> e in lh_table)
            {

                if (e == null)
                    continue;
                _hash = e.K.GetHashCode();
                _index = _hash & (capacity_new - 1);
                insert(new_lh_table, _index, new Entries<Key, Value>(e.K, e.V));

                Entries<Key, Value> inner_e = e.Next;

                while (inner_e != null)
                {
                    _hash = inner_e.K.GetHashCode();
                    _index = _hash & (capacity_new - 1);
                    insert(new_lh_table, _index, new Entries<Key, Value>(inner_e.K, inner_e.V));
                    inner_e = inner_e.Next;
                }
            }


            for (int i = 0; i < lh_table.Length; i++)
            {
                lh_table[i] = null;

            }

            lh_table = new_lh_table;
        }
        /// <summary>
        /// Implementation of Table<K,V>.Contains() method
        /// </summary>
        /// <param name="k">key</param>
        /// <returns>retruns True if the key present else false</returns>
        public bool Contains(Key k)
        {
            if (k == null)
                return false;
            int _hashCode = k.GetHashCode();
            int _index = _hashCode & (lh_table.Length - 1);
            Entries<Key, Value> temp = lh_table[_index];
            if (temp == null)
                return false;
            if (temp.K.Equals(k))
                return true;
            while (temp.Next != null)
            {
                if (temp.Next.K.Equals(k))
                {

                    return true;
                }
                temp = temp.Next;

            }

            return false;
        }


        /// <summary>
        /// inserts the element in the given index
        /// </summary>
        /// <param name="table">buckets</param>
        /// <param name="index">index</param>
        /// <param name="element">the Entry to be inserted</param>
        private void insert(Entries<Key, Value>[] table, int index, Entries<Key, Value> element)
        {

            Entries<Key, Value> temp = table[index];
            if (temp == null)
            {

                table[index] = element;
                keys.Add(element.K);
                size++;

            }
            else
            {

                if (temp.K.Equals(element.K))
                {
                    //Console.WriteLine("Replacing " + element.K);
                    temp.V = element.V;
                    return;

                }
                else
                {

                    while (temp.Next != null)
                    {
                        temp = temp.Next;
                        if (temp.K.Equals(element.K))
                        {
                            //Console.WriteLine("Replacing " + element.K);
                            temp.V = element.V;
                            return;

                        }

                    }
                    temp.Next = element;
                    keys.Add(element.K);
                    size++;
                }
            }
        }


        /// <summary>
        ///Returns the value if key is present in the table.Else throws NonExistentKey exception.
        /// </summary>
        /// <param name="k">key</param>
        /// <returns>value</returns>
        public Value Get(Key k)
        {

            if (k == null)
                throw new NonExistentKey<Key>(k);

            int hashcode = k.GetHashCode();
            int index = hashcode & (lh_table.Length - 1);
            Entries<Key, Value> temp = lh_table[index];
            if (temp == null)
                throw new NonExistentKey<Key>(k);
            if (temp.K.Equals(k))
                return temp.V;
            while (temp.Next != null)
            {
                temp = temp.Next;
                if (temp.K.Equals(k))
                {

                    return temp.V;
                }


            }

            throw new NonExistentKey<Key>(k);

        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();

        }

        /// <summary>
        /// Enumerator to be used in foreach loop
        /// </summary>
        /// <returns>IEnumerator for that list of keys</returns>
        public IEnumerator<Key> GetEnumerator()
        {
            return keys.GetEnumerator();
        }

    }


    public class Name
    {

        private string name;

        public string _Name
        {

            get
            {

                return name;

            }
        }
        public Name(string s)
        {
            name = s;
        }

        public override bool Equals(Object o)
        {
            return name.Equals(((Name)o)._Name);

        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override string ToString()
        {
            return name;
        }

    }


    class TestTable
    {

        public static void test()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("12 tests");
            Console.WriteLine("=========================");
            test_12();
            Console.WriteLine("=========================");
            Console.WriteLine("19 tests");
            Console.WriteLine("=========================");
            test_19();
            Console.WriteLine("=========================");
            Console.WriteLine("2000 tests");
            Console.WriteLine("=========================");
            test_2000();

        }

        public static void test_19()
        {
            try
            {
                Table<string, string> lh_19 = TableFactory.Make<String, String>(19, 0.9);
                string[] countries = { "United Arab Emirates", "Nigeria", "Ghana", "Pitcairn Islands", "Ethiopia", "Algeria", "Niue", "Jordan", "Netherlands", "Andorra", "Turkey", "Madagascar", "Samoa", "Turkmenistan", "Eritrea", "Kazakhstan", "Paraguay", "Greece", "Cook Islands" };
                string[] capitals = { "******", "Abuja", "Accra", "Adamstown", "Addis Ababa", "Algiers", "Alofi", "******", "Amsterdam", "Andorra la Vella", "******", "Antananarivo", "Apia", "Ashgabat", "Asmara", "Astana", "Asunción", "Athens", "Avarua" };

                ///Notice  United Arab Emirates,Jordan and Turkey have ****** in capitals


                ///Insert key-value pairs in the LinkedHashTable        
                for (int i = 0; i < 19; i++)
                {
                    lh_19.Put(countries[i], capitals[i]);
                }

                ///Get all the entries in the LinkedHashedTable using Enumerator in a foreach loop
                foreach (String key in countries)
                {
                    Console.WriteLine(key + " -> " + lh_19.Get(key));
                }

                Console.WriteLine("=========================");


                ///Update the value for key = "Jordan"
                lh_19.Put("Jordan", "Amman");

                ///Add a null key
                lh_19.Put("null", "ABCD");
                ///Update the value for key = "United Arab Emirates"
                lh_19.Put("United Arab Emirates", "Abu Dhabi");
                ///Update the value for key = "Turkey"
                lh_19.Put("Turkey", "Istanbul");

                ///Get all the entries in the LinkedHashedTable using Enumerator in a foreach loop
                foreach (String key in countries)
                {
                    Console.WriteLine(key + " -> " + lh_19.Get(key));
                }

                Console.WriteLine("=========================");


                ///Check whether the key = "Pitcairn Islands" exists in the LinkedHashTable using Contains
                if (lh_19.Contains("Pitcairn Islands"))
                {
                    ///Get value corresponding to the key = "Pitcairn Islands" if it exists
                    Console.Write("Pitcairn Islands -> ");
                    Console.WriteLine(lh_19.Get("Pitcairn Islands"));

                }


                ///Get value corresponding to the key = "Ethiopia"
                Console.Write("Ethiopia -> ");
                Console.WriteLine(lh_19.Get("Ethiopia"));


                /// Call contains on a Non-Existent Key
                Console.Write("Japan?? ");
                Console.WriteLine(lh_19.Contains("Japan"));



                /// Get the value of a Non-Existent Key
                Console.Write("Japan -> ");
                Console.WriteLine(lh_19.Get("Japan"));
            }

            catch (NonExistentKey<String> nek)
            {
                Console.WriteLine(nek.Message);
                Console.WriteLine(nek.StackTrace);

            }

        }


        public static void test_12()
        {

            try
            {

                Table<Name, int> lh_12 = TableFactory.Make<Name, int>(6, 0.88);
                string[] names = { "Yvonne Brown", "Aristotle Daniels", "Karina Bauer", "Yoko Head", "Vaughan Walter", "Vincent Strickland", "Ciara Cleveland", "Tallulah Banks", "Kennedy Patton", "Hu Avila", "Quinlan Price", "Fleur Munoz" };
                int[] ssn = { 514708684, 704603406, 630891516, 837136709, 226724596, 930732435, 280351858, 352807298, 812250334, 764766739, 701316235, 165208690 };
                Name[] names_objects = new Name[names.Length];


                ///Create an array of Name objects to use as keys
                ///Name class has Equals,GetHashCode and ToString methods overridden
                for (int i = 0; i < names.Length; i++)
                {
                    names_objects[i] = new Name(names[i]);
                }

                //////Insert 5 key-value pairs in the LinkedHashTable
                for (int i = 0; i < 5; i++)
                {
                    lh_12.Put(names_objects[i], ssn[i]);
                }

                ///Get all the entries in the LinkedHashedTable using Enumerator in a foreach loop   
                foreach (Name n in lh_12)
                {
                    Console.WriteLine(n._Name + " -> " + lh_12.Get(n));
                }

                Console.WriteLine("=========================");
                //////Insert remaining key-value pairs in the LinkedHashTable
                for (int i = 5; i < names.Length; i++)
                {
                    lh_12.Put(names_objects[i], ssn[i]);
                }


                ///Get all the entries in the LinkedHashedTable using Enumerator in a foreach loop   
                foreach (Name n in lh_12)
                {
                    Console.WriteLine(n._Name + " -> " + lh_12.Get((n)));
                }

                Console.WriteLine("=========================");


                ///Check whether the key = "Kennedy Patton" exists in the LinkedHashTable using Contains
                if (lh_12.Contains(new Name("Kennedy Patton")))
                {
                    ///Get value corresponding to the key = "Kennedy Patton" if it exists
                    Console.WriteLine(new Name("Kennedy Patton") + " -> " + lh_12.Get(new Name("Kennedy Patton")));
                }


                //////Get value corresponding to the key = "Hu Avila"
                Console.WriteLine("Hu Avila" + " -> " + lh_12.Get(new Name("Hu Avila")));


                ///Put a value for an existing key = "Yoko Head"
                lh_12.Put(new Name("Yoko Head"), 999999);

                ///Verify whether the value was updated for key = "Yoko Head"
                Console.WriteLine("Yoko Head" + " -> " + lh_12.Get(new Name("Yoko Head")));


                /// Call contains on a Non-Existent Key
                Console.WriteLine("Chiquita Whitney ?? " + lh_12.Contains(new Name("Chiquita Whitney")));


                /// Call Get on a Non-Existent Key
                lh_12.Get(new Name("Chiquita Whitney"));

            }

            catch (NonExistentKey<Name> nek)
            {
                Console.WriteLine(nek.Message);
                Console.WriteLine(nek.StackTrace);
            }




        }


        public static void test_2000()
        {

            try
            {

                Table<string, string> lh_2000 = TableFactory.Make<String, String>(500, 0.70);
                char[] x = { 'A', 'A', 'A', 'A' };
                Dictionary<string, string> l = new Dictionary<string, string>();
                ///Keys are 4 characters
                ///1st character can be  any character from  A to J
                ///2nd character can be  any character from  A to E
                ///3rd character can be  any character from  A to D
                ///4th character can be  any character from  A to J
                ///egs = ACCF,EBCG
                ///and value is same characters but in lower case
                ///eg for key value pair is (EBCG,ebcg)


                ///Inserting the key-value pair in LinkedHashtable
                for (int a = 0; a < 10; a++)
                {

                    for (int b = 0; b < 5; b++)
                    {

                        for (int c = 0; c < 4; c++)
                        {

                            for (int d = 0; d < 10; d++)
                            {


                                lh_2000.Put(new string(x), new string(x).ToLower());
                                x[3] = (char)('A' + (d + 1));
                            }

                            x[2] = (char)('A' + (c + 1));


                        }
                        x[1] = (char)('A' + b + 1);

                    }

                    x[0] = (char)('A' + a + 1);
                }


                int track = 0;


                //Printing every 100th entry using Get
                foreach (string k in lh_2000)
                {
                    if (track % 100 == 0)
                    {
                        Console.WriteLine(k + " -> " + lh_2000.Get(k));
                    }
                    track++;
                }

                Console.WriteLine("=========================");

                ///Use Contains to check whether key "ABCD" is present.
                if (lh_2000.Contains("ABCD"))
                {
                    //if key "ABCD" is present update its value
                    lh_2000.Put("ABCD", "PPPP");
                }
                /// get the value corresponding to key "ABCD" to see its properly updated 
                Console.WriteLine("ABCD" + " -> " + lh_2000.Get("ABCD"));
                //for key "ACCF" update its value  
                lh_2000.Put("ACCF", "QQQQ");
                /// get the value corresponding to key "ACCF" to see its properly updated 
                Console.WriteLine("ACCF" + " -> " + lh_2000.Get("ACCF"));


                ///insert a new key-value pair (ZZZZ,zzzz) 
                lh_2000.Put("ZZZZ", "zzzz");
                /// retrieve the value of key = "ZZZZ" to see if it is inserted properly 
                Console.WriteLine("ZZZZ" + " -> " + lh_2000.Get("ZZZZ"));

                /// Call contains on a Non-Existent Key
                Console.WriteLine("KAAA ?? " + lh_2000.Contains("KAAA"));

                /// call get on non-existent key
                lh_2000.Get("KAAA");

            }

            catch (NonExistentKey<String> nek)
            {
                Console.WriteLine(nek.Message);
                Console.WriteLine(nek.StackTrace);
            }
        }


    }





}
