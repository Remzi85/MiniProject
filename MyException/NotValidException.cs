﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp__Library_Management_Application_.MyException
{
    internal class NotValidException : Exception
    {
        public NotValidException()
        {
        }

        public NotValidException(string? message) : base(message)
        {
        }
    }
}
