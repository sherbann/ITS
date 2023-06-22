namespace ITS.Domain
{
    internal class User
    {
        #region(Fields)
        public string password;
        public bool blocked;
        #endregion
        #region(Properties)
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Tel { get; private set; }
        public Dictionary<string, Tour> Tours { get; }// modified IDictionary with Dictionary
        #endregion
        #region(Constructors)
        public User(string email, string name, string tel, string password, bool blocked)
        {
            this.Email = email;
            this.Name = name;
            this.Tel = tel;
            this.password = password;
            this.blocked = blocked;
            this.Tours = new Dictionary<string, Tour>();
        }
        public User(string email, string name, string tel, string password)
        {
            this.Email = email;
            this.Name = name;
            this.Tel = tel;
            this.password = password;
            this.Tours = new Dictionary<string, Tour>();
        }
        private User() { }
        #endregion
        #region(Methods)
        IDictionary<string, User> Get()
        {
            throw new NotImplementedException();
        }
        public User Get(string email)
        {
            if (!File.Exists(StreamHelper.GetPath("Users.txt")))
                return null;// file does not exists
            using (StreamReader sr = StreamHelper.GetReader("Users.txt"))
            {
                string stream = sr.ReadLine();
                while (stream != email)
                {
                    if (sr.EndOfStream)
                        return null; // got to end of file, id not found
                    else
                        stream = email;// keep reading
                }//Stream is email, start to read for the rest
                string n = sr.ReadLine();
                string t = sr.ReadLine();
                string p = sr.ReadLine();
                bool b = Convert.ToBoolean(sr.ReadLine());
                User user = new User(email, n, t, p, b);// we have a user
                stream = sr.ReadLine();// look for possible tours
                while (stream != null)
                {
                    if (Helper.EmailFormed(stream))
                        return user;// next email is found - no more tours
                    Tour tour = new Tour();
                    tour = tour.Get(stream);
                    user.Tours.Add(tour.Id, tour);
                    stream = sr.ReadLine();
                }
                return user;
            }
        }
        public static User Login(string email, string password)
        {
            //Create a user object using constructor
            User user = new User();
            //Asign to user call of Get(email)
            user = user.Get(email);
            //Check if user is null, if so return null
            if (user == null) { return null; }
            //Check if user.password equals pass, if so return 
            if (user.password == password)
            // user object
            {
                return user;
            }
            return null;
            //After method is tested , implement try-catch

        }
        public static bool Register(string email, string name, string tel,
            string pass, string pass2)
        {
            try
            {
                // check if two passwords are the same
                if (pass != pass2)
                {
                    return false;
                }
                // check if email is EmailFormed
                if (!Helper.EmailFormed(email))
                {
                    return false;
                }
                //create the object using constructor
                User user = new User();
                //check if email is taken 
                if (user.Get(email) != null)
                {
                    return false;
                }
                //call user.Add()
                user.Add();
                //return true
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static bool ChangePassword(string email, string password,
            string newPassword, string newPassword2)
        {
            throw new NotImplementedException();
        }
        public bool Add()
        {
            //Using keyword 'this' to reference user object..
            // Check if email is null, if so return false
            if (this.Email == null)
            {
                return false;
            }
            //Check if user object already exists (check result
            // of Get(email)) if so return false
            if (Get(this.Email) == null) return false;
            //Use a StreamWriter in a Using construct to ...
            //Write Email, Name, Tel, password, blocked
            using (StreamWriter sw = StreamHelper.GetWriter("Users.txt", true))
            {
                sw.WriteLine(this.Email);
                sw.WriteLine(this.Name);
                sw.WriteLine(this.Tel);
                sw.WriteLine(this.password);
                sw.WriteLine(this.blocked);
                if (this.Tours.Count != 0)
                {//Iterate through Tours.Values..(use foreach loop)
                    foreach (var tour in this.Tours.Values)
                    {
                        sw.WriteLine(tour.Id);
                        tour.Add();
                    }
                }

            }
            return true;
            // after Register method is tested , implement try-catch

            throw new NotImplementedException();
        }
        public bool Update()
        {
            throw new NotImplementedException();
        }
        bool Delete()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
