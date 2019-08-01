namespace GigHub.Dtos
{

    //!    CREATED FROM THE ATTEANDANCECONTROLLER WEB-API

    //todo  TO WRAP THE [FROM BODY]GIGID PARAMETER - IN INDEX.CSHTML, LETS create another class above 


    public class AttendanceDto //?  DTO - Data Transfer Object
    {                       //todo DTO - An architectural pattern to send data across processes
        public int GigId { get; set; }
    }
}