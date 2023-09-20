namespace dotnetapp.Models{
    public class FrenchTuitionBookingException:Exception{
        public KathakClassBookingException(string message):base(message)
        {}
    }
}