using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingDateProposer.DataLayer.Services
{
    public interface IDbInitializer
    {
        public void Initialize();
        public void Seed();
    }
}
