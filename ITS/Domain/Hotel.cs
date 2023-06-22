using System.Globalization;

namespace ITS.Domain
{
    internal class Hotel
    {
        #region(Properties)
        public string Id { get; private set; }
        public string Name { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        #endregion

        #region(Constructors)
        string GetId()
        { // Create rnd of type Random
            Random rnd = new Random();
            //Create id and return it
            return "H" + rnd.Next(1001, 1999);
        }
        public Hotel(string name, string departure, string arrival)
        { //Note the use of keyword 'this' and call 
            //to method 'GetId'
            this.Id = GetId();
            this.Name = name;
            // Prepare IFormatProvider to convert string to DateTime
            IFormatProvider provider = CultureInfo.InvariantCulture;
            this.Departure = DateTime.ParseExact
                (departure, "dd/MM/yyyy HH:mm:ss", provider);
            this.Arrival = DateTime.ParseExact
                (arrival, "dd/MM/yyyy HH:mm:ss", provider);
        }
        private Hotel(string id, string name, DateTime arrival,
            DateTime departure)
        {
            this.Id = id;
            this.Name = name;
            this.Arrival = arrival;
            this.Departure = departure;
        }
        public Hotel() { }
        #endregion

        #region(Methods)
        public bool Add()
        { // try-catch ensures catastrophic failure of execution
            try
            {
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
                    using (StreamWriter sw = StreamHelper.GetWriter("Hotels.txt", true))
                    {
                        sw.WriteLine(this.Id);
                        sw.WriteLine(this.Name);
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
        public bool Add(ICollection<Hotel> hotels)
        {
            try
            {
                foreach (Hotel hotel in hotels)
                {
                    hotel.Add();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        public IDictionary<string, Hotel> Get()
        {// Method has to be public if it is to be used from outside the class
            try
            {
                //string filePath = Directory.GetCurrentDirectory() + "\\Flights.txt";
                using (StreamReader sr = StreamHelper.GetReader("Hotels.txt"))
                {
                    ;// create sr object of type StreamReader
                    if (!File.Exists(StreamHelper.GetPath("Hotels.txt")))
                    {//Note the negation '!'. Test see if data file exists, if not return null
                        return null;
                    }
                    // Create a data dictionary: For each flight, Key id Id and value 
                    //is flight object
                    IDictionary<string, Hotel> hotels = new Dictionary<string, Hotel>();
                    while (!sr.EndOfStream)//Until we get to end of data file
                    {//read a set of values for Fields and place in variables
                        string i = sr.ReadLine();
                        string n = sr.ReadLine();
                        DateTime d = Convert.ToDateTime(sr.ReadLine());
                        DateTime a = Convert.ToDateTime(sr.ReadLine());
                        // Create flight object using constructor
                        Hotel hotel = new Hotel(i, n, d, a);//use private constructor
                        hotels.Add(i, hotel); // add to dictionary
                    }
                    //sr.Close();
                    return hotels; // Return to dictionary
                }
            }
            catch
            {
                return null;
            }
        }
        public Hotel Get(string id)
        {
            try
            {
                //string filePath = Directory.GetCurrentDirectory() + "\\Flights.txt";
                using (StreamReader sr = StreamHelper.GetReader("Hotels.txt"))
                {
                    if (!File.Exists(StreamHelper.GetPath("Hotels.txt"))) { return null; };

                    while (!sr.EndOfStream) // Until we get to end of data file..
                    { //Read a set of values for Fields and place in variables
                        if (sr.ReadLine() == id)
                        {
                            string i = sr.ReadLine();
                            string n = sr.ReadLine();
                            DateTime d = Convert.ToDateTime(sr.ReadLine());
                            DateTime a = Convert.ToDateTime(sr.ReadLine());
                            //sr.Close();
                            Hotel hotel = new Hotel(id, n, d, a);
                            return hotel;
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
                IDictionary<string, Hotel> hotels = Get();//get dictionary
                if (!hotels.Keys.Contains(id))//Check Id is valid
                { return false; }
                hotels.Remove(id); // Remove hotel from dictionary
                // need to add hotel back in
                return Add(hotels.Values);
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
    }
}
