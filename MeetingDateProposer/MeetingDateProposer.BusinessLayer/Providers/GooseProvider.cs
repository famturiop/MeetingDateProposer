using System.Collections.Generic;
using System.Linq;
using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public class GooseProvider
    {
        private readonly ApplicationContext _applicationContext;

        //public GooseProvider()
        //{
        //    _applicationContext = new ApplicationContext();
        //}

        //public int GetGooseCount()
        //{
        //    return _applicationContext.Gooses.Count();
        //}

        //public List<Goose> GetAllGooses()
        //{
        //    return _applicationContext.Gooses.ToList();
        //}

        //public void SaveGoose(Goose goose)
        //{
        //    _applicationContext.Gooses.Add(goose);
        //    _applicationContext.SaveChanges();
        //}
    }
}
