﻿using Biblioseca.Model.Exceptions;

namespace Biblioseca.Model
{
    public class Author : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual bool Deleted { get; set; }

        public static Author Create(string firstName, string lastName)
        {
            Ensure.NotNull(firstName, "El nombre no puede ser nulo. ");
            Ensure.NotNull(lastName, "El apellido no puede ser nulo. ");

            Author author = new Author
            {
                FirstName = firstName,
                LastName = lastName
            };

            return author;
        }

        public virtual void SetFirstName(string firstName)
        {
            Ensure.NotNull(firstName, "El nombre no puede ser nulo. ");
            FirstName = firstName;
        }

        public virtual void SetLastName(string lastName)
        {
            Ensure.NotNull(lastName, "El apellido no puede ser nulo. ");
            LastName = lastName;
        }

        public virtual void MarkAsDeleted()
        {
            Deleted = true;
        }
    }
}