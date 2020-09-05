using System;

namespace LearnBySpeaking.Domain.Models
{
    public class AppParameter
    {
        public AppParameter(string name, string value, string description, string createUser)
        {
            CreateDate = DateTime.Now;
            CreateUser = createUser;

            Name = name;
            Value = value;
            Description = description;
        }

        // Empty constructor for EF
        protected AppParameter()
        {
        }

        public void Update(string name, string value, string description, string updateUser)
        {
            UpdateDate = DateTime.Now;
            UpdateUser = updateUser;

            Name = name;
            Value = value;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }

        public DateTime? UpdateDate { get; set; }
        public string UpdateUser { get; set; }
    }
}