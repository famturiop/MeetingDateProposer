using System.Dynamic;
using System.Runtime.InteropServices.ComTypes;

namespace MeetingDateProposer.Domain.Models
{
    public  interface IReceiverApiData
    {
        object Receive(User user)
        {
            return new object();
        }

    }
}