using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Dtos
{
        //!    CREATED FROM THE ATTEANDANCECONTROLLER WEB-API

        //todo  TO WRAP THE [FROM BODY]GIGID PARAMETER - IN INDEX.CSHTML, LETS create another class above 


        public class FollowingDto //?  DTO - Data Transfer Object
        {                       //todo DTO - An architectural pattern to send data across processes
            public string FolloweeId { get; set; }
        }
}