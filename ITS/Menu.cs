using ITS.Domain;
using System.Globalization;

namespace ITS
{
    internal class Menu
    {
        public static void GetFlightMenu()
        {
            char option = '1';
            while (option == '1' || option == '2' || option == '3'
                || option == '4' || option == '5')
            {
                Console.WriteLine();
                Console.WriteLine("Press 1 to get all your flights.");
                Console.WriteLine("Press 2 to get one flight by Id.");
                Console.WriteLine("Press 3 to add a flight.");
                Console.WriteLine("Press 4 to change a flight.");
                Console.WriteLine("Press 5 to delete a flight.");
                Console.WriteLine();
                option = Convert.ToChar(Console.ReadLine());
                IFormatProvider provider = CultureInfo.InvariantCulture;
                switch (option)
                {

                    case '1':
                        #region(Case1)
                        Flight flight1 = new Flight(); // need obj before we can call its method
                        IDictionary<string, Flight> flights1 = flight1.Get();// get dictionary
                        if (flights1.Count == 0) //If dictionary is empty
                            Console.WriteLine("You have no flights !");
                        else
                        {
                            Print(flights1);
                        }
                        break;
                    #endregion
                    case '2':
                        #region(Case2)
                        Console.Write("Input id of the flight :");
                        string id = Console.ReadLine();
                        Flight flight2 = new Flight();
                        flight2 = flight2.Get(id);
                        if (flight2 == null) { Console.WriteLine("No such flight !"); }
                        else
                        {
                            Print(flight2);
                        }
                        break;
                    #endregion
                    case '3':
                        #region(Case3)
                        // read the data and place in variables
                        Console.Write("Departure city :");
                        string f = Console.ReadLine();
                        Console.Write("Destinaton city :");
                        string t = Console.ReadLine();
                        Console.Write("Deaparture date 'dd/MM/yyyy HH:mm:00' :");
                        string d = Console.ReadLine();
                        Console.Write("Arrival date 'dd/MM/yyyy HH:mm:00' :");
                        string a = Console.ReadLine();
                        Flight flight3 = new Flight(f, t, d, a);// Call constructor
                        //if (flight3.Add())//Invoke Add() on object, returns true
                        if (+flight3)//Invoke Add() on object, returns true
                            Console.WriteLine("Flight is added !");
                        else // Return false
                            Console.WriteLine("Failed to add flight !");
                        break;
                    #endregion

                    case '4':
                        #region(Case4)
                        // Get all flights
                        Flight flight4 = new Flight();
                        IDictionary<string, Flight> flights4 = flight4.Get();
                        if (flights4.Count == 0)
                        {
                            Console.WriteLine("You have no flights !");
                            break;
                        }
                        else //Print all flights
                        {
                            Print(flights4);
                        }
                        //Get id of flight to be changed
                        Console.Write("Input id of flight you want to change :");
                        Console.WriteLine();
                        string id4 = Console.ReadLine();
                        if (flights4.Keys.Contains(id4))
                        {
                            Console.Write("New departure city : ");
                            flight4.From = Console.ReadLine();
                            Console.Write("New destination city : ");
                            flight4.To = Console.ReadLine();
                            Console.Write("New departure date 'dd/MM/yyyy HH:mm:00' :");
                            flight4.Departure = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm:00", provider);
                            Console.Write("New arrival date 'dd/MM/yyyy HH:mm:00' :");
                            flight4.Arrival = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm:00", provider);
                            flight4.Update(id4);
                            goto case '1';
                        }
                        else { Console.WriteLine("Flight id is not recognized !"); }
                        break;
                    #endregion
                    case '5':
                        #region(Case5)
                        // Get all flights
                        Flight flight5 = new Flight();
                        IDictionary<string, Flight> flights5 = flight5.Get();
                        if (flights5.Count == 0)
                        {
                            Console.WriteLine("You have no flights !");
                        }
                        else //Print all flights
                        {
                            Print(flights5);
                        }
                        // Getid of flight to be deleted
                        Console.WriteLine("Input id of flight you want to delete : ");
                        Console.WriteLine();
                        string id5 = Console.ReadLine();
                        if (flights5.Keys.Contains(id5)) // id is found
                        {
                            //flight5.Delete(id5); //Delete flight
                            if (-flight5)
                            {
                                Console.WriteLine("Flight {0} has been deleted!", id5);
                            }
                        }
                        else
                        {
                            Console.WriteLine("We don't have that flight !");
                        }
                        goto case '1';

                    #endregion
                    default:
                        break;
                }
            }


        }
        #region(Utilities)
        static void Print(Flight flight)
        {
            if (flight != null)
            {
                Console.WriteLine("Id : {0}", flight.Id);
                Console.WriteLine("From : {0}", flight.From);
                Console.WriteLine("To : {0}", flight.To);
                Console.WriteLine("Departing  : {0}", flight.Departure);
                Console.WriteLine("Arriving  : {0}", flight.Arrival);
                Console.WriteLine();
            }
        }
        static void Print(IDictionary<string, Flight> flight)
        {
            Console.WriteLine("Your flights are :");
            foreach (var item in flight)
            {
                Console.WriteLine("Id : {0}", item.Value.Id);
                Console.WriteLine("From : {0}", item.Value.From);
                Console.WriteLine("To : {0}", item.Value.To);
                Console.WriteLine("Departing : {0}", item.Value.Departure);
                Console.WriteLine("Arriving : {0}", item.Value.Arrival);
                Console.WriteLine();
            }


        }
        static void Print(List<Flight> flights)
        {
            foreach (var flight in flights)
            {
                Print(flight);
            }
        }
        static void Print(Hotel hotel) //New
        {
            if (hotel != null)
            {
                Console.WriteLine("Id : {0}", hotel.Id);
                Console.WriteLine("Name : {0}", hotel.Name);
                Console.WriteLine("Departing  : {0}", hotel.Departure);
                Console.WriteLine("Arriving  : {0}", hotel.Arrival);
                Console.WriteLine();
            }
        }
        static void Print(IDictionary<string, Hotel> hotel)//New
        {
            Console.WriteLine("Your Hotels are :");
            foreach (var item in hotel)
            {
                Console.WriteLine("Id : {0}", item.Value.Id);
                Console.WriteLine("Name : {0}", item.Value.Name);
                Console.WriteLine("Arriving : {0}", item.Value.Arrival);
                Console.WriteLine("Departing : {0}", item.Value.Departure);
                Console.WriteLine();
            }
        }
        static void Print(List<Hotel> hotels) //New
        {
            foreach (var hotel in hotels)
            {
                Print(hotel);
            }
        }
        static void Print(Tour tour) //New
        {
            if (tour != null)
            {
                Console.WriteLine("Id : {0}", tour.Id);
                Console.WriteLine("Name : {0}", tour.Name);
                Console.WriteLine("Description  : {0}", tour.Description);
                Console.WriteLine("Added on  : {0}", tour.DateCreated);
                Console.WriteLine("Flights  : {0}", tour.Flights);
                Console.WriteLine("Hotels : {0}", tour.Hotels);
            }
        }
        static void Print(IDictionary<string, Tour> tour)//New
        {
            Console.WriteLine("Your Tours are :");
            foreach (var item in tour)
            {
                Console.WriteLine("Id : {0}", item.Value.Id);
                Console.WriteLine("Name : {0}", item.Value.Name);
                Console.WriteLine("Description : {0}", item.Value.Description);
                Console.WriteLine("Added on  : {0}", item.Value.DateCreated);
                foreach (var flight in item.Value.Flights)
                {
                    Print(flight);
                }
                foreach (var hotel in item.Value.Hotels)
                {
                    Print(hotel);
                }
                Console.WriteLine();
            }
        }
        #endregion
        public static void GetTourMenu()
        {
            char option = '1';
            while (option == '1' || option == '2' || option == '3'
                || option == '4' || option == '5' || option == '6'
                || option == '7')
            {
                Console.WriteLine();
                Console.WriteLine("Press 1 to get one of your tours");
                Console.WriteLine("Press 2 to get all your tours");
                Console.WriteLine("Press 3 to add a tour");
                Console.WriteLine("Press 4 to add a flight to a tour");
                Console.WriteLine("Press 5 to make changes");
                Console.WriteLine("Press 6 to delete tour");
                Console.WriteLine("Press 7 to add a hotel to tour");
                Console.WriteLine();
                option = Convert.ToChar(Console.ReadLine()[0]);
                switch (option)
                {
                    case '1': //Get 1 tour
                        #region(Case1)

                        Console.WriteLine("Input the id of Tour : ");
                        string id1 = Console.ReadLine();
                        Tour tour1 = new Tour(); // Create tour1 object
                        tour1 = tour1.Get(id1);// use id to get tour
                        if (tour1 != null)
                        {// if is not null , print it
                            Print(tour1);
                        }
                        else
                        {
                            Console.WriteLine("We don't have that tour !");
                        }
                        break;
                    #endregion
                    case '2': //Get all tours
                        #region(Case2)
                        Tour tour2 = new Tour();
                        IDictionary<string, Tour> tours2 = tour2.Get();
                        if (tours2 == null || tours2.Count == 0)
                        {
                            Console.WriteLine("You don't have tours !");
                        }
                        else
                        {
                            Print(tours2);
                        }
                        break;
                    #endregion
                    case '3': //Add a tour
                        #region(Case3)
                        Console.Write("What is the name for tour : ");
                        string name = Console.ReadLine();
                        Console.Write("What is the description for tour : ");
                        string description = Console.ReadLine();
                        //To implement:
                        // Declare tour3 object of type Tour
                        Tour tour3;
                        //Create tour3 object using the constructor
                        tour3 = new Tour(name, description);
                        if (!tour3.Add())
                        {
                            Console.WriteLine("Something went wrong !");
                        }
                        else
                        {
                            Print(tour3.Get(tour3.Id));
                            Console.WriteLine("Tour added successfully !");
                        }
                        break;
                    #endregion

                    case '4': //Add a flight to tour
                        #region(Case4)
                        Tour tour4 = new Tour();
                        IDictionary<string, Tour> tours4 = tour4.Get();
                        if (tour4 == null)
                        {
                            Console.WriteLine("You don't have tours !");
                            GetTourMenu();
                        }
                        Print(tours4);
                        Console.WriteLine("What is the Id of tour to add Flight to : ");
                        string id4 = Console.ReadLine();
                        tour4 = tour4.Get(id4);
                        if (tour4 == null)
                        {
                            Console.WriteLine("We don't have that tour");
                            GetTourMenu();
                        }
                        Console.Write("Where is the flight from : ");
                        string from = Console.ReadLine();
                        Console.Write("Where is the flight to : ");
                        string to = Console.ReadLine();
                        Console.Write("When is the departure : ");
                        string departure = Console.ReadLine();
                        Console.Write("When is the arrival : ");
                        string arrival = Console.ReadLine();
                        Flight flight4 = new Flight(from, to, departure, arrival);
                        if (tour4.Add(flight4))
                            goto case '2';
                        else
                            Console.WriteLine("Something went wrong !");

                        break;
                    #endregion

                    case '5': //Update a tour
                        #region(Case5)
                        Tour tour5 = new Tour();
                        IDictionary<string, Tour> tours5 = tour5.Get();
                        if (tours5 == null)
                        {
                            Console.WriteLine("You don't have tours !");
                            GetTourMenu();
                        }
                        Print(tours5);
                        Console.Write("What is the Id of tour to change : ");
                        string id5 = Console.ReadLine();
                        tour5 = tour5.Get(id5);
                        if (tour5 != null)
                        {
                            Console.Write("What is the new name : ");
                            tour5.Name = Console.ReadLine();
                            Console.Write("What is the new description :");
                            tour5.Description = Console.ReadLine();
                            tour5.Update();
                            goto case '2';
                        }
                        Console.WriteLine("Something went wrong");
                        break;
                    #endregion

                    case '6': //Delete a tour
                        #region(Case6)
                        Tour tour6 = new Tour(); // create tour 
                        IDictionary<string, Tour> tours6 = tour6.Get(); // create collection of tour
                        if (tours6 == null)
                        {
                            Console.WriteLine("You don't have tours !");
                            GetTourMenu();
                        }
                        Print(tours6);
                        Console.Write("What is the Id of tour to delete? :");
                        string id6 = Console.ReadLine();
                        if (tour6.Delete(id6))
                            goto case '2';
                        Console.WriteLine("Something went wrong !");
                        break;
                    #endregion
                    case '7': //Add a hotel to tour
                        #region(Case7)
                        Tour tour7 = new Tour();
                        IDictionary<string, Tour> tours7 = tour7.Get();
                        if (tour7 == null)
                        {
                            Console.WriteLine("You don't have tours !");
                            GetTourMenu();
                        }
                        Print(tours7);
                        Console.WriteLine("What is the Id of tour to add Hotel to : ");
                        string id7 = Console.ReadLine();
                        tour7 = tour7.Get(id7);
                        if (tour7 == null)
                        {
                            Console.WriteLine("We don't have that tour");
                            GetTourMenu();
                        }
                        Console.Write("What is the name of the Hotel : ");
                        string HotelName = Console.ReadLine();
                        Console.Write("When is the check-in : ");
                        string HotelArrival = Console.ReadLine();
                        Console.Write("Where is the check-out : ");
                        string HotelDeparture = Console.ReadLine();
                        Hotel hotel7 = new Hotel(HotelName, HotelArrival, HotelDeparture);
                        if (tour7.Add(hotel7))
                            goto case '2';
                        else
                            Console.WriteLine("Something went wrong !");
                        break;
                    #endregion
                    default:
                        break;

                }
            }
        }

