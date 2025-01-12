﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityLibrary.Models
{
    public class LoanRating
    {
        public BookInfo ApiBook { get; set; }
        public Loan loan { get; set; }
        public User currentUser { get; set; }
        public User otherUser { get; set; }

        public string otherEmail { get; set; }
    }
}
