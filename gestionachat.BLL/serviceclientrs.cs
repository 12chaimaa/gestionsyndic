using gestionachat.DAL;
using System.Collections.Concurrent;

namespace
    
    gestionachat.BLL
{
    public class serviceclientrs
    {
        public serviceclientrs()
        {
       
        }
        public List<int> get()
        {

            CientDAL Cdal = new CientDAL();
            var ls = Cdal.getCLINT();
            return ls;
        }
    }
}