        public static void GetUserMenu()
        {
            char option = '1';
            while (option == '1' || option == '2' || option == '3')
            {
                Console.WriteLine();
                Console.WriteLine("Press 1 to Login");
                Console.WriteLine("Press 2 to Register");
                Console.WriteLine("Press 3 to Change Password");
                Console.WriteLine();

                option = Convert.ToChar(Console.ReadLine()[0]);
                switch (option)
                {
                    case '1': //Login
                        #region(Case1)
                        // read email
                        Console.Write("What is your email address : ");
                        string email1 = Console.ReadLine();
                        //check if email is EmailFormed
                        while (!Helper.EmailFormed(email1))
                        {
                            Console.Write("What is your email address : ");
                        }
                        // read password
                        Console.Write("Input your password : ");
                        string pass1 = Console.ReadLine();
                        // declare user object and create user
                        // using call of Login method passing
                        // email and password
                        User user1 = User.Login(email1, pass1);
                        // check if user is null , if so report failure
                        //and  call GetUserMenu
                        if (user1 == null)
                        {
                            Console.WriteLine("Unable to login !");
                            GetUserMenu();
                        }
                        // call method GetTourMenu passing user object
                        GetTourMenu(user1);
                        break;
                    #endregion
                    case '2': //Register
                        #region(Case2)
                        Console.Write("What is your email address : ");
                        //Read email
                        string email2 = Console.ReadLine();
                        // check if email is EmailFormed if 
                        //not  keep prompting
                        while (!Helper.EmailFormed(email2))
                        {
                            Console.Write("What is your email address : ");
                        }
                        // read name
                        Console.Write("What is your name : ");
                        string name = Console.ReadLine();
                        // read tel
                        Console.Write("What is your phone number : ");
                        string tel = Console.ReadLine();
                        // read password
                        Console.Write("Input your password : ");
                        string pass = Console.ReadLine();
                        // read password2
                        Console.Write("Retype your password : ");
                        string pass2 = Console.ReadLine();

                        if (Helper.CheckPassword(pass, pass2))
                        {
                            // call register , if true 
                            if (User.Register(email2, name, tel, pass, pass2))
                            {
                                User user = new User(email2, name, tel, pass);
                                user.Add();
                                Console.WriteLine("You have been registered !");
                            }
                            else
                            {
                                Console.WriteLine("Unable to register !");
                            }

                        }
                        // call GetUserMenu()
                        GetUserMenu();
                        break;
                    #endregion

                    case '3': //Change Password
                        #region(Case3)
                        break;
                    #endregion
                    default:
                        break;

                }
            }
        }
        public static void GetTourMenu(User user)
        {
            char option = '1';
            while (option == '1' || option == '2' || option == '3'
                || option == '4' || option == '5' || option == '6'
                || option == '7')
            {
                Console.WriteLine();
                Console.WriteLine("Press 1 to get one of your tours");
                Console.WriteLine("Press 2 to get all your tours");
                Console.WriteLine("Press 3 to add a tour");
                Console.WriteLine("Press 4 to add a flight to a tour");
                Console.WriteLine("Press 5 to make changes");
                Console.WriteLine("Press 6 to delete tour");
                Console.WriteLine("Press 7 to add a hotel to tour");
                Console.WriteLine();
                option = Convert.ToChar(Console.ReadLine()[0]);
                switch (option)
                {
                    case '1': //Get 1 tour
                        #region(Case1)

                        Console.WriteLine("Input the id of Tour : ");
                        string id1 = Console.ReadLine();
                        Tour tour1 = new Tour(); // Create tour1 object
                        tour1 = tour1.Get(id1);// use id to get tour
                        if (tour1 != null)
                        {// if is not null , print it
                            Print(tour1);
                        }
                        else
                        {
                            Console.WriteLine("We don't have that tour !");
                        }
                        break;
                    #endregion
                    case '2': //Get all tours
                        #region(Case2)
                        Tour tour2 = new Tour();
                        IDictionary<string, Tour> tours2 = tour2.Get();
                        if (tours2 == null || tours2.Count == 0)
                        {
                            Console.WriteLine("You don't have tours !");
                        }
                        else
                        {
                            Print(tours2);
                        }
                        break;
                    #endregion
                    case '3': //Add a tour
                        #region(Case3)
                        Console.Write("What is the name for tour : ");
                        string name = Console.ReadLine();
                        Console.Write("What is the description for tour : ");
                        string description = Console.ReadLine();
                        //To implement:
                        // Declare tour3 object of type Tour
                        Tour tour3;
                        //Create tour3 object using the constructor
                        tour3 = new Tour(name, description);
                        if (!tour3.Add())
                        {
                            Console.WriteLine("Something went wrong !");
                        }
                        else
                        {
                            Print(tour3.Get(tour3.Id));
                            Console.WriteLine("Tour added successfully !");
                        }
                        break;
                    #endregion

                    case '4': //Add a flight to tour
                        #region(Case4)
                        Tour tour4 = new Tour();
                        IDictionary<string, Tour> tours4 = tour4.Get();
                        if (tour4 == null)
                        {
                            Console.WriteLine("You don't have tours !");
                            GetTourMenu();
                        }
                        Print(tours4);
                        Console.WriteLine("What is the Id of tour to add Flight to : ");
                        string id4 = Console.ReadLine();
                        tour4 = tour4.Get(id4);
                        if (tour4 == null)
                        {
                            Console.WriteLine("We don't have that tour");
                            GetTourMenu();
                        }
                        Console.Write("Where is the flight from : ");
                        string from = Console.ReadLine();
                        Console.Write("Where is the flight to : ");
                        string to = Console.ReadLine();
                        Console.Write("When is the departure : ");
                        string departure = Console.ReadLine();
                        Console.Write("When is the arrival : ");
                        string arrival = Console.ReadLine();
                        Flight flight4 = new Flight(from, to, departure, arrival);
                        if (tour4.Add(flight4))
                            goto case '2';
                        else
                            Console.WriteLine("Something went wrong !");

                        break;
                    #endregion

                    case '5': //Update a tour
                        #region(Case5)
                        Tour tour5 = new Tour();
                        IDictionary<string, Tour> tours5 = tour5.Get();
                        if (tours5 == null)
                        {
                            Console.WriteLine("You don't have tours !");
                            GetTourMenu();
                        }
                        Print(tours5);
                        Console.Write("What is the Id of tour to change : ");
                        string id5 = Console.ReadLine();
                        tour5 = tour5.Get(id5);
                        if (tour5 != null)
                        {
                            Console.Write("What is the new name : ");
                            tour5.Name = Console.ReadLine();
                            Console.Write("What is the new description :");
                            tour5.Description = Console.ReadLine();
                            tour5.Update();
                            goto case '2';
                        }
                        Console.WriteLine("Something went wrong");
                        break;
                    #endregion

                    case '6': //Delete a tour
                        #region(Case6)
                        Tour tour6 = new Tour(); // create tour 
                        IDictionary<string, Tour> tours6 = tour6.Get(); // create collection of tour
                        if (tours6 == null)
                        {
                            Console.WriteLine("You don't have tours !");
                            GetTourMenu();
                        }
                        Print(tours6);
                        Console.Write("What is the Id of tour to delete? :");
                        string id6 = Console.ReadLine();
                        if (tour6.Delete(id6))
                            goto case '2';
                        Console.WriteLine("Something went wrong !");
                        break;
                    #endregion
                    case '7': //Add a hotel to tour
                        #region(Case7)
                        Tour tour7 = new Tour();
                        IDictionary<string, Tour> tours7 = tour7.Get();
                        if (tour7 == null)
                        {
                            Console.WriteLine("You don't have tours !");
                            GetTourMenu();
                        }
                        Print(tours7);
                        Console.WriteLine("What is the Id of tour to add Hotel to : ");
                        string id7 = Console.ReadLine();
                        tour7 = tour7.Get(id7);
                        if (tour7 == null)
                        {
                            Console.WriteLine("We don't have that tour");
                            GetTourMenu();
                        }
                        Console.Write("What is the name of the Hotel : ");
                        string HotelName = Console.ReadLine();
                        Console.Write("When is the check-in : ");
                        string HotelArrival = Console.ReadLine();
                        Console.Write("Where is the check-out : ");
                        string HotelDeparture = Console.ReadLine();
                        Hotel hotel7 = new Hotel(HotelName, HotelArrival, HotelDeparture);
                        if (tour7.Add(hotel7))
                            goto case '2';
                        else
                            Console.WriteLine("Something went wrong !");
                        break;
                    #endregion
                    default:
                        break;

                }
            }
        }
    }
}

