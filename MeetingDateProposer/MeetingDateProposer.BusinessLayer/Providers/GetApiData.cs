using System.Dynamic;
using System.Runtime.InteropServices.ComTypes;

namespace MeetingDateProposer.Domain.Models
{
    public  interface IReceiverApiData
    {
        object Receive(User Calendar)
        {
            return new object();
        }

    }
}