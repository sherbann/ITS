using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace ITS.Domain
{
    class Flight
    {
        #region (Fields)
        string id;
        string from;
        string to;
        DateTime departure;
        DateTime arrival;
        #endregion

        #region(Properties)
        public string Id // Note use of capital I
        {
            get { return id; }
            // Note private setter, Id is to be used only
            // inside class
            private set { id = value; }
        }
        public string From
        {
            get { return from; }
            set { from = value; }
        }

        public string To
        {
            get { return to; }
            set { to = value; }
        }
        public DateTime Departure
        {
            get { return departure; }
            set { departure = value; }
        }
        public DateTime Arrival
        {
            get { return arrival; }
            set { arrival = value; }
        }
        #endregion

        #region(Constructors)
        string GetId()
        { // Create rnd of type Random
            Random rnd = new Random();
            //Create id and return it
            return "F" + rnd.Next(1001, 1999);
        }
        public Flight(string from, string to,
            string departure, string arrival)
        { //Note the use of keyword 'this' and call 
            //to method 'GetId'
            this.Id = GetId();
            this.From = from;
            this.To = to;
            // Prepare IFormatProvider to convert string to DateTime
            IFormatProvider provider = CultureInfo.InvariantCulture;
            this.Departure = DateTime.ParseExact
                (departure, "dd/MM/yyyy HH:mm:ss", provider);
            this.Arrival = DateTime.ParseExact
                (arrival, "dd/MM/yyyy HH:mm:ss", provider);
        }
        public Flight() // Default constructed reinstated use only with class Flight
        {

        }
        private Flight(string id, string from, string to, DateTime departure, DateTime arrival)
        {
            this.id = id;
            this.from = from;
            this.to = to;
            this.departure = departure;
            this.arrival = arrival;
        }// Points to Fields. Private. To used in Flight only.

        #endregion

        #region(Methods)
        public bool Add()
        { // try-catch ensures catastrophic failure of execution
            try { 
            //Create the path to the data file
            //string filePath = Directory.GetCurrentDirectory() + "\\Flights.txt";
                //File.Create(filePath).Close();
                //Create the sw object, sw opens the file using filePath
                // 2nd parameter 'true' creates the file if the file does
                //not already exist
                //StreamWriter sw = new StreamWriter(filePath, true);
                //sw.Close();
                if (Get(this.Id) == null) // If it does not exists
                {
                    using (StreamWriter sw = StreamHelper.GetWriter("Flights.txt", true))
                    { 
                        sw.WriteLine(this.Id);
                        sw.WriteLine(this.From);
                        sw.WriteLine(this.To);
                        sw.WriteLine(this.Departure);
                        sw.WriteLine(this.Arrival);
                        //sw.Close();
                        //sw.Dispose();
                    }
                }
            // sw.Close();
            //sw.WriteLine(this.From);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Add(ICollection<Flight> flights)
        {
            try
            {
                foreach (Flight flight in flights)
                {
                    flight.Add();
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public IDictionary<string, Flight> Get()
        {// Method has to be public if it is to be used from outside the class
            try
            {
                //string filePath = Directory.GetCurrentDirectory() + "\\Flights.txt";
                using (StreamReader sr = StreamHelper.GetReader("Flights.txt"))
                {
                    ;// create sr object of type StreamReader
                    if (!File.Exists(StreamHelper.GetPath("Flights.txt")))
                    {//Note the negation '!'. Test see if data file exists, if not return null
                        return null;
                    }
                    // Create a data dictionary: For each flight, Key id Id and value 
                    //is flight object
                    IDictionary<string, Flight> flights = new Dictionary<string, Flight>();
                    while (!sr.EndOfStream)//Until we get to end of data file
                    {//read a set of values for Fields and place in variables
                        string i = sr.ReadLine();
                        string f = sr.ReadLine();
                        string t = sr.ReadLine();
                        DateTime d = Convert.ToDateTime(sr.ReadLine());
                        DateTime a = Convert.ToDateTime(sr.ReadLine());
                        // Create flight object using constructor
                        Flight flight = new Flight(i, f, t, d, a);//use private constructor
                        flights.Add(i, flight); // add to dictionary
                    }
                    sr.Close();
                    return flights; // Return to dictionary
                }
            }
            catch
            {
                return null;
            }
        }
        public Flight Get(string id)
        {
            try
            {
                //string filePath = Directory.GetCurrentDirectory() + "\\Flights.txt";
                using (StreamReader sr =StreamHelper.GetReader("Flights.txt"))
                {
                    if (!File.Exists(StreamHelper.GetPath("Flights.txt"))) { return null; };

                    while (!sr.EndOfStream) // Until we get to end of data file..
                    { //Read a set of values for Fields and place in variables
                        if (sr.ReadLine() == id)
                        {
                            string f = sr.ReadLine();
                            string t = sr.ReadLine();
                            DateTime d = Convert.ToDateTime(sr.ReadLine());
                            DateTime a = Convert.ToDateTime(sr.ReadLine());
                            sr.Close();
                            Flight flight = new Flight(id, f, t, d, a);
                            return flight;
                        }
                    }
                    //sr.Close();
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public bool Delete(string id)
        {
            try
            {
                IDictionary<string, Flight> flights = Get();//get dictionary
                if (!flights.Keys.Contains(id))//Check Id is valid
                { return false; }
                flights.Remove(id); // Remove flight from dictionary
                // Need to delete the file Flights.txt
                File.Delete(StreamHelper.GetPath("Flights.txt"));
                // need to add flights back in
                return Add(flights.Values);//We already have the method
            }
            catch
            {
                return false;
            }

        }
        public bool Update(string id)
        {
            try
            {
                if (Delete(id))
                {
                    this.Id = id;
                    return this.Add();
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region(Operators)
        public  static bool operator + (Flight flight)
        {
            try
            {
                flight.Add();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool operator - (Flight flight)
        {
            try
            {
                flight.Delete(flight.id);
                return true;
            }
            catch
            {
                return false;
            }
        }






        #endregion
    }
}
