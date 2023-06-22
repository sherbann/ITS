namespace ITS.Domain
{
    internal class Tour
    {
        #region(Properties)
        public string Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Flight> Flights { get; set; }
        public List<Hotel> Hotels { get; set; }
        #endregion

        #region(Constructors)
        string GetId()
        { // Create rnd of type Random
            Random rnd = new Random();
            //Create id and return it
            return "T" + rnd.Next(1001, 1999);
        }
        public Tour(string name, string description)
        { //Note the use of keyword 'this' and call 
            //to method 'GetId'
            this.Id = GetId();
            this.Name = name;
            this.Description = description;
            // Prepare IFormatProvider to convert string to DateTime
            //IFormatProvider provider = CultureInfo.InvariantCulture;
            this.DateCreated = System.DateTime.Now;
            this.Flights = new List<Flight>();
            this.Hotels = new List<Hotel>();
        }
        private Tour(string id, string name, string description,
            DateTime dateCreated)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.DateCreated = DateTime.Now;
            this.Flights = new List<Flight>();
            this.Hotels = new List<Hotel>();
        }
        public Tour() // default
        { }
        #endregion

        #region(Methods)    

        public IDictionary<string, Tour> Get()
        {

            try
            {
                if (!File.Exists(StreamHelper.GetPath("Tours.txt")))
                {
                    return null;
                }
                IDictionary<string, Tour> tours = new Dictionary<string, Tour>();
                using (StreamReader sr = StreamHelper.GetReader("Tours.txt"))
                {
                    Tour tour;
                    while (!sr.EndOfStream) // until we get to end of data file..
                    {
                        string stream = sr.ReadLine();
                        if (stream[0] == 'T' &&
                            int.TryParse(stream.Substring(1), out int v1))
                        {
                            tour = Get(stream);
                            tours.Add(stream, tour);
                        }
                    }
                }
                return tours;
            }
            catch
            {
                return null;
            }

        }
        public Tour Get(string id)
        {
            try
            {
                if (!File.Exists(StreamHelper.GetPath("Tours.txt")))// Does file exist?If not..
                    return null;
                using (StreamReader sr = StreamHelper.GetReader("Tours.txt"))
                {
                    string stream = sr.ReadLine();
                    while (stream != id)
                    {
                        if (sr.EndOfStream)
                            return null;//Go to end of file, id not found
                        else
                            stream = sr.ReadLine(); // Keep reading
                    } //Stream is id
                    if (stream != id) //Never found it!
                        return null;
                    string name = sr.ReadLine(); // id is found , begin to read
                    string description = sr.ReadLine();
                    DateTime dateCreated = Convert.ToDateTime(sr.ReadLine());
                    //Use the private constructor to create tour
                    Tour tour = new Tour(id, name, description, dateCreated);
                    stream = sr.ReadLine();// Keep on reading for Flight and Hotel ids
                    while (stream != null &&
                        !(stream[0] == 'T' &&
                          int.TryParse(stream.Substring(1), out int v1)))
                    {
                        if (stream[0] == 'F' && int.TryParse(stream.Substring(1), out int v3))
                        {
                            Flight flight = new Flight();
                            flight = flight.Get(stream);// Get Flight
                            tour.Flights.Add(flight);// Add Flight to collection
                        }
                        if (stream[0] == 'H' && int.TryParse(stream.Substring(1), out int v2))
                        {
                            // It's a hotel id
                            Hotel hotel = new Hotel();
                            hotel = hotel.Get(stream); //get hotel
                            tour.Hotels.Add(hotel); // Add Hotel to collection
                        }
                        stream = sr.ReadLine();
                    }
                    return tour;
                }
            }
            catch
            {
                return null;
            }
        }
        public bool Add()
        {
            try
            {
                //To implement
                //Implement an if statement to check and make sure to return false 
                //if either of Name or Description properties are null
                if (string.IsNullOrEmpty(this.Name) || string.IsNullOrEmpty(this.Description))
                {
                    return false;
                }
                //Implement an if statement to check and make sure to return false
                // if the tour(user) already exists
                if (Get(this.Id) != null)
                {
                    return false; // We already have the tour
                }
                using (StreamWriter sw = StreamHelper.GetWriter("Tours.txt", true))
                {
                    sw.WriteLine(this.Id); //writes Id                               
                    sw.WriteLine(this.Name); // Call Writeline passing name                   
                    sw.WriteLine(this.Description);// Call Writeline passing description                   
                    sw.WriteLine(this.DateCreated);// Call Writeline passing DateTime.Now

                    if (this.Flights.Count != 0)

                        foreach (Flight flight in this.Flights)
                        {
                            sw.WriteLine(flight.Id);
                            flight.Add();
                            //Flight f = new Flight();
                            //f.Add(this.Flights);
                        }

                    if (this.Hotels.Count != 0) // last update  27/11/2022
                        foreach (Hotel hotel in this.Hotels)
                        {
                            sw.WriteLine(hotel.Id);
                            hotel.Add();
                        }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool Update()
        {
            try
            {
                if (Get(this.Id) == null) return false;
                if (Delete(this.Id))
                {
                    return this.Add();
                }
                return false;

            }
            catch
            {
                return false;
            }
        }
        public bool Delete(string id)
        {

            //try
            //{

            if (Get(id) == null) // Make sure tour is not Null
            { return false; }
            IDictionary<string, Tour> tours = Get(); // get dictionary
            if (!tours.Keys.Contains(id)) // Is id valid?
            { return false; }
            tours.Remove(id);//Remove from dictionary

            File.Delete(StreamHelper.GetPath("Tours.txt"));//delete Tours.txt
            File.Delete(StreamHelper.GetPath("Flights.txt"));// delete Flights.txt
            File.Delete(StreamHelper.GetPath("Hotels.txt")); // delete Hotels.txt
            foreach (Tour tour in tours.Values)
            {
                tour.Add();
            }
            return true;
            //}
            //catch
            //{
            //    return false;
            //}

        }
        public bool Add(Flight flight)
        {
            try
            {
                // get the tour object
                Tour tour = Get(this.Id);
                //add the flight object to tour object's
                tour.Flights.Add(flight);
                // update tour object
                tour.Update();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Add(Hotel hotel)
        {
            try
            {
                // get the tour object
                Tour tour = Get(this.Id);
                //add the hotel object to tour object's
                tour.Hotels.Add(hotel);
                // update tour object
                tour.Update();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region(Global Variables)
        #endregion

    }
}
